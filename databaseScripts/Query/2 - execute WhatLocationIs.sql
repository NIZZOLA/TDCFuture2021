use MvpConf2021
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[WhatLocationIs]
		@Lat = -23.2159214	,
		@Long = -47.2685902

SELECT	'Return Value' = @return_value

GO

--select * from places