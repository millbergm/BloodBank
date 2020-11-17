using System.Collections.Generic;
using System;

namespace Bloodbank
{
    public class Bloodbank
    {
        DBRepository db = new DBRepository(connectionString);
        const string connectionString = "Server = 40.85.84.155; Database = OOPgroup2; User = Student10; Password = zombie-virus@2020;";
        public void AddUser(User user)
        {           
            db.WriteUserToDB(user);
        }

        public bool ValidateUserLogin(string userID, string password)
        {            
            int auth = Convert.ToInt32(db.CheckUserLogin(userID, password));
            if (auth == 1 || auth == 2)
            {
                return true;
            }
            return false;
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

        public List<BloodDonor> GetListForRequestDonation(BloodGroup bloodgroup)
        {
            List<BloodDonor> requestDonation = new List<BloodDonor>();            
            foreach (var donor in db.RequestDonations(bloodgroup))
            {
                requestDonation.Add(donor);
            }
            return requestDonation;
        }
    }
}
