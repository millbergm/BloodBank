using System.Collections.Generic;
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
        public IEnumerable<User> CheckUserFromDB()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                //TODO
                return sqlConnection.Query<User>("SELECT nånting....");
            }
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        public IEnumerable<Donation> CheckAmountOfBlood()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    //TODO
                    return sqlConnection.Query<Donation>("SELECT nåt...");
                }
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        public void WriteDonationToDB(Donation donation)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    //TODO
                    sqlConnection.Execute("INSERT::::");
                }
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}