select * from ItemInvoices
select * from CustomerInvoices
select * from Products

insert into Products values('Monitor', 400)

Delete from Products where ProductId =6
Delete from ItemInvoices where ItemInvoiceId = 8
Delete from CustomerInvoices where InvoiceId =5

DBCC CHECKIDENT ('ItemInvoices', RESEED, 0);
