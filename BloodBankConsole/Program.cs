using System;
using System.Data.SqlClient;
using System.Threading;

namespace Bloodbank
{
    class Program
    {
            const ConsoleKey keyLoggin = ConsoleKey.L;
            const ConsoleKey keyAccount = ConsoleKey.S;
            const ConsoleKey keyQuit = ConsoleKey.Q;
            const ConsoleKey keyCheckAmountBlood = ConsoleKey.A;
            const ConsoleKey keyRegisterNewDonation = ConsoleKey.D;
            const ConsoleKey keyRequestBloodDonation = ConsoleKey.R;
            const ConsoleKey keyLoggout = ConsoleKey.L;
        static void Main(string[] args)
        {
            bool isRunning = true;
            Bloodbank bb = new Bloodbank();




            PrintWelcomePageBloodBank();
            Thread.Sleep(500);
            while (isRunning)
            {
                PrintStartMenyOption();
                switch (Console.ReadKey().Key)
                {
                    case keyLoggin:
                    {
                        break;
                    }
                    case keyAccount:
                    {
                        break;
                    }
                    case keyQuit:
                    {
                        isRunning = false;
                        break;
                    }
                }
                
            }

            Console.Write("First name: ");
            string firstName = "Viktor"; //Console.ReadLine();
            Console.Write("Last name: ");
            string lastName = "Ahlin"; //Console.ReadLine();
            string idnumber = "19901019-5261";
            string email = "Viktor@gmail.com";
            int availabletodonate = 1;
            int healthOK = 1;
            DateTime latestDonation = DateTime.Now;

            BloodGroup bloodGroup = BloodGroup.AB;
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

            // Skriv ut mängden blod / blodgrupp via storeprocedure
            foreach (var item in bb.StoredBlood())
            {
                Console.WriteLine($"{item.AmountOfBlood} Enheter : Blodgrupp {item.Bloodgroup}");
            }

            foreach (var item in bb.GetAllUsers())
            {
                Console.WriteLine($"AnvändarID: {item.IDNumber} Password: {item.PassWord}");
            }

            PrintBloodGroupMeny();
            //Information för att STAFF ska skicka mail om förfrågan av blod
            int bloodgroup = 1;
            BloodGroup type = (BloodGroup)bloodgroup;
            foreach (var item in bb.GetListForRequestDonation(bloodgroup))
            {
                Console.WriteLine($"Till: {item.Email}, Hej {item.FirstName}!, vi behöver mer blod av just din blodgrupp, blodgrupp: {type}");
            }
        }

        private static void PrintWelcomePageBloodBank()
        {
            Console.Clear();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("--- Välommen till Blodbanken---");
            Console.WriteLine("---            :)           ---");
            Console.WriteLine("---                         ---");
        }
        private static void PrintStartMenyOption()
        {
            Console.Clear();
            Console.WriteLine($"-------------------------------");
            Console.WriteLine($"{keyLoggin} : Logga in");
            Console.WriteLine($"{keyAccount} : Registrera nytt donator konto");
            Console.WriteLine($"{keyQuit} : Avsluta");
        }
        private static void PrintStaffMenyOption()
        {
            Console.Clear();
            Console.WriteLine($"-------------------------------");
            Console.WriteLine($"{keyCheckAmountBlood} : Kolla hur mycket blod som finns");
            Console.WriteLine($"{keyRegisterNewDonation} : Registrera ny bloddonation");
            Console.WriteLine($"{keyRequestBloodDonation} : Skicka ut mail med förfrågan om att donera blod");
            Console.WriteLine($"{keyLoggout} : Logga ut");
        }
        private static void PrintBloodGroupMeny()
        {
            foreach (int menuChoiceNumber in Enum.GetValues(typeof(BloodGroup)))
            {
                Console.WriteLine("{0}. Blodgrupp {1}", menuChoiceNumber, Enum.GetName(typeof(BloodGroup), menuChoiceNumber));
            }
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

        //Se över siffrorna i metoden nedan sen !
        private static int ReadLineAsInt(string printString, int maxValue)
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
                    if ((maxValue == 0 || output <= maxValue) && output >= 0) success = true;
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
