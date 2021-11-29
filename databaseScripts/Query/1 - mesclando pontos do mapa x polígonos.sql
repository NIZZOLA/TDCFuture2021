use MvpConf2021
-- mesclando pontos do mapa x polígonos
declare @g geography, @circle geography

set @g = geography::Point(-23.2159214 ,-47.26859020000001, 4326);
set @circle=@g.STBuffer(30000);

SELECT geography::Point( Latitude, Longitude, 4326) LocationPoint, Name FROM PLACES
union all

select Polygon, locationName
  from location where LocationType=5

  union all

  select 
     @circle,
	 'regiao' name