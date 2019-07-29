using WishList.Shared.Notify;
using WishList.Shared.Notify.Notifications;

namespace WishList.Domain.Entities
{
    public class Wish : Notify
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
         
        protected Wish() { }

        private Wish(int userId, int productId)
        {
            Validate(userId, productId);

            UserId = userId;
            ProductId = productId;
        }

        public static Wish Create(int userId, int productId)
        {
            return new Wish(userId, productId);
        }

        private void Validate(int userId, int productId)
        {
            if (userId < 1)
                AddNotification<Violation>("UserId must be greater than zero");

            if (productId < 1)
                AddNotification<Violation>("ProductId must be greater than zero");
        }
    }
}
