using System;
using System.IO;
using System.Threading;
using BloodbankFunc;

namespace BloodbankUI
{
    class Program
    {
        const ConsoleKey keyLoggin = ConsoleKey.L;
        const ConsoleKey keyCreateAccount = ConsoleKey.R;
        const ConsoleKey keyQuit = ConsoleKey.Q;
        const ConsoleKey keyCheckAmountBlood = ConsoleKey.A;
        const ConsoleKey keyRegisterNewDonation = ConsoleKey.D;
        const ConsoleKey keyRegisterNewStaffAccount = ConsoleKey.J;
        const ConsoleKey keyRequestBloodDonation = ConsoleKey.R;
        const ConsoleKey keyReviseInformation = ConsoleKey.F;
        const ConsoleKey keyLoggout = ConsoleKey.L;
        const ConsoleKey keyJa = ConsoleKey.J;
        const ConsoleKey keyNej = ConsoleKey.N;
        static void Main(string[] args)
        {
            bool isRunning = true;
            BloodBank bb = new BloodBank();

            PrintWelcomePageBloodBank();
            Thread.Sleep(1500);
            while (isRunning)
            {
                PrintStartMenuOption();
                switch (Console.ReadKey(true).Key)
                {
                    case keyLoggin:
                        {
                            int loginStatus = 0;
                            Console.Clear();
                            Console.WriteLine("Skriv in ditt personnummer:");
                            string inputUserID = ReadLineAsString(":>");
                            if (IsDigitsOnly(inputUserID) == false)
                            {
                                Console.WriteLine("Du angav personnummer av fel format.");
                                PauseProgram();
                                break;
                            }
                            Console.WriteLine("Skriv in ditt lösenord:");
                            string inputPassword = ReadLineAsString(":>");
                            try
                            {
                                loginStatus = bb.ValidateUserLogin(inputUserID, inputPassword);
                            }
                            catch
                            {
                                Console.WriteLine("Inloggningen misslyckades. Angav du rätt uppgifter? Är du registrerad?");
                                PauseProgram();
                                break;
                            }
                            if (loginStatus == 1)
                            {
                                bool donormenu = true;
                                BloodDonor loggedInDonor = null;

                                while (donormenu)
                                {
                                    try
                                    {
                                        foreach (var loggedInUser in bb.GetUserInfo(inputUserID))
                                        {
                                            loggedInDonor = new BloodDonor(loggedInUser.FirstName, loggedInUser.LastName, loggedInUser.ID, loggedInUser.Email, Convert.ToBoolean(loggedInUser.AvailableToDonate), Convert.ToBoolean(loggedInUser.HealthOK), loggedInUser.BloodGroupID, loggedInUser.LatestDonation);
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Något verkar strula med databasen. Är du ansluten till internet?");
                                        donormenu = false;
                                        PauseProgram();
                                    }

                                    PrintDonorMenu(loggedInDonor.FirstName, loggedInDonor.LastName);
                                    switch (Console.ReadKey(true).Key)
                                    {
                                        case keyReviseInformation:
                                            {
                                                Console.Clear();
                                                foreach (var loggedInUser in bb.GetUserInfo(inputUserID))
                                                {
                                                    loggedInDonor.AvailableToDonate = Convert.ToBoolean(loggedInUser.AvailableToDonate);
                                                }
                                                string available = "ej tillgänglig";
                                                if (loggedInDonor.AvailableToDonate == true)
                                                {
                                                    available = "tillgänglig";
                                                }

                                                Console.WriteLine($"Din status är satt som {available} för donation.");
                                                Console.WriteLine($"Vill du ändra detta? [{keyJa}/{keyNej}]");
                                                bool loopAgain = true;
                                                while (loopAgain)
                                                {
                                                    ConsoleKey input = Console.ReadKey(true).Key;
                                                    if (input == keyJa)
                                                    {
                                                        try
                                                        {
                                                            bb.ChangeDonationStatus(loggedInDonor.AvailableToDonate, loggedInDonor.IDNumber);
                                                        }
                                                        catch
                                                        {
                                                            Console.WriteLine("Det gick inte att uppdatera din status. Vänligen försök igen.");
                                                            Console.WriteLine("Tryck valfri knapp för att fortsätta.");
                                                            Console.WriteLine($"Tryck {keyQuit} för att återgå till menyn.");
                                                        }
                                                        Console.WriteLine($"\nDin status är nu ändrad.");
                                                        Console.WriteLine("\nDu skickas nu tillbaka till menyn.");
                                                        Thread.Sleep(3000);
                                                        loopAgain = false;                                                            
                                                    }
                                                    else if (input == keyNej)
                                                    {
                                                        Console.WriteLine("\nDu skickas nu tillbaka till menyn.");
                                                        Thread.Sleep(1500);
                                                        loopAgain = false;
                                                    }
                                                }
                                                break;
                                            }
                                        case keyLoggout:
                                            {
                                                donormenu = false;
                                                break;
                                            }
                                    }
                                }
                                
                            }

                            else if (loginStatus == 2)
                            {
                                Staff loggedInStaff = null;
                                try
                                {
                                    foreach (var loggedInUser in bb.GetUserInfo(inputUserID))
                                    {
                                        loggedInStaff = new Staff(loggedInUser.FirstName, loggedInUser.LastName, loggedInUser.ID, loggedInUser.Title);
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Något verkar strula med databasen. Är du ansluten till internet?");
                                    break;
                                }


                                bool staffmenu = true;
                                while (staffmenu)
                                {
                                    PrintStaffMenuOption(loggedInStaff.FirstName, loggedInStaff.LastName, loggedInStaff.Title);
                                    switch (Console.ReadKey(true).Key)
                                    {
                                        case keyCheckAmountBlood:
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Tillgängligt blod");
                                                Console.WriteLine("-------------------------------");
                                                try
                                                {
                                                    foreach (Donation donation in bb.StoredBlood())
                                                    {   
                                                        BloodGroup bloodGroup = (BloodGroup)donation.Bloodgroup;
                                                                                                             
                                                        Console.WriteLine($"{donation.AmountOfBlood} Enheter : Blodgrupp {bloodGroup}");
                                                    }
                                                }
                                                catch
                                                {
                                                    Console.WriteLine("Kunde inte hämta data från databasen");
                                                }
                                                PauseProgram();
                                                break;
                                            }
                                        case keyRegisterNewDonation:
                                            {
                                                bool loopAgain = true;
                                                while (loopAgain)
                                                {
                                                    Console.Clear();
                                                    Console.Write("Antal enheter: ");
                                                    int amountOfBlood = ReadStringAsInt(":> ", 1, 10);

                                                    Console.Write("Donator-ID (12 siffror): ");
                                                    string donorID = ReadLineAsStringOfDidgits(12, ":> ");
                                                    Donation donation = new Donation(amountOfBlood, donorID, loggedInStaff.IDNumber);
                                                    try
                                                    {
                                                        bb.AddDonation(donation);
                                                        Console.WriteLine("\nDonationen är nu registrerad!");
                                                        loopAgain = false;
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("Nu gick det lite fel med donationen!");
                                                        Console.WriteLine("Tryck valfri knapp för att fortsätta.");
                                                        Console.WriteLine($"Tryck {keyQuit} för att återgå till menyn.");
                                                        ConsoleKey input = Console.ReadKey(true).Key;
                                                        if (input == keyQuit) loopAgain = false;
                                                    }
                                                    PauseProgram();
                                                }
                                                break;
                                            }
                                        case keyRequestBloodDonation:
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Vilken Blodgrupp behövs det mer av?");
                                                PrintBloodGroupMenu();
                                                int bloodgroup = BloodGroupChoice();
                                                BloodGroup type = (BloodGroup)bloodgroup;
                                                try
                                                {
                                                    foreach (BloodDonor bloodDonor in bb.GetListForRequestDonation(bloodgroup))
                                                    {
                                                        Console.WriteLine($"Till: {bloodDonor.Email}, Hej {bloodDonor.FirstName}!, vi behöver mer blod av just din blodgrupp, blodgrupp: {type}");
                                                    }
                                                }
                                                catch
                                                {
                                                    Console.WriteLine("Kunde inte hitta någon med den valda blodgruppen.");
                                                }

                                                PauseProgram();
                                                break;
                                            }
                                        case keyRegisterNewStaffAccount:
                                            {
                                                Console.WriteLine("Registrera personal:");
                                                bool loopAgain = true;
                                                while (loopAgain)
                                                {
                                                    Console.WriteLine("Skriv in personal förnamn");
                                                    string staffFirstName = ReadLineAsString(":> ");
                                                    Console.WriteLine("Skriv in personal efternamn");
                                                    string staffLastName = ReadLineAsString(":> ");
                                                    Console.WriteLine("Skriv in personal lösenord");
                                                    string staffPassword = ReadLineAsString(":> ");
                                                    Console.WriteLine("Skriv in personal IDnummer, 12 siffror");
                                                    string staffiDNumber = ReadLineAsStringOfDidgits(12, ":> ");
                                                    Console.WriteLine("Skriv in personal jobbtitel");
                                                    string staffTitle = ReadLineAsString(":> ");

                                                    User newStaff = new Staff(staffFirstName, staffLastName, staffiDNumber, staffTitle, staffPassword);
                                                    try
                                                    {
                                                        bb.AddUser(newStaff);
                                                        loopAgain = false;
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("Nu gick det lite fel!");
                                                        Console.WriteLine("Tryck valfri knapp för att fortsätta.");
                                                        Console.WriteLine($"Tryck {keyQuit} för att återgå till menyn.");
                                                        ConsoleKey input = Console.ReadKey(true).Key;
                                                        if (input == keyQuit) loopAgain = false;
                                                    }
                                                    Console.WriteLine("Personalen är registrerad.");
                                                }
                                                PauseProgram();
                                                break;
                                            }
                                        case keyLoggout:
                                            {
                                                staffmenu = false;
                                                break;
                                            }
                                    }
                                }
                            }
                            else if (loginStatus == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("ID-nummer eller lösenord är fel");
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
                                PrintRequiredToDonate();
                                Thread.Sleep(3000);

                                bool healthOK = true;
                                bool availableToDonate = true;
                                bool loopAgain = true;
                                while (loopAgain)
                                {
                                    Console.Clear();
                                    Console.Write("Skriv in ditt personnummer, 12 siffror (YYYYMMDDXXXX): ");
                                    string idNumber = ReadLineAsStringOfDidgits(12, ":> ");
                                    Console.Write("\nSkriv in förnamn");
                                    string firstName = ReadLineAsString(":> ");
                                    Console.Write("\nSkriv in efternamn");
                                    string lastName = ReadLineAsString(":> ");
                                    Console.WriteLine("\nVilken blodgrupp tillhör du (1-4): ");
                                    PrintBloodGroupMenu();
                                    int bloodGroup = BloodGroupChoice();
                                    Console.WriteLine($":> {bloodGroup}");
                                    BloodGroup bloodtype = (BloodGroup)bloodGroup;
                                    Console.Write("\nSkriv in din email: ");
                                    string eMail = ReadLineAsString(":> ");
                                    Console.Write("\nVälj ett lösenord: ");
                                    string passWord = ReadLineAsString(":> ");

                                    Console.Clear();
                                    Console.WriteLine("Du har skrivit in följande:");
                                    Console.WriteLine($"Personnummer: {idNumber}\nFörnamn: {firstName}\nEfternamn: {lastName}\nBlodgrupp: {bloodtype}\nEmail: {eMail}\nLösenord: {passWord}");
                                    Console.WriteLine($"Stämmer Uppgifterna? [{keyJa}/{keyNej}]");
                                    ConsoleKey correctInformation = Console.ReadKey(true).Key;
                                    if (correctInformation == keyJa)
                                    {
                                        BloodDonor newDonor = new BloodDonor(firstName, lastName, idNumber, eMail, availableToDonate, healthOK, bloodGroup, passWord);
                                        try
                                        {
                                            bb.AddUser(newDonor);
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Kunde inte lägga till användaren.");
                                        }

                                        loopAgain = false;
                                    }
                                    else if (correctInformation == keyNej)
                                    {
                                        Console.WriteLine("\nAjdå, då får du fylla i alla uppgifterna igen..    :) \n");
                                        Console.WriteLine("Tryck valfri knapp för att fortsätta.");
                                        Console.WriteLine($"Tryck {keyQuit} för att återgå till menyn.");
                                        ConsoleKey input = Console.ReadKey(true).Key;
                                        if (input == keyQuit) loopAgain = false;
                                    }
                                }
                                PauseProgram();
                            }
                            else
                            {
                                PrintNotRequiredEnough();
                                Thread.Sleep(3000);
                                Console.
                                Clear();
                                Console.WriteLine("Du skickas nu tillbaka till startmenyn");
                                Thread.Sleep(3000);
                            }
                            break;
                        }
                    case keyQuit:
                        {
                            Console.WriteLine("\nTack för att du besökte oss, Välkommen Åter!");
                            Thread.Sleep(1000);
                            isRunning = false;
                            break;
                        }

                }
            }
        }

