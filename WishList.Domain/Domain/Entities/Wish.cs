using WishList.Shared.Result;

namespace WishList.Domain.Entities
{
    public class Wish 
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
         
        protected Wish() { }

        private Wish(int userId, int productId)
        {
            UserId = userId;
            ProductId = productId;
        }

        public static Result<Wish> Create(int userId, int productId)
        {
            var validateResult = Validate(userId, productId);

            if (validateResult.Failure) return validateResult;

            return OperationResult.Created(new Wish(userId, productId));
        }

        private static Result<Wish> Validate(int userId, int productId)
        {
            if (userId < 1)
                return OperationResult.BadRequest<Wish>("UserId must be greater than zero");

            if (productId < 1)
                return OperationResult.BadRequest<Wish>("ProductId must be greater than zero");

            return OperationResult.OK<Wish>();
        }
    }
}
