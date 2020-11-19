# BloodBank
BloodBank GroupProjekt

Struktur för indöpning, stavning + ordning (?)

typ : Properies / input variabel:

(User)
string : FirstName / X
string : LastName / X
string : IDNumber / X
string : PassWord / X

(Staff)
string : X / firstName
string : X / lastName
string : X / iDNumber                        <--- FEL variabel namn?>
string: X /                       "ingen variabel för password" <-- FEL? >
string: Title / title

(BloodDonor)     Två konstruktorer
X / firstName
X / lastName
X / idNumber
X /                               "ingen variabel för password" <-- FEL? >
string: Email / eMail                        <--- FEL variabel namn?>
int : AvailableToDonate / availableToDonate
int: HelthOK / healthOK
Enum-BloodGroup : BloodGroup / bloodGroup
DateTime : LatestDonation / latestDonation      <-- sitter den rätt i konstruktorn? >

(Donation)     Två konstruktorer
Enum-BloodGroup : Bloodgroup / bloodGroup              <-- FEL propertie namn? >
int : AmountOfBlood / amountOfBlood
string : DonorID / donorID
string : StaffID / staffID


Konstruktorer:
 public Staff(string firstName, string lastName, string iDNumber, string title)

 public BloodDonor (string firstName, string eMail, BloodGroup bloodGroup)

 public BloodDonor (string firstName, string lastName, string idNumber, string eMail, int availableToDonate, int healthOK, BloodGroup bloodGroup, DateTime latestDonation)

 public Donation (int amountOfBlood, BloodGroup bloodGroup)

 public Donation (int amountOfBlood, string donorID, string staffID)


