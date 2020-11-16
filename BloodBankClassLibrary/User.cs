namespace Bloodbank
{
    public abstract class User
    {
        protected string FirstName { get; set; }
        protected string LastName { get; set; }
        protected long IDNumber { get; set;}
    }
}