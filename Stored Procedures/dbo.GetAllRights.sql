SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================  
-- Author:  Pezanne Khambatta 
-- Create date: 18th April 2019  
-- Description: Get All Rights 
-- EXEC [dbo].[GetAllRights]
-- =============================================  
CREATE PROCEDURE [dbo].[GetAllRights]
AS  
BEGIN  
			SELECT ID
			,Name
			,Description
FROM		Rights
ORDER BY	Name ASC
END

GO
