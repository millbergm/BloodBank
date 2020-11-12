using System;

namespace Bloodbank
{
    class Program
    {
        static void Main(string[] args)
        {
            BloodDonor donor1 = new BloodDonor("Maria", "Larsson", 8604241234, "maria.larsson@hotmail.com", BloodGroup.A);

            if (donor1.AvailableToDonate)
            {
                Console.WriteLine(donor1.FirstName + " " + donor1.LastName + " " + donor1.BloodGroup + " " + donor1.IDNumber);
            }

            Console.WriteLine(donor1);
        }
    }
}
