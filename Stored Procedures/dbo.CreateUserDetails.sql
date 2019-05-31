SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Pezanne Khambatta
-- Create date: 12 April, 2019
-- Description:	Create User Details 
-- =============================================
CREATE PROCEDURE [dbo].[CreateUserDetails]
	@FirstName			NVARCHAR(50),
    @LastName			NVARCHAR(50),
	@Mobile				NVARCHAR(50),
	@CompanyName		NVARCHAR(50)

AS
BEGIN  
    DECLARE @P_FirstName		NVARCHAR(256) = @FirstName,
			@P_LastName			NVARCHAR(256) = @LastName,
			@P_Mobile			NVARCHAR(256) = @Mobile,
			@P_CompanyName		NVARCHAR(256) = @CompanyName
       
    INSERT INTO dbo.UserDetails(FirstName, LastName, Mobile, CompanyName)  
    VALUES(@P_FirstName, @P_LastName, @P_Mobile, @P_CompanyName)
	SELECT SCOPE_IDENTITY() AS UserDetailsID  
END
GO
