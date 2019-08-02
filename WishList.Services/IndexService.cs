using System;
using System.Threading.Tasks;
using WishList.Repositories.Index.Interfaces;
using WishList.Services.Interfaces;
using WishList.Shared.Exception;
using WishList.Shared.Notify.Notifications;
using WishList.Shared.Result;

namespace WishList.Services
{
    public class IndexService<TModel> : BaseService, IIndexService<TModel> where TModel : class
    {
        private readonly IIndexRepository<TModel> indexRepository;

        public IndexService(IIndexRepository<TModel> indexRepository)
        {
            this.indexRepository = indexRepository;
        }

        public async Task<IResultBase> IndexDocumentAsync(TModel model)
        {
            try
            {
                await this.indexRepository.IndexDocumentAsync(model);
            }
            catch (Exception ex)
            {
                new InternalServerErrorResult(ex.GetExceptionMessages());
            }

            return new OkResult();
        }

        public async Task<IResultBase> UpdateDocumentAsync(int id, TModel model)
        {
            try
            {
                await this.indexRepository.UpdateDocumentAsync(id, model);
            }
            catch (Exception ex)
            {
                new InternalServerErrorResult(ex.GetExceptionMessages());
            }

            return new OkResult();
        }

        public async Task DeleteDocumentAsync(int id)
        {
            try
            {
                await this.indexRepository.DeleteDocumentAsync(id);
            }
            catch (Exception ex)
            {
                new InternalServerErrorResult(ex.GetExceptionMessages());
            }

            return new OkResult();
        }
    }
}
