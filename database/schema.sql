CREATE DATABASE [7ElephantDb];
GO

USE [7ElephantDb];
GO

CREATE TABLE [dbo].[Phone]
(
    [ProductId] INT NOT NULL PRIMARY KEY IDENTITY,
    [Brand] VARCHAR(50) NULL,
    [Model] VARCHAR(50) NULL,
    [Price] MONEY NULL,
    [Cost] MONEY NULL,
    [Url] VARCHAR(MAX) NULL
);
GO

CREATE TABLE [dbo].[Order]
(
    [OrderId] INT NOT NULL PRIMARY KEY IDENTITY,
    [UserEmail] VARCHAR(100) NULL,
    [ProductId] INT NOT NULL,
    [Quantity] INT NULL,
    [Total] MONEY NULL,
    [Address] VARCHAR(MAX) NULL
);
GO
