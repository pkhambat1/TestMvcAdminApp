SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================    
-- Author:  Pezanne Khambatta    
-- Create date: 16th April 2019    
-- Description: Map Roles to User
-- EXEC [dbo].[AssignUserRole] 3, 18  
-- =============================================    
CREATE PROCEDURE [dbo].[AssignRolesToUser]    
    @UserID   INT,    
    @RoleIDs  NVARCHAR(300)    
AS    
BEGIN    
    DECLARE @P_UserID	INT = @UserID,  
			@P_RoleIDs  NVARCHAR(300) = @RoleIDs  

    /* Remove existing mapping */
    DELETE [dbo].[UserRoles] WHERE UserID = @P_UserID
     
    /* Insert New Mapping */
    INSERT INTO [dbo].[UserRoles](UserID, RoleID)    
    SELECT @P_UserID, Value from SplitString(@P_RoleIDs)    
  
END
GO
