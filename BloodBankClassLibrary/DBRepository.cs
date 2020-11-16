using System.Data.SqlClient;
namespace Bloodbank
{
    class DBReopository
    {
        private string ConnectionString { get; set; }

        public DBReopository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public bool WriteUserToDB(User user)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                try
                {
                    //TODO
                    sqlConnection.Execute("INSERT nånting....");
                }
                catch (System.Exception)
                {
                    
                    throw;
                }
                
            }
        }
        public IEnumerable<User> CheckUserFoomDB()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                //TODO
                sqlConnection.Query<User>("SELECT nånting....");
            }
        }
        public IEnumerable<Donation> CheckAmountOfBlood()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                //TODO
                sqlConnection.Query<Donation>("SELECT nåt...")
            }
        }
        public WriteDonationToDB()
        {

        }
    }
}