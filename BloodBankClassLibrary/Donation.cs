namespace BloodbankFunc
{
    public class Donation
    {
        public BloodGroup Bloodgroup { get; set; }
        public int AmountOfBlood { get; set; }
        public string DonorID { get; set; }
        public string StaffID { get; set; }

        public Donation (int amountOfBlood, BloodGroup bloodGroup)
        {
            this.Bloodgroup = bloodGroup;
            this.AmountOfBlood = amountOfBlood;
        }
        public Donation (int amountOfBlood, string donorID, string staffID)
        {
            this.AmountOfBlood = amountOfBlood;
            this.DonorID = donorID;
            this.StaffID = staffID;
        }
    }
}