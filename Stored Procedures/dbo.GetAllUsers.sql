SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================  
-- Author:  Pezanne Khambatta (Gaurav Bhavsar)
-- Create date: 16th April 2019  
-- Description: Get All Users
-- EXEC [dbo].[GetAllUsers]
-- =============================================  
CREATE PROCEDURE [dbo].[GetAllUsers]
AS  
BEGIN  

	SELECT		A.ID as ID 
				,A.FirstName
				,A.LastName
				,A.CompanyName
				,A.Mobile  
				,C.ID as RoleID
				,C.Name as RoleName
				,C.Description as RoleDescription
	FROM		dbo.UserDetails A  
	LEFT JOIN	dbo.UserRoles B
	ON			B.UserID = A.ID
	LEFT JOIN	dbo.Roles C
	ON			C.ID = B.RoleID
	ORDER BY	A.FirstName ASC
	
END

GO
