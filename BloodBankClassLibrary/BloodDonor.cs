using System;

namespace Bloodbank
{
    public class BloodDonor : User
        {
        public string Email { get; set; }
        public bool AvailableToDonate { get; set; } = true;
        public bool HealthOK { get; set; } = true;
        public BloodGroup BloodGroup { get; set; }
        public DateTime LatestDonation { get; set; }

        public BloodDonor (string firstname, string lastname, string idnumber, string email, BloodGroup bloodgroup)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.IDNumber = idnumber;
            this.Email = email;
            this.BloodGroup = bloodgroup;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + IDNumber + " " + Email + " " + BloodGroup;
        }



    }
}
