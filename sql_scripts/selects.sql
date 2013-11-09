--2003
Select * from Messdaten
	where year(Datum) = 2003
--7.2004
Select * from Messdaten
	where year(Datum) = 2004 and month(Datum) = 7 
--3.9.2005
Select * from Messdaten
	where year(Datum) = 2005 and MONTH(Datum) = 9 and day(Datum) = 3

use ThermoDB

--	2012 - 2013
select Convert( nvarchar(30),Datum, 104),CONVERT(nvarchar(30), Datum, 108), Temperatur from Messdaten
where (year(Datum) Between '2012' and '2013')
order by Datum DESC

--	2012 - 10.2013
select Convert( nvarchar(30),Datum, 104),CONVERT(nvarchar(30), Datum, 108), Temperatur from Messdaten
where Datum between convert(datetime, '1.1.2012') and convert(datetime,DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,'1.10.2013')+1,0)) )
order by Datum DESC

--	27.9.2012 - 1.10.2013
select Convert( nvarchar(30),Datum, 104),CONVERT(nvarchar(30), Datum, 108), Temperatur from Messdaten
where Datum between CONVERT(datetime, '9.27.2012') and convert(datetime, '1.10.2013')
order by Datum DESC

