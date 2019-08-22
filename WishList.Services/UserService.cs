using AutoMapper;
using System;
using System.Threading.Tasks;
using WishList.Domain.Entities;
using WishList.Domain.Repositories;
using WishList.Repositories.ReadOnly.Interfaces;
using WishList.Services.Interfaces;
using WishList.Services.Models.User;
using WishList.Shared.Exception;
using WishList.Shared.Result;

namespace WishList.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserQueryRepository userQueryRepository;
        private readonly IIndexService<UserModel> indexService;
        private readonly IUnitOfWork unitOfWork;

        public UserService(
            IUserRepository userRepository,
            IUserQueryRepository userQueryRepository,
            IIndexService<UserModel> indexService,
            IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.userQueryRepository = userQueryRepository;
            this.indexService = indexService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<UserModel>> Create(UserCreationModel userModel)
        {
            try
            {
                var userResult = User.Create(userModel.Name, userModel.Email);

                if (userResult.Failure) return OperationResult.BadRequest<UserModel>(userResult);

                if (await userQueryRepository.UserExists(userModel.Email))
                    return OperationResult.Conflict<UserModel>($"User already exists with email {userModel.Email}");

                this.userRepository.Add(userResult.Value);

                unitOfWork.Save();

                var model = Mapper.Map<UserModel>(userResult.Value);

                var indexResult = await indexService.IndexDocumentAsync(model);

                if (indexResult.Failure)
                {
                    await Rollback(userResult.Value);

                    return OperationResult.InternalServerError<UserModel>(indexResult);
                }

                return OperationResult.Created<UserModel>(model);
            }
            catch (Exception ex)
            {
                return OperationResult.InternalServerError<UserModel>(ex.GetExceptionMessages());
            }
        }

        private async Task Rollback(User user)
        {
            await this.indexService.DeleteDocumentAsync(user.Id);

            this.userRepository.Remove(user);

            this.unitOfWork.Save();
        }
    }
}
