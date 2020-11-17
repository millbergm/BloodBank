namespace Bloodbank
{
    public class Donation
    {
        public BloodGroup Bloodgroup { get; set; }
        public int AmountOfBlood { get; set; }
        public long DonorID { get; set; }
        public long StaffID { get; set; }

        public Donation (BloodGroup bloodGroup, int amountOfBlood, long donorID, long staffID)
        {
            this.Bloodgroup = bloodGroup;
            this.AmountOfBlood = amountOfBlood;
            this.DonorID = donorID;
            this.StaffID = staffID;
        }
    }
}