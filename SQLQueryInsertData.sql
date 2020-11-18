insert into BloodGroups (ID, Typ)
	values(1, 'O'),(2, 'A'), (3, 'B'), (4, 'AB')

go

insert into Donors(FirstName,LastName,ID, Email, BloodGroupID)
	values('Maria', 'Larsson', '19860424-1234', 'maria.larsson@hotmail.com', 2)

select * from Donors

select donors.id, firstname, LastName, BloodGroups.Typ from Donors
	inner join BloodGroups on BloodGroups.ID = Donors.BloodGroupID

insert into Staff
	values('995-00', 'Marcus', 'Millberg', 'Konstig Titel', 'password')

update Staff set id = '995-00'
where ID = '099500'
		
select * from staff

select * from donors

delete from Donors
where IDNumber = '1234567'

select * from BloodBank

exec RequestDonation 1

update Donors set PassWord = 'password'

delete from Staff
where ID = '995-00'

update Donors
set IDNumber = '199010195261'
where IDNumber = '19901019-5261'