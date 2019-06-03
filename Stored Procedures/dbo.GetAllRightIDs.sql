SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================  
-- Author:  Pezanne Khambatta 
-- Create date: 10th May 2019  
-- Description: Get List of All RightIDs
-- EXEC GetAllRightIDs
-- =============================================  
CREATE PROCEDURE [dbo].[GetAllRightIDs]
AS  
BEGIN  
	SELECT ID from dbo.Rights
END

GO
