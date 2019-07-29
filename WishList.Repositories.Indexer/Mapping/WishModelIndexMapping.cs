using WishList.Models.Product;
using WishList.Models.Wish;
using WishList.Repositories.Index.Context;

namespace WishList.Repositories.Index.Mapping
{
    public class WishModelIndexMapping
    {
        public WishModelIndexMapping(IndexContext context)
        {
            context.Indices.Create(context.GetIndexName<WishModel>(), c => c
               .Map<WishModel>(m => m
                   .Properties(p => p
                       .Number(nm => nm.Name(n => n.Id))
                       .Nested<ProductModel>(nt => nt.Name(n => n.Products))
                   )
               )
           );
        }
    }
}
