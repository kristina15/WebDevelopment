using System.Configuration;

namespace SCB.Surkova.CreditApprovalSystem.DAL
{
    public abstract class ConfigurationDao
    {
        protected string _connectionString;

        public ConfigurationDao()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }
    }
}
