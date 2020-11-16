create or alter procedure GetUserLogin(@idnumber varchar(50), @password varchar(50))
as
declare @value int = 0
begin
if exists (select Donors.ID from Donors where Donors.ID = @idnumber and Donors.PassWord = @password)
begin
	set @value = 1
end
if exists (select * from staff where Staff.FirstName = @idnumber and Staff.PassWord = @password)
begin
	set @value = 2
end
	return @value
END

go

create or alter procedure RequestDonation(@bloodgroup int)
as
begin
select FirstName, Email, BloodGroups.Typ from donors
	inner join BloodGroups on BloodGroups.ID = Donors.BloodGroupID
	where BloodGroups.ID = @bloodgroup
end

go

create or alter procedure AddDonor(
	@idnumber varchar(50),
	@firstname varchar(50),
	@lastname varchar(50),
	@availabletodonate bit,
	@healthok bit,
	@bloodgroupid int,
	@email varchar(50),
	@password varchar(50))
as
begin
INSERT INTO Donors (ID, FirstName, LastName, AvailableToDonate, HealthOK, BloodGroupID, Email, PassWord) 
	VALUES (@idnumber, @firstname, @lastname, @availabletodonate, @healthok, @bloodgroupid, @email, @password)
end

go

create or alter procedure AddStaff(
	@idnumber varchar(50),
	@firstname varchar(50),
	@lastname varchar(50),
	@title varchar(50),
	@password varchar(50))
as
begin
INSERT INTO Staff (ID, FirstName, LastName, Title, PassWord) 
	VALUES (@idnumber, @firstname, @lastname, @title, @password)
end

go

create or alter procedure AddDonation(
	@amountofblood int,
	@bloodgroupid int,
	@donorid varchar(50),
	@staffid varchar(50))
as
begin
insert into BloodBank(AmountOfBlood, BloodGroupID, DonorID, StaffID)
	values(@amountofblood, @bloodgroupid, @donorid, @staffid)
update Donors
	set LatestDonation = getdate()
	where donors.id = @donorid
end

go

create or alter procedure GetDonorInfo(@idnumber varchar(50))
as
begin
select Donors.FirstName, Donors.LastName, Donors.ID, Donors.Email, Donors.AvailableToDonate, Donors.HealthOK, Donors.BloodGroupID, Donors.LatestDonation from Donors	
end