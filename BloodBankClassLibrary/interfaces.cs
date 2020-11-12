namespace Bloodbank
{
    interface IUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int IDnumber { get; set;}
    }
}