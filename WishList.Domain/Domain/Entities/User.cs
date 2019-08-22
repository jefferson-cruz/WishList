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

        public static Result<User> Create(string name, string email)
        {
            return Create(0, name, email);
        }

        public static Result<User> Create(int id, string name, string email)
        {
            var validateName = ValidateName(name);
            if (validateName.Failure) return validateName;

            var validateEmail = ValidateEmail(email);
            if (validateName.Failure) return validateName;

            return OperationResult.Created(new User(id, name, email));
        }

        private static Result<User> ValidateEmail(string value)
        {
            if (string.IsNullOrEmpty(value?.Trim()))
                return OperationResult.BadRequest<User>("Email Is required");

            const string pattern = @"\A(?:[a-zA-Z0-9!#$%&'*+/=?^_`{|}-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?)\Z";

            if (!Regex.IsMatch(value, pattern))
                return OperationResult.BadRequest<User>("Invalid Email");

            return OperationResult.OK<User>();
        }

        private static Result<User> ValidateName(string value)
        {
            if (string.IsNullOrEmpty(value?.Trim()))
                return OperationResult.BadRequest<User>("Name is required");

            if (value.Length < 3 && value.Length > 150)
                return OperationResult.BadRequest<User>("Name must be 3-150 characters");

            return OperationResult.OK<User>();
        }
    }
}
