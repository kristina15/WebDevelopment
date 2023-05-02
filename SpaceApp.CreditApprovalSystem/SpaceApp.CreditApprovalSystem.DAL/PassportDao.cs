using Microsoft.Extensions.Configuration;
using SpaceApp.CreditApprovalSystem.DALContracts;
using SpaceApp.CreditApprovalSystem.Entities;
using SpaceApp.CreditApprovalSystem.Entities.Enum;
using System.Data;
using System.Data.SqlClient;

namespace SpaceApp.CreditApprovalSystem.DAL;

public class PassportDao : BaseDao, IPassportDao
{
    public PassportDao(IConfiguration configuration) : base(configuration)
    { }

    public void AddPassport(Passport value)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddPassport";
            cmd.Parameters.AddWithValue(@"Series", value.Series);
            cmd.Parameters.AddWithValue(@"Number", value.Number);

            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void AddScan(Passport value, ScanFile scan)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddScanToPassport";
            cmd.Parameters.AddWithValue(@"PassportId", value.Id);
            cmd.Parameters.AddWithValue(@"ScanId", scan.Id);

            connection.Open();
            cmd.ExecuteReader();
        }
    }

    public Passport GetPassportBySeriesAndNumber(Passport value)
    {
        Passport passport;
        using (var connection = new SqlConnection(_connectionString))
        {
            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetPassport";
            cmd.Parameters.AddWithValue(@"Series", value.Series);
            cmd.Parameters.AddWithValue(@"Number", value.Number);

            connection.Open();
            var reader = cmd.ExecuteReader();
            passport = GetPassport(out passport, reader);
        }

        return passport;
    }

    public Passport GetPassportById(int id)
    {
        Passport passport;
        using (var connection = new SqlConnection(_connectionString))
        {
            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetPassportById";
            cmd.Parameters.AddWithValue(@"Id", id);

            connection.Open();
            var reader = cmd.ExecuteReader();
            passport = GetPassport(out passport, reader);
        }

        return passport;
    }

    public void UpdatePassport(Passport value)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpdatePassport";
            cmd.Parameters.AddWithValue(@"Id", value.Id);
            cmd.Parameters.AddWithValue(@"Series", value.Series);
            cmd.Parameters.AddWithValue(@"Number", value.Number);

            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }

    private static Passport GetPassport(out Passport value, SqlDataReader reader)
    {
        value = null;
        if (reader.Read())
        {
            value = new Passport
            {
                Id = (int)reader["Id"],
                Number = reader["Number"] as string,
                Series = reader["Series"] as string,
                Scans = reader["ScanId"] == DBNull.Value ? null : new List<ScanFile>()
                    {
                        new ScanFile
                        {
                            Id = (int)reader["ScanId"],
                            Title = TitleTypesEnum.Passport,
                            Link = (byte[])reader["Link"]
                        }
                    }
            };

            while (reader.Read())
            {
                value.Scans.Add
                (
                    new ScanFile
                    {
                        Id = (int)reader["ScanId"],
                        Title = TitleTypesEnum.Passport,
                        Link = (byte[])reader["Link"]
                    }
                );
            }
        }

        return value;
    }
}
