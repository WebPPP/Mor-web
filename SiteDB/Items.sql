CREATE TABLE [dbo].[Items]
(
	[ItemID] INT NOT NULL  IDENTITY, 
    [Name] NCHAR(50) NOT NULL, 
    [Description] TEXT NOT NULL, 
    [Price] INT NOT NULL, 
    [DepartmentID] INT NOT NULL, 
    [ImgPath] TEXT NOT NULL, 
    CONSTRAINT [FK_Items_Department] FOREIGN KEY ([DepartmentID]) REFERENCES [Department]([DepartmentID]), 
    CONSTRAINT [PK_Items] PRIMARY KEY ([ItemID])
)
