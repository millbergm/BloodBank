using System.Data.SqlClient;
using Dapper;
namespace Bloodbank
{
    class DBRepository
    {
        private string ConnectionString { get; set; }

        public DBRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public bool WriteUserToDB(User user)
        {
           try
           {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    //TODO
                    sqlConnection.Execute("INSERT nånting....");
                }
           }
           catch
           {
               
               return false;
           }
            return true;
        }
        public IEnumerable<User> CheckUserFomDB()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                //TODO
                return sqlConnection.Query<User>("SELECT nånting....");
            }
        }
        public IEnumerable<Donation> CheckAmountOfBlood()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                //TODO
                return sqlConnection.Query<Donation>("SELECT nåt...")
            }
        }
        public WriteDonationToDB()
        {

        }
    }
}