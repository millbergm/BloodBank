using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System;

namespace BloodbankFunc
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
                    sqlConnection.Execute("EXEC AddDonor @IDNumber, @Firstname, @LastName, @AvailableToDonate, @HealthOK, @Bloodgroup, @Email, @PassWord;", user);
                }
                else if (user.GetType() == typeof(Staff))
                {
                    sqlConnection.Execute("EXEC AddStaff @IDNumber, @FirstName, @LastName, @Title, @PassWord;", user);
                }

            }
        }

        public IEnumerable<dynamic> GetUserFromDB(string idNumber)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                return sqlConnection.Query<dynamic>($"EXEC GetUserInfo {idNumber}");
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
                
        public IEnumerable<int> CheckUserLogin(string userID, string password)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                return sqlConnection.Query<int>($"EXEC GetUserLogin {userID}, {password};");
                //0 = does not exist, 1 = donor, 2 = staff
            }
        }

        public void UpdateAvailableForDonation(bool changeValue, string userID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Execute($"EXEC UpdateAvailableForDonation {userID}, {Convert.ToInt16(changeValue)};");
            }
        }
    }
}
