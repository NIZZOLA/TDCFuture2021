USE [MvpConf2021]
GO

/****** Object:  StoredProcedure [dbo].[LocationInsert]    Script Date: 06/11/2021 16:28:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[LocationInsert]

	 @Lat decimal(15,9), 
	 @Long decimal(15,9), 
	 @GeoMultiPoly varchar(max), 
	 @PlaceName varchar(max), 
	 @PlaceCode varchar(3) = '', 
	 @PlaceType integer

AS BEGIN

	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	DECLARE @g geography = geography::Point( @Lat, @Long, 4326);
	DECLARE @pol geography = geography::STGeomFromText(@GeoMultiPoly, 4326);

    INSERT INTO LOCATION ( LocationName, LocationCode, LocationPoint, Polygon, LocationType , Latitude, Longitude)
                values ( @PlaceName, @PlaceCode, @g, @pol , @PlaceType , @Lat, @Long );

    SELECT TOP 1 * FROM LOCATION WHERE LOCATIONID = (	SELECT MAX(LocationId) FROM LOCATION );

END;
GO


