CREATE TABLE [dbo].[Department]
(
	[DepartmentID] INT NOT NULL  IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Department] PRIMARY KEY ([DepartmentID])
)
