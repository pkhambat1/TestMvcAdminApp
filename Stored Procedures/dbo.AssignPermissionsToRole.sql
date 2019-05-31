SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================    
-- Author:  Pezanne Khambatta    
-- Create date: 23th April 2019    
-- Description: Map Permissions to Role
-- EXEC [dbo].[AssignPermissionsToRole] 3, 18  
-- =============================================    
CREATE PROCEDURE [dbo].[AssignPermissionsToRole]    
    @RoleID   INT,    
    @PermissionIDs  NVARCHAR(300)    
AS    
BEGIN    
    DECLARE @P_RoleID			INT = @RoleID,  
			@P_PermissionIDs	NVARCHAR(300) = @PermissionIDs

    /* Remove existing mapping */
    DELETE [dbo].[RolePermissions] WHERE RoleID = @P_RoleID
     
    /* Insert New Mapping */
    INSERT INTO [dbo].[RolePermissions](RoleID, PermissionID)    
    SELECT @P_RoleID, Value from SplitString(@P_PermissionIDs)    
  
END
GO
