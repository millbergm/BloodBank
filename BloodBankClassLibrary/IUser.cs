namespace Bloodbank
{
    interface IUser
    {
        string Firstname { get; set; }
        string Lastname { get; set; }
        int IDnumber { get; set;}
    }
}