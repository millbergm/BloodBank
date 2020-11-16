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
        public void WriteUserToDB(User user)
        {
           for (int i = 0; i < 5; i++)
           {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        if (user.GetType() == typeof(BloodDonor))
                        {
                            sqlConnection.Execute("INSERT INTO Donors (ID, FirstName, LastName, AvailableToDonate, HealthOK, BloodGroupID,) VALUES (@user.firstname)", user);
                        }
                        else if (user.GetType() == typeof(Staff))
                        {
                            sqlConnection.Execute("INSERT INTO ()");
                        }
                        
                    }
                }
                catch
                {
                    if (i == 5)
                    {
                        throw;
                    }
                }
           }
        }
        public IEnumerable<User> CheckUserFomDB()
        {
            for (int i = 0; i < 5; i++)
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
                    if (i == 5)
                    {
                        throw;
                    }
                }
            }
            return null;
        }
        public IEnumerable<Donation> CheckAmountOfBlood()
        {
            for (int i = 0; i < 5; i++)
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
                    if (i == 5)
                    {
                        throw;
                    }
                }
            }
            return null;
        }
        public void WriteDonationToDB(Donation donation)
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        //TODO
                        sqlConnection.Execute("INSERT::::");
                        continue;
                    }
                }
                catch (System.Exception)
                {
                    if (i == 5)
                    {
                        throw;
                    }
                }
            }
            
        }
    }
}