        private static void PrintWelcomePageBloodBank()
        {
            Console.Clear();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("--- Välkommen till Blodbanken---");
            Console.WriteLine("---            :)           ---");
            Console.WriteLine("---                         ---");
        }
        private static void PrintRequiredToDonate()
        {
            Console.Clear();
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("---  Grattis du är godkänd för att ge blod ---");
            Console.WriteLine("---        Tack För Din Vilja !            ---");
        }
        private static void PrintNotRequiredEnough()
        {
            Console.Clear();
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("---    Tyvärr uppfyller du inte kraven     ---");
            Console.WriteLine("---   för att kunna donera blod just nu.   ---");
            Console.WriteLine("---        Tack För Din Vilja !            ---");
        }

        private static void PrintStartMenuOption()
        {
            Console.Clear();
            Console.WriteLine($"Start meny");
            Console.WriteLine($"-------------------------------");
            Console.WriteLine($"{keyLoggin} : Logga in");
            Console.WriteLine($"{keyCreateAccount} : Registrera nytt donator konto");
            Console.WriteLine($"{keyQuit} : Avsluta");
        }
        private static void PrintStaffMenuOption(string firstName, string lastName, string titel)
        {
            Console.Clear();
            Console.WriteLine($"Du är inloggad som personal: {firstName} {lastName}: {titel}");
            Console.WriteLine($"-------------------------------");
            Console.WriteLine($"{keyCheckAmountBlood} : Kolla hur mycket blod som finns");
            Console.WriteLine($"{keyRegisterNewDonation} : Registrera ny bloddonation");
            Console.WriteLine($"{keyRequestBloodDonation} : Skicka ut mail med förfrågan om att donera blod");
            Console.WriteLine($"{keyRegisterNewStaffAccount} : Registrera ett nytt personalkonto");
            Console.WriteLine($"{keyLoggout} : Logga ut");
        }

        private static void PrintDonorMenu(string firstName, string lastName)
        {
            Console.Clear();
            Console.WriteLine($"Du är inloggad som bloddonator: {firstName} {lastName}");
            Console.WriteLine($"-------------------------------");
            Console.WriteLine($"{keyReviseInformation} : Vill du ändra din status som donator?");
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
        private static int ReadStringAsInt(string printString, int minValue, int maxValue)
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
                    if ((maxValue == 0 || output <= maxValue) && output >= minValue) success = true;
                    else Console.WriteLine($"Skriv antal enheter mellan {minValue} - {maxValue}");
                }
                catch
                {
                    Console.WriteLine("Hoppsan, du skrev inte in en siffra! Försök igen.");
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
