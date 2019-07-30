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

        public async Task<UserModel> Create(UserCreationModel userModel)
        {
            try
            {
                if (await userQueryRepository.UserExists(userModel.Email))
                {
                    AddNotification<Conflict>($"User already exists with email {userModel.Email}");

                    return null;
                }

                var user = User.Create(userModel.Name, userModel.Email);

                AddNotifications(user.Notifications);

                if (HasNotification) return null;

                this.userRepository.Add(user);

                unitOfWork.Save();

                var model = Mapper.Map<UserModel>(user);

                await indexService.IndexDocumentAsync(model);

                if (indexService.HasNotifications)
                {
                    AddNotifications(indexService.Notifications);

                    await Rollback(user);

                    return null;
                }

                return model;
            }
            catch (Exception ex)
            {
                AddNotification<Failure>(ex.GetExceptionMessages());

                return null;
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
