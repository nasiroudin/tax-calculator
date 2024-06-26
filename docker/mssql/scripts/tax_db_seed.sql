USE [TAX_DB]
GO

INSERT INTO TaxConfiguration (PostalCode, TaxCalculationType)
VALUES
    ('7441', 'Progressive'),
    ('A100', 'FlatValue'),
    ('7000', 'FlatRate'),
    ('1000', 'Progressive');
GO

INSERT INTO [dbo].[TaxDetail] ([PostalCode],[AnnualIncome],[TaxCalculationType],[CalculatedTax],[CreatedOn]) 
VALUES 
    ('1000',56897.0000000000000,'Progressive',10411.7500000000000,'2023-12-09 14:25:44.370'),
    ('1000',56897.0000000000000,'Progressive',10411.7500000000000,'2023-12-09 14:26:16.943'),
    ('7441',56897.0000000000000,'Progressive',10411.7500000000000,'2023-12-09 14:26:23.173'),
    ('1000',56897.0000000000000,'Progressive',10411.7500000000000,'2023-12-09 14:26:29.200'),
    ('1000',552.0000000000000,'Progressive',55.2000000000000,'2023-12-09 14:29:16.410'),
    ('7000',552.0000000000000,'FlatRate',96.6000000000000,'2023-12-09 14:29:22.910'),
    ('7000',552.0000000000000,'FlatRate',96.6000000000000,'2023-12-09 14:29:31.760'),
    ('7000',552.0000000000000,'FlatRate',96.6000000000000,'2023-12-09 14:29:34.170'),
    ('7000',552.0000000000000,'FlatRate',96.6000000000000,'2023-12-09 14:29:36.393'),
    ('7000',5599.0000000000000,'FlatRate',979.8250000000000,'2023-12-09 14:29:41.773'),
    ('1000',25897.0000000000000,'Progressive',3467.0500000000000,'2023-12-09 20:35:57.333');
GO


INSERT INTO [dbo].[User] ([Username],[PasswordHash],[CreatedOn]) 
VALUES 
    ('admin','$2a$12$aJvEIjSPtNSCc7LgPmRr5ekiNbbU/nIVwRPs/9SNuYws/LnkK7fHa','2023-12-09 18:00:43.980');
GO