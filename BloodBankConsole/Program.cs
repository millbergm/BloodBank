using System;

namespace Bloodbank
{
    class Program
    {
        static void Main(string[] args)
        {
            Bloodbank bb = new Bloodbank();

            Console.Write("First name: ");
            string firstName = "Anna"; //Console.ReadLine();
            Console.Write("Last name: ");
            string lastName = "Svensson"; //Console.ReadLine();
            string idnumber = "1234567";
            string email = "test@mail.com";
            BloodGroup bloodGroup = BloodGroup.O;
            User newDonor = new BloodDonor(firstName, lastName, idnumber, email, bloodGroup);
            bb.AddUser(newDonor);




            BloodDonor donor1 = new BloodDonor("Maria", "Larsson", "8604241234", "maria.larsson@hotmail.com", BloodGroup.A);

            // if (donor1.AvailableToDonate)
            // {
            //     Console.WriteLine(donor1.FirstName + " " + donor1.LastName + " " + donor1.BloodGroup + " " + donor1.IDNumber);
            // }



            Console.WriteLine(donor1);
        }
    }
}
