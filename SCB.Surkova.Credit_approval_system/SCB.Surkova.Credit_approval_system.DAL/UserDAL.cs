using SCB.Surkova.CreditApprovalSystem.DAL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SCB.Surkova.CreditApprovalSystem.DAL
{
    public class UserDAL : ConfigurationDao, IUserDAL
    {
        public UserDAL() : base()
        { }

        public void AddUser(User value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddUser";
                cmd.Parameters.AddWithValue(@"FirstName", value.FirstName);
                cmd.Parameters.AddWithValue(@"Surname", value.Surname);
                if (value.Patronymic == null)
                {
                    cmd.Parameters.AddWithValue(@"Patronic", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue(@"Patronic", value.Patronymic);
                }
                cmd.Parameters.AddWithValue(@"Login", value.Login);
                cmd.Parameters.AddWithValue(@"Password", value.HashPassword);
                cmd.Parameters.AddWithValue(@"PassportId", value.Passport.Id);
                if (value.AdditionalFile == null)
                {
                    cmd.Parameters.AddWithValue(@"AdditionalScanId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue(@"AdditionalScanId", value.AdditionalFile.Id);
                }

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public User GetUserByLoginAndPassword(User value)
        {
            User user = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetUserByLoginAndPassword";
                cmd.Parameters.AddWithValue(@"Login", value.Login);
                cmd.Parameters.AddWithValue(@"Password", value.HashPassword);
                connection.Open();

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = GetBasicUser(reader);

                    while (reader.Read())
                    {
                        if (!user.Roles.Contains((string)reader["Title"]))
                        {
                            user.Roles.Add((string)reader["Title"]);
                        }
                    }
                };
            }

            return user;
        }

        public User GetUserByLogin(string login)
        {
            User user = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetUserByLogin";
                cmd.Parameters.AddWithValue(@"Login", login);
                connection.Open();

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = GetBasicUser(reader);

                    while (reader.Read())
                    {
                        if (user.Roles.Contains((string)reader["Title"]))
                        {
                            if (user.Passport.Scans != null)
                            {
                                user.Passport.Scans.Add(
                                new ScanFile
                                {
                                    Id = (int)reader["PassportScanId"],
                                    Title = TypeTitles.Passport,
                                    Link = (byte[])reader["Link"]
                                });
                            }
                        }
                        else
                        {
                            user.Roles.Add((string)reader["Title"]);
                        }
                    }
                }
            }

            return user;
        }

        public void UpdatePassword(User value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdatePassword";
                cmd.Parameters.AddWithValue(@"Id", value.Id);
                cmd.Parameters.AddWithValue(@"NewPassword", value.HashPassword);

                connection.Open();
                cmd.ExecuteReader();
            }
        }

        public IEnumerable<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetUsers";
                connection.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var value = users
                                .Where(u => u.Id == (int)reader["Id"])
                                .Count() > 0 ? users
                                .First(u => u.Id == (int)reader["Id"]) : null;

                    if (value == null)
                    {
                        users.Add(new User
                        {
                            Id = (int)reader["Id"],
                            FirstName = (string)reader["FirstName"],
                            Surname = (string)reader["Surname"],
                            Patronymic = (reader["Patronic"] == DBNull.Value) ? null : (string)reader["Patronic"],
                            HashPassword = (byte[])reader["Password"],
                            Login = (string)reader["Login"],
                            Passport = new Passport
                            {
                                Id = (int)reader["PassportId"],
                                Series = (string)reader["Series"],
                                Number = (string)reader["Number"]
                            },
                            AdditionalFile = (reader["AdditionalScanId"] == DBNull.Value) ? null : new ScanFile
                            {
                                Id = (int)reader["AdditionalScanId"],
                                Title = (TypeTitles)((int)reader["AdditionalScanTypeId"]),
                                Link = (byte[])reader["AdditionalScanLink"]
                            },
                            Roles = new List<string>
                        {
                            (string)reader["Title"]
                        }
                        });
                    }
                    else
                    {
                        if (!value.Roles.Contains((string)reader["Title"]))
                        {
                            users.First(u => u.Id == (int)reader["Id"]).Roles.Add((string)reader["Title"]);
                        }
                    }
                }
            }

            return users;
        }

        public User GetUserById(int id)
        {
            User user = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetUserByID";
                cmd.Parameters.AddWithValue(@"Id", id);
                connection.Open();

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = GetBasicUser(reader);

                    while (reader.Read())
                    {
                        if (user.Roles.Contains((string)reader["Title"]))
                        {
                            if (user.Passport.Scans != null)
                            {
                                user.Passport.Scans.Add(
                                new ScanFile
                                {
                                    Id = (int)reader["PassportScanId"],
                                    Title = TypeTitles.Passport,
                                    Link = (byte[])reader["Link"]
                                });
                            }
                        }
                        else
                        {
                            user.Roles.Add((string)reader["Title"]);
                        }
                    }
                }
            }

            return user;
        }

        public void AddRole(User value, string role)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddRoleForUser";
                cmd.Parameters.AddWithValue(@"Id", value.Id);
                cmd.Parameters.AddWithValue(@"Title", role);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddScan(User value, ScanFile scan)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddScanForUser";
                cmd.Parameters.AddWithValue(@"Id", value.Id);
                cmd.Parameters.AddWithValue(@"ScanId", scan.Id);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateUser(User value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateUser";
                cmd.Parameters.AddWithValue(@"Id", value.Id);
                cmd.Parameters.AddWithValue(@"FirstName", value.FirstName);
                cmd.Parameters.AddWithValue(@"Surname", value.Surname);
                if (value.Patronymic == null)
                {
                    cmd.Parameters.AddWithValue(@"Patronic", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue(@"Patronic", value.Patronymic);
                }

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<User> GetUserBySurname(string surname)
        {
            List<User> users = new List<User>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetUserBySurname";
                cmd.Parameters.AddWithValue(@"Surname", surname);
                connection.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (users.Where(u => u.Id == (int)reader["Id"]).Count() == 0)
                    {
                        users.Add(new User
                        {
                            Id = (int)reader["Id"],
                            FirstName = (string)reader["FirstName"],
                            Surname = (string)reader["Surname"],
                            Patronymic = (reader["Patronic"] == DBNull.Value) ? null : (string)reader["Patronic"],
                            HashPassword = (byte[])reader["Password"],
                            Login = (string)reader["Login"],
                            Passport = new Passport
                            {
                                Id = (int)reader["PassportId"],
                                Series = (string)reader["Series"],
                                Number = (string)reader["Number"]
                            },
                            AdditionalFile = (reader["AdditionalScanId"] == DBNull.Value) ? null : new ScanFile
                            {
                                Id = (int)reader["AdditionalScanId"],
                                Title = (TypeTitles)((int)reader["AdditionalScanTypeId"]),
                                Link = (byte[])reader["AdditionalScanLink"]
                            },
                            Roles = new List<string>
                        {
                            (string)reader["Title"]
                        }
                        });
                    }
                    else
                    {
                        users.First(u => u.Id == (int)reader["Id"]).Roles.Add((string)reader["Title"]);
                    }
                }
            }

            return users;
        }

        private static User GetBasicUser(SqlDataReader reader)
        {
            return new User
            {
                Id = (int)reader["Id"],
                FirstName = reader["FirstName"] as string,
                Surname = reader["Surname"] as string,
                Patronymic = (reader["Patronic"] == DBNull.Value) ? null : reader["Patronic"] as string,
                HashPassword = (byte[])reader["Password"],
                Login = reader["Login"] as string,
                Passport = new Passport
                {
                    Id = (int)reader["PassportId"],
                    Series = reader["Series"] as string,
                    Number = reader["Number"] as string,
                    Scans = (reader["PassportScanId"] == DBNull.Value) ? null : new List<ScanFile>
                        {
                            new ScanFile
                            {
                            Id = (int)reader["PassportScanId"],
                            Title = TypeTitles.Passport,
                            Link = (byte[])reader["Link"]
                            }}
                },
                AdditionalFile = (reader["AdditionalScanId"] == DBNull.Value) ? null : new ScanFile
                {
                    Id = (int)reader["AdditionalScanId"],
                    Title = (TypeTitles)((int)reader["AdditionalScanTypeId"]),
                    Link = (byte[])reader["AdditionalScanLink"]
                },
                Roles = reader["Title"] == DBNull.Value ? null : new List<string>
                        {
                            reader["Title"] as string
                        }
            };
        }
    }
}
