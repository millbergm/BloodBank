using System;

namespace Bloodbank
{
    public class BloodDonor : User
        {
        public string Email { get; set; }
        public int AvailableToDonate { get; set; }
        public int HealthOK { get; set; }
        public BloodGroup BloodGroup { get; set; }
        public DateTime LatestDonation { get; set; }

        public BloodDonor (string firstname, string lastname, string idnumber, string email, int availabletodonate, int healthOK, BloodGroup bloodgroup, DateTime latestDonation)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.IDNumber = idnumber;
            this.Email = email;
            this.AvailableToDonate = availabletodonate;
            this.HealthOK = healthOK;
            this.BloodGroup = bloodgroup;
            this.LatestDonation = latestDonation;
        }

        public override string ToString() => $"{FirstName}, {LastName}, {IDNumber}, {Email}, {BloodGroup}";

    }
}
