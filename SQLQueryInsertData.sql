insert into BloodGroups (ID, Typ)
	values(1, 'O'),(2, 'A'), (3, 'B'), (4, 'AB')

go

insert into Donors(FirstName,LastName,ID, Email, BloodGroupID)
	values('Maria', 'Larsson', '19860424-1234', 'maria.larsson@hotmail.com', 2)

select * from Donors

select donors.id, firstname, LastName, BloodGroups.Typ from Donors
	inner join BloodGroups on BloodGroups.ID = Donors.BloodGroupID

insert into Staff (ID, FirstName, LastName, Title, PassWord)
	values ( '123456789012', 'Emelie', 'Broberg', 'Chef', 'password')


select * from BloodGroups
		
select * from staff

select * from donors

select * from BloodBank

delete from Donors
where ID = '1234567'

update Donors set PassWord = 'password'

delete from Donors
where ID = '1234567'

update Donors
set 
