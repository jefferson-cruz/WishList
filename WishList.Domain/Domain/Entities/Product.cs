using System.Collections.Generic;
using WishList.Shared.Result;

namespace WishList.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<Wish> Wishes { get; set; }

        protected Product() { }

        private Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Result<Product> Create(string name)
        {
            return Create(0, name);
        }

        public static Result<Product> Create(int id, string name)
        {
            if (string.IsNullOrEmpty(name?.Trim()))
                return OperationResult.BadRequest<Product>("Name is required");

            if (name.Length < 3 && name.Length > 150)
                return OperationResult.BadRequest<Product>("Name must be 3-150 characters");

            return OperationResult.OK(new Product(id, name));
        }

    }
}
