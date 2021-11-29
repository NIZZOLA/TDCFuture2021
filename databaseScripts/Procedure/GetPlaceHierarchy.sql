USE [MvpConf2021]
GO

/****** Object:  StoredProcedure [dbo].[GetPlaceHierarchy]    Script Date: 06/11/2021 16:27:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetPlaceHierarchy]

	@PlaceId int 

AS BEGIN

	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	--declare @point geography;
	--select @point = LocationPoint from Places where placeid=@PlaceId

	--select @point ;
	declare @Lat decimal(15,9),@Long decimal(15,9);

	select  @Lat = Latitude,
			@Long = Longitude
		from Places where placeid = @Placeid;

	declare @point geography = geography::Point(@Lat,@Long, 4326);

	SELECT *
	 FROM (
			select 
			   lo.LocationId,
			   lo.LocationName,
			   lo.Polygon.MakeValid() Polygon,
			   lo.LocationType,
			   lo.Polygon.MakeValid().STIsValid() Valid,
			   lo.Latitude,
			   lo.Longitude
   
			   from Location lo
		  ) A
		   where A.Polygon.STIntersects(@point)  = 1
		   and A.Valid = 1

END;
GO


