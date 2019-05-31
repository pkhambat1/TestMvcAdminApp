SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================  
-- Author:  Pezanne Khambatta
-- Create date: 23rd April 2019  
-- Description: Get Role Name by RoleID
-- EXEC GetRoleNameByRoleID 6
-- =============================================  
CREATE PROCEDURE [dbo].[GetRoleNameByRoleID]

	@RoleID   INT

AS
BEGIN  
	DECLARE @P_RoleID		INT = @RoleID
	SELECT Name FROM dbo.Roles WHERE ID = @P_RoleID
END

GO
