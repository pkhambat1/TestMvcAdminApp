SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================  
-- Author:  Pezanne Khambatta 
-- Create date: 18th April 2019  
-- Description: Get All Permissions 
-- EXEC GetAllPermissions
-- =============================================  
CREATE PROCEDURE [dbo].[GetAllPermissions]
AS  
BEGIN  
	SELECT		A.ID as ID 
				,A.Name
				,A.Description
				,Count(D.RoleID) as RolesCount
				,C.ID as RightID
				,C.Name as RightName
				,C.Description as RightDescription
	FROM		dbo.Permissions A  
	LEFT JOIN	dbo.PermissionRights B
	ON			B.PermissionID = A.ID
	LEFT JOIN	dbo.Rights C
	ON			C.ID = B.RightID
	LEFT JOIN	dbo.RolePermissions D
	ON			D.PermissionID = A.ID
	GROUP BY	A.ID, A.Name, A.Description, C.ID, C.Name, C.Description
END
GO
