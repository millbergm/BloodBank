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
        const ConsoleKey keyRegisterNewStaffAccount = ConsoleKey.J;
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
                                BloodDonor loggedInDonor = null;
                                foreach (var loggedInUser in bb.GetLoggedInUser(inputUserID))
                                {
                                    loggedInDonor = new BloodDonor(loggedInUser.FirstName, loggedInUser.LastName, loggedInUser.ID, loggedInUser.Email, Convert.ToBoolean(loggedInUser.availableToDonate), Convert.ToBoolean(loggedInUser.HealthOK), loggedInUser.BloodGroupID, loggedInUser.LatestDonation);
                                    // Console.WriteLine(loggedInUser.FirstName);
                                    // Console.WriteLine(loggedInUser.LastName);
                                    // Console.WriteLine(loggedInUser.ID);
                                    // Console.WriteLine(loggedInUser.Email);
                                    // Console.WriteLine(Convert.ToBoolean(loggedInUser.availableToDonate));
                                    // Console.WriteLine(Convert.ToBoolean(loggedInUser.HealthOK));
                                    // Console.WriteLine((BloodGroup)loggedInUser.BloodGroupID);
                                    // Console.WriteLine(loggedInUser.LatestDonation);
                                }
                                Console.WriteLine($"Du är inloggad som {loggedInDonor.FirstName} {loggedInDonor.LastName}");
                                string available = "ej tillgänglig";
                                if (loggedInDonor.AvailableToDonate == true)
                                {
                                    available = "tillgänglig";
                                }
                                Console.WriteLine($"Din status är satt som {available} för donation.");
                                Console.WriteLine("Vill du ändra detta? [J/N]");
                                if (Console.ReadKey(true).Key == ConsoleKey.J)
                                {

                                }
                                PauseProgram();
                            }
                            else if (bb.ValidateUserLogin(inputUserID, inputPassword) == 2)
                            {
                                Staff loggedInStaff;
                                foreach (var loggedInUser in bb.GetLoggedInUser(inputUserID))
                                {
                                    loggedInStaff = new Staff(loggedInUser.FirstName, loggedInUser.LastName, loggedInUser.ID, loggedInUser.Title);
                                    Console.WriteLine(loggedInUser.FirstName);
                                    Console.WriteLine(loggedInUser.LastName);
                                    Console.WriteLine(loggedInUser.ID);
                                    Console.WriteLine(loggedInUser.Title);
                                }
                                Console.WriteLine($"Du är inloggad som personal, {loggedInStaff.FirstName}, {loggedInStaff.LastName}");
                                PrintStaffMenuOption();
                                while (true)
                                {
                                    switch (Console.ReadKey(true).Key)
                                    {
                                        case keyCheckAmountBlood:
                                            {
                                                foreach (var item in bb.StoredBlood())
                                                {
                                                    Console.WriteLine($"{item.AmountOfBlood} Enheter : Blodgrupp {item.Bloodgroup}");
                                                }
                                                PauseProgram();
                                                break;
                                            }
                                        case keyRegisterNewDonation:
                                            {
                                                Console.Write("Antal enheter: ");
                                                int amountOfBlood;
                                                while (true)
                                                {
                                                    try
                                                    {
                                                        amountOfBlood = Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("du måste skriva en siffra");
                                                    }
                                                }
                                                Console.Write("Donator-ID: ");
                                                string donorID = ReadLineAsString(":>");
                                                if (IsDigitsOnly(inputUserID) == false)
                                                {
                                                    Console.WriteLine("Du angav ett ID av fel format.");
                                                    PauseProgram();
                                                    break;
                                                }
                                                string staffID = loggedInStaff.IDNumber; //fylls i automatiskt via inloggningen
                                                Donation donation = new Donation(amountOfBlood, donorID, staffID);
                                                try
                                                {
                                                    bb.AddDonation(donation);
                                                    Console.WriteLine("Donationen är nu registrerad!");
                                                }
                                                catch (SqlException e)
                                                {
                                                    Console.WriteLine(e);
                                                    Console.WriteLine("Nu gick det lite fel med donationen!");
                                                }
                                                PauseProgram();
                                                break;
                                            }
                                        case keyRequestBloodDonation:
                                            {
                                                Console.Write("Vilken Blodgrupp Behövs mer av?");
                                                PrintBloodGroupMenu();
                                                int bloodgroup = BloodGroupChoice();
                                                BloodGroup type = (BloodGroup)bloodgroup;
                                                foreach (var item in bb.GetListForRequestDonation(bloodgroup))
                                                {
                                                    Console.WriteLine($"Till: {item.Email}, Hej {item.FirstName}!, vi behöver mer blod av just din blodgrupp, blodgrupp: {type}");
                                                }
                                                PauseProgram();
                                                break;
                                            }
                                        case keyRegisterNewStaffAccount:
                                            {
                                                Console.WriteLine("Registrera personal");
                                                PauseProgram();
                                                break;
                                            }
                                        case keyLoggout:
                                            {
                                                break;
                                            }
                                    }
                                }
                                //PauseProgram();
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
                                string idNumber = ReadLineAsStringWhenOnlyDidgits(":>"); // se om denna funkar som önskat !
                                Console.Write("\nSkriv in förnamn: ");
                                string firstName = ReadLineAsString(":>");
                                Console.Write("\nSkriv in efternamn: ");
                                string lastName = ReadLineAsString(":>");
                                Console.WriteLine("\nVilken blodgrupp tillhör du (1-4): ");
                                PrintBloodGroupMenu();
                                int bloodGroup = BloodGroupChoice();
                                BloodGroup bloodtype = (BloodGroup)bloodGroup;
                                Console.Write("\nSkriv in din email: ");
                                string eMail = ReadLineAsString(":>");
                                Console.Write("\nVälj ett lösenord: ");
                                string passWord = ReadLineAsString(":>");

                                Console.Clear();
                                Console.WriteLine("Du har skrivit in följande:");
                                Console.WriteLine($"Personnummer: {idNumber}\nFörnamn: {firstName}\nEfternamn: {lastName}\nBlodgrupp: {bloodtype}\nEmail: {eMail}\nLösenord: {passWord}");
                                Console.WriteLine("Stämmer Uppgifterna? J/N");
                                while (true)
                                {
                                    ConsoleKey input = Console.ReadKey(true).Key;
                                    if (input == ConsoleKey.J)
                                    {
                                        BloodDonor newDonor = new BloodDonor(firstName, lastName, idNumber, eMail, availableToDonate, healthOK, bloodGroup, passWord);
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
                        }
                    case keyQuit:
                        {
                            isRunning = false;
                            break;
                        }
                }

            }
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
            Console.WriteLine($"{keyRegisterNewStaffAccount} : Registrera ett nytt personalkonto");
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
        private static string ReadLineAsStringWhenOnlyDidgits(string printString)
        {
            string output = "";
            string input = "";
            bool success = false;
            bool successDigits = true;
            do
            {
                successDigits = true;
                Console.Write(printString);
                input = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(input))
                {
                    foreach (char c in input)
                    {
                        if (c < '0' || c > '9')
                            success = false;
                            Console.WriteLine("Du får endast ange en text av siffror");
                    }
                    success = true;
                }
                else Console.WriteLine("Hoppsan! Du skrev inte in något.");
            } while (!success && !successDigits);

            return output = input;
        }

        //Se över siffrorna i metoden nedan sen !
        private static int ReadLineAsInt(string printString, int maxValue) //obs denna kanske inte används? ta bort sen isf
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
