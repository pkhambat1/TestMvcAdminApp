SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================  
-- Author:  Pezanne Khambatta
-- Create date: 23rd April 2019  
-- Description: Get Identity UserName by UserID
-- EXEC GetUserNameByUserID 15
-- =============================================  
CREATE PROCEDURE [dbo].[GetUserNameByUserID]

	@UserID   INT

AS
BEGIN  
	DECLARE @P_UserID		INT = @UserID
	SELECT UserName FROM dbo.AspNetUsers WHERE UserID = @P_UserID
END

GO
