
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/06/2017 19:45:22
-- Generated from EDMX file: C:\Users\peter\Desktop\VVS\Jaws\DAL\Data.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Datenbank];
GO
IF SCHEMA_ID(N'Jaws-DB') IS NULL EXECUTE(N'CREATE SCHEMA [Jaws-DB]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[Jaws-DB].[FK_PersonalArbeitsvertrag]', 'F') IS NOT NULL
    ALTER TABLE [Jaws-DB].[PersonalSet] DROP CONSTRAINT [FK_PersonalArbeitsvertrag];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[Jaws-DB].[MarktSet]', 'U') IS NOT NULL
    DROP TABLE [Jaws-DB].[MarktSet];
GO
IF OBJECT_ID(N'[Jaws-DB].[PersonalSet]', 'U') IS NOT NULL
    DROP TABLE [Jaws-DB].[PersonalSet];
GO
IF OBJECT_ID(N'[Jaws-DB].[ArbeitsvertragSet]', 'U') IS NOT NULL
    DROP TABLE [Jaws-DB].[ArbeitsvertragSet];
GO
IF OBJECT_ID(N'[Jaws-DB].[SchichtSet]', 'U') IS NOT NULL
    DROP TABLE [Jaws-DB].[SchichtSet];
GO
IF OBJECT_ID(N'[Jaws-DB].[LieferantSet]', 'U') IS NOT NULL
    DROP TABLE [Jaws-DB].[LieferantSet];
GO
IF OBJECT_ID(N'[Jaws-DB].[ArtikelSet]', 'U') IS NOT NULL
    DROP TABLE [Jaws-DB].[ArtikelSet];
GO
IF OBJECT_ID(N'[Jaws-DB].[PrognoseSet]', 'U') IS NOT NULL
    DROP TABLE [Jaws-DB].[PrognoseSet];
GO
IF OBJECT_ID(N'[Jaws-DB].[WarengruppeSet]', 'U') IS NOT NULL
    DROP TABLE [Jaws-DB].[WarengruppeSet];
GO
IF OBJECT_ID(N'[Jaws-DB].[BelegSet]', 'U') IS NOT NULL
    DROP TABLE [Jaws-DB].[BelegSet];
GO
IF OBJECT_ID(N'[Jaws-DB].[LieferartSet]', 'U') IS NOT NULL
    DROP TABLE [Jaws-DB].[LieferartSet];
GO
IF OBJECT_ID(N'[Jaws-DB].[RolleSet]', 'U') IS NOT NULL
    DROP TABLE [Jaws-DB].[RolleSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'MarktSet'
CREATE TABLE [Jaws-DB].[MarktSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Wert] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PersonalSet'
CREATE TABLE [Jaws-DB].[PersonalSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Vorname] nvarchar(max)  NOT NULL,
    [Straße] nvarchar(max)  NOT NULL,
    [Hausnummer] nvarchar(max)  NOT NULL,
    [Postleitzahl] nvarchar(max)  NOT NULL,
    [Ort] nvarchar(max)  NOT NULL,
    [IBAN] nvarchar(max)  NOT NULL,
    [BIC] nvarchar(max)  NOT NULL,
    [Steuerklasse] nvarchar(max)  NOT NULL,
    [Telefon] nvarchar(max)  NOT NULL,
    [Mobil] nvarchar(max)  NOT NULL,
    [ArbeitsvertragId] int  NOT NULL,
    [ArbeitsvertragId1] int  NOT NULL
);
GO

-- Creating table 'ArbeitsvertragSet'
CREATE TABLE [Jaws-DB].[ArbeitsvertragSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Wochenstunden] nvarchar(max)  NOT NULL,
    [Stundenlohn] nvarchar(max)  NOT NULL,
    [PersonalId] int  NOT NULL
);
GO

-- Creating table 'SchichtSet'
CREATE TABLE [Jaws-DB].[SchichtSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Startzeit_soll] nvarchar(max)  NOT NULL,
    [Endzeit_soll] nvarchar(max)  NOT NULL,
    [Startzeit_ist] nvarchar(max)  NOT NULL,
    [Endzeit_ist] nvarchar(max)  NOT NULL,
    [Pause] nvarchar(max)  NOT NULL,
    [PersonalId] int  NOT NULL
);
GO

