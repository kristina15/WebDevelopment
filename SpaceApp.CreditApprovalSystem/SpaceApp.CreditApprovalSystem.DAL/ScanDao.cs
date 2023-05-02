using Microsoft.Extensions.Configuration;
using SpaceApp.CreditApprovalSystem.DALContracts;
using SpaceApp.CreditApprovalSystem.Entities;
using SpaceApp.CreditApprovalSystem.Entities.Enum;
using System.Data;
using System.Data.SqlClient;

namespace SpaceApp.CreditApprovalSystem.DAL;

public class ScanDao : BaseDao, IScanDao
{
    public ScanDao(IConfiguration configuration) : base(configuration)
    { }

    public ScanFile AddScan(ScanFile value)
    {
        ScanFile file = null;
        using (var connection = new SqlConnection(_connectionString))
        {
            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddScan";
            cmd.Parameters.AddWithValue(@"TypeId", (int)value.Title);
            cmd.Parameters.AddWithValue(@"Link", value.Link);

            connection.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                file = new ScanFile
                {
                    Id = (int)reader["Id"],
                    Link = (byte[])reader["Link"],
                    Title = (TitleTypesEnum)(int)reader["TypeId"]
                };
            }
        }

        return file;
    }

    public ScanFile GetScanById(int id)
    {
        ScanFile file = null;
        using (var connection = new SqlConnection(_connectionString))
        {
            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetScanById";
            cmd.Parameters.AddWithValue(@"Id", id);
            connection.Open();

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                file = new ScanFile
                {
                    Id = (int)reader["Id"],
                    Link = (byte[])reader["Link"],
                    Title = (TitleTypesEnum)(int)reader["TypeId"]
                };
            }
        }
        return file;
    }

    public void UpdateScan(ScanFile value)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpdateScan";
            cmd.Parameters.AddWithValue(@"Id", value.Id);
            cmd.Parameters.AddWithValue(@"TypeId", (int)value.Title);
            cmd.Parameters.AddWithValue(@"Link", value.Link);

            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
