using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishList.Models.Product;
using WishList.Repositories.Index.Context;
using WishList.Repositories.ReadOnly.Interfaces;
using WishList.Services.Models.Common;

namespace WishList.Repositories.ReadOnly
{
    public class ProductQueryRepository : IProductQueryRepository
    {
        private readonly IndexContext context;

        public ProductQueryRepository(IndexContext context) 
        {
            this.context = context;
        }

        public async Task<IEnumerable<ProductModel>> GetAll(PaginationModel paginationModel)
        {
            var search = await context.SearchAsync<ProductModel>(x => x
                .From(paginationModel.Offset)
                .Size(paginationModel.PageSize)
                .Query(q => q.MatchAll())
                .Index(context.GetIndexName<ProductModel>()));

            return search.Documents;
        }

        public async Task<ProductModel> GetById(int id)
        {
            var search = await context.SearchAsync<ProductModel>(x => x
                .Index(context.GetIndexName<ProductModel>())
                .Query(q => q.Term(t => t.Field("id").Value(id))
            ));

            return search.Documents.Count > 0 ? search.Documents.First() : null;
        }

        public async Task<bool> ProductExists(string nameOfProduct)
        {
            var search = await context.SearchAsync<ProductModel>(x => x
               .Index(context.GetIndexName<ProductModel>())
               .Query(q => q
                    .Match(m => m
                        .Field("name").Query(nameOfProduct)
                    )
                )
            );

            return search.Documents.Count > 0;
        }

        public async Task<bool> ProductExists(int id)
        {
            var search = await context.SearchAsync<ProductModel>(x => x
               .Index(context.GetIndexName<ProductModel>())
               .Query(q => q.Term(t => t.Field("id").Value(id))
           ));

            return search.Documents.Count > 0;
        }
    }
}
