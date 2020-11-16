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
                            sqlConnection.Execute("EXEC AddDonor (IDNumber, FirstName, LastName, AvailableToDonate, HealthOK, BloodGroupID, Email) VALUES (@user.IDNumber, @user.Firstname, @user.LastName, @user.AvailableToDonate, @user.HealthOK, @Bloodgroup, @Email)", user);
                        }
                        else if (user.GetType() == typeof(Staff))
                        {
                            sqlConnection.Execute("EXEC AddStaff (IDNumber, FirstName, LastName, Title) VALUES (@IDNumber, @FirstName, @LastName)", user);
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
        public IEnumerable<User> GetUserFromDB()
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        //TODO
                        return sqlConnection.Query<User>("EXEC GetDonor");
                        
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
                        sqlConnection.Execute("INSERT INTO BloodBank (AmountOfBlood, BloodGroupID, DonorID, StaffID) VALUES (@donation.AmountOfBlood, @donation.Bloodgroup, @donation.DonorID, @donation.StaffID)", donation);
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
        public IEnumerable<string> RequestDonations(BloodGroup bloodgroup)
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        return sqlConnection.Query<string>("EXEC RequestDonation @bloodgroup;", bloodgroup);
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
        public IEnumerable<User> CheckUserLogin(string userID, string password)
        {
             for (int i = 0; i < 5; i++)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        //TODO
                        return sqlConnection.Query<User>("EXEC GetUserLogin @idnumber, @password;", (userID, password));
                        // 0 = does not exist, 1 = donor, 2 = staff
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
    }
}