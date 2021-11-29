use MvpConf2021
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[GetPlacesByLocationId]
		@LocationId = 1
		--,@PlaceType = 'Padaria'
		

SELECT	'Return Value' = @return_value

GO
