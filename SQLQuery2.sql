USE CurseWork;
go
Create procedure RoleChoose
@Users nvarchar(50),
@Password nvarchar(50)
as
begin
Select Role From Authoris WHERE Users = @Users AND Password = @Password
end;




go
Create procedure Register
@Users nvarchar(50),
@Password nvarchar(50),
@Role nvarchar(50),
@Balancez INT
as
begin
INSERT INTO Authoris VALUES (@Users,@Password,@Role,@Balancez)
end;
go
Create procedure InsertInUse
@Number nvarchar(50)
as
begin
Select count(*) from Arendation where NumberOfAuto=@Number;
end;

exec InsertInUse @Number=N'24131';

go
Create procedure Balances
@Uzverzzz nvarchar(50)
as
begin
SELECT Balancez FROM Authoris WHERE Users IN (@Uzverzzz)
end;
go
Create procedure CarSelect
as 
begin
SELECT * FROM CarTable
end;
go
Create procedure SelectMark
as
begin
SELECT CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable
end;

go
create procedure Search
@Script nvarchar(50)
as
begin
SELECT CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable WHERE CarTable.TypeOfAuto like '%'+@Script+'%'
end;


go

create procedure SearchCombobox1
@Script nvarchar(50),
@Combobox nvarchar(50)
as
begin
SELECT distinct CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable, Arendation WHERE CarTable.TypeOfAuto like '%' + @Script + '%' and Arendation.PlacementID = @Combobox
end;
drop procedure SearchCombobox1;

go
create procedure searchComboPlace
@Script nvarchar(50),
@Combobox nvarchar(50)
as
begin
SELECT distinct CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable, Arendation WHERE CarTable.TypeOfAuto like '%' + @Script + '%' and Arendation.PlacementID = @Combobox
end;
drop procedure searchComboPlace1;
drop procedure SearchAllCombo;

go
create procedure searchComboPlace1
@Script1 nvarchar(50),
@Combobox nvarchar(50)
as
begin
SELECT distinct CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable, Arendation WHERE CarTable.Auto like '%' + @Script1 + '%' and Arendation.PlacementID = @Combobox
end;
go
create procedure SearchAutoMark
@Script nvarchar(50)
as
begin
SELECT CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable WHERE CarTable.Auto like '%'+@Script+'%'
end;

go
create procedure SearchAllCombo
@Script nvarchar(50),
@Script1 nvarchar(50),
@Combobox nvarchar(50)
as
begin
SELECT distinct CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable,Arendation WHERE CarTable.TypeOfAuto like '%' + @Script + '%' and CarTable.Auto like '%' + @Script1 + '%' and Arendation.PlacementID = @Combobox
end;
go


create procedure SearchAll
@Script nvarchar(50),
@Script1 nvarchar(50)
as
begin
SELECT CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable WHERE CarTable.TypeOfAuto like '%' + @Script + '%' and CarTable.Auto like '%' + @Script1 + '%'
end;
go

create procedure ArendationAll
as
begin
SELECT * FROM Arendation
end;
go

create procedure ArendationSum
as
begin
SELECT sum(ToPay) FROM Arendation
end;

go
create procedure PartForDate
@Begin date,
@End date
as
begin
SELECT * FROM Arendation Where DataOfDock Between @Begin And @End
end;

go
create procedure testingPractise
as
begin
SELECT NumberOfAuto,sum(ToPay) as alias_name FROM Arendation Where DataOfDock Between '2019-05-01' And '2019-05-31' group by NumberOfAuto;

end;


go
create procedure Practmark
as
begin
select AutoMark.Mark,sum(ToPay) as alias_name from  AutoMark inner join CarTable on CarTable.ID_Mark=AutoMark.ID_Mark inner join Arendation on
Arendation.NumberOfAuto=CarTable.NumberOfAuto Where DataOfDock Between '2019-05-01' And '2019-05-31' group by AutoMark.Mark;
end;
select AutoMark.Mark,sum(ToPay) as alias_name from  AutoMark inner join CarTable on CarTable.ID_Mark=AutoMark.ID_Mark inner join Arendation on
Arendation.NumberOfAuto=CarTable.NumberOfAuto Where DataOfDock Between '2019-05-01' And '2019-05-31' group by AutoMark.Mark;

