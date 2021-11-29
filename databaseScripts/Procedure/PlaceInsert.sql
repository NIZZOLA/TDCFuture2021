USE [MvpConf2021]
GO

/****** Object:  StoredProcedure [dbo].[PlaceInsert]    Script Date: 06/11/2021 16:28:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[PlaceInsert]

	 @Lat decimal(15,9), 
	 @Long decimal(15,9), 
	 @Name varchar(max), 
	 @Address varchar(max),
	 @Description varchar(max) = '',
	 @PhoneNumber varchar(15),
	 @Snippet varchar(max) = '',
	 @styleUrl varchar(max) = '',
	 @Id varchar(15) = ''

AS BEGIN

	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	DECLARE @point geography = geography::STPointFromText('POINT(' + CAST(@Lat AS VARCHAR(20)) + ' ' + CAST(@Long AS VARCHAR(20)) + ')', 4326);

	INSERT INTO [dbo].[Places]
           ([Name]
           ,[Address]
           ,[PhoneNumber]
           ,[Snippet]
           ,[Description]
           ,[StyleUrl]
           ,[Latitude]
           ,[Longitude]
           ,[LocationPoint]
           ,[Id])
     VALUES
			( @Name,
			  @Address,
			  @PhoneNumber,
			  @Snippet,
			  @Description,
			  @styleUrl,
			  @Lat,
			  @Long,
			  @point,
			  @Id )
		
	SELECT TOP 1 * FROM [DBO].[Places] where PlaceId =(SELECT MAX(PlaceId)  FROM [dbo].[Places]);

END;
GO


