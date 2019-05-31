SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================  
-- Author:  Pezanne Khambatta
-- Create date: 9th May 2019  
-- Description: Get All Rights (no repeats) with PermissionIDs = @PermissionIDs
-- EXEC GetRightsHavingPermissions '1027,1028,1029,1030'
-- =============================================  
CREATE PROCEDURE [dbo].[GetRightsHavingPermissions]

	@PermissionIDs   NVARCHAR(300)

AS
BEGIN  

    DECLARE		@P_PermissionIDs	NVARCHAR(300) = @PermissionIDs

	SELECT DISTINCT		A.ID
						,A.Name
						,A.Description
						--,B.PermissionID
	FROM				dbo.Rights A
	JOIN				dbo.PermissionRights B
	ON					B.RightID = A.ID
	WHERE				B.PermissionID IN (SELECT VALUE FROM SplitString(@P_PermissionIDs))

END

GO