select Type.AutoType,sum(ToPay) as alias_name from Type inner join AutoMark on Type.ID_Type=AutoMark.ID_Type inner join CarTable on CarTable.ID_Mark=AutoMark.ID_Mark inner join Arendation on 
Arendation.NumberOfAuto=CarTable.NumberOfAuto Where DataOfDock Between '2019-05-01' And '2019-05-31' group by Type.AutoType;

go
create procedure PractType
as
begin
select Type.AutoType,sum(ToPay) as alias_name from Type inner join AutoMark on Type.ID_Type=AutoMark.ID_Type inner join CarTable on CarTable.ID_Mark=AutoMark.ID_Mark inner join Arendation on 
Arendation.NumberOfAuto=CarTable.NumberOfAuto Where DataOfDock Between '2019-05-01' And '2019-05-31' group by Type.AutoType;
end;


go
create procedure SumForDate
@Begin date,
@End date
as
begin
SELECT sum(ToPay) FROM Arendation Where DataOfDock Between @Begin And @End
end;

go
create procedure ArendForUser
@Uzv nvarchar(50)
as
begin
SELECT * FROM Arendation Where Users IN (@Uzv)
end;
drop procedure ArendForUser

go
Create procedure SumForUser
@Uzv nvarchar(50)
as 
begin
SELECT sum(ToPay) FROM Arendation Where Users IN(@Uzv)
end;
go
Create procedure UsersBalance
@Uzv nvarchar(50)
as 
begin
SELECT Balancez FROM Authoris Where Users IN(@Uzv)
end;

go
Create procedure UpdateBalance
@Uzv nvarchar(50),
@balance1 integer
as 
begin
Update Authoris set Balancez=@balance1 Where Users IN(@Uzv)
end;
drop procedure UpdateBalance;

go

Create procedure AddUser
as 
begin
INSERT INTO Authoris (Users,Password,Role,Balancez) VALUES (@Users, @Password,@Role,@Balancez);
end;

go
Create procedure AutoSearch
@NumberAuto nvarchar(50)
as
begin
Select count(*) from Arendation Where NumberOfAuto = @NumberAuto
end;

go

Create procedure UserFullDock
@NumberAuto nvarchar(50),
@Date date
as
begin
Select * from Arendation Where @Date  > ALL(Select DateEnd from Arendation where NumberOfAuto = @NumberAuto) And NumberOfAuto =@NumberAuto;
end;

go
Create procedure selectUser
@Username nvarchar(50)
as
begin
Select * from Authoris where Users = @Username
end;


go
exec RoleChoose @Users=N'User', @Password=N'-1152142086';
exec Balances @Uzverzzz=N'User';
exec CarSelect;
exec SelectMark;
exec Search @Script=N'Легковая';
exec SearchAutoMark @Script=N'Сузуки';
exec SearchAll @Script=N'Легковая', @Script1=N'Сузуки';
exec ArendationAll;
exec ArendationSum;
exec PartForDate @Begin=N'10.10.2017', @End=N'10.10.2019';
exec ArendForUser @Uzv=N'User1';
exec SumForUser @Uzv=N'User';
exec UsersBalance @Uzv=N'User';
exec UpdateBalance @balance1=N'100',@Uzv=N'User1';
exec AutoSearch @NumberAuto=N'12345';
exec selectUser @Username=N'User';
exec UserFullDock @Date=N'2019-07-05',@NumberAuto=N'42121';

Select * from dbo.Arendation;
select * from Placement;
select NumberOfAuto from Arendation where DateEnd > SYSDATETIME() and Start<SYSDATETIME();

select CarTable.Auto ,CarTable.NumberOfAuto, CarTable.TypeOfAuto,Placement.PlacementID as PlacementID from Placement inner join CarTable
on Arendation.PlacementID=CarTable.PlacementID;

select CarTable.Auto ,CarTable.NumberOfAuto, CarTable.TypeOfAuto from CarTable where CarTable.NumberOfAuto Not in (select NumberOfAuto from Arendation where DateEnd > SYSDATETIME() and Start<SYSDATETIME())

