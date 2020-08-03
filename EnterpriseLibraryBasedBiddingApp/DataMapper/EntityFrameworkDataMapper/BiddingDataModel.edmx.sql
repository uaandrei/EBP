
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/20/2013 18:11:33
-- Generated from EDMX file: C:\Users\Andrei\Documents\Visual Studio 2012\Projects\EBP\EnterpriseLibraryBasedBiddingApp\DataMapper\EntityFrameworkDataMapper\BiddingDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BiddingTest];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProductCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_ProductCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductBid]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bids] DROP CONSTRAINT [FK_ProductBid];
GO
IF OBJECT_ID(N'[dbo].[FK_UserBid]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bids] DROP CONSTRAINT [FK_UserBid];
GO
IF OBJECT_ID(N'[dbo].[FK_CategorySubcategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Categories] DROP CONSTRAINT [FK_CategorySubcategory];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_UserProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserRatings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRatings] DROP CONSTRAINT [FK_UserUserRatings];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_UserRole];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Bids]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bids];
GO
IF OBJECT_ID(N'[dbo].[UserRatings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRatings];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO

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
    [BidCurrency] nvarchar(10)  NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [Description] nvarchar(500)  NOT NULL,
    [Available] bit  NOT NULL,
    [UserId] int  NOT NULL
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
    [Password] nvarchar(20)  NOT NULL,
    [BanEndDate] datetime  NULL,
    [RoleId] int  NOT NULL
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

-- Creating table 'UserRatings'
CREATE TABLE [dbo].[UserRatings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Rating] decimal(5,2)  NOT NULL,
    [Description] nvarchar(500)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [Description] nvarchar(500)  NOT NULL
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

-- Creating primary key on [Id] in table 'UserRatings'
ALTER TABLE [dbo].[UserRatings]
ADD CONSTRAINT [PK_UserRatings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
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

-- Creating foreign key on [UserId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_UserProduct]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProduct'
CREATE INDEX [IX_FK_UserProduct]
ON [dbo].[Products]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'UserRatings'
ALTER TABLE [dbo].[UserRatings]
ADD CONSTRAINT [FK_UserUserRatings]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserRatings'
CREATE INDEX [IX_FK_UserUserRatings]
ON [dbo].[UserRatings]
    ([UserId]);
GO

-- Creating foreign key on [RoleId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_RoleUser]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleUser'
CREATE INDEX [IX_FK_RoleUser]
ON [dbo].[Users]
    ([RoleId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------