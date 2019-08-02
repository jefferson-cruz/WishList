using System.Collections.Generic;
using System.Text.RegularExpressions;
using WishList.Shared.Result;

namespace WishList.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public ICollection<Wish> Wishes { get; set; }

        protected User() { }

        private User(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public static IResultBase<User> Create(string name, string email)
        {
            return Create(0, name, email);
        }

        public static IResultBase<User> Create(int id, string name, string email)
        {
            var validateName = ValidateName(name);

            if (validateName.Failure)
                return validateName;

            var validateEmail = ValidateEmail(email);

            if (validateName.Failure) return validateName;

            return new CreatedResult<User>(new User(id, name, email));
        }

        private static IResultBase<User> ValidateEmail(string value)
        {
            if (string.IsNullOrEmpty(value?.Trim()))
                return new BadRequestResult<User>("Email Is required");

            const string pattern = @"\A(?:[a-zA-Z0-9!#$%&'*+/=?^_`{|}-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?)\Z";

            if (!Regex.IsMatch(value, pattern))
                return new BadRequestResult<User>("Invalid Email");

            return new OkResult<User>();
        }

        private static IResultBase<User> ValidateName(string value)
        {
            if (string.IsNullOrEmpty(value?.Trim()))
                return new BadRequestResult<User>("Name is required");

            if (value.Length < 3 && value.Length > 150)
                return new BadRequestResult<User>("Name must be 3-150 characters");

            return new OkResult<User>();
        }
    }
}
