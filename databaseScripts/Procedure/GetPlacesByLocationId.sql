USE [MvpConf2021]
GO

/****** Object:  StoredProcedure [dbo].[GetPlacesByLocationId]    Script Date: 06/11/2021 16:28:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetPlacesByLocationId]

		@LocationId int,
		@PlaceType varchar(50) = ''

AS BEGIN

	DECLARE @poly geography;
	
	SELECT @poly = Polygon
	FROM   Location
	WHERE  LocationId=@LocationId;
	
	SELECT *
	 FROM (
			SELECT 
			   PL.*
			   FROM Places pl
			   WHERE ( @PlaceType = '' or @PlaceType = pl.Snippet )
		  ) A
		  WHERE @poly.STIntersects(a.LocationPoint)  = 1
END
GO


