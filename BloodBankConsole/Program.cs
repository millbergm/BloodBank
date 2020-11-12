using System;

namespace Bloodbank
{
    class Program
    {
        static void Main(string[] args)
        {
            BloodDonor donor1 = new BloodDonor("Maria", "Larsson", 8604241234, "maria.larsson@hotmail.com", BloodGroup.A);

            Console.WriteLine(donor1);
        }
    }
}
