namespace Bloodbank
{
    public abstract class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IDNumber { get; set;}
        public string PassWord { get; set; }
    }
}