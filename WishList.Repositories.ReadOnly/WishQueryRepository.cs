using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishList.Models.Product;
using WishList.Models.Wish;
using WishList.Repositories.Index.Context;
using WishList.Repositories.ReadOnly.Interfaces;
using WishList.Services.Models.Common;

namespace WishList.Repositories.ReadOnly
{
    public class WishQueryRepository : IWishQueryRepository
    {
        private readonly IndexContext context;

        public WishQueryRepository(IndexContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ProductModel>> GetAll(int userId, PaginationModel paginationModel)
        {
            var search = await context.SearchAsync<WishModel>(x => x
               .Query(q => q.Term(f => f.Field("_id").Value(userId)))
               .Index(context.GetIndexName<WishModel>()));

            return search.Documents.Any() ? 
                search.Documents.First().Products
                    .Skip(paginationModel.Offset)
                    .Take(paginationModel.PageSize)
                    .OrderBy(x=> x.Id) : 
                Enumerable.Empty<ProductModel>();
        }

        public async Task<WishModel> GetByUser(int userId)
        {
            var search = await context.SearchAsync<WishModel>(x => x
               .Query(q => q.Term(f => f.Field("_id").Value(userId)))
               .Index(context.GetIndexName<WishModel>()));

            return search.Documents.FirstOrDefault();
        }
    }
}
