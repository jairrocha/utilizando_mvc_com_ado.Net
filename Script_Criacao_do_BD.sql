IF OBJECT_ID('dbo.Employee', 'U') IS NOT NULL
DROP TABLE dbo.Employee
GO
CREATE TABLE [dbo].[Employee](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] VARCHAR(50) NULL,
	[Position] VARCHAR(50) NULL,
    [Office] VARCHAR(50) NULL,
	[Age] INT NULL,
	[Salary] INT NULL,
  CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED
  (
	[Id] ASC
  )
)

GO

/*TO CREATE EMPLOYEE*/

IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'spAddNew'
)
DROP PROCEDURE dbo.spAddNew
GO
CREATE PROCEDURE dbo.spAddNew

    @Name NVARCHAR(50), 
    @Position NVARCHAR(50),
    @Office NVARCHAR(50),
    @Age INT,
    @Salary INT 
AS
BEGIN
    INSERT INTO dbo.Employee VALUES 
    (
        @Name,
        @Position,
        @Office,
        @Age,
        @Salary
    )
END

GO

/*TO UPDATE EMPLOYEE*/

IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'spUdateEmployee'
)
DROP PROCEDURE dbo.spUdateEmployee
GO
CREATE PROCEDURE dbo.spUdateEmployee
    @Id INT,
    @Name NVARCHAR(50), 
    @Position NVARCHAR(50),
    @Office NVARCHAR(50),
    @Age INT,
    @Salary INT 
AS
BEGIN
    UPDATE Employee SET
        [Name] = @Name,
        [Position] = @Position,
        [Office] = @Office,
        [Age] = @Age,
        [Salary] = @Salary
    WHERE [Id] = @Id    
END

GO

/*GET EMPLOYEE*/

IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'spGetEmployee'
)
DROP PROCEDURE dbo.spGetEmployee
GO
CREATE PROCEDURE dbo.spGetEmployee
    @Id INT = NULL
AS
BEGIN
    IF (@Id IS NULL)
        SELECT * FROM dbo.Employee
    ELSE
        SELECT * FROM dbo.Employee WHERE Id = @Id
END

GO

IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'spDeleteEmployee'
)
DROP PROCEDURE dbo.spDeleteEmployee
GO

-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.spDeleteEmployee
    @Id INT
AS
BEGIN
    DELETE FROM dbo.Employee WHERE Id = @Id;
END
