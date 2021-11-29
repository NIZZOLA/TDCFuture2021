USE [MvpConf2021]
GO

/****** Object:  StoredProcedure [dbo].[GetPlacesOn]    Script Date: 06/11/2021 16:28:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetPlacesOn]

	 @Lat decimal(15,9), 
	 @Long decimal(15,9),
	 @PlaceType varchar(50) = '',
	 @Ray int = 1000

AS BEGIN

	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	DECLARE @point geography = geography::Point(@Lat,@Long, 4326);
	DECLARE @Circle geography = @point.STBuffer(@Ray);

	SELECT *
	 FROM (
			SELECT 
			   PL.*
			   FROM Places pl
			   WHERE ( @PlaceType = '' or @PlaceType = pl.Snippet )
		  ) A
		  WHERE @circle.STIntersects(a.LocationPoint)  = 1

END;
GO


