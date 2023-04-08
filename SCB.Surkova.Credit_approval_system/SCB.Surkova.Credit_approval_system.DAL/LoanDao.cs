using SCB.Surkova.CreditApprovalSystem.DAL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SCB.Surkova.CreditApprovalSystem.DAL
{
    public class LoanDao : ConfigurationDao, ILoanDao
    {
        public LoanDao() : base()
        { }

        public void AddLoan(Loan value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddApplication";
                cmd.Parameters.AddWithValue(@"UserId", value.UserId);
                cmd.Parameters.AddWithValue(@"Sum", value.Sum);
                cmd.Parameters.AddWithValue(@"PassportId", value.PassportId);
                cmd.Parameters.AddWithValue(@"DateCreate", value.DateCreate);
                cmd.Parameters.AddWithValue(@"Status", ((byte)value.Status));
                cmd.Parameters.AddWithValue(@"AdditionalScanId", value.AdditionalScanId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Loan> GetLoansOfUser(User value)
        {
            List<Loan> applications = new List<Loan>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetApplicationsOfUser";
                cmd.Parameters.AddWithValue(@"id", value.Id);
                connection.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    applications.Add(new Loan
                    {
                        Id = (int)reader["Id"],
                        UserId = (int)reader["UserId"],
                        Sum = (long)reader["Sum"],
                        DateCreate = (DateTime)reader["DateCreate"],
                        Status = (Status)((int)reader["StatusId"]),
                        AdditionalScanId = (int)reader["AdditionalScanId"],
                        PassportId = (int)reader["PassportId"]
                    });
                }
            }

            return applications;
        }

        public Loan GetLoanById(int id)
        {
            Loan application = new Loan();
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetApplication";
                cmd.Parameters.AddWithValue(@"Id", id);
                connection.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    application = new Loan
                    {
                        Id = (int)reader["Id"],
                        UserId = (int)reader["UserId"],
                        Sum = (long)reader["Sum"],
                        DateCreate = (DateTime)reader["DateCreate"],
                        Status = (Status)((int)reader["StatusId"]),
                        AdditionalScanId = (int)reader["AdditionalScanId"],
                        PassportId = (int)reader["PassportId"]
                    };
                }
            }

            return application;
        }

        public IEnumerable<Loan> GetCurrentLoans()
        {
            List<Loan> applications = new List<Loan>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetCurrentApplications";
                connection.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    applications.Add(new Loan
                    {
                        Id = (int)reader["Id"],
                        UserId = (int)reader["UserId"],
                        Sum = (long)reader["Sum"],
                        DateCreate = (DateTime)reader["DateCreate"],
                        Status = (Status)((int)reader["StatusId"]),
                        AdditionalScanId = (int)reader["AdditionalScanId"],
                        PassportId = (int)reader["PassportId"]
                    });
                }
            }

            return applications;
        }

        public IEnumerable<Loan> GetHistoryOfLoans()
        {
            {
                List<Loan> applications = new List<Loan>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GetHistoryOfApplications";
                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        applications.Add(new Loan
                        {
                            Id = (int)reader["Id"],
                            UserId = (int)reader["UserId"],
                            Sum = (long)reader["Sum"],
                            DateCreate = (DateTime)reader["DateCreate"],
                            Status = (Status)((int)reader["StatusId"]),
                            AdditionalScanId = (int)reader["AdditionalScanId"],
                            PassportId = (int)reader["PassportId"]
                        });
                    }
                }

                return applications;
            }
        }

        public void UpdateStatus(Loan value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateStatus";
                cmd.Parameters.AddWithValue(@"Id", value.Id);
                cmd.Parameters.AddWithValue(@"NewStatus", ((int)value.Status));

                connection.Open();
                cmd.ExecuteReader();
            }
        }

        public void DeleteLoan(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteLoan";
                cmd.Parameters.AddWithValue(@"Id", id);

                connection.Open();
                cmd.ExecuteReader();
            }
        }
    }
}
