using System;

namespace Bloodbank
{
    public class BloodDonor : IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IDNumber { get; set; }
        public string Email { get; set; }
        public bool AvailableToDonate { get; set; } = true;
        public bool HealthOK { get; set; } = true;
        public Bloodtype BloodType { get; set; }
        public DateTime LatestDonation { get; set; }

        public BloodDonor (string firstname, string lastname, int idnumber, string email, Bloodtype bloodtype)
        {
            this.FirstName = firstname;
            LastName = lastname;
            IDNumber = idnumber;
            Email = email;
            BloodType = bloodtype;
            

        }



    }
}
