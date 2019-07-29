using System;
using System.Collections.Generic;
using WishList.Models.Product;

namespace WishList.Models.Wish
{
    public class WishModel
    {
        /// <summary>
        /// UserId
        /// </summary>
        public int Id { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