-- Creating table 'LieferantSet'
CREATE TABLE [Jaws-DB].[LieferantSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Hausnummer] nvarchar(max)  NOT NULL,
    [Postleitzahl] nvarchar(max)  NOT NULL,
    [Ort] nvarchar(max)  NOT NULL,
    [Telefon] nvarchar(max)  NOT NULL,
    [Mobil] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [IBAN] nvarchar(max)  NOT NULL,
    [BIC] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ArtikelSet'
CREATE TABLE [Jaws-DB].[ArtikelSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [GTIN] nvarchar(max)  NOT NULL,
    [Bestand] nvarchar(max)  NOT NULL,
    [Nettoverkaufspreis] nvarchar(max)  NOT NULL,
    [LieferantId] int  NOT NULL,
    [WarengruppeId] int  NOT NULL
);
GO

-- Creating table 'PrognoseSet'
CREATE TABLE [Jaws-DB].[PrognoseSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Datum] nvarchar(max)  NOT NULL,
    [Abverkauf_soll] nvarchar(max)  NOT NULL,
    [Abverkauf_ist] nvarchar(max)  NOT NULL,
    [ArtikelId] int  NOT NULL
);
GO

-- Creating table 'WarengruppeSet'
CREATE TABLE [Jaws-DB].[WarengruppeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Steuersatz] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'BelegSet'
CREATE TABLE [Jaws-DB].[BelegSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LieferartId] int  NOT NULL
);
GO

