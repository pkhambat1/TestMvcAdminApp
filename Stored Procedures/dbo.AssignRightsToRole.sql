SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================    
-- Author:  Pezanne Khambatta    
-- Create date: 9th May 2019    
-- Description: Map Rights to Role
-- EXEC [dbo].[AssignRightsToRole] 3, '18, 19'  
-- =============================================    
CREATE PROCEDURE [dbo].[AssignRightsToRole]
    @RoleID			INT,    
    @RightIDs		NVARCHAR(300)    
AS    
BEGIN    
    DECLARE @P_RoleID			INT = @RoleID,  
			@P_RightIDs			NVARCHAR(300) = @RightIDs

    /* Remove existing mapping */
    DELETE [dbo].[RoleRights] WHERE @RoleID = @P_RoleID
     
    /* Insert New Mapping */
    INSERT INTO [dbo].[RoleRights](RoleID, RightID)    
    SELECT @P_RoleID, Value from SplitString(@P_RightIDs)    
  
END
GO
