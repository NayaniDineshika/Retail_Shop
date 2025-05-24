select * from ItemInvoices
select * from CustomerInvoices
select * from Products

DELETE FROM Products;


INSERT INTO Products (ProductName, UnitPrice) VALUES 
('Monitor', 10000),
('Keyboard' ,5000),
('Gaming Mouse', 2000),
('Printer', 9000),
('Mouse Pad', 1000);


Delete from Products where ProductId =6
Delete from ItemInvoices where ItemInvoiceId = 8
Delete from CustomerInvoices where InvoiceId =5

DBCC CHECKIDENT ('ItemInvoices', RESEED, 0);
