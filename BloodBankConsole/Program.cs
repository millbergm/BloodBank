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

            // Console.Write("First name: ");
            // string firstName = "Viktor"; //Console.ReadLine();
            // Console.Write("Last name: ");
            // string lastName = "Ahlin"; //Console.ReadLine();
            // string idnumber = "19901019-5261";
            // string email = "Viktor@gmail.com";
            // int availabletodonate = 1;
            // int healthOK = 1;
            // DateTime latestDonation = DateTime.Now;
            
            // BloodGroup bloodGroup = BloodGroup.AB;
            // User newDonor = new BloodDonor(firstName, lastName, idnumber, email, availabletodonate, healthOK, bloodGroup, latestDonation);
            // try
            // {
            //     bb.AddUser(newDonor);
            // }
            // catch (SqlException)
            // {
            //     //Console.WriteLine(e);
            //     Console.WriteLine("Nu gick det lite fel !");
            // }
            
            // Console.ReadLine();

            // Console.WriteLine("Enter staff first name:");
            // string staffFirstName = "Linda";  //Console.ReadLine();
            // Console.WriteLine("Enter staff last name:");
            // string staffLastName = "Gren";  //Console.ReadLine();
            // string staffiDNumber = "099500";
            // string staffTitle = "Supervisor";

            // User newStaff = new Staff(staffFirstName, staffLastName, staffiDNumber, staffTitle);
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

            // int amountOfBlood = 1;
            // string donorID = "19901019-5261";
            // string staffID = "099500"; //fylls i automatiskt via inloggningen
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

            // string userID = "66316";
            // string password = "password";
            // if (bb.ValidateUserLogin(userID, password))
            // {
            //     //bb.SetActiveUserAccount();
            //     Console.WriteLine("Du är inloggad!");
            // }
            // else
            // {
            //     Console.WriteLine("Du är INTE inloggad!");
            // }

                // Skriv ut mängden blod / blodgrupp via storeprocedure
            foreach (var item in bb.StoredBlood())
            {
                Console.WriteLine($"{item.AmountOfBlood} Enheter : Blodgrupp {item.Bloodgroup}");
            }

                // Information för att STAFF ska skicka mail om förfrågan av blod
            foreach (var item in bb.GetListForRequestDonation(BloodGroup.A))
            {
                Console.WriteLine($"Till: {item.Email}, Hej {item.FirstName}!, vi behöver mer blod av just din blodgrupp, blodgrupp:{BloodGroup.A}");
            }
        }

        
    }
}
