using System;

namespace BloodbankFunc
{
    public class BloodDonor : User
        {
        public string Email { get; set; }
        public bool AvailableToDonate { get; set; }
        public bool HealthOK { get; set; }        
        public int BloodGroup { get; set; }
        public DateTime LatestDonation { get; set; }

        // public BloodDonor()
        // { 
        // }

        // public BloodDonor (string firstName, string eMail, int bloodGroup)
        // {
        //     this.FirstName = firstName;
        //     this.Email = eMail;
        //     this.BloodGroup = bloodGroup;
        // }
        public BloodDonor (string firstName, string lastName, string idNumber, string eMail, bool availableToDonate, bool healthOK, int bloodGroup, DateTime latestDonation)
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
        public BloodDonor (string firstName, string lastName, string idNumber, string eMail, bool availableToDonate, bool healthOK, int bloodGroup, string passWord)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.IDNumber = idNumber;
            this.Email = eMail;
            this.AvailableToDonate = availableToDonate;
            this.HealthOK = healthOK;           
            this.BloodGroup = bloodGroup;
            this.PassWord = passWord;
        } 
    }
}
