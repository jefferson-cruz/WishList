using AutoMapper;
using System;
using System.Threading.Tasks;
using WishList.Domain.Entities;
using WishList.Domain.Repositories;
using WishList.Repositories.ReadOnly.Interfaces;
using WishList.Services.Interfaces;
using WishList.Services.Models.User;
using WishList.Shared.Exception;
using WishList.Shared.Notify.Notifications;
using WishList.Shared.Result;

namespace WishList.Services
{
    public class UserService : BaseService, IUserService
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

        public async Task<IResultBase> Create(UserCreationModel userModel)
        {
            try
            {
                var user = User.Create(userModel.Name, userModel.Email);

                if (user.Failure)
                    return user.Result;

                if (await userQueryRepository.UserExists(userModel.Email))
                    return new ConflictResult($"User already exists with email {userModel.Email}");

                this.userRepository.Add(user.Value);

                unitOfWork.Save();

                var model = Mapper.Map<UserModel>(user.Value);

                await indexService.IndexDocumentAsync(model);

                if (indexService.HasResults)
                {
                    AddResults(indexService.Results);

                    await Rollback(user.Value);

                    return null;
                }

                return new CreatedResult<UserModel>(model);
            }
            catch (Exception ex)
            {
                return Shared.Result.Results.InternalServerError(ex.GetExceptionMessages());
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
