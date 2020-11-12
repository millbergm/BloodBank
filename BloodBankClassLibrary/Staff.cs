//using system.collections.generic;

namespace Bloodbank
{
    class Staff : IUser
    {
        public string FirstName { get; set; }   
        public string LastName { get; set; }    
        public long IDNumber { get; set; }   
        public string Title { get; set; }

        public Staff(string firstName, string lastName, long iDNumber, string title)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.IDNumber = iDNumber;
            this.Title = title;
        }
    }
}