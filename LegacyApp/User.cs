using System;
using System.Text.RegularExpressions;

namespace LegacyApp
{
    public class User
    {
        private User()
        { }

        public Client Client { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string EmailAddress { get; private set; }
        public string FirstName { get; private set; }
        public string SurName { get; private set; }
        public bool HasCreditLimit { get; internal set; }
        public decimal CreditLimit { get; internal set; }

        public static User Create(
            Client client, 
            DateTime dateOfBirth, 
            string emailAddress, 
            string firstName, 
            string surName
        )
        {
            return new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = emailAddress,
                FirstName = firstName,
                SurName = surName,
            };
        }

        public static bool TryCreateUser(
            Client client,
            DateTime dateOfBirth,
            string emailAddress,
            string firstName,
            string surName,
            out User user
        )
        {
            if (!AreUserInfoValid(firstName, surName, emailAddress, dateOfBirth))
            {
                user = null;
                return false;
            }

            user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = emailAddress,
                FirstName = firstName,
                SurName = surName,
            };

            return true;
        }

        public bool IsCreditValid()
        {
            if (HasCreditLimit && CreditLimit < 500)
            {
                return false;
            }

            return true;
        }

        private static bool AreUserInfoValid(
            string firstname,
            string surname,
            string email,
            DateTime dateOfBirth
        )
        {
            if (string.IsNullOrWhiteSpace(firstname) || string.IsNullOrWhiteSpace(surname))
            {
                return false;
            }

            if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                return false;
            }

            var now = DateTime.Now;
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month ||
                (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)
            )
            {
                age--;
            }

            if (age < 21)
            {
                return false;
            }

            return true;
        }
    }
}
