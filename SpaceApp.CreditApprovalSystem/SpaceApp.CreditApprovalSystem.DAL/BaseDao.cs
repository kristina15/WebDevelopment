using Microsoft.Extensions.Configuration;
using SpaceApp.CreditApprovalSystem.DALContracts;
using System.Data.Common;

namespace SpaceApp.CreditApprovalSystem.DAL;

public class BaseDao
{
    protected readonly string _connectionString;

    public BaseDao(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("CreditApprovalSystem");
    }
}
