using System;
using System.Text.RegularExpressions;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(
            string firstname, 
            string surname, 
            string email, 
            DateTime dateOfBirth, 
            int clientId
        )
        {
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            if (User.TryCreateUser(client, dateOfBirth, email, firstname, surname, out var user))
            {
                return false;
            }

            FillUserClientInfo(client, ref user);

            if (!user.IsCreditValid())
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }

        private static User FillUserClientInfo(Client client, ref User user)
        {

            if (client.Name == "VeryImportantClient")
            {
                FillVeryImportantClientUser(ref user);
            }
            else if (client.Name == "ImportantClient")
            {
                FillImportantClientUser(ref user);
            }
            else
            {
                FillOrdinaryUser(ref user);
            }

            return user;
        }

        private static void FillOrdinaryUser(ref User user)
        {
            user.HasCreditLimit = true;
            var userCreditService = new UserCreditServiceClient();
            var creditLimit = userCreditService.GetCreditLimit(
                user.FirstName, 
                user.SurName, 
                user.DateOfBirth
            );
            user.CreditLimit = creditLimit;
        }

        private static void FillImportantClientUser(ref User user)
        {
            user.HasCreditLimit = true;
            var userCreditService = new UserCreditServiceClient();
            var creditLimit = userCreditService.GetCreditLimit(
                user.FirstName, 
                user.SurName, 
                user.DateOfBirth
            );
            creditLimit *= 2;
            user.CreditLimit = creditLimit;
        }

        private static void FillVeryImportantClientUser(ref User user)
        {
            user.HasCreditLimit = false;
        }
    }
}
