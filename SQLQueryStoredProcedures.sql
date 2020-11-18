create or alter procedure GetUserLogin(@idnumber varchar(50), @password varchar(50))
as
declare @value int = 0
begin
if exists (select Donors.IDNumber from Donors where Donors.IDNumber = @idnumber and Donors.PassWord = @password)
begin
	set @value = 1
end
if exists (select * from staff where Staff.ID = @idnumber and Staff.PassWord = @password)
begin
	set @value = 2
end
	select @value
END

go

create or alter procedure GetUserLogin2
as
begin
select Donors.IDNumber as IDNumber, Donors.PassWord as PassWord from Donors
	union
select Staff.ID as IDNumber, Staff.PassWord as PassWord from Staff
end

go

create or alter procedure RequestDonation @bloodgroup int
as
begin
select FirstName, Email, BloodGroupID as BloodGroup from donors
	where BloodGroupID = @bloodgroup and HealthOK = 1 and AvailableToDonate = 1
end

go

create or alter procedure AddDonor(
	@idnumber varchar(50),
	@firstname varchar(50) null,
	@lastname varchar(50) null,
	@availabletodonate bit null,
	@healthok bit null ,
	@bloodgroupid int,
	@email varchar(50) null,
	@password varchar(50) null)
as
begin
INSERT INTO Donors (IDNumber, FirstName, LastName, AvailableToDonate, HealthOK, BloodGroupID, Email, PassWord) 
	VALUES (@idnumber, @firstname, @lastname, @availabletodonate, @healthok, @bloodgroupid, @email, @password)
end

go

create or alter procedure AddStaff(
	@idnumber varchar(50),
	@firstname varchar(50),
	@lastname varchar(50),
	@title varchar(50),
	@password varchar(50) null)
as
begin
INSERT INTO Staff (ID, FirstName, LastName, Title, PassWord) 
	VALUES (@idnumber, @firstname, @lastname, @title, @password)
end

go

create or alter procedure AddDonation(
	@amountofblood int,
	@donorid varchar(50),
	@staffid varchar(50))
as
declare @bloodgroupid int = (Select Donors.BloodGroupID From Donors where Donors.IDNumber = @donorid)
begin
insert into BloodBank(AmountOfBlood, BloodGroupID, DonorID, StaffID)
	values(@amountofblood, @bloodgroupid, @donorid, @staffid)
update Donors
	set LatestDonation = getdate()
	where donors.IDNumber = @donorid
end

go

create or alter procedure GetDonor(@idnumber varchar(50))
as
begin
select Donors.FirstName, Donors.LastName, Donors.IDNumber, Donors.Email, Donors.AvailableToDonate, Donors.HealthOK, Donors.BloodGroupID, Donors.LatestDonation from Donors	
end

go

create or alter procedure CheckBloodBank
as
begin
select sum(BloodBank.AmountOfBlood) as AmountOfBlood, BloodBank.BloodGroupID as Bloodgroup from BloodBank
group by BloodBank.BloodGroupID
end

go

create or alter procedure GetUserInfo (@idnumber varchar(50))
as
begin
if exists (select Donors.IDNumber from Donors where Donors.IDNumber = @idnumber)
	begin
	select FirstName, LastName, IDNumber, Email, AvailableToDonate, LatestDonation, BloodGroupID from Donors
	end
if exists (select Staff.ID from Staff where Staff.ID = @idnumber)
	begin
	select FirstName, LastName, ID, Title from Staff
	end
end

go

create or alter procedure UpdateDonor(@idnumber varchar(50), @availabletodonate bit)
as
begin
update Donors
set AvailableToDonate = @availabletodonate
where IDNumber = @idnumber
end
