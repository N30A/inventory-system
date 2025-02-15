USE master;

IF DB_ID('InventoryDB') IS NULL
BEGIN
	CREATE DATABASE InventoryDB;
END

GO
USE InventoryDB;

IF NOT EXISTS (SELECT name FROM sys.tables WHERE name = 'Category')
BEGIN
	CREATE TABLE Category (
		CategoryID INT IDENTITY PRIMARY KEY,
		Name NVARCHAR(100) NOT NULL UNIQUE,
		Description NVARCHAR(250),
		ParentCategoryID INT NULL REFERENCES Category(CategoryID)
	);
END

IF NOT EXISTS (SELECT name FROM sys.tables WHERE name = 'Item')
BEGIN
	CREATE TABLE Item (
		ItemID INT IDENTITY PRIMARY KEY,
		Name NVARCHAR(100) NOT NULL UNIQUE,
		Description NVARCHAR(500),
		SalePrice MONEY NOT NULL CHECK (SalePrice >= 0),
		DeletedAt DATETIME NULL,
		CategoryID INT NULL REFERENCES Category(CategoryID) ON DELETE SET NULL
	);
END

IF NOT EXISTS (SELECT name FROM sys.tables WHERE name = 'Supplier')
BEGIN
	CREATE TABLE Supplier (
		SupplierID INT IDENTITY PRIMARY KEY,
		Name NVARCHAR(100) UNIQUE NOT NULL,
		Email VARCHAR(100) NULL,
		Phone VARCHAR(25) NULL,
		Address NVARCHAR(100) NULL,
		DeletedAt DATETIME NULL
	);
END

IF NOT EXISTS (SELECT name FROM sys.tables WHERE name = 'ItemSupplier')
BEGIN
	CREATE TABLE ItemSupplier (
		ItemSupplierID INT IDENTITY PRIMARY KEY,
		ItemID INT NOT NULL REFERENCES Item(ItemID) ON DELETE CASCADE,
		SupplierID INT NOT NULL REFERENCES Supplier(SupplierID) ON DELETE CASCADE,
		PurchasePrice MONEY NOT NULL CHECK (PurchasePrice >= 0),
		Quantity INT NOT NULL CHECK (Quantity >= 0),
		Date DATE NOT NULL
	);
END

IF NOT EXISTS (SELECT name FROM sys.tables WHERE name = 'Warehouse')
BEGIN
	CREATE TABLE Warehouse (
		WarehouseID INT IDENTITY PRIMARY KEY,
		Name VARCHAR(100) NOT NULL UNIQUE,
		Address NVARCHAR(100) NOT NULL,
		DeletedAt DATETIME NULL
	);
END

IF NOT EXISTS (SELECT name FROM sys.tables WHERE name = 'ItemWarehouse')
BEGIN
	CREATE TABLE ItemWarehouse (
		ItemID INT,
		WarehouseID INT,
		Quantity INT NOT NULL CHECK (Quantity >= 0),
		InventoryDate DATE NULL,

		PRIMARY KEY (ItemID, WarehouseID),
		FOREIGN KEY (ItemID) REFERENCES Item(ItemID) ON DELETE CASCADE,
		FOREIGN KEY (WarehouseID) REFERENCES Warehouse(WarehouseID) ON DELETE CASCADE
	);
END
