
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/05/2013 17:45:12
-- Generated from EDMX file: C:\Users\Andrei\Documents\Visual Studio 2012\Projects\EBP\EnterpriseLibraryBasedBiddingApp\DataMapper\EntityFrameworkDataMapper\BiddingDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Bidding];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CategoryId] int  NOT NULL,
    [BidStartDate] datetime  NOT NULL,
    [BidEndDate] datetime  NOT NULL,
    [StartingPrice] decimal(18,0)  NOT NULL,
    [BidCurrency] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [ParentCategory_Id] int  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [Password] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'Bids'
CREATE TABLE [dbo].[Bids] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [Sum] decimal(18,0)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Bids'
ALTER TABLE [dbo].[Bids]
ADD CONSTRAINT [PK_Bids]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_ProductCategory]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCategory'
CREATE INDEX [IX_FK_ProductCategory]
ON [dbo].[Products]
    ([CategoryId]);
GO

-- Creating foreign key on [ProductId] in table 'Bids'
ALTER TABLE [dbo].[Bids]
ADD CONSTRAINT [FK_ProductBid]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductBid'
CREATE INDEX [IX_FK_ProductBid]
ON [dbo].[Bids]
    ([ProductId]);
GO

-- Creating foreign key on [UserId] in table 'Bids'
ALTER TABLE [dbo].[Bids]
ADD CONSTRAINT [FK_UserBid]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserBid'
CREATE INDEX [IX_FK_UserBid]
ON [dbo].[Bids]
    ([UserId]);
GO

-- Creating foreign key on [ParentCategory_Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [FK_CategorySubcategory]
    FOREIGN KEY ([ParentCategory_Id])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategorySubcategory'
CREATE INDEX [IX_FK_CategorySubcategory]
ON [dbo].[Categories]
    ([ParentCategory_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------