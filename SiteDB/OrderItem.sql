CREATE TABLE [dbo].[OrderItem]
(
	[OrderID] INT NOT NULL , 
    [ItemID] INT NOT NULL, 
    [Amount] INT NOT NULL, 
    [PricePerItem] INT NOT NULL, 
    CONSTRAINT [FK_OrderItem_Items] FOREIGN KEY ([ItemID]) REFERENCES [Items]([ItemID]),
	CONSTRAINT [FK_OrderItem_Orders] FOREIGN KEY ([OrderID]) REFERENCES [Orders]([OrderID]), 
    CONSTRAINT [PK_OrderItem] PRIMARY KEY ([OrderID]),
)
