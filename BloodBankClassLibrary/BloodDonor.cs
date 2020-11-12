using System;

namespace Bloodbank
{
    public class BloodDonor : IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long IDNumber { get; set; }
        public string Email { get; set; }
        public bool AvailableToDonate { get; set; } = true;
        public bool HealthOK { get; set; } = true;
        public BloodGroup BloodGroup { get; set; }
        public DateTime LatestDonation { get; set; }

        public BloodDonor (string firstname, string lastname, long idnumber, string email, BloodGroup bloodgroup)
        {
            FirstName = firstname;
            LastName = lastname;
            IDNumber = idnumber;
            Email = email;
            BloodGroup = bloodgroup;
        }



    }
}
