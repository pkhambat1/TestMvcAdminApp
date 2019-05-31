SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================  
-- Author:  Pezanne Khambatta 
-- Create date: 15th May 2019  
-- Description: Get number of roles with given Permission
-- EXEC GetRolesCountForPermission 1029
-- =============================================  
CREATE PROCEDURE [dbo].[GetRolesCountForPermission]
  
	@PermissionID		INT
AS
BEGIN  
	DECLARE @P_PermissionID				INT = @PermissionID

	SELECT		Count(RoleID) as RolesCount
	FROM		dbo.RolePermissions
	WHERE		PermissionID = @P_PermissionID
END
GO
