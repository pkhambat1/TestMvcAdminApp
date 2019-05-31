SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Pezanne Khambatta
-- Create date: 15th May, 2019
-- Description:	Delete role by roleID
-- EXEC DeleteRole '1076'
-- =============================================
CREATE PROCEDURE [dbo].[DeleteRole]
	@RoleID		INT
AS
	DECLARE @P_RoleID		INT = @RoleID
BEGIN
	DELETE FROM dbo.Roles WHERE ID = @P_RoleID
END

GO
