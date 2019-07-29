using System.Collections.Generic;
using System.Threading.Tasks;
using WishList.Services.Models.Common;

namespace WishList.Services.Interfaces
{
    public interface IIndexService<TModel> where TModel : class
    {
        Task IndexDocumentAsync(TModel model, string indexName = null);
        Task UpdateDocumentAsync(int id, TModel model);
        Task DeleteDocumentAsync(int id, TModel model);
    }
}
