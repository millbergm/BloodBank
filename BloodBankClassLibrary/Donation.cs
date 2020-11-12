namespace Bloodbank
{
    class Donation
    {
        public Bloodgroup Bloodgroup { get; set; }
        public int AmountOfBlood { get; set; }
        public long DonorID { get; set; }
        public long StaffID { get; set; }

        public Donation (Bloodgroup bloodgroup, int amountOfBlood, long donorID, long staffID)
        {
            this.Bloodgroup = bloodgroup;
            this.AmountOfBlood = amountOfBlood;
            this.DonorID = donorID;
            this.StaffID = staffID
        }
    }
}