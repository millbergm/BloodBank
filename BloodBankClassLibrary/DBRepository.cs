using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System;

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
                            sqlConnection.Execute("EXEC AddDonor @IDNumber, @Firstname, @LastName, @AvailableToDonate, @HealthOK, @Bloodgroup, @Email, '';", user);
                            //sqlConnection.Execute("INSERT INTO Donors (IDNumber, BloodGroupID) VALUES (@IDNumber, @BloodGroup);", user);
                            break;
                        }
                        else if (user.GetType() == typeof(Staff))
                        {
                            sqlConnection.Execute("EXEC AddStaff (IDNumber, FirstName, LastName, Title) VALUES (@IDNumber, @FirstName, @LastName)", user);
                        }
                        
                    }
                }
                catch (SqlException e)
                {
                    if (i == 4)
                    {
                        Console.WriteLine(e);               
                    }
                }
           }
        }
        public IEnumerable<User> GetUserFromDB(object o)
        {
            int loginStatus = Convert.ToInt32(o);
            for (int i = 0; i < 5; i++)
            {
                if (loginStatus == 1)
                {
                    ///////////////////////////////////
                }
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        //TODO/////////////////////////////////////////////////////////////////////////////////////////
                        return sqlConnection.Query<User>("EXEC GetDonor");
                        
                    }
                }
                catch (System.Exception)
                {
                    if (i == 4)
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
                    if (i == 4)
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
                        sqlConnection.Execute("EXEC AddDonation (@AmountOfBlood, @Bloodgroup, @DonorID, @StaffID)", donation);
                        continue;
                    }
                }
                catch (System.Exception)
                {
                    if (i == 4)
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
                    if (i == 4)
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
                        GetUserFromDB(sqlConnection.Query<User>("EXEC GetUserLogin @idnumber, @password;", (userID, password)));
                        // 0 = does not exist, 1 = donor, 2 = staff
                    }
                }
                catch (System.Exception)
                {
                    if (i == 4)
                    {
                        throw;
                    }
                }
            }
            return null;
        } 
    }
}