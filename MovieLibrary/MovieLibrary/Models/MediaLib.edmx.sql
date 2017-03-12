
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 06/26/2011 18:21:15
-- Generated from EDMX file: C:\Users\Stefan\documents\visual studio 2010\Projects\MovieLibrary\MovieLibrary\Models\MediaLib.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MediaLib];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TV_ShowSeason]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SeasonSet] DROP CONSTRAINT [FK_TV_ShowSeason];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserMedien]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMediaSet] DROP CONSTRAINT [FK_UserUserMedien];
GO
IF OBJECT_ID(N'[dbo].[FK_EpisodeSeason]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EpisodeSet] DROP CONSTRAINT [FK_EpisodeSeason];
GO
IF OBJECT_ID(N'[dbo].[FK_UserBooksBook]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMediaSet_UserBook] DROP CONSTRAINT [FK_UserBooksBook];
GO
IF OBJECT_ID(N'[dbo].[FK_ActorVideo_Actor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActorVideo] DROP CONSTRAINT [FK_ActorVideo_Actor];
GO
IF OBJECT_ID(N'[dbo].[FK_ActorVideo_Video]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActorVideo] DROP CONSTRAINT [FK_ActorVideo_Video];
GO
IF OBJECT_ID(N'[dbo].[FK_BookMovie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MediaSet_Movie] DROP CONSTRAINT [FK_BookMovie];
GO
IF OBJECT_ID(N'[dbo].[FK_RequestUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BorrowRequestSet] DROP CONSTRAINT [FK_RequestUser];
GO
IF OBJECT_ID(N'[dbo].[FK_RequestUserMedia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BorrowRequestSet] DROP CONSTRAINT [FK_RequestUserMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_BorrowedDetailsUserMedia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BorrowedDetailsSet] DROP CONSTRAINT [FK_BorrowedDetailsUserMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_InsertRequestMedia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InsertRequestSet] DROP CONSTRAINT [FK_InsertRequestMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_InsertRequestUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InsertRequestSet] DROP CONSTRAINT [FK_InsertRequestUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUser_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserUser] DROP CONSTRAINT [FK_UserUser_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUser_User1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserUser] DROP CONSTRAINT [FK_UserUser_User1];
GO
IF OBJECT_ID(N'[dbo].[FK_BorrowedDetailsUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BorrowedDetailsSet] DROP CONSTRAINT [FK_BorrowedDetailsUser];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthorBook_Author]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuthorBook] DROP CONSTRAINT [FK_AuthorBook_Author];
GO
IF OBJECT_ID(N'[dbo].[FK_AuthorBook_Book]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuthorBook] DROP CONSTRAINT [FK_AuthorBook_Book];
GO
IF OBJECT_ID(N'[dbo].[FK_QuoteMovieMovie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuoteSet_QuoteMovie] DROP CONSTRAINT [FK_QuoteMovieMovie];
GO
IF OBJECT_ID(N'[dbo].[FK_QuoteTV_ShowEpisode]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuoteSet_QuoteTV_Show] DROP CONSTRAINT [FK_QuoteTV_ShowEpisode];
GO
IF OBJECT_ID(N'[dbo].[FK_QuoteBookBook]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuoteSet_QuoteBook] DROP CONSTRAINT [FK_QuoteBookBook];
GO
IF OBJECT_ID(N'[dbo].[FK_QuoteMedia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuoteSet] DROP CONSTRAINT [FK_QuoteMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_QuoteUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuoteSet] DROP CONSTRAINT [FK_QuoteUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserMovieMovie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMediaSet_UserMovie] DROP CONSTRAINT [FK_UserMovieMovie];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTV_ShowSeason]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMediaSet_UserTV_Show] DROP CONSTRAINT [FK_UserTV_ShowSeason];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUser1_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserUser1] DROP CONSTRAINT [FK_UserUser1_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUser1_User1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserUser1] DROP CONSTRAINT [FK_UserUser1_User1];
GO
IF OBJECT_ID(N'[dbo].[FK_Video_inherits_Media]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MediaSet_Video] DROP CONSTRAINT [FK_Video_inherits_Media];
GO
IF OBJECT_ID(N'[dbo].[FK_TV_Show_inherits_Video]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MediaSet_TV_Show] DROP CONSTRAINT [FK_TV_Show_inherits_Video];
GO
IF OBJECT_ID(N'[dbo].[FK_UserBook_inherits_UserMedia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMediaSet_UserBook] DROP CONSTRAINT [FK_UserBook_inherits_UserMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_Book_inherits_Media]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MediaSet_Book] DROP CONSTRAINT [FK_Book_inherits_Media];
GO
IF OBJECT_ID(N'[dbo].[FK_Actor_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet_Actor] DROP CONSTRAINT [FK_Actor_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_Movie_inherits_Video]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MediaSet_Movie] DROP CONSTRAINT [FK_Movie_inherits_Video];
GO
IF OBJECT_ID(N'[dbo].[FK_Author_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet_Author] DROP CONSTRAINT [FK_Author_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_QuoteMovie_inherits_Quote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuoteSet_QuoteMovie] DROP CONSTRAINT [FK_QuoteMovie_inherits_Quote];
GO
IF OBJECT_ID(N'[dbo].[FK_QuoteTV_Show_inherits_Quote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuoteSet_QuoteTV_Show] DROP CONSTRAINT [FK_QuoteTV_Show_inherits_Quote];
GO
IF OBJECT_ID(N'[dbo].[FK_QuoteBook_inherits_Quote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuoteSet_QuoteBook] DROP CONSTRAINT [FK_QuoteBook_inherits_Quote];
GO
IF OBJECT_ID(N'[dbo].[FK_UserVideo_inherits_UserMedia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMediaSet_UserVideo] DROP CONSTRAINT [FK_UserVideo_inherits_UserMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_UserMovie_inherits_UserVideo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMediaSet_UserMovie] DROP CONSTRAINT [FK_UserMovie_inherits_UserVideo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTV_Show_inherits_UserVideo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMediaSet_UserTV_Show] DROP CONSTRAINT [FK_UserTV_Show_inherits_UserVideo];
GO
IF OBJECT_ID(N'[dbo].[FK_Admin_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet_Admin] DROP CONSTRAINT [FK_Admin_inherits_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[EpisodeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EpisodeSet];
GO
IF OBJECT_ID(N'[dbo].[MediaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MediaSet];
GO
IF OBJECT_ID(N'[dbo].[QuoteSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuoteSet];
GO
IF OBJECT_ID(N'[dbo].[SeasonSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SeasonSet];
GO
IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[UserMediaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserMediaSet];
GO
IF OBJECT_ID(N'[dbo].[BorrowRequestSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BorrowRequestSet];
GO
IF OBJECT_ID(N'[dbo].[BorrowedDetailsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BorrowedDetailsSet];
GO
IF OBJECT_ID(N'[dbo].[InsertRequestSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InsertRequestSet];
GO
IF OBJECT_ID(N'[dbo].[PersonSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet];
GO
IF OBJECT_ID(N'[dbo].[MediaSet_Video]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MediaSet_Video];
GO
IF OBJECT_ID(N'[dbo].[MediaSet_TV_Show]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MediaSet_TV_Show];
GO
IF OBJECT_ID(N'[dbo].[UserMediaSet_UserBook]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserMediaSet_UserBook];
GO
IF OBJECT_ID(N'[dbo].[MediaSet_Book]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MediaSet_Book];
GO
IF OBJECT_ID(N'[dbo].[PersonSet_Actor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet_Actor];
GO
IF OBJECT_ID(N'[dbo].[MediaSet_Movie]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MediaSet_Movie];
GO
IF OBJECT_ID(N'[dbo].[PersonSet_Author]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet_Author];
GO
IF OBJECT_ID(N'[dbo].[QuoteSet_QuoteMovie]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuoteSet_QuoteMovie];
GO
IF OBJECT_ID(N'[dbo].[QuoteSet_QuoteTV_Show]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuoteSet_QuoteTV_Show];
GO
IF OBJECT_ID(N'[dbo].[QuoteSet_QuoteBook]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuoteSet_QuoteBook];
GO
IF OBJECT_ID(N'[dbo].[UserMediaSet_UserVideo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserMediaSet_UserVideo];
GO
IF OBJECT_ID(N'[dbo].[UserMediaSet_UserMovie]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserMediaSet_UserMovie];
GO
IF OBJECT_ID(N'[dbo].[UserMediaSet_UserTV_Show]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserMediaSet_UserTV_Show];
GO
IF OBJECT_ID(N'[dbo].[UserSet_Admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet_Admin];
GO
IF OBJECT_ID(N'[dbo].[ActorVideo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActorVideo];
GO
IF OBJECT_ID(N'[dbo].[UserUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserUser];
GO
IF OBJECT_ID(N'[dbo].[AuthorBook]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuthorBook];
GO
IF OBJECT_ID(N'[dbo].[UserUser1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserUser1];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'EpisodeSet'
CREATE TABLE [dbo].[EpisodeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SeasonId] int  NOT NULL,
    [Number] int  NOT NULL
);
GO

-- Creating table 'MediaSet'
CREATE TABLE [dbo].[MediaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [OriginalTitle] nvarchar(max)  NULL,
    [Rating] float  NOT NULL,
    [Genre] nvarchar(max)  NULL,
    [Cover] varbinary(max)  NULL,
    [Pending] bit  NOT NULL,
    [TotalRaters] int  NOT NULL,
    [AverageRating] float  NOT NULL,
    [AddingDate] datetime  NOT NULL
);
GO

-- Creating table 'QuoteSet'
CREATE TABLE [dbo].[QuoteSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QuoteString] nvarchar(max)  NOT NULL,
    [Language] nvarchar(max)  NOT NULL,
    [Invocations] int  NOT NULL,
    [MediaId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [Character] nvarchar(max)  NOT NULL,
    [OccurenceTime] nvarchar(max)  NULL,
    [Ranking] int  NULL
);
GO

-- Creating table 'SeasonSet'
CREATE TABLE [dbo].[SeasonSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] int  NOT NULL,
    [TV_ShowId] int  NOT NULL
);
GO

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Verified] bit  NOT NULL,
    [Closed] bit  NOT NULL,
    [VerificationCode] nvarchar(max)  NULL
);
GO

-- Creating table 'UserMediaSet'
CREATE TABLE [dbo].[UserMediaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StoragePlace] nvarchar(max)  NOT NULL,
    [MediaStatus] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'BorrowRequestSet'
CREATE TABLE [dbo].[BorrowRequestSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [UserTo] nvarchar(max)  NOT NULL,
    [UserMedia_Id] int  NOT NULL
);
GO

-- Creating table 'BorrowedDetailsSet'
CREATE TABLE [dbo].[BorrowedDetailsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [BorrowedFromUserId] int  NOT NULL,
    [DateOfReturn] datetime  NOT NULL,
    [NameTo] nvarchar(max)  NULL,
    [TakeBackRequest] bit  NOT NULL,
    [UserMedia_Id] int  NOT NULL
);
GO

-- Creating table 'InsertRequestSet'
CREATE TABLE [dbo].[InsertRequestSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [RequestDate] datetime  NOT NULL,
    [Media_Id] int  NOT NULL
);
GO

-- Creating table 'PersonSet'
CREATE TABLE [dbo].[PersonSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MediaSet_Video'
CREATE TABLE [dbo].[MediaSet_Video] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'MediaSet_TV_Show'
CREATE TABLE [dbo].[MediaSet_TV_Show] (
    [ShowBeginning] datetime  NOT NULL,
    [ShowEnding] datetime  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'UserMediaSet_UserBook'
CREATE TABLE [dbo].[UserMediaSet_UserBook] (
    [BookId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'MediaSet_Book'
CREATE TABLE [dbo].[MediaSet_Book] (
    [Isbn] nvarchar(max)  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'PersonSet_Actor'
CREATE TABLE [dbo].[PersonSet_Actor] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'MediaSet_Movie'
CREATE TABLE [dbo].[MediaSet_Movie] (
    [ReleaseDate] datetime  NOT NULL,
    [Id] int  NOT NULL,
    [Book_Id] int  NULL
);
GO

-- Creating table 'PersonSet_Author'
CREATE TABLE [dbo].[PersonSet_Author] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'QuoteSet_QuoteMovie'
CREATE TABLE [dbo].[QuoteSet_QuoteMovie] (
    [MovieId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'QuoteSet_QuoteTV_Show'
CREATE TABLE [dbo].[QuoteSet_QuoteTV_Show] (
    [EpisodeId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'QuoteSet_QuoteBook'
CREATE TABLE [dbo].[QuoteSet_QuoteBook] (
    [BookId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'UserMediaSet_UserVideo'
CREATE TABLE [dbo].[UserMediaSet_UserVideo] (
    [StorageType] nvarchar(max)  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'UserMediaSet_UserMovie'
CREATE TABLE [dbo].[UserMediaSet_UserMovie] (
    [MovieId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'UserMediaSet_UserTV_Show'
CREATE TABLE [dbo].[UserMediaSet_UserTV_Show] (
    [SeasonId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'UserSet_Admin'
CREATE TABLE [dbo].[UserSet_Admin] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'ActorVideo'
CREATE TABLE [dbo].[ActorVideo] (
    [Actor_Id] int  NOT NULL,
    [Video_Id] int  NOT NULL
);
GO

-- Creating table 'UserUser'
CREATE TABLE [dbo].[UserUser] (
    [Friends_Id] int  NOT NULL,
    [UserUser_User_Id] int  NOT NULL
);
GO

-- Creating table 'AuthorBook'
CREATE TABLE [dbo].[AuthorBook] (
    [Author_Id] int  NOT NULL,
    [Book_Id] int  NOT NULL
);
GO

-- Creating table 'UserUser1'
CREATE TABLE [dbo].[UserUser1] (
    [UserUser1_User1_Id] int  NOT NULL,
    [Requestlist_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'EpisodeSet'
ALTER TABLE [dbo].[EpisodeSet]
ADD CONSTRAINT [PK_EpisodeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MediaSet'
ALTER TABLE [dbo].[MediaSet]
ADD CONSTRAINT [PK_MediaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuoteSet'
ALTER TABLE [dbo].[QuoteSet]
ADD CONSTRAINT [PK_QuoteSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SeasonSet'
ALTER TABLE [dbo].[SeasonSet]
ADD CONSTRAINT [PK_SeasonSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserMediaSet'
ALTER TABLE [dbo].[UserMediaSet]
ADD CONSTRAINT [PK_UserMediaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BorrowRequestSet'
ALTER TABLE [dbo].[BorrowRequestSet]
ADD CONSTRAINT [PK_BorrowRequestSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BorrowedDetailsSet'
ALTER TABLE [dbo].[BorrowedDetailsSet]
ADD CONSTRAINT [PK_BorrowedDetailsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InsertRequestSet'
ALTER TABLE [dbo].[InsertRequestSet]
ADD CONSTRAINT [PK_InsertRequestSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [PK_PersonSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MediaSet_Video'
ALTER TABLE [dbo].[MediaSet_Video]
ADD CONSTRAINT [PK_MediaSet_Video]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MediaSet_TV_Show'
ALTER TABLE [dbo].[MediaSet_TV_Show]
ADD CONSTRAINT [PK_MediaSet_TV_Show]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserMediaSet_UserBook'
ALTER TABLE [dbo].[UserMediaSet_UserBook]
ADD CONSTRAINT [PK_UserMediaSet_UserBook]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MediaSet_Book'
ALTER TABLE [dbo].[MediaSet_Book]
ADD CONSTRAINT [PK_MediaSet_Book]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonSet_Actor'
ALTER TABLE [dbo].[PersonSet_Actor]
ADD CONSTRAINT [PK_PersonSet_Actor]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MediaSet_Movie'
ALTER TABLE [dbo].[MediaSet_Movie]
ADD CONSTRAINT [PK_MediaSet_Movie]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonSet_Author'
ALTER TABLE [dbo].[PersonSet_Author]
ADD CONSTRAINT [PK_PersonSet_Author]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuoteSet_QuoteMovie'
ALTER TABLE [dbo].[QuoteSet_QuoteMovie]
ADD CONSTRAINT [PK_QuoteSet_QuoteMovie]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuoteSet_QuoteTV_Show'
ALTER TABLE [dbo].[QuoteSet_QuoteTV_Show]
ADD CONSTRAINT [PK_QuoteSet_QuoteTV_Show]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuoteSet_QuoteBook'
ALTER TABLE [dbo].[QuoteSet_QuoteBook]
ADD CONSTRAINT [PK_QuoteSet_QuoteBook]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserMediaSet_UserVideo'
ALTER TABLE [dbo].[UserMediaSet_UserVideo]
ADD CONSTRAINT [PK_UserMediaSet_UserVideo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserMediaSet_UserMovie'
ALTER TABLE [dbo].[UserMediaSet_UserMovie]
ADD CONSTRAINT [PK_UserMediaSet_UserMovie]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserMediaSet_UserTV_Show'
ALTER TABLE [dbo].[UserMediaSet_UserTV_Show]
ADD CONSTRAINT [PK_UserMediaSet_UserTV_Show]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet_Admin'
ALTER TABLE [dbo].[UserSet_Admin]
ADD CONSTRAINT [PK_UserSet_Admin]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Actor_Id], [Video_Id] in table 'ActorVideo'
ALTER TABLE [dbo].[ActorVideo]
ADD CONSTRAINT [PK_ActorVideo]
    PRIMARY KEY NONCLUSTERED ([Actor_Id], [Video_Id] ASC);
GO

-- Creating primary key on [Friends_Id], [UserUser_User_Id] in table 'UserUser'
ALTER TABLE [dbo].[UserUser]
ADD CONSTRAINT [PK_UserUser]
    PRIMARY KEY NONCLUSTERED ([Friends_Id], [UserUser_User_Id] ASC);
GO

-- Creating primary key on [Author_Id], [Book_Id] in table 'AuthorBook'
ALTER TABLE [dbo].[AuthorBook]
ADD CONSTRAINT [PK_AuthorBook]
    PRIMARY KEY NONCLUSTERED ([Author_Id], [Book_Id] ASC);
GO

-- Creating primary key on [UserUser1_User1_Id], [Requestlist_Id] in table 'UserUser1'
ALTER TABLE [dbo].[UserUser1]
ADD CONSTRAINT [PK_UserUser1]
    PRIMARY KEY NONCLUSTERED ([UserUser1_User1_Id], [Requestlist_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TV_ShowId] in table 'SeasonSet'
ALTER TABLE [dbo].[SeasonSet]
ADD CONSTRAINT [FK_TV_ShowSeason]
    FOREIGN KEY ([TV_ShowId])
    REFERENCES [dbo].[MediaSet_TV_Show]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TV_ShowSeason'
CREATE INDEX [IX_FK_TV_ShowSeason]
ON [dbo].[SeasonSet]
    ([TV_ShowId]);
GO

-- Creating foreign key on [UserId] in table 'UserMediaSet'
ALTER TABLE [dbo].[UserMediaSet]
ADD CONSTRAINT [FK_UserUserMedien]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserMedien'
CREATE INDEX [IX_FK_UserUserMedien]
ON [dbo].[UserMediaSet]
    ([UserId]);
GO

-- Creating foreign key on [SeasonId] in table 'EpisodeSet'
ALTER TABLE [dbo].[EpisodeSet]
ADD CONSTRAINT [FK_EpisodeSeason]
    FOREIGN KEY ([SeasonId])
    REFERENCES [dbo].[SeasonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EpisodeSeason'
CREATE INDEX [IX_FK_EpisodeSeason]
ON [dbo].[EpisodeSet]
    ([SeasonId]);
GO

-- Creating foreign key on [BookId] in table 'UserMediaSet_UserBook'
ALTER TABLE [dbo].[UserMediaSet_UserBook]
ADD CONSTRAINT [FK_UserBooksBook]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[MediaSet_Book]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserBooksBook'
CREATE INDEX [IX_FK_UserBooksBook]
ON [dbo].[UserMediaSet_UserBook]
    ([BookId]);
GO

-- Creating foreign key on [Actor_Id] in table 'ActorVideo'
ALTER TABLE [dbo].[ActorVideo]
ADD CONSTRAINT [FK_ActorVideo_Actor]
    FOREIGN KEY ([Actor_Id])
    REFERENCES [dbo].[PersonSet_Actor]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Video_Id] in table 'ActorVideo'
ALTER TABLE [dbo].[ActorVideo]
ADD CONSTRAINT [FK_ActorVideo_Video]
    FOREIGN KEY ([Video_Id])
    REFERENCES [dbo].[MediaSet_Video]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActorVideo_Video'
CREATE INDEX [IX_FK_ActorVideo_Video]
ON [dbo].[ActorVideo]
    ([Video_Id]);
GO

-- Creating foreign key on [Book_Id] in table 'MediaSet_Movie'
ALTER TABLE [dbo].[MediaSet_Movie]
ADD CONSTRAINT [FK_BookMovie]
    FOREIGN KEY ([Book_Id])
    REFERENCES [dbo].[MediaSet_Book]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BookMovie'
CREATE INDEX [IX_FK_BookMovie]
ON [dbo].[MediaSet_Movie]
    ([Book_Id]);
GO

-- Creating foreign key on [UserId] in table 'BorrowRequestSet'
ALTER TABLE [dbo].[BorrowRequestSet]
ADD CONSTRAINT [FK_RequestUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RequestUser'
CREATE INDEX [IX_FK_RequestUser]
ON [dbo].[BorrowRequestSet]
    ([UserId]);
GO

-- Creating foreign key on [UserMedia_Id] in table 'BorrowRequestSet'
ALTER TABLE [dbo].[BorrowRequestSet]
ADD CONSTRAINT [FK_RequestUserMedia]
    FOREIGN KEY ([UserMedia_Id])
    REFERENCES [dbo].[UserMediaSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RequestUserMedia'
CREATE INDEX [IX_FK_RequestUserMedia]
ON [dbo].[BorrowRequestSet]
    ([UserMedia_Id]);
GO

-- Creating foreign key on [UserMedia_Id] in table 'BorrowedDetailsSet'
ALTER TABLE [dbo].[BorrowedDetailsSet]
ADD CONSTRAINT [FK_BorrowedDetailsUserMedia]
    FOREIGN KEY ([UserMedia_Id])
    REFERENCES [dbo].[UserMediaSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BorrowedDetailsUserMedia'
CREATE INDEX [IX_FK_BorrowedDetailsUserMedia]
ON [dbo].[BorrowedDetailsSet]
    ([UserMedia_Id]);
GO

-- Creating foreign key on [Media_Id] in table 'InsertRequestSet'
ALTER TABLE [dbo].[InsertRequestSet]
ADD CONSTRAINT [FK_InsertRequestMedia]
    FOREIGN KEY ([Media_Id])
    REFERENCES [dbo].[MediaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InsertRequestMedia'
CREATE INDEX [IX_FK_InsertRequestMedia]
ON [dbo].[InsertRequestSet]
    ([Media_Id]);
GO

-- Creating foreign key on [UserId] in table 'InsertRequestSet'
ALTER TABLE [dbo].[InsertRequestSet]
ADD CONSTRAINT [FK_InsertRequestUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InsertRequestUser'
CREATE INDEX [IX_FK_InsertRequestUser]
ON [dbo].[InsertRequestSet]
    ([UserId]);
GO

-- Creating foreign key on [Friends_Id] in table 'UserUser'
ALTER TABLE [dbo].[UserUser]
ADD CONSTRAINT [FK_UserUser_User]
    FOREIGN KEY ([Friends_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserUser_User_Id] in table 'UserUser'
ALTER TABLE [dbo].[UserUser]
ADD CONSTRAINT [FK_UserUser_User1]
    FOREIGN KEY ([UserUser_User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUser_User1'
CREATE INDEX [IX_FK_UserUser_User1]
ON [dbo].[UserUser]
    ([UserUser_User_Id]);
GO

-- Creating foreign key on [BorrowedFromUserId] in table 'BorrowedDetailsSet'
ALTER TABLE [dbo].[BorrowedDetailsSet]
ADD CONSTRAINT [FK_BorrowedDetailsUser]
    FOREIGN KEY ([BorrowedFromUserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BorrowedDetailsUser'
CREATE INDEX [IX_FK_BorrowedDetailsUser]
ON [dbo].[BorrowedDetailsSet]
    ([BorrowedFromUserId]);
GO

-- Creating foreign key on [Author_Id] in table 'AuthorBook'
ALTER TABLE [dbo].[AuthorBook]
ADD CONSTRAINT [FK_AuthorBook_Author]
    FOREIGN KEY ([Author_Id])
    REFERENCES [dbo].[PersonSet_Author]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Book_Id] in table 'AuthorBook'
ALTER TABLE [dbo].[AuthorBook]
ADD CONSTRAINT [FK_AuthorBook_Book]
    FOREIGN KEY ([Book_Id])
    REFERENCES [dbo].[MediaSet_Book]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthorBook_Book'
CREATE INDEX [IX_FK_AuthorBook_Book]
ON [dbo].[AuthorBook]
    ([Book_Id]);
GO

-- Creating foreign key on [MovieId] in table 'QuoteSet_QuoteMovie'
ALTER TABLE [dbo].[QuoteSet_QuoteMovie]
ADD CONSTRAINT [FK_QuoteMovieMovie]
    FOREIGN KEY ([MovieId])
    REFERENCES [dbo].[MediaSet_Movie]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuoteMovieMovie'
CREATE INDEX [IX_FK_QuoteMovieMovie]
ON [dbo].[QuoteSet_QuoteMovie]
    ([MovieId]);
GO

-- Creating foreign key on [EpisodeId] in table 'QuoteSet_QuoteTV_Show'
ALTER TABLE [dbo].[QuoteSet_QuoteTV_Show]
ADD CONSTRAINT [FK_QuoteTV_ShowEpisode]
    FOREIGN KEY ([EpisodeId])
    REFERENCES [dbo].[EpisodeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuoteTV_ShowEpisode'
CREATE INDEX [IX_FK_QuoteTV_ShowEpisode]
ON [dbo].[QuoteSet_QuoteTV_Show]
    ([EpisodeId]);
GO

-- Creating foreign key on [BookId] in table 'QuoteSet_QuoteBook'
ALTER TABLE [dbo].[QuoteSet_QuoteBook]
ADD CONSTRAINT [FK_QuoteBookBook]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[MediaSet_Book]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuoteBookBook'
CREATE INDEX [IX_FK_QuoteBookBook]
ON [dbo].[QuoteSet_QuoteBook]
    ([BookId]);
GO

-- Creating foreign key on [MediaId] in table 'QuoteSet'
ALTER TABLE [dbo].[QuoteSet]
ADD CONSTRAINT [FK_QuoteMedia]
    FOREIGN KEY ([MediaId])
    REFERENCES [dbo].[MediaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuoteMedia'
CREATE INDEX [IX_FK_QuoteMedia]
ON [dbo].[QuoteSet]
    ([MediaId]);
GO

-- Creating foreign key on [UserId] in table 'QuoteSet'
ALTER TABLE [dbo].[QuoteSet]
ADD CONSTRAINT [FK_QuoteUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuoteUser'
CREATE INDEX [IX_FK_QuoteUser]
ON [dbo].[QuoteSet]
    ([UserId]);
GO

-- Creating foreign key on [MovieId] in table 'UserMediaSet_UserMovie'
ALTER TABLE [dbo].[UserMediaSet_UserMovie]
ADD CONSTRAINT [FK_UserMovieMovie]
    FOREIGN KEY ([MovieId])
    REFERENCES [dbo].[MediaSet_Movie]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserMovieMovie'
CREATE INDEX [IX_FK_UserMovieMovie]
ON [dbo].[UserMediaSet_UserMovie]
    ([MovieId]);
GO

-- Creating foreign key on [SeasonId] in table 'UserMediaSet_UserTV_Show'
ALTER TABLE [dbo].[UserMediaSet_UserTV_Show]
ADD CONSTRAINT [FK_UserTV_ShowSeason]
    FOREIGN KEY ([SeasonId])
    REFERENCES [dbo].[SeasonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTV_ShowSeason'
CREATE INDEX [IX_FK_UserTV_ShowSeason]
ON [dbo].[UserMediaSet_UserTV_Show]
    ([SeasonId]);
GO

-- Creating foreign key on [UserUser1_User1_Id] in table 'UserUser1'
ALTER TABLE [dbo].[UserUser1]
ADD CONSTRAINT [FK_UserUser1_User]
    FOREIGN KEY ([UserUser1_User1_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Requestlist_Id] in table 'UserUser1'
ALTER TABLE [dbo].[UserUser1]
ADD CONSTRAINT [FK_UserUser1_User1]
    FOREIGN KEY ([Requestlist_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUser1_User1'
CREATE INDEX [IX_FK_UserUser1_User1]
ON [dbo].[UserUser1]
    ([Requestlist_Id]);
GO

-- Creating foreign key on [Id] in table 'MediaSet_Video'
ALTER TABLE [dbo].[MediaSet_Video]
ADD CONSTRAINT [FK_Video_inherits_Media]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[MediaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'MediaSet_TV_Show'
ALTER TABLE [dbo].[MediaSet_TV_Show]
ADD CONSTRAINT [FK_TV_Show_inherits_Video]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[MediaSet_Video]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'UserMediaSet_UserBook'
ALTER TABLE [dbo].[UserMediaSet_UserBook]
ADD CONSTRAINT [FK_UserBook_inherits_UserMedia]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[UserMediaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'MediaSet_Book'
ALTER TABLE [dbo].[MediaSet_Book]
ADD CONSTRAINT [FK_Book_inherits_Media]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[MediaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'PersonSet_Actor'
ALTER TABLE [dbo].[PersonSet_Actor]
ADD CONSTRAINT [FK_Actor_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'MediaSet_Movie'
ALTER TABLE [dbo].[MediaSet_Movie]
ADD CONSTRAINT [FK_Movie_inherits_Video]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[MediaSet_Video]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'PersonSet_Author'
ALTER TABLE [dbo].[PersonSet_Author]
ADD CONSTRAINT [FK_Author_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuoteSet_QuoteMovie'
ALTER TABLE [dbo].[QuoteSet_QuoteMovie]
ADD CONSTRAINT [FK_QuoteMovie_inherits_Quote]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuoteSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuoteSet_QuoteTV_Show'
ALTER TABLE [dbo].[QuoteSet_QuoteTV_Show]
ADD CONSTRAINT [FK_QuoteTV_Show_inherits_Quote]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuoteSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuoteSet_QuoteBook'
ALTER TABLE [dbo].[QuoteSet_QuoteBook]
ADD CONSTRAINT [FK_QuoteBook_inherits_Quote]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuoteSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'UserMediaSet_UserVideo'
ALTER TABLE [dbo].[UserMediaSet_UserVideo]
ADD CONSTRAINT [FK_UserVideo_inherits_UserMedia]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[UserMediaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'UserMediaSet_UserMovie'
ALTER TABLE [dbo].[UserMediaSet_UserMovie]
ADD CONSTRAINT [FK_UserMovie_inherits_UserVideo]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[UserMediaSet_UserVideo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'UserMediaSet_UserTV_Show'
ALTER TABLE [dbo].[UserMediaSet_UserTV_Show]
ADD CONSTRAINT [FK_UserTV_Show_inherits_UserVideo]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[UserMediaSet_UserVideo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'UserSet_Admin'
ALTER TABLE [dbo].[UserSet_Admin]
ADD CONSTRAINT [FK_Admin_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------