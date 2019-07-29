using Nest;
using WishList.Models.Product;
using WishList.Repositories.Index.Context;

namespace WishList.Repositories.Index.Mapping
{
    public class ProductModelIndexMapping
    {
        public ProductModelIndexMapping(IndexContext context)
        {
            context.Indices.Create(context.GetIndexName<ProductModel>(), c => c
                .Map<ProductModel>(m => m
                    .Properties(p => p
                        .Number(nm => nm.Name(n => n.Id).Type(NumberType.Integer))
                        .Text(tx => tx.Name(n => n.Name).Analyzer("keyword"))
                    )
                ));
        }
    }
}
