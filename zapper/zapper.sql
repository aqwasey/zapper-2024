CREATE TABLE BusinessGroups (
    BusinessGroupId INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    BusinessSector VARCHAR(20) NOT NULL, -- NPO, Pty, Corporation --
    Industry VARCHAR(40) NOT NULL, -- Insurance, Finance, Logistics, Retail, etc --
    Description VARCHAR(100) NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastModifiedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Customers (
    CustomerId BIGINT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(20) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    BirthDate DATE() NULL,
    Gender VARCHAR(6) NULL, -- Male, Female --
    Email VARCHAR(100) UNIQUE NOT NULL,
    PhoneNumber VARCHAR(15),
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastModifiedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Merchants (
    MerchantId BIGINT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    BusinessGroupId INT REFERENCES BusinessGroups(BusinessGroupId),
    Email VARCHAR(100) UNIQUE NOT NULL,
    Address VARCHAR(255),
    City VARCHAR(50),
    State VARCHAR(50),
    ZipCode VARCHAR(10),
    Country VARCHAR(50),
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastModifiedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Transactions (
    TransactionId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    CustomerId BIGINT REFERENCES Customers(CustomerId),
    MerchantId BIGINT REFERENCES Merchants(MerchantId),
    PaymentMethodId INT REFERENCES PaymentMethods(PaymentMethodId),
    Amount DECIMAL(10,2) NOT NULL,
    Currency VARCHAR(3) DEFAULT 'USD',
    TransactionDate DATETIME DEFAULT GETDATE(),
    Status VARCHAR(20) CHECK (Status IN ('Pending', 'Completed', 'Failed')),
    Description VARCHAR(255),
    ReferenceNumber VARCHAR(50),
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastModifiedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE TransactionLog (
    LogId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    TransactionId UNIQUEIDENTIFIER REFERENCES Transactions(TransactionId) ON DELETE CASCADE,
    Action VARCHAR(50), -- e.g., 'Created', 'Updated', 'Cancelled'
    ItemId VARCHAR(100),
    Details VARCHAR(255),
    Timestamp DATETIME DEFAULT GETDATE()
);

CREATE TABLE PaymentOptions (
    PaymentOptionId INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(50) NOT NULL,
    Description VARCHAR(255) NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastModifiedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE PaymentMethods (
    PaymentMethodId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId BIGINT REFERENCES Customers(CustomerId),
    PaymentOptionId INT REFERENCES PaymentOptions(PaymentOptionId),
    Provider VARCHAR(50),
    AccountIdentifier VARCHAR(100),
    IsDefault BIT DEFAULT 0,
    LastUsedDate DATETIME,
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastModifiedDate DATETIME DEFAULT GETDATE()
);
