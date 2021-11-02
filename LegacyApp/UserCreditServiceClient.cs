using System;
using System.Configuration;
using System.Net.Http;

namespace LegacyApp
{
    public class UserCreditServiceClient
    {
        public decimal GetCreditLimit(string firstName, string surName, DateTime dateOfBirth)
        {
            var serviceAddress = ConfigurationManager.AppSettings["CreditServiceAddress"];
            var credit = new HttpClient().GetStringAsync($"{serviceAddress}/getCreditLimit?firstName={firstName}&surName={surName}&dateOfBirth={dateOfBirth}").GetAwaiter().GetResult();
            return decimal.Parse(credit);
        }
    }
}
