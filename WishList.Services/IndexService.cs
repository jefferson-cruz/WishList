using System;
using System.Threading.Tasks;
using WishList.Repositories.Index.Interfaces;
using WishList.Services.Interfaces;
using WishList.Shared.Result;

namespace WishList.Services
{
    public class IndexService<TModel> : IIndexService<TModel> where TModel : class
    {
        private readonly IIndexRepository<TModel> indexRepository;

        public IndexService(IIndexRepository<TModel> indexRepository)
        {
            this.indexRepository = indexRepository;
        }

        public async Task<Result> IndexDocumentAsync(TModel model)
        {
            try
            {
                await this.indexRepository.IndexDocumentAsync(model);

                return OperationResult.OK();
            }
            catch (Exception ex)
            {
                return OperationResult.InternalServerError(ex);
            }
        }

        public async Task<Result> UpdateDocumentAsync(int id, TModel model)
        {
            try
            {
                await this.indexRepository.UpdateDocumentAsync(id, model);

                return OperationResult.OK();
            }
            catch (Exception ex)
            {
                return OperationResult.InternalServerError(ex);
            }
        }

        public async Task<Result> DeleteDocumentAsync(int id)
        {
            try
            {
                await this.indexRepository.DeleteDocumentAsync(id);

                return OperationResult.OK();
            }
            catch (Exception ex)
            {
                return OperationResult.InternalServerError(ex);
            }
        }
    }
}
