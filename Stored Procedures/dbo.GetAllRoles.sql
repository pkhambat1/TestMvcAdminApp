SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================  
-- Author:  Pezanne Khambatta 
-- Create date: 18th April 2019  
-- Description: Get All Roles 
-- EXEC [dbo].[GetAllRoles]
-- =============================================  
CREATE PROCEDURE [dbo].[GetAllRoles]
AS  
BEGIN  

	SELECT		A.ID 
				,A.Name
				,A.Description
				,Count(D.UserID) as UsersCount
				,C.ID as PermissionID
				,C.Name as PermissionName
				,C.Description as PermissionDescription
	FROM		dbo.Roles A  
	LEFT JOIN	dbo.RolePermissions B
	ON			B.RoleID = A.ID
	LEFT JOIN	dbo.Permissions C
	ON			C.ID = B.PermissionID
	LEFT JOIN	dbo.UserRoles D
	ON			D.RoleID = A.ID
	GROUP BY	A.ID, A.Name, A.Description, C.ID, C.Name, C.Description 
	ORDER BY	A.Name ASC

END

GO
