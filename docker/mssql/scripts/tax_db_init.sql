USE master
GO

IF NOT EXISTS
    (
        SELECT *
        FROM sys.databases
        WHERE name = 'TAX_DB'
    )
    BEGIN
        CREATE DATABASE TAX_DB
    END
GO

USE TAX_DB
GO

CREATE TABLE TaxConfiguration
(
    Id BIGINT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    PostalCode VARCHAR(4),
    TaxCalculationType VARCHAR(11),
    CreatedOn DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE TaxDetail
(
    Id BIGINT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    PostalCode VARCHAR(4),
    AnnualIncome DECIMAL(30, 13),
    TaxCalculationType VARCHAR(11),
    CalculatedTax DECIMAL(30, 13),
    CreatedOn DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [User]
(
    Id BIGINT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    Username VARCHAR(50),
    PasswordHash VARCHAR(500),
    CreatedOn DATETIME DEFAULT GETDATE()
);
GO