select * from CarTable where CarTable.NumberOfAuto Not in (select NumberOfAuto from Arendation where DateEnd > SYSDATETIME() and Start<SYSDATETIME())

--go
--Create procedure IDUpdate
--as 
--begin
--Update CarTable set PlacementID=1 Where PlacementID is null
--end;
--exec IDUpdate;
--drop procedure IDUpdate
go
Create procedure CarSelect123
as 
begin
SELECT * FROM CarTable where CarTable.NumberOfAuto Not in (select NumberOfAuto from Arendation where DateEnd > SYSDATETIME() and Start<SYSDATETIME())
end;
drop procedure CarSelect
go
Create procedure CarSelectAdmin
as 
begin
SELECT * FROM CarTable;
end;

go
Create procedure PlaceSelectAdmin
as 
begin
SELECT * FROM Placement;
end;

go
Create procedure PlaceSelectUser
as 
begin
SELECT * FROM Placement;
end;

go
Create procedure PlaceSelectType
as 
begin
SELECT Distinct TypeOfAuto FROM CarTable;
end;

go
Create procedure PlaceSelectMark
as 
begin
SELECT Distinct Auto FROM CarTable;
end;
drop procedure PlaceSelectType
go
Create procedure CarSelectArend
@Number int,
@StartTime date,
@EndTime date
as 
begin
SELECT * FROM Arendation where (Arendation.NumberOfAuto =@Number and ((Start<=@StartTime and @StartTime<=DateEnd ) or ( Start<=@EndTime and @EndTime<=DateEnd )))
end;
drop procedure CarSelectArend
exec selectUser @Username=N'User';

exec CarSelectArend @Number=N'12345',@StartTime=N'2019-11-07',@EndTime=N'2019-11-09'
go
create procedure Search
@Script nvarchar(50)
as
begin
SELECT CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable inner join ##AnotherOrder on ##AnotherOrder.OtherAuto=CarTable.NumberOfAuto WHERE CarTable.TypeOfAuto like '%'+@Script+'%'
end;
drop procedure Search

go
create procedure searchComboPlace
@Script nvarchar(50),
@Combobox nvarchar(50)
as
begin
SELECT CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable inner join ##AnotherOrder on ##AnotherOrder.OtherAuto=CarTable.NumberOfAuto WHERE CarTable.TypeOfAuto like '%' + @Script + '%' and ##AnotherOrder.OtherNum = @Combobox
end;

SELECT CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable inner join ##AnotherOrder on ##AnotherOrder.OtherAuto=CarTable.NumberOfAuto WHERE CarTable.TypeOfAuto like 'Легковая' and ##AnotherOrder.OtherNum = 1


SELECT CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable inner join ##AnotherOrder on ##AnotherOrder.OtherAuto=CarTable.NumberOfAuto WHERE CarTable.TypeOfAuto like '%'+@Script+'%'

drop procedure searchComboPlace

go
create procedure SearchCombo
@Combobox nvarchar(50)
as
begin
SELECT distinct CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable, Arendation WHERE CarTable.NumberOfAuto Not in (select NumberOfAuto from Arendation where DateEnd >= SYSDATETIME() and Start<=SYSDATETIME())
end;
exec SearchCombo @Combobox=N'1';





drop table ##OrdersSum;
select * from #OrdersSum;
go
create procedure NewSelect
as
begin
truncate table ##OrdersSum
Insert into ##OrdersSum
SELECT Arendation.NumberOfAuto,Arendation.Start,Arendation.DateEnd,Arendation.PlacementID FROM Arendation where (Arendation.NumberOfAuto Not in (select NumberOfAuto from Arendation where DateEnd >= (CAST(GETDATE() AS DATE)) and Start <= (CAST(GETDATE() AS DATE))) and DateEnd < (CAST(GETDATE() AS DATE)))order by Arendation.DateEnd;
end;
drop procedure NewSelect;
exec NewSelect;
drop table ##OrdersSum;


select * from ##OrdersSum;

select * from Arendation;


drop table ##Order
go
create procedure SmallOrderSelect
as
begin
truncate table ##Order
insert into ##Order select ##OrdersSum.ArNum,MAX(##OrdersSum.ArEnd) from ##OrdersSum group by ##OrdersSum.ArNum;
end;
drop procedure SmallOrderSelect
exec SmallOrderSelect;
select * from ##Order;



