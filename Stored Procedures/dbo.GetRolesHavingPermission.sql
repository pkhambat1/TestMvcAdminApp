SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================  
-- Author:  Pezanne Khambatta
-- Create date: 9th May 2019  
-- Description: Get All Roles with PermissionID = @PermissionID
-- EXEC GetRolesHavingPermission '1027'
-- =============================================  
CREATE PROCEDURE [dbo].[GetRolesHavingPermission]

	@PermissionID   INT

AS
BEGIN  
    DECLARE @P_PermissionID		INT = @PermissionID

	SELECT		A.ID
				,A.Name
				,A.Description
				--,B.PermissionID
	FROM		dbo.Roles A
	JOIN		dbo.RolePermissions B
	ON			B.RoleID = A.ID
	WHERE		B.PermissionID = @P_PermissionID
END

GO
