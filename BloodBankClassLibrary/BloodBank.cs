using System.Collections.Generic;
using System;

namespace Bloodbank
{
    public class Bloodbank
    {
        const string connectionString = "Server = 40.85.84.155; Database = OOPgroup2; User = Student10; Password = zombie-virus@2020;";
        public void AddUser(User user)
        {
            var db = new DBRepository(connectionString);
            db.WriteUserToDB(user);
        }

        public bool ValidateUserLogin(string userID, string password)
        {
                var db = new DBRepository(connectionString);
                int auth = Convert.ToInt32(db.CheckUserLogin(userID, password));
                   if (auth == 1 || auth == 2)
                   {
                        return true; 
                   }                
                    return false;   
        }

        public void AddDonation(Donation donation)
        {
            var db = new DBRepository(connectionString);
            db.WriteDonationToDB(donation);
        }

        // List<Donation> StoredBlood()
        // {

        // }

        public void FileBloodDonation()
        {

        }

        // List<> RequestDonation()
        // {

        // }
    }    
}