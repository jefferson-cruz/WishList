using System.Collections.Generic;
using System.Threading.Tasks;
using WishList.Services.Models.Common;

namespace WishList.Services.Interfaces
{
    public interface IIndexService<TModel> : IBaseService where TModel : class
    {
        Task IndexDocumentAsync(TModel model);
        Task UpdateDocumentAsync(int id, TModel model);
        Task DeleteDocumentAsync(int id);
    }
}
