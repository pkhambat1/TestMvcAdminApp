SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Pezanne Khambatta
-- Create date: 15th May, 2019
-- Description:	Delete permission by permission
-- EXEC DeletePermission '1076'
-- =============================================
CREATE PROCEDURE [dbo].[DeletePermission]
	@PermissionID		INT
AS
	DECLARE @P_PermissionID		INT = @PermissionID
BEGIN
	DELETE FROM dbo.Permissions WHERE ID = @P_PermissionID
END

GO
