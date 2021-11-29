USE [MvpConf2021]
GO

/****** Object:  StoredProcedure [dbo].[WhatLocationIs]    Script Date: 06/11/2021 16:29:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[WhatLocationIs]

	 @Lat decimal(15,9), 
	 @Long decimal(15,9),
	 @LocationType integer = 0

AS BEGIN

	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	declare @point geography = geography::Point(@Lat,@Long, 4326);

	SELECT *
	 FROM (
			select 
			   lo.LocationId,
			   lo.LocationName,
			   lo.Polygon.MakeValid() Polygon,
			   lo.LocationType,
			   lo.latitude,
			   lo.longitude
   
			   from Location lo
			   where ( @LocationType = 0 or lo.LocationType = @LocationType )
		  ) A
		   where A.Polygon.STIntersects(@point)  = 1

END;
GO


