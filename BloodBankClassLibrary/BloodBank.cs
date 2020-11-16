using System.Collections.Generic;

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

        // Login()
        // {

        // }

        public bool TryValidateUser(string username, string password)
        {
            // foreach (User userLOKAL in users)
            // {
            //     if (userLOKAL.Name == username) //ifall användaren finns "gå in här"
            //     {
            //         if (userLOKAL.Password == password)
            //         {
            //             return true;
            //         }
            //         else
            //         {
            //             return false;
            //         }
            //     }
            // }
            // return false;
        }

        void AddDonation(Donation donation)
        {
            var db = new DBRepository(connectionString);
            db.WriteDonationToDB(donation);
        }

        // List<Donation> StoredBlood()
        // {

        // }

        void FileBloodDonation()
        {

        }

        // List<> RequestDonation()
        // {

        // }
    }    
}