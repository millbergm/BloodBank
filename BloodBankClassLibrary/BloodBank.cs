using System.Collections.Generic;

namespace Bloodbank
{
    public class Bloodbank
    {
        void AddNewBloodDonor(string firstname, string lastname, long idnumber, string email, BloodGroup bloodgroup)
        {
            BloodDonor newDonor = new BloodDonor(firstname, lastname, idnumber, email, bloodgroup);

        }

        Login()
        {

        }

        void AddDonation(BloodGroup bloodgroup, int amountOfBlood, long donorID, long staffID)
        {
            Donation newDonation = new Donation (bloodgroup, amountOfBlood, donorID, staffID);
        }

        List<Donation> StoredBlood()
        {

        }

        void FileBloodDonation()
        {

        }

        List<> RequestDonation()
        {

        }
    }    
}