using System;
using System.Data.SqlClient;

namespace Bloodbank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Bloodbank bb = new Bloodbank();

            Console.Write("First name: ");
            string firstName = "Anna"; //Console.ReadLine();
            Console.Write("Last name: ");
            string lastName = "Svensson"; //Console.ReadLine();
            string idnumber = "1234567";
            string email = "test@mail.com";
            int availabletodonate = 1;
            int healthOK = 1;
            DateTime latestDonation = DateTime.Now;
            
            BloodGroup bloodGroup = BloodGroup.O;
            User newDonor = new BloodDonor(firstName, lastName, idnumber, email, availabletodonate, healthOK, bloodGroup, latestDonation);
            try
            {
                bb.AddUser(newDonor);
            }
            catch (SqlException)
            {
                //Console.WriteLine(e);
                Console.WriteLine("Nu gick det lite fel !");
            }
            
            // Console.ReadLine();

            // Console.WriteLine("Enter staff first name:");
            // string staffFirstName = Console.ReadLine();
            // Console.WriteLine("Enter staff last name:");
            // string staffLastName = Console.ReadLine();
            // string staffiDNumber = "787878";
            // string staffTitle = "Supervisor";

            // User newStaff = new Staff(firstName, lastName, staffiDNumber, staffTitle);
            // try
            // {
            //     bb.AddUser(newStaff);
            // }
            // catch (SqlException)
            // {
            //     //Console.WriteLine(e);
            //     Console.WriteLine("Nu gick det lite fel!");
            // }

            //Console.ReadLine();

            // int amountOfBlood = 3;
            // string donorID = idnumber;
            // string staffID = "66316"; //fylls i automatiskt via inloggningen
            // Donation donation = new Donation(amountOfBlood, donorID, staffID);
            // try
            // {
            //     bb.AddDonation(donation);
            // }
            // catch (SqlException e)
            // {
            //     Console.WriteLine(e);
            //     Console.WriteLine("Nu gick det lite fel med donationen!");
            // }

            string userID = "66316";
            string password = "password";
            if (bb.ValidateUserLogin(userID, password))
            {
                //bb.SetActiveUserAccount();
                Console.WriteLine("Du är inloggad!");
            }
            else
            {
                Console.WriteLine("Du är INTE inloggad!");
            }


            Console.WriteLine();
            bb.StoredBlood();






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
