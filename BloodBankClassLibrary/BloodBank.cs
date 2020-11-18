using System.Collections.Generic;
using System;

namespace Bloodbank
{
    public class Bloodbank
    {
        const string connectionString = "Server = 40.85.84.155; Database = OOPgroup2; User = Student10; Password = zombie-virus@2020;";
        DBRepository db = new DBRepository(connectionString);
        
        public void AddUser(User user)
        {           
            db.WriteUserToDB(user);
        }

        public bool ValidateUserLogin(string userID, string password)
        {            
            IEnumerable<int> checkedAuth = db.CheckUserLogin(userID, password);
            foreach (var item in checkedAuth)
            {
                int auth = Convert.ToInt32(item);
                if (auth == 1 || auth == 2)
                {
                    return true;
                }   
            }
            return false;
        }

        public IEnumerable<User>  GetActiveUser(string userID)
        {
            return db.GetUserFromDB(userID);
        }

        public void AddDonation(Donation donation)
        {           
            db.WriteDonationToDB(donation);
        }

        public List<Donation> StoredBlood()
        {
            List<Donation> storedBlood = new List<Donation>();            
            foreach (var donation in db.CheckAmountOfBlood())
            {
                storedBlood.Add(donation);
            }
            return storedBlood;
        }

        public List<BloodDonor> GetListForRequestDonation(int bloodgroup)
        {
            List<BloodDonor> requestDonation = new List<BloodDonor>();            
            foreach (var donor in db.RequestDonations(bloodgroup))
            {
                requestDonation.Add(donor);
            }
            return requestDonation;
        }

        // public List<User> GetAllUsers()
        // {
        //     List<User> allUsers = new List<User>();            
        //     foreach (var user in db.GetUserLogin())
        //     {
        //         allUsers.Add(user);
        //     }
        //     return allUsers;
        // }
    }
}
