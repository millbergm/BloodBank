namespace BloodbankFunc
{
    public class Donation
    {
        public int Bloodgroup { get; set; }
        public int AmountOfBlood { get; set; }
        public string DonorID { get; set; }
        public string StaffID { get; set; }

        public Donation (int amountOfBlood, int bloodGroup)
        {
            this.AmountOfBlood = amountOfBlood;
            this.Bloodgroup = bloodGroup;
        }
      
        public Donation (int amountOfBlood, string donorID, string staffID)
        {
            this.AmountOfBlood = amountOfBlood;
            this.DonorID = donorID;
            this.StaffID = staffID;
        }
    }
}