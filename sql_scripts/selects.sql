Select * from Messdaten
	where year(Datum) = 2003

Select * from Messdaten
	where year(Datum) = 2004 and month(Datum) = 7 

Select * from Messdaten
	where year(Datum) = 2005 and MONTH(Datum) = 9 and day(Datum) = 3

Select datename(WEEKDAY, Datum) from Messdaten
	where Datum between dateadd(week, datediff(week, 0, '2005-09-03 06:53:45.000'), 7) and dateadd(week, datediff(week, 0, '2005-09-03 06:53:45.000'), 14)
	order by Datum ASC

