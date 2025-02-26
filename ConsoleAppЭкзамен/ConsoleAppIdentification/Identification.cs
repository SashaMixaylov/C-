using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppIdentification
{
    public class AuthenticationManager : Connection
    {
        public User AuthenticateUser(string login, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT UserID, Login, Role FROM Users WHERE Login = @Login";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);

                    using (var read = command.ExecuteReader())
                    {
                        if (read.Read())
                        {
                            
                            return new User
                            {
                                UserID = (int)read["UserID"],
                                Login = read["Login"].ToString(),
                                Role = read["Role"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}
