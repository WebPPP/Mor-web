CREATE TABLE [dbo].[Orders]
(
	[OrderID] INT NOT NULL  IDENTITY, 
    [UserID] INT NOT NULL, 
    [DateTime] DATETIME NOT NULL,
	CONSTRAINT [FK_Orders_Users] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID]), 
    CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderID])
)
