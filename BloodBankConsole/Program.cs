using System;
using System.Data.SqlClient;
using System.Threading;
using System.Collections.Generic;
using System.IO;

namespace Bloodbank
{
    class Program
    {
        const ConsoleKey keyLoggin = ConsoleKey.L;
        const ConsoleKey keyCreateAccount = ConsoleKey.S;
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
            Thread.Sleep(1000);
            while (isRunning)
            {
                PrintStartMenuOption();
                switch (Console.ReadKey(true).Key)
                {
                    case keyLoggin:
                    {
                        Console.Clear();
                        Console.WriteLine("Skriv in ditt användar-ID:");
                        string inputUserID = ReadLineAsString(":>");
                        if (IsDigitsOnly(inputUserID) == false)
                        {
                            Console.WriteLine("Du angav ett ID av fel format.");
                            PauseProgram();
                            break;
                        }
                        Console.WriteLine("Skriv in ditt lösenord:");
                        string inputPassword = ReadLineAsString(":>");

                        if (bb.ValidateUserLogin(inputUserID, inputPassword) == 1)
                        {
                            BloodDonor loggedInDonor;
                            foreach (var loggedInUser in bb.GetLoggedInUser(inputUserID))
                            {
                                Console.WriteLine(loggedInUser);
                                loggedInDonor = new BloodDonor(loggedInUser.FirstName, loggedInUser.LastName, loggedInUser.IDNumber, loggedInUser.Email, loggedInUser.availableToDonate, loggedInUser.HealthOK, loggedInUser.BloodGroupID, loggedInUser.LatestDonation);
                            }
                            
                            
                        
                            
                            Console.WriteLine("Du är inloggad som donator");
                            PauseProgram();
                        }
                        else if (bb.ValidateUserLogin(inputUserID, inputPassword) == 2)
                        {
                            bb.GetLoggedInUser(inputUserID);
                            Console.WriteLine("Du är inloggad som personal");
                            PauseProgram();
                        }
                        else
                        {
                            Console.WriteLine("FEL...");
                            PauseProgram();
                            break;
                        }
                        break;
                    }
                    case keyCreateAccount:
                        {
                            Console.Clear();                            
                            if (QuestionHealthForm())
                            {
                                bool healthOK = true;
                                bool availableToDonate = true;
                                Console.Write("Skriv in ditt personnummer, 12 siffror (YYYYMMDDXXXX): ");
                                string idNumber = Console.ReadLine();
                                Console.Write("\nSkriv in förnamn: ");
                                string firstName = Console.ReadLine();
                                Console.Write("\nSkriv in efternamn: ");
                                string lastName = Console.ReadLine();
                                Console.WriteLine("\nVilken blodgrupp tillhör du: ");
                                PrintBloodGroupMenu();
                                int bloodGroup = BloodGroupChoice();
                                BloodGroup bloodtype = (BloodGroup)bloodGroup;
                                Console.Write("\nSkriv in din email: ");
                                string eMail = Console.ReadLine();
                                Console.Write("\nVälj ett lösenord: ");
                                string passWord = Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine("Du har skrivit in följande:");
                                Console.WriteLine($"Personnummer: {idNumber}\nFörnamn: {firstName}\nEfternamn: {lastName}\nBlodgrupp: {bloodtype}\nEmail: {eMail}\nLösenord: {passWord}");
                                Console.WriteLine("Stämmer Uppgifterna? J/N");                                
                                while (true)
                                {
                                    ConsoleKey input = Console.ReadKey(true).Key;
                                    if (input == ConsoleKey.J)
                                    {
                                        DateTime created = DateTime.Today;
                                        BloodDonor newDonor = new BloodDonor(firstName, lastName, idNumber, eMail, availableToDonate, healthOK, bloodGroup, created);
                                        bb.AddUser(newDonor);                                        
                                        break;
                                    }
                                    else if (input == ConsoleKey.N)
                                    {
                                        break;
                                    }
                                }
                                PauseProgram();
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Du är inte godkänd som blodgivare");
                                PauseProgram();
                                break;
                            }
                            //Donator: userID, Förnamn, efternamn, tillgänglig för donation, hälsa ok, blodgrupp, email, lösenord
                            //AddUser();
                        }
                    case keyQuit:
                        {
                            isRunning = false;
                            break;
                        }
                }

            }

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

            // foreach (var item in bb.GetAllUsers())
            // {
            //     Console.WriteLine($"AnvändarID: {item.IDNumber} Password: {item.PassWord}");
            // }


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
        private static void PrintStartMenuOption()
        {
            Console.Clear();
            Console.WriteLine($"-------------------------------");
            Console.WriteLine($"{keyLoggin} : Logga in");
            Console.WriteLine($"{keyCreateAccount} : Registrera nytt donator konto");
            Console.WriteLine($"{keyQuit} : Avsluta");
        }
        private static void PrintStaffMenuOption()
        {
            Console.Clear();
            Console.WriteLine($"-------------------------------");
            Console.WriteLine($"{keyCheckAmountBlood} : Kolla hur mycket blod som finns");
            Console.WriteLine($"{keyRegisterNewDonation} : Registrera ny bloddonation");
            Console.WriteLine($"{keyRequestBloodDonation} : Skicka ut mail med förfrågan om att donera blod");
            Console.WriteLine($"{keyLoggout} : Logga ut");
        }
        private static void PrintBloodGroupMenu()
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
                if (!String.IsNullOrWhiteSpace(input)) success = true;
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

        private static void PauseProgram()
        {
            Console.WriteLine("\nTryck valfri tangent för att gå vidare");
            Console.ReadKey();
            Console.Clear();
        }

        private static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        private static bool QuestionHealthForm()
        {
            StreamReader HealthQuestionFile = new StreamReader("Survey.md");
            string healthQuestion = HealthQuestionFile.ReadLine();

            while ((healthQuestion = HealthQuestionFile.ReadLine()) != null)
            {
                Console.WriteLine(healthQuestion);
                while (true)
                {
                    ConsoleKey input = Console.ReadKey(true).Key;
                    if (input == ConsoleKey.N)
                    {
                        break;
                    }
                    else if (input == ConsoleKey.J)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static int BloodGroupChoice()
        {
            ConsoleKey input;
            while (true)
            {
                input = Console.ReadKey(true).Key;                
                switch (input)
                {
                    case ConsoleKey.D1:
                        {
                            return 1;
                        }

                    case ConsoleKey.D2:
                        {
                            return 2;
                        }

                    case ConsoleKey.D3:
                        {
                            return 3;
                        }

                    case ConsoleKey.D4:
                        {
                            return 4;
                        }
                }
            }
        }

    }
}
