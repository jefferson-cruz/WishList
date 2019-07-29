using System.Collections.Generic;
using System.Text.RegularExpressions;
using WishList.Shared.Notify;
using WishList.Shared.Notify.Notifications;

namespace WishList.Domain.Entities
{
    public class User : Notify
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public ICollection<Wish> Wishes { get; set; }

        protected User() { }

        private User(int id, string name, string email)
        {
            ValidateName(name);
            ValidateEmail(email);

            Id = id;
            Name = name;
            Email = email;
        }

        public static User Create(string name, string email)
        {
            return Create(0, name, email);
        }

        public static User Create(int id, string name, string email)
        {
            return new User(id, name, email);
        }

        private void ValidateEmail(string value)
        {
            if (string.IsNullOrEmpty(value?.Trim()))
            {
                AddNotification<Violation>("Email Is required");

                return;
            }

            const string pattern = @"\A(?:[a-zA-Z0-9!#$%&'*+/=?^_`{|}-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?)\Z";

            if (!Regex.IsMatch(value, pattern))
                AddNotification<Violation>("Invalid email");
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
