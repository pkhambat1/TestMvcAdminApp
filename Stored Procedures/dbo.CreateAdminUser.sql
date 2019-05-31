SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================  
-- Author:  Pezanne Khambatta  
-- Create date: 6th April 2019  
-- Description: Create User  
-- EXEC CreateUser 'Pez', 'Khambatta', 'pez@email.com', 'pkhambat', 'pez@1234'  
-- =============================================  
CREATE PROCEDURE [dbo].[CreateAdminUser]
    @FirstName			NVARCHAR(50),
    @LastName			NVARCHAR(50),
	@Email				NVARCHAR(50),
	@Username			NVARCHAR(50),
	@Password			NVARCHAR(50)  
AS  
BEGIN  
    DECLARE @P_FirstName		NVARCHAR(256) = @FirstName,
			@P_LastName			NVARCHAR(256) = @LastName,
			@P_Email			NVARCHAR(256) = @Email,
			@P_Username			NVARCHAR(256) = @Username,
			@P_Password			NVARCHAR(256) = @Password
       
    INSERT INTO dbo.AdminUsers (FirstName, LastName, Email, Username, Password)  
    VALUES(@P_FirstName, @P_LastName, @P_Email, @P_Username, @P_Password)
  
END
GO
