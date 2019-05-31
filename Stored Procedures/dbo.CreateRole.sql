SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Pezanne Khambatta
-- Create date: 12th April, 2019
-- Description:	Create Role
-- EXEC CreateRole 'Technology Admin', 'stuff'
-- =============================================
CREATE PROCEDURE [dbo].[CreateRole]
    @Name			NVARCHAR(256),
	@Description	NVARCHAR(256)
AS
BEGIN
	DECLARE @P_Name			NVARCHAR(256) = @Name,
			@P_Description	NVARCHAR(256) = @Description

	INSERT INTO dbo.Roles (Name, Description)
	VALUES (@P_Name, @P_Description)
	SELECT SCOPE_IDENTITY() AS RoleID  
END

GO
