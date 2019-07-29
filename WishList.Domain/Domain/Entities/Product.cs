using System.Collections.Generic;
using WishList.Shared.Notify;
using WishList.Shared.Notify.Notifications;

namespace WishList.Domain.Entities
{
    public class Product : Notify
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<Wish> Wishes { get; set; }

        protected Product() { }

        private Product(int id, string name)
        {
            ValidateName(name);

            Id = id;
            Name = name;
        }

        public static Product Create(string name)
        {
            return Create(0, name);
        }

        public static Product Create(int id, string name)
        {
            return new Product(id, name);
        }

        private void ValidateName(string value)
        {
            if (string.IsNullOrEmpty(value?.Trim()))
            {
                AddNotification<Violation>("Name is required");

                return;
            }

            if (value.Length < 3 && value.Length > 150)
                AddNotification<Violation>("Name must be 3-150 characters");
        }
    }
}
