namespace Bloodbank
{
    interface IUser
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        int IDNumber { get; set;}
    }
}