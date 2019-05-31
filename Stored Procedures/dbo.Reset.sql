SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Pezanne Khambatta
-- Create date: 13th May, 2019
-- Description:	Reset Roles, UserRoles, AspNetUserRoles, and AspNetRoleClaims
-- EXEC Reset 
-- =============================================
CREATE PROCEDURE [dbo].[Reset]
AS
BEGIN
	DELETE FROM dbo.Roles
	DELETE FROM dbo.UserRoles
	DELETE FROM dbo.AspNetRoles
	DELETE FROM dbo.AspNetRoleClaims
END

GO
