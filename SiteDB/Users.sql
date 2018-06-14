CREATE TABLE [dbo].[Users]
(
	[UserID] INT NOT NULL IDENTITY,
    [UserName] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [Name] VARCHAR(50) NOT NULL, 
    [Email] VARCHAR(50) NOT NULL, 
    [Address] VARCHAR(50) NOT NULL, 
    [RoleID] INT NOT NULL DEFAULT 5, 
    CONSTRAINT [FK_Users_Roles] FOREIGN KEY ([RoleID]) REFERENCES [Roles]([RoleID]), 
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserID])
)
