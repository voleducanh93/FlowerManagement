CREATE DATABASE FlowerDB;
GO

USE FlowerDB;
GO

CREATE TABLE Flowers (
    FlowerId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Color NVARCHAR(50),
    Type NVARCHAR(50),
    Price DECIMAL(18, 2),
    StockQuantity INT
);
INSERT INTO Flowers (Name, Color, Type, Price, StockQuantity)
VALUES 
('Rose', 'Red', 'Flower', 10.00, 100),
('Tulip', 'Yellow', 'Flower', 8.50, 50),
('Lily', 'White', 'Flower', 12.00, 30),
('Sunflower', 'Yellow', 'Flower', 5.00, 200),
('Daisy', 'White', 'Flower', 7.00, 80);
