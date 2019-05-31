SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================  
-- Author:  Pezanne Khambatta
-- Create date: 18th April 2019  
-- Description: Get Rights by PermissionID
-- EXEC GetRightsByPermissionID '3'
-- =============================================  
CREATE PROCEDURE [dbo].[GetRightsByPermissionID]

	@PermissionID   INT

AS
BEGIN  
    DECLARE @P_PermissionID		INT = @PermissionID

    SELECT      ID as PermissionID
				,Name as PermissionName
				,Description as PermissionDescription
    FROM        dbo.Permissions
    WHERE       ID = @P_PermissionID
         
    SELECT      A.ID as ID
				,A.Name as Name
				,A.Description as Description
				,CASE WHEN C.ID IS NULL THEN 0 ELSE 1 END as IsAssigned  
    FROM        (SELECT ID, Name, Description FROM Permissions WHERE ID = @P_PermissionID) C  
    JOIN        PermissionRights B  
    ON          C.ID = B.PermissionID
    RIGHT JOIN  Rights A  
    ON          B.RightID = A.ID  
END

GO
