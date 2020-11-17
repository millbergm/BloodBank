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

        public BloodDonor(string idNumber, string passWord)
        { 
            this.IDNumber = idNumber;
            this.PassWord = passWord;
        }

        public BloodDonor (string firstName, string eMail, BloodGroup bloodGroup)
        {
            this.FirstName = firstName;
            this.Email = eMail;
            this.BloodGroup = bloodGroup;
        }
        public BloodDonor (string firstName, string lastName, string idNumber, string eMail, int availableToDonate, int healthOK, BloodGroup bloodGroup, DateTime latestDonation)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.IDNumber = idNumber;
            this.Email = eMail;
            this.AvailableToDonate = availableToDonate;
            this.HealthOK = healthOK;
            this.BloodGroup = bloodGroup;
            this.LatestDonation = latestDonation;
        }

        public override string ToString() => $"{FirstName}, {LastName}, {IDNumber}, {Email}, {BloodGroup}";

    }
}
