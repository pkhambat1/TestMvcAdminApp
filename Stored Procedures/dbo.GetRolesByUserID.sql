SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================  
-- Author:  Pezanne Khambatta
-- Create date: 18th April 2019  
-- Description: Get Roles By User ID
-- EXEC GetRolesByUserID '15'
-- =============================================  
CREATE PROCEDURE [dbo].[GetRolesByUserID]

	@UserID   INT

AS
BEGIN  
    DECLARE @P_UserID		INT = @UserID

    SELECT      ID as UserID
				,FirstName
				,LastName
    FROM        dbo.UserDetails
    WHERE       ID = @P_UserID
         
    SELECT      A.ID,
				A.Name,
				A.Description,
				CASE WHEN C.ID IS NULL THEN 0 ELSE 1 END as IsAssigned  
    FROM        (SELECT ID, FirstName, LastName FROM UserDetails WHERE ID = @P_UserID) C  
    JOIN        UserRoles B  
    ON          C.ID = B.UserID
    RIGHT JOIN  Roles A  
    ON          B.RoleID = A.ID  
END

GO
