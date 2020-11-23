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

        public IEnumerable<dynamic> GetUserInfo(string userID)
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
            foreach (Donation donation in db.CheckAmountOfBlood())
            {
                storedBlood.Add(donation);
            }
            return storedBlood;
        }

        public List<BloodDonor> GetListForRequestDonation(int bloodgroup)
        {
            List<BloodDonor> requestDonation = new List<BloodDonor>();    
            foreach (BloodDonor bloodDonor in db.RequestDonations(bloodgroup))
            {
                requestDonation.Add(bloodDonor);
            }
            return requestDonation;
        }

        public void ChangeDonationStatus (bool availableForDonation, string idNumber)
        {
            if (availableForDonation == true)
            {
                db.UpdateAvailableForDonation(false, idNumber);
            }
            else if (availableForDonation == false)
            {
                db.UpdateAvailableForDonation(true, idNumber);
            }
        }      
    }
}
