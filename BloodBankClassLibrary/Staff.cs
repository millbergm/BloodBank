//using system.collections.generic;

namespace Bloodbank
{
    class Staff : User
    {
        public string Title { get; set; }

        public Staff(string firstName, string lastName, string iDNumber, string title)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.IDNumber = iDNumber;
            this.Title = title;
        }
    }
}