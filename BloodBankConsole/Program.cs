using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using BloodbankFunc;

namespace BloodbankUI
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
            BloodBank bb = new BloodBank();

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
                                foreach (var loggedInUser in bb.GetUserInfo(inputUserID))
                                {
                                    loggedInDonor = new BloodDonor(loggedInUser.FirstName, loggedInUser.LastName, loggedInUser.ID, loggedInUser.Email, Convert.ToBoolean(loggedInUser.AvailableToDonate), Convert.ToBoolean(loggedInUser.HealthOK), loggedInUser.BloodGroupID, loggedInUser.LatestDonation);
                                }
                                Console.WriteLine($"Du är inloggad som bloddonator: {loggedInDonor.FirstName} {loggedInDonor.LastName}");
                                string available = "ej tillgänglig";
                                if (loggedInDonor.AvailableToDonate == true)
                                {
                                    available = "tillgänglig";
                                }
                                Console.WriteLine($"Din status är satt som {available} för donation.");
                                Console.WriteLine("Vill du ändra detta? [J/N]");
                                if (Console.ReadKey(true).Key == ConsoleKey.J)
                                {
                                    bb.ChangeDonationStatus(loggedInDonor.AvailableToDonate, loggedInDonor.IDNumber);
                                    Console.WriteLine("Din status är uppdaterad.");
                                }
                                else if (Console.ReadKey(true).Key == ConsoleKey.N)
                                {
                                    break;
                                }

                                PauseProgram();
                            }
                            else if (bb.ValidateUserLogin(inputUserID, inputPassword) == 2)
                            {
                                Staff loggedInStaff = null;
                                foreach (var loggedInUser in bb.GetUserInfo(inputUserID))
                                {
                                    loggedInStaff = new Staff(loggedInUser.FirstName, loggedInUser.LastName, loggedInUser.ID, loggedInUser.Title);
                                }
                                Console.WriteLine($"Du är inloggad som personal: {loggedInStaff.FirstName} {loggedInStaff.LastName}");
                                bool menu = true;
                                while (menu)
                                {
                                    PrintStaffMenuOption();
                                    switch (Console.ReadKey(true).Key)
                                    {
                                        case keyCheckAmountBlood:
                                        {
                                            foreach (Donation donation in bb.StoredBlood())
                                            {
                                                Console.WriteLine($"{donation.AmountOfBlood} Enheter : Blodgrupp {donation.Bloodgroup}");
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
                                            Console.Write("Donator-ID (12 siffror): ");
                                            string donorID = ReadLineAsStringOfDidgits(12, ":>");
                                            Donation donation = new Donation(amountOfBlood, donorID, loggedInStaff.IDNumber);
                                            try
                                            {
                                                bb.AddDonation(donation);
                                                Console.WriteLine("Donationen är nu registrerad!");
                                            }
                                            catch (SqlException e)
                                            {
                                                Console.WriteLine(e);
                                                Console.WriteLine("Nu gick det lite fel med donationen!");
                                                // "Försök igen eller återgå till menyn ?" 
                                            }
                                            PauseProgram();
                                            break;
                                        }
                                        case keyRequestBloodDonation:
                                        {
                                            Console.WriteLine("Vilken Blodgrupp behövs det mer av?");
                                            PrintBloodGroupMenu();
                                            int bloodgroup = BloodGroupChoice();
                                            BloodGroup type = (BloodGroup) bloodgroup;
                                            foreach (var item in bb.GetListForRequestDonation(bloodgroup))
                                            {
                                                Console.WriteLine($"Till: {item.Email}, Hej {item.FirstName}!, vi behöver mer blod av just din blodgrupp, blodgrupp: {type}");
                                            }
                                            PauseProgram();
                                            break;
                                        }
                                        case keyRegisterNewStaffAccount:
                                        {
                                            Console.WriteLine("Registrera personal:");
                                            bool loopAgain = true;
                                            while (loopAgain)
                                            /// Loop varför??
                                            {
                                                Console.WriteLine("Skriv in personal förnamn");
                                                string staffFirstName = ReadLineAsString(":> ");
                                                Console.WriteLine("Skriv in personal efternamn");
                                                string staffLastName = ReadLineAsString(":> ");
                                                Console.WriteLine("Skriv in personal IDnummer, 12 siffror");
                                                string staffiDNumber = ReadLineAsStringOfDidgits(12, ":> ");
                                                Console.WriteLine("Skriv in personal jobbtitel");
                                                string staffTitle = ReadLineAsString(":> ");

                                                User newStaff = new Staff(staffFirstName, staffLastName, staffiDNumber, staffTitle);
                                                try
                                                {
                                                    bb.AddUser(newStaff);
                                                    loopAgain = false;
                                                }
                                                catch (SqlException)
                                                {
                                                    //Console.WriteLine(e);
                                                    Console.WriteLine("Nu gick det lite fel!");
                                                    //"Vill du försöka igen eller gå tillbaka till menyn ?"
                                                }
                                                Console.WriteLine("Personalen är registrerad.");
                                            }
                                            PauseProgram();
                                            break;
                                        }
                                        case keyLoggout:
                                        {
                                            menu = false;
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
                                Console.Clear();
                                #region load
                                Console.WriteLine("-------------------------------");
                                Console.WriteLine("---       GRATTIS!          ---");
                                Console.WriteLine("---     Du är godkänd       ---");
                                Console.WriteLine("---    för att ge blod      ---");
                                Thread.Sleep(1000);
                                #endregion

                                bool healthOK = true;
                                bool availableToDonate = true;

                                bool loopAgain = true;
                                while (loopAgain)
                                {
                                    Console.Write("Skriv in ditt personnummer, 12 siffror (YYYYMMDDXXXX): ");
                                    string idNumber = ReadLineAsStringOfDidgits(12, ":> "); // se om denna funkar som önskat !
                                    Console.Write("\nSkriv in förnamn");
                                    string firstName = ReadLineAsString(":> ");
                                    Console.Write("\nSkriv in efternamn");
                                    string lastName = ReadLineAsString(":> ");
                                    Console.WriteLine("\nVilken blodgrupp tillhör du (1-4): ");
                                    PrintBloodGroupMenu();
                                    int bloodGroup = BloodGroupChoice();

                                    int inputBloodGroup = ReadKeyAsInt(":> ", 1, 4); //kan man få "emum length", ist för 4?
                                    //int bloodGroupCount = Enum.GetValues(typeof(BloodGroup)).Cast<Int>().Last();

                                    BloodGroup bloodtype = (BloodGroup) bloodGroup;
                                    Console.Write("\nSkriv in din email: ");
                                    string eMail = ReadLineAsString(":>");
                                    Console.Write("\nVälj ett lösenord: ");
                                    string passWord = ReadLineAsString(":>");

                                    Console.Clear();
                                    Console.WriteLine("Du har skrivit in följande:");
                                    Console.WriteLine($"Personnummer: {idNumber}\nFörnamn: {firstName}\nEfternamn: {lastName}\nBlodgrupp: {bloodtype}\nEmail: {eMail}\nLösenord: {passWord}");
                                    Console.WriteLine("Stämmer Uppgifterna? J/N");
                                    ConsoleKey correctInformation = Console.ReadKey(true).Key;
                                    if (correctInformation == ConsoleKey.J)
                                    {
                                        BloodDonor newDonor = new BloodDonor(firstName, lastName, idNumber, eMail, availableToDonate, healthOK, bloodGroup, passWord);
                                        bb.AddUser(newDonor);
                                        loopAgain = false;
                                    }
                                    else if (correctInformation == ConsoleKey.N)
                                    {
                                        Console.WriteLine("Ajdå, då får nu fylla i alla uppgifterna igen..    :) ");
                                        // Eller vill du g¨å tillbaka till menyn?
                                    }
                                }
                                PauseProgram();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Du uppfyller tyvärr inte kraven för att vara blodgivare..   :( ");
                                Console.WriteLine("Du skickas nu tillbaka till start menyn");
                                PauseProgram();
                            }
                            break;
                        }
                    case keyQuit:
                        {
                            Console.WriteLine("Tack för att du besökte oss, hej då!");
                            Thread.Sleep(1000);
                            isRunning = false;
                            break;
                        }
                }

            }

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
        private static string ReadLineAsStringOfDidgits(int inputLength, string printString)
        {
            string output = "";
            string input = "";
            bool success = false;

            do
            {
                Console.Write(printString);
                input = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(input))
                {
                    if (IsDigitsOnly(input) == true)
                    {
                        if (input.Length == inputLength) success = true;
                        else
                        {
                            Console.WriteLine($"Du måste ange ID nummer med {inputLength} siffror");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Du angav ett ID av fel format, endast siffror tack.");
                    }
                }
                else Console.WriteLine("Hoppsan! Du skrev inte in något.");
            } while (!success);

            return output = input;
        }
        private static int ReadKeyAsInt(string printString, int minValue, int maxValue)
        {
            int output = -1;
            bool success = false;

            do
            {
                Console.Write(printString);
                ConsoleKey input = Console.ReadKey(true).Key;
                try
                {
                    output = Convert.ToInt32(input);
                    if ((maxValue == 0 || output <= maxValue) && output >= minValue) success = true;
                    else Console.WriteLine($"Skriv en siffra mellan {minValue} - {maxValue}");
                }
                catch
                {
                    Console.WriteLine("Hoppsan, du skrev inte in en siffra eller en för stort/litet tal! Försök igen.");
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