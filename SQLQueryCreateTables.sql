create table BloodGroups ( ID int primary key, Typ varchar (2));

go

create table Donors (
	ID varchar(15) primary key,
	FirstName varchar(50),
	LastName varchar(50),
	AvailableToDonate bit default 1,
	HealthOK bit default 1,
	BloodGroupID int foreign key references BloodGroups(ID),
	LatestDonation Date);

go

create table Staff(
	ID varchar(15) primary key,
	FirstName varchar(50),
	LastName varchar(50),
	Title varchar(50));

go

Create table BloodBank(
	ID int identity(1,1) primary key,
	AmountOfBlood int,
	BloodGroupID int foreign key references BloodGroups(ID),
	DonorID varchar(15) foreign key references Donors(ID),
	StaffID varchar(15) foreign key references Staff(ID));

go

alter table BloodBank
	add DateOfDonation Date default getdate();

alter table staff
	add PassWord varchar(50)

alter table Donors
	add PassWord varchar(50)

alter table Donors
alter Column LatestDonation set Default '1900-01-01'