using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WhatsAppDAL.Model;

namespace WhatsAppDAL
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly WhatsAppDbContext _dbContext;
        private bool _disposed;

        public WhatsAppService(WhatsAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<long> CreateUser(UserViewModel user)
        {
            /* var name = new SqlParameter
             {
                 ParameterName = "@Name",
                 Value = user.Name,
                 SqlDbType = SqlDbType.VarChar,
                 Direction = ParameterDirection.Input,
                 Size = 20

             };
             var email = new SqlParameter
             {
                 ParameterName = "@Email",
                 Value = user.Email,
                 SqlDbType = SqlDbType.VarChar,
                 Direction = ParameterDirection.Input,
                 Size = 20

             };
             var phoneNumber = new SqlParameter
             {
                 ParameterName = "@Phone",
                 Value = user.PhoneNumber,
                 SqlDbType = SqlDbType.VarChar,
                 Direction = ParameterDirection.Input,
                 Size = 20

             };
             var profileImage = new SqlParameter
             {
                 ParameterName = "@ProfileImage",
                 Value = user.ProfilePhoto,
                 SqlDbType = SqlDbType.VarChar,
                 Direction = ParameterDirection.Input,
                 Size = 20

             };*/
            SqlConnection sqlConn = await _dbContext.OpenConnection();

            string insertQuery =
                $"INSERT INTO USERS (Name, Email, PhoneNumber, ProfileImage)" +
                $" VALUES (@Name, @Email, @Phone, @ProfileImage); SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";

          using (SqlCommand command = new SqlCommand(insertQuery, sqlConn))
            {

                command.Parameters.AddRange(new SqlParameter[]
                {
                new SqlParameter
            {
                ParameterName = "@Name",
                Value = user.Name,
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Size = 20

            },
                new SqlParameter
            {
                ParameterName = "@Email",
                Value = user.Email,
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Size = 20

            },
                new SqlParameter
            {
                ParameterName = "@Phone",
                Value = user.PhoneNumber,
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Input,
                Size = 20

            },
                new SqlParameter
            {
                ParameterName = "@ProfileImage",
                Value = user.ProfilePhoto,
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Size = 20

            }


                });

                //await command.ExecuteNonQueryAsync();

                /*string usedIdQuery =
                    $"SELECT SCOPE_IDENTITY();";

                command.CommandText = usedIdQuery;*/

                // SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                long userId = (long) await command.ExecuteScalarAsync();

                return userId;


            };

        }


        public async Task<int> UpdateUser(int id, UserViewModel user)
        {
            try
            {
                var sqlConn = await _dbContext.OpenConnection();
                string insertQuery =
                    $"UPDATE USERS SET  Name = '{user.Name}', Email = '{user.Email}', " +
                    $"PhoneNumber = '{user.PhoneNumber}', ProfileImage = '{user.ProfilePhoto}' WHERE id = {id}";


                SqlCommand command = new SqlCommand(insertQuery, sqlConn);

                await command.ExecuteNonQueryAsync();
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }


            /*  string usedIdQuery =
                  $"SELECT SCOPE_IDENTITY();";

              command.CommandText = usedIdQuery;

              SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

              while (reader.Read())
              {
                  return (int)(decimal)reader.GetValue(0);
              }*/



        }

        public async Task<int> DeleteUser(int id)
        {
            try
            {
                var sqlConn = await _dbContext.OpenConnection();
                string insertQuery =
                    $"DELETE FROM USERS  WHERE id = {id}";
                SqlCommand command = new SqlCommand(insertQuery, sqlConn);

                await command.ExecuteNonQueryAsync();
                Console.WriteLine("Successfully Deleted");
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<UserViewModel> GetUser(int id)
        {
            var sqlConn = await _dbContext.OpenConnection();

            string insertQuery =
                $"SELECT * FROM USERS WHERE id = {id}";

            SqlCommand command = new SqlCommand(insertQuery, sqlConn);

            UserViewModel user = new UserViewModel();
            using (SqlDataReader myDataReader = command.ExecuteReader())
            {
                while (myDataReader.Read())
                {
                    user.Name = myDataReader["name"].ToString();
                    user.Email = myDataReader["email"].ToString();
                    user.PhoneNumber = myDataReader["phoneNumber"].ToString();
                    user.ProfilePhoto = myDataReader["ProfileImage"].ToString();
                }
                return user;
            }
        }

        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            var sqlConn = await _dbContext.OpenConnection();

            string insertQuery =
                $"SELECT * FROM USERS";

            SqlCommand command = new SqlCommand(insertQuery, sqlConn);

            List<UserViewModel> users = new List<UserViewModel>();
            using (SqlDataReader myDataReader = command.ExecuteReader())
            {
                while (myDataReader.Read())
                {
                    users.Add(new UserViewModel()
                    {
                        Name = myDataReader["name"].ToString(),
                        Email = myDataReader["email"].ToString(),
                        PhoneNumber = myDataReader["phoneNumber"].ToString(),
                        ProfilePhoto = myDataReader["ProfileImage"].ToString()
                    });

                }

                return users;

            }
        }


        protected virtual void Dispose(bool disposing)
        {

            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}