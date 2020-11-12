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
        public Bloodtype bloodtype
        public DateTime LatestDonation { get; set; }

    }
}
