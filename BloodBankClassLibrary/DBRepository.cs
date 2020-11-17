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
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                if (user.GetType() == typeof(BloodDonor))
                {
                    sqlConnection.Execute("EXEC AddDonor @IDNumber, @Firstname, @LastName, @AvailableToDonate, @HealthOK, @Bloodgroup, @Email, '';", user);
                }
                else if (user.GetType() == typeof(Staff))
                {
                    sqlConnection.Execute("EXEC AddStaff @IDNumber, @FirstName, @LastName, @Title, '';", user);
                }

            }
        }

        public IEnumerable<User> GetUserFromDB(object o)
        {
            int loginStatus = Convert.ToInt32(o);
            if (loginStatus == 1)
            {
                ///////////////////////////////////
            }
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                //TODO/////////////////////////////////////////////////////////////////////////////////////////
                return sqlConnection.Query<User>("EXEC GetDonor");
            }
        }
        public IEnumerable<Donation> CheckAmountOfBlood()   //funkar
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                //TODO
                return sqlConnection.Query<Donation>("EXEC CheckBloodBank");
            }
        }
        public void WriteDonationToDB(Donation donation)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Execute("EXEC AddDonation @AmountOfBlood, @DonorID, @StaffID", donation);
            }
        }
        public IEnumerable<BloodDonor> RequestDonations(int bloodgroup)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {                
                return sqlConnection.Query<BloodDonor>($"EXEC RequestDonation {bloodgroup}");
            }
        }  
                
        // public IEnumerable<BloodDonor> CheckUserLogin(string userID, string password)
        // {
        //     using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
        //     {
        //         List<BloodDonor> userLoggedin = new List<BloodDonor>();
        //         userLoggedin.Add(sqlConnection.Query<int>("EXEC GetUserLogin @idnumber, @password;")); //new BloodDonor{IDNumber = @idnumber, @password = password})); //????
        //         return userLoggedin;
        //     }
        // }

        public IEnumerable<User> GetUserLogin()  
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                //TODO
                return sqlConnection.Query<User>("EXEC GetUserLogin2");
            }
        }
    }
}
