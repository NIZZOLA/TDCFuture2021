use MvpConf2021
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[GetPlaceHierarchy]
		@PlaceId = 48

SELECT	'Return Value' = @return_value

GO

--select * from places