go 
create procedure TableOrdersSum
as
begin
if not exists (select * from tempdb.sys.objects where name='##OrdersSum')
CREATE TABLE ##OrdersSum
(ArNum INT,
ArSt DATE,
ArEnd DATE,
ArId INT
)
end;
go 
create procedure TableOrder
as
begin
if not exists (select * from tempdb.sys.objects where name='##Order')
CREATE TABLE ##Order
(ArNum1 INT,
ArEnd1 DATE
)
end;
go
create procedure AnotherTableOrder
as
begin
if not exists (select * from tempdb.sys.objects where name='##AnotherOrder')
CREATE TABLE ##AnotherOrder
(OtherNum INT,
OtherStart DATE,
OtherEnd DATE,
OtherAuto INT
)
end;
SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'
select * from tempdb.sys.objects

go
create procedure InsertTablePlacement
as
begin
if not exists (select * from tempdb.sys.objects where name='##InsertPlacement')
CREATE TABLE ##InsertPlacement
(Sells INT,
Type NVARCHAR(50),
Mark NVARCHAR(50),
Number INT,
ID INT
)
end;
drop procedure TableOrder
drop table ##AnotherOrder;
go
create procedure Inner1
as
begin
truncate table ##AnotherOrder
insert into ##AnotherOrder select Arendation.PlacementID, Arendation.Start, Arendation.DateEnd, Arendation.NumberOfAuto from Arendation inner join ##Order on ##Order.ArNum1=Arendation.NumberOfAuto and ##Order.ArEnd1=Arendation.DateEnd;
end;
drop procedure Inner1;
exec Inner1;
select * from ##AnotherOrder;
go
create procedure Window1
as
begin
select distinct CarTable.Sells, CarTable.TypeOfAuto, CarTable.Auto, CarTable.NumberOfAuto from CarTable inner join ##AnotherOrder on ##AnotherOrder.OtherAuto=CarTable.NumberOfAuto
inner join AutoMark on AutoMark.ID_Mark=CarTable.ID;
end;
exec Window1;
drop procedure Window1;

drop table ##InsertPlacement;
go
create procedure WindowInsert
as
begin
truncate table ##InsertPlacement
insert into ##InsertPlacement select distinct CarTable.Sells, CarTable.TypeOfAuto, CarTable.Auto, CarTable.NumberOfAuto,##AnotherOrder.OtherNum from CarTable inner join ##AnotherOrder on ##AnotherOrder.OtherAuto=CarTable.NumberOfAuto;
end;
drop procedure WindowInsert;

select * from ##InsertPlacement;
SELECT distinct ##OrdersSum.ArNum FROM ##OrdersSum WHERE ##OrdersSum.ArNum Not in (select NumberOfAuto from Arendation where DateEnd > SYSDATETIME() and Start<SYSDATETIME())





