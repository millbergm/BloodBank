using System.Collections.Generic;
using System;

namespace BloodbankFunc
{
    public class BloodBank
    {
        const string connectionString = "Server = 40.85.84.155; Database = OOPgroup2; User = Student10; Password = zombie-virus@2020;";
        DBRepository db = new DBRepository(connectionString);
        
        public void AddUser(User user)
        {           
            db.WriteUserToDB(user);
        }

        public int ValidateUserLogin(string userID, string password)
        {            
            IEnumerable<int> checkedAuth = db.CheckUserLogin(userID, password);
            foreach (var item in checkedAuth)
            {
                int auth = Convert.ToInt32(item);
                return auth;
            }
            return 0;
        }

        public IEnumerable<dynamic> GetLoggedInUser(string userID)
        {
      
        return db.GetUserFromDB(userID);

        }


        

       /* public IEnumerable<Object>  GetActiveStaff(string userID)
        {
            return db.GetUserFromDB(userID);
        }*/

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
