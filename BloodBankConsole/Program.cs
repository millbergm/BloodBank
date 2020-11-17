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

            foreach (var item in bb.GetAllUsers())
            {
                Console.WriteLine($"AnvändarID: {item.IDNumber} Password: {item.PassWord}");
            }

            //Information för att STAFF ska skicka mail om förfrågan av blod
            int bloodgroup = 2;
            //BloodGroup bloodgroup = BloodGroup.A;
            foreach (var item in bb.GetListForRequestDonation(bloodgroup))
            {
                Console.WriteLine($"Till: {item.Email}, Hej {item.FirstName}!, vi behöver mer blod av just din blodgrupp, blodgrupp:{bloodgroup}");
            }
        }


        private static void WelcomePageBloodBank()
        {
            Console.Clear();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("--- Välommen till Blodbanken---");
            Console.WriteLine("---            :)           ---");
        }
        private static void StartPageOption()
        {
            Console.Clear();
            Console.WriteLine($"-------------------------------");
            Console.WriteLine($"keyLoggin : Logga in");
            Console.WriteLine($"keyAccount : Registrera ny användare");
            Console.WriteLine($"keyQuit : Avsluta");
        }
        private static void StaffPageOption()
        {
            Console.Clear();
            Console.WriteLine($"-------------------------------");
            Console.WriteLine($"keyCheckAmountBlood : Kolla hur mycket blod som finns");
            Console.WriteLine($"keyRegisterNewDonation : Registrera ny bloddonation");
            Console.WriteLine($"keyRequestBloodDonation : Skicka ut mail med förfrågan om att donera blod");
            Console.WriteLine($"keyQuit : Avsluta");
        }
          private static string ReadLineAsString(string printString)
    {
        string output = "";
        string input = "";
        bool success = false;

        do
        {
            Console.Write(printString);
            input = Console.ReadLine();
            if (!String.IsNullOrEmpty(input)) success = true;
            else Console.WriteLine("Hoppsan! Du skrev inte in något.");
        } while (!success);


        return output = input;
    }
     private static int ReadLineAsInt(string printString, int maxValue = -1)
    {
        int output = -1;
        bool success = false;


        do
        {
            Console.Write(printString);
            string input = Console.ReadLine();
            try
            {
                output = Convert.ToInt32(input);
                if ((maxValue == -1 || output <= maxValue) && output >= 0) success = true;
                else Console.WriteLine("Skriv en siffra mellan 1 - " + maxValue);
            }
            catch
            {
                Console.WriteLine("Du skrev inte in en siffra eller ett alldelles för stort tal. Försök igen, skriv in en siffra.");
            }
        } while (!success);


        return output;
    }


    }
}
