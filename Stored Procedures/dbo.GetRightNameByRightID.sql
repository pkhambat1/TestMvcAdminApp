SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================  
-- Author:  Pezanne Khambatta
-- Create date: 10th May 2019  
-- Description: Get Right Name by RightID
-- EXEC GetRightNameByRightID 1006
-- =============================================  
CREATE PROCEDURE [dbo].[GetRightNameByRightID]

	@RightID		INT

AS
BEGIN  
	DECLARE @P_RightID		INT = @RightID
	SELECT Name FROM dbo.Rights
	WHERE ID = @P_RightID
END

GO
