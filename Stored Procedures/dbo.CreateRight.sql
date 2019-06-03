SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Pezanne Khambatta
-- Create date: 18th April, 2019
-- Description:	Create Right
-- EXEC CreateRight 'Create' 
-- =============================================
CREATE PROCEDURE [dbo].[CreateRight]
    @Name			NVARCHAR(256),
	@Description	NVARCHAR(256)
AS
BEGIN
	DECLARE @P_Name			NVARCHAR(256) = @Name,
			@P_Description	NVARCHAR(256) = @Description

	INSERT INTO dbo.Rights (Name, Description)
	VALUES (@P_Name, @Description)
	SELECT SCOPE_IDENTITY() AS RightID  
END

GO
