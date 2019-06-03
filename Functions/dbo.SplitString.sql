SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Gaurav Bhavsar (Borrowed by Pezanne Khambatta)
-- Create date: 16th April 2019
-- Description:	Split comma separated string into table
-- =============================================
CREATE FUNCTION [dbo].[SplitString]
(
	@input AS Varchar(MAX)
)
RETURNS @Result TABLE(Value VARCHAR(50))
AS
BEGIN
	
	DECLARE @x XML

	SELECT @x = CAST('<A>'+ REPLACE(@input,',','</A><A>')+ '</A>' AS XML)
	
	INSERT INTO @Result(Value)
	SELECT t.value('.', 'varchar(max)') AS data
	FROM @x.nodes('/A') AS x(t)

	RETURN

END
GO
