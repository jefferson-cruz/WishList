using System.Threading.Tasks;
using WishList.Repositories.Index.Interfaces;
using WishList.Services.Interfaces;
using WishList.Shared.Notify.Notifications;

namespace WishList.Services
{
    public class IndexService<TModel> : IIndexService<TModel> where TModel : class
    {
        private readonly IIndexRepository<TModel> indexRepository;

        public IndexService(IIndexRepository<TModel> indexRepository)
        {
            this.indexRepository = indexRepository;
        }

        public async Task IndexDocumentAsync(TModel model, string indexName = null)
        {
            await this.indexRepository.IndexDocumentAsync(model, indexName);
        }

        public async Task UpdateDocumentAsync(int id, TModel model)
        {
            await this.indexRepository.UpdateDocumentAsync(id, model);
        }

        public async Task DeleteDocumentAsync(int id, TModel model)
        {
            await this.indexRepository.DeleteDocumentAsync(id, model);
        }
    }
}
