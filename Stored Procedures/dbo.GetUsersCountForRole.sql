SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================  
-- Author:  Pezanne Khambatta 
-- Create date: 15th May 2019  
-- Description: Get number of users with given Role
-- EXEC GetUsersCountForRole 1082
-- =============================================  
CREATE PROCEDURE [dbo].[GetUsersCountForRole]
	@RoleID			INT
AS
BEGIN  
	DECLARE @P_RoleID				INT = @RoleID

	SELECT		Count(UserID) as UsersCount
	FROM		dbo.UserRoles
	WHERE		RoleID = @P_RoleID
END
GO
