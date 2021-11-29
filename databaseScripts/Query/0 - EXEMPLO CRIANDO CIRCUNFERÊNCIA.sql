use MvpConf2021
declare @g geography, @h geography
set @g=geography::STGeomFromText('POINT(-23.2159214 -47.26859020000001)',4326); 

set @h=@g.STBuffer(3000);


select @h, 'area' name

 union all

select geography::Point( p.Longitude, p.Latitude ,4326 ),
	   p.Name name
 from places p





 