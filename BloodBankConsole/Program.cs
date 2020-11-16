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
            User newDonor = new BloodDonor(firstName, lastName, idnumber, email, availabletodonate, healthOK, bloodGroup, latestDonation);
            bb.AddUser(newDonor);


            

    // logga in
    //         string name;
    //         string password;
    //         Console.WriteLine("Skriv in ditt användarnamn");
    //         Console.Write(":> ");
    //         name = Console.ReadLine();
    //         Console.WriteLine("Skriv in ditt lösenord");
    //         Console.Write(":> ");
    //         password = Console.ReadLine();

    //         //  ---Act
    //         if (TryValidateUser(name, password))
    //         {
    //             Console.WriteLine("Inloggningen lyckades, välkommen!");
    //         }
    //         else
    //         {
    //             Console.WriteLine("Falaktigt användarnamn eller lösenord");
    //         }

        }
    }
}
