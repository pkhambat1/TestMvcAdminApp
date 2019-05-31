SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================    
-- Author:  Pezanne Khambatta    
-- Create date: 23th April 2019    
-- Description: Map Rights to Permission
-- EXEC [dbo].[AssignRightsToPermission] 3, 18  
-- =============================================    
CREATE PROCEDURE [dbo].[AssignRightsToPermission]
    @PermissionID	INT,    
    @RightIDs		NVARCHAR(300)    
AS    
BEGIN    
    DECLARE @P_PermissionID		INT = @PermissionID,  
			@P_RightIDs			NVARCHAR(300) = @RightIDs

    /* Remove existing mapping */
    DELETE [dbo].[PermissionRights] WHERE PermissionID = @P_PermissionID
     
    /* Insert New Mapping */
    INSERT INTO [dbo].[PermissionRights](PermissionID, RightID)    
    SELECT @P_PermissionID, Value from SplitString(@P_RightIDs)    
  
END
GO
