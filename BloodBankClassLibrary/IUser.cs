namespace Bloodbank
{
    interface IUser
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        long IDNumber { get; set;}
    }
}