using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public class UserDataAccess
    {
        public static void AddUser(User user)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand()
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "uspAddUser"
                };
                command.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 50) { Value = user.FirstName });
                command.Parameters.Add(new SqlParameter("@SurName", SqlDbType.NVarChar, 50) { Value = user.SurName });
                command.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = user.DateOfBirth });
                command.Parameters.Add(new SqlParameter("@EmailAddress", SqlDbType.NVarChar, 50) { Value = user.EmailAddress });
                command.Parameters.Add(new SqlParameter("@HasCreditLimit", SqlDbType.Bit) { Value = user.HasCreditLimit });
                command.Parameters.Add(new SqlParameter("@CreditLimit", SqlDbType.Money) { Value = user.CreditLimit });
                command.Parameters.Add(new SqlParameter("@ClientId", SqlDbType.Int) { Value = user.Client.Id });

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
