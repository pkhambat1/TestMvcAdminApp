SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================  
-- Author:  Pezanne Khambatta
-- Create date: 18th April 2019  
-- Description: Get Permissions by RoleID
-- EXEC GetPermissionsByRoleID 1065
-- =============================================  
CREATE PROCEDURE [dbo].[GetPermissionsByRoleID]

	@RoleID   INT

AS
BEGIN  

    DECLARE @P_RoleID		INT = @RoleID

    SELECT      ID as RoleID
				,Name as RoleName
				,Description as RoleDescription
    FROM        dbo.Roles
    WHERE       ID = @P_RoleID
         
    SELECT      A.ID as ID
				,A.Name as Name
				,A.Description as Description
				,CASE WHEN C.ID IS NULL THEN 0 ELSE 1 END as IsAssigned  
    FROM        (SELECT ID, Name, Description FROM Roles WHERE ID = @P_RoleID) C  
    JOIN        RolePermissions B  
    ON          C.ID = B.RoleID
    RIGHT JOIN	Permissions A  
    ON          B.PermissionID = A.ID  

END
GO