go
create procedure NewTable
as
begin
select * from ##OrdersSum
--where колонка = (select max(колонка) from та же таблица)
select ##OrdersSum.ArNum,MAX(##OrdersSum.ArEnd) from ##OrdersSum group by ##OrdersSum.ArNum;
select 
SELECT Arendation.NumberOfAuto,Arendation.Start,Arendation.DateEnd,Arendation.PlacementID FROM  Arendation WHERE Arendation.NumberOfAuto=(select ##OrdersSum.ArNum,MAX(##OrdersSum.ArEnd) from ##OrdersSum group by ##OrdersSum.ArNum);





drop procedure SearchCombo;

go
create procedure SearchAutoMark
@Script nvarchar(50)
as
begin
SELECT CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable inner join ##AnotherOrder on ##AnotherOrder.OtherAuto=CarTable.NumberOfAuto WHERE CarTable.Auto like '%'+@Script+'%'
end;
go
create procedure searchComboPlace1
@Script1 nvarchar(50),
@Combobox nvarchar(50)
as
begin
SELECT CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable inner join ##AnotherOrder on ##AnotherOrder.OtherAuto=CarTable.NumberOfAuto WHERE CarTable.Auto like '%' + @Script1 + '%' and ##AnotherOrder.OtherNum = @Combobox
end;
go
create procedure SearchAll
@Script nvarchar(50),
@Script1 nvarchar(50)
as
begin
SELECT CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable inner join ##AnotherOrder on ##AnotherOrder.OtherAuto=CarTable.NumberOfAuto WHERE CarTable.TypeOfAuto like '%' + @Script + '%' and CarTable.Auto like '%' + @Script1 + '%'
end;
SELECT CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable inner join ##AnotherOrder on ##AnotherOrder.OtherAuto=CarTable.NumberOfAuto WHERE CarTable.Auto like '%'+@Script+'%'

go
create procedure SearchAllCombo
@Script nvarchar(50),
@Script1 nvarchar(50),
@Combobox nvarchar(50)
as
begin
SELECT CarTable.Auto,CarTable.NumberOfAuto,CarTable.TypeOfAuto,CarTable.Sells FROM CarTable inner join ##AnotherOrder on ##AnotherOrder.OtherAuto=CarTable.NumberOfAuto WHERE CarTable.TypeOfAuto like '%' + @Script + '%' and CarTable.Auto like '%' + @Script1 + '%' and ##AnotherOrder.OtherNum = @Combobox
end;
drop procedure SearchAllCombo


go










go
create procedure CountPlace
@Place1 int
as
begin
SELECT count(*) FROM Arendation where Arendation.PlacementID=@Place1 and(Arendation.NumberOfAuto in (select NumberOfAuto from Arendation where DateEnd > (CAST(GETDATE() AS DATE)) and Start <= (CAST(GETDATE() AS DATE))) and DateEnd < (CAST(GETDATE() AS DATE)));
end;
go
create procedure AllPlace
@PlaceAll int
as
begin
select NumOfCars from Placement where PlacementID=@PlaceAll;
end;








--Create procedure UpdatePlace
--@PlaceID nvarchar(50),
--@CarNum int
--as 
--begin
--Update CarTable set PlacementID=@PlaceID Where NumberOfAuto IN(@CarNum)
--end;
--drop procedure UpdatePlace;
--exec UpdatePlace @PlaceID=N'1',@CarNum=N'12345';
--select * from CarTable;

go
Create procedure UpdateBalance
@Uzv nvarchar(50),
@balance1 integer
as 
begin
Update Authoris set Balancez=@balance1 Where Users IN(@Uzv)
end;

go
Create procedure DeleteAuto
@CarNum int
as 
begin
select count(*) from Arendation where NumberOfAuto=@CarNum
end;
drop procedure DeleteAuto;

go
Create procedure Deser
@ID nvarchar(50)
as 
begin
select * from Placement where PlacementID=@ID
end;

go
declare @t1 table (id int)
go
Create procedure Deser
@t1 table(id int)
as 
begin
merge dbo.Placement as T_Base
using @t1 as T_Source
on(T_Base.PlacementID=T_Source.PlacementID)
when matched then 
update set CarPlacement=T_Source.PlacementID

end;


exec DeleteAuto @CarNum=N'24131';

go
Create procedure DeletePlacement
@PlacementID nvarchar(50)
as 
begin
select count(*) from Arendation where PlacementID=@PlacementID
end;

exec DeletePlacement @PlacementID=N'1';

drop procedure DeletePlacement;



SELECT * FROM Placement





declare @xmlcol xml='D:\TestData.xml'
select [PlacementID]


CREATE TABLE #ProductSummary
(CarPlacement NVARCHAR(50),
PlacementID NVARCHAR(50))

declare @x xml
select @x=P
go
Create procedure Deser
@ID nvarchar(50)
as 
begin
select * from Placement where PlacementID=@ID
end;
go
Create procedure SelectFTable
@PlacementID nvarchar(50),
@CarPlacement nvarchar(50),
@NumOfCars nvarchar(50),
@Phone nvarchar(50),
@Address nvarchar(50)
as 
if not exists(select * from Placement where CarPlacement=@CarPlacement)
begin
insert into Placement(CarPlacement,NumOfCars,Phone,Address) values (@CarPlacement,@NumOfCars,@Phone,@Address)
end
else
begin
if exists(select * from Placement where CarPlacement=@CarPlacement)
update Placement set NumOfCars=@NumOfCars,Phone=@Phone,Address=@Address where CarPlacement=@CarPlacement
end;
drop procedure SelectFTable;

go
Create procedure Deziri
@h int=0,
@Stringxml nvarchar(3000)
as 
begin
exec sp_xml_preparedocument @h output,@Stringxml
select * from openxml(@h, '/DocumentElement/Placement',0)
with([CarPlacement] nvarchar(50),[PlacementID] nvarchar(50))
exec sp_xml_removedocument @h
insert Placement select [CarPlacement],[PlacementID]
from openxml(@h,'/DocumentElement/Placement',0)
with([CarPlacement] nvarchar(50),[PlacementID] nvarchar(50))
end;

declare @xmlcol xml='D:\TestData.xml'


CREATE TABLE #Test
(CarPlacement NVARCHAR(50),
PlacementID NVARCHAR(50))

select * from Arendation;
Declare @cnt INT=0;
while  @cnt<10000
begin
INSERT INTO Arendation (Start, DateEnd,Days,ToPay,NumberOfAuto,DataOfDock,Users,PlacementID) VALUES ('2019-11-29', '2019-11-29',1,0,'24131','2019-11-29','Admin',3)
SET @cnt = @cnt + 1;
end;
go
Create procedure AddNewOrder
@Start date,
@DateEnd date,
@Days int,
@NumberOfAuto int,
@DataOfDock date,
@ToPay int,
@Users nvarchar(50),
@PlacementID int
as
begin
INSERT INTO Arendation (Start, DateEnd,Days,ToPay,NumberOfAuto,DataOfDock,Users,PlacementID) VALUES (@Start, @DateEnd,@Days,@ToPay,@NumberOfAuto,@DataOfDock,@Users,@PlacementID) 
end;
drop procedure AddNewOrder;
exec AddNewOrder @Start, @DateEnd,@Days,@ToPay,@NumberOfAuto,@DataOfDock,@Users,@PlacementID
select * from Arendation;
drop table Placement;


go
create procedure PlacementSearch
@CarPlacement NVARCHAR(50)
as
begin
select PlacementID from Placement where CarPlacement=@CarPlacement
end;
exec PlacementSearch @CarPlacement=N'Пушкинская';
select PlacementID from Placement where CarPlacement='Пушкинская'

select * from Arendation;



USE CurseWork
drop procedure BackupData
USE master
USE CurseWork;

Select distinct Arendation.NumberOfAuto from Arendation;




go
create procedure BackupData
as
begin
backup database CurseWork with init
to disk='D:\CurseWork.bak'
end;

go
create procedure DropTable
as
begin
alter database CurseWork set single_user with rollback immediate 
drop database CurseWork
end;
drop procedure RestoreData

go
create procedure RestoreData
as
begin
restore database CurseWork
from disk='D:\CurseWork.bak'
with replace
end;



select * from CurseWork.sql_modules
select * from sys.sql_modules

SELECT * FROM SYS.PROCEDURES
EXECUTE SP_HELPTEXT 'searchComboPlace1'

go
create procedure SearchAutoMark  
@Script1 nvarchar(50)  
as  
begin  
SELECT AutoMark.Payment, AutoMark.Mark,Type.AutoType,CarTable.NumberOfAuto from AutoMark,Type, CarTable inner join ##AnotherOrder on ##AnotherOrder.OtherAuto=CarTable.NumberOfAuto   
WHERE (AutoMark.Mark like '%' + @Script1 + '%' and CarTable.ID_Mark=AutoMark.ID_Mark and AutoMark.ID_Type=Type.ID_Type)   
end;

go
create procedure OnlyPlacements  
@Combobox nvarchar(50)  
as  
begin  
SELECT AutoMark.Payment, AutoMark.Mark,Type.AutoType,CarTable.NumberOfAuto from AutoMark,Type, CarTable inner join ##AnotherOrder on ##AnotherOrder.OtherAuto=CarTable.NumberOfAuto   
WHERE (CarTable.ID_Mark=AutoMark.ID_Mark and AutoMark.ID_Type=Type.ID_Type and ##AnotherOrder.OtherNum = @Combobox)   
end;

exec OnlyPlacements
exec OnlyPlacements @Combobox=N'1';