-- Creating table 'LieferartSet'
CREATE TABLE [Jaws-DB].[LieferartSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RolleSet'
CREATE TABLE [Jaws-DB].[RolleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PersonalRolle'
CREATE TABLE [Jaws-DB].[PersonalRolle] (
    [Personal_Id] int  NOT NULL,
    [Rolle_Id] int  NOT NULL
);
GO

-- Creating table 'BelegArtikel'
CREATE TABLE [Jaws-DB].[BelegArtikel] (
    [Beleg_Id] int  NOT NULL,
    [Artikel_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'MarktSet'
ALTER TABLE [Jaws-DB].[MarktSet]
ADD CONSTRAINT [PK_MarktSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonalSet'
ALTER TABLE [Jaws-DB].[PersonalSet]
ADD CONSTRAINT [PK_PersonalSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ArbeitsvertragSet'
ALTER TABLE [Jaws-DB].[ArbeitsvertragSet]
ADD CONSTRAINT [PK_ArbeitsvertragSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SchichtSet'
ALTER TABLE [Jaws-DB].[SchichtSet]
ADD CONSTRAINT [PK_SchichtSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LieferantSet'
ALTER TABLE [Jaws-DB].[LieferantSet]
ADD CONSTRAINT [PK_LieferantSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ArtikelSet'
ALTER TABLE [Jaws-DB].[ArtikelSet]
ADD CONSTRAINT [PK_ArtikelSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PrognoseSet'
ALTER TABLE [Jaws-DB].[PrognoseSet]
ADD CONSTRAINT [PK_PrognoseSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WarengruppeSet'
ALTER TABLE [Jaws-DB].[WarengruppeSet]
ADD CONSTRAINT [PK_WarengruppeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BelegSet'
ALTER TABLE [Jaws-DB].[BelegSet]
ADD CONSTRAINT [PK_BelegSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LieferartSet'
ALTER TABLE [Jaws-DB].[LieferartSet]
ADD CONSTRAINT [PK_LieferartSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RolleSet'
ALTER TABLE [Jaws-DB].[RolleSet]
ADD CONSTRAINT [PK_RolleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Personal_Id], [Rolle_Id] in table 'PersonalRolle'
ALTER TABLE [Jaws-DB].[PersonalRolle]
ADD CONSTRAINT [PK_PersonalRolle]
    PRIMARY KEY CLUSTERED ([Personal_Id], [Rolle_Id] ASC);
GO

-- Creating primary key on [Beleg_Id], [Artikel_Id] in table 'BelegArtikel'
ALTER TABLE [Jaws-DB].[BelegArtikel]
ADD CONSTRAINT [PK_BelegArtikel]
    PRIMARY KEY CLUSTERED ([Beleg_Id], [Artikel_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ArbeitsvertragId1] in table 'PersonalSet'
ALTER TABLE [Jaws-DB].[PersonalSet]
ADD CONSTRAINT [FK_PersonalArbeitsvertrag]
    FOREIGN KEY ([ArbeitsvertragId1])
    REFERENCES [Jaws-DB].[ArbeitsvertragSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonalArbeitsvertrag'
CREATE INDEX [IX_FK_PersonalArbeitsvertrag]
ON [Jaws-DB].[PersonalSet]
    ([ArbeitsvertragId1]);
GO

-- Creating foreign key on [Personal_Id] in table 'PersonalRolle'
ALTER TABLE [Jaws-DB].[PersonalRolle]
ADD CONSTRAINT [FK_PersonalRolle_Personal]
    FOREIGN KEY ([Personal_Id])
    REFERENCES [Jaws-DB].[PersonalSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Rolle_Id] in table 'PersonalRolle'
ALTER TABLE [Jaws-DB].[PersonalRolle]
ADD CONSTRAINT [FK_PersonalRolle_Rolle]
    FOREIGN KEY ([Rolle_Id])
    REFERENCES [Jaws-DB].[RolleSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonalRolle_Rolle'
CREATE INDEX [IX_FK_PersonalRolle_Rolle]
ON [Jaws-DB].[PersonalRolle]
    ([Rolle_Id]);
GO

-- Creating foreign key on [PersonalId] in table 'SchichtSet'
ALTER TABLE [Jaws-DB].[SchichtSet]
ADD CONSTRAINT [FK_PersonalSchicht]
    FOREIGN KEY ([PersonalId])
    REFERENCES [Jaws-DB].[PersonalSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonalSchicht'
CREATE INDEX [IX_FK_PersonalSchicht]
ON [Jaws-DB].[SchichtSet]
    ([PersonalId]);
GO

-- Creating foreign key on [LieferantId] in table 'ArtikelSet'
ALTER TABLE [Jaws-DB].[ArtikelSet]
ADD CONSTRAINT [FK_LieferantArtikel]
    FOREIGN KEY ([LieferantId])
    REFERENCES [Jaws-DB].[LieferantSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LieferantArtikel'
CREATE INDEX [IX_FK_LieferantArtikel]
ON [Jaws-DB].[ArtikelSet]
    ([LieferantId]);
GO

-- Creating foreign key on [WarengruppeId] in table 'ArtikelSet'
ALTER TABLE [Jaws-DB].[ArtikelSet]
ADD CONSTRAINT [FK_ArtikelWarengruppe]
    FOREIGN KEY ([WarengruppeId])
    REFERENCES [Jaws-DB].[WarengruppeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ArtikelWarengruppe'
CREATE INDEX [IX_FK_ArtikelWarengruppe]
ON [Jaws-DB].[ArtikelSet]
    ([WarengruppeId]);
GO

-- Creating foreign key on [LieferartId] in table 'BelegSet'
ALTER TABLE [Jaws-DB].[BelegSet]
ADD CONSTRAINT [FK_BelegLieferart]
    FOREIGN KEY ([LieferartId])
    REFERENCES [Jaws-DB].[LieferartSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BelegLieferart'
CREATE INDEX [IX_FK_BelegLieferart]
ON [Jaws-DB].[BelegSet]
    ([LieferartId]);
GO

-- Creating foreign key on [ArtikelId] in table 'PrognoseSet'
ALTER TABLE [Jaws-DB].[PrognoseSet]
ADD CONSTRAINT [FK_PrognoseArtikel]
    FOREIGN KEY ([ArtikelId])
    REFERENCES [Jaws-DB].[ArtikelSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PrognoseArtikel'
CREATE INDEX [IX_FK_PrognoseArtikel]
ON [Jaws-DB].[PrognoseSet]
    ([ArtikelId]);
GO

-- Creating foreign key on [Beleg_Id] in table 'BelegArtikel'
ALTER TABLE [Jaws-DB].[BelegArtikel]
ADD CONSTRAINT [FK_BelegArtikel_Beleg]
    FOREIGN KEY ([Beleg_Id])
    REFERENCES [Jaws-DB].[BelegSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Artikel_Id] in table 'BelegArtikel'
ALTER TABLE [Jaws-DB].[BelegArtikel]
ADD CONSTRAINT [FK_BelegArtikel_Artikel]
    FOREIGN KEY ([Artikel_Id])
    REFERENCES [Jaws-DB].[ArtikelSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BelegArtikel_Artikel'
CREATE INDEX [IX_FK_BelegArtikel_Artikel]
ON [Jaws-DB].[BelegArtikel]
    ([Artikel_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------