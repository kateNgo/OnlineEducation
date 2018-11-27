 CREATE DATABASE OnlineEducation 

GO
/* USE [OnlineEducation] */
use [OnlineEducation]
GO

/****** Object:  Table [dbo].[Account]    Script Date: 29/10/2018 9:32:09 AM ******/
CREATE TABLE [Account](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Email] [varchar](100) NOT NULL UNIQUE,
	[Password] [varchar](100) NOT NULL,
	[Name] [varchar](100) NULL
) ON [PRIMARY]

GO


INSERT INTO [dbo].[Account]
           ([Email]
           ,[Password]
           ,[Name])
     VALUES
           ('kate@asims.com.au','aaaa','Kate Ngo')
GO


CREATE TABLE [HelpOnlineLevel1](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Description] [varchar](500) NULL,
	[ImageFile] [varchar](100) NULL,
	[IndexTopic] int ,
	[URL] [varchar](100) NULL
) ON [PRIMARY]

GO
CREATE TABLE [HelpOnlineLevel2](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY ,
	[Title] [varchar](100) NOT NULL,
	[Description] [varchar](500) NULL,
	[ImageFile] [varchar](100) NULL,
	[URL] [varchar](100) NULL,
	[IndexTopic] int ,
	[ParentId] [int] NOT NULL FOREIGN KEY REFERENCES [HelpOnlineLevel1] ([Id]) 
		ON DELETE CASCADE ON UPDATE CASCADE
) ON [PRIMARY]

GO

/****** Object:  Table [HelpOnlineLevel3]    Script Date: 29/10/2018 9:29:21 AM ******/

CREATE TABLE [HelpOnlineLevel3](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Title] [varchar](100) NOT NULL,
	[Description] [varchar](500) NULL,
	[URL] [varchar](500) NULL,
	[IndexTopic] int ,
	[ParentId] [int] NOT NULL FOREIGN KEY REFERENCES [HelpOnlineLevel2] ([Id]) 
		ON DELETE CASCADE ON UPDATE CASCADE
) ON [PRIMARY]

GO


CREATE TABLE [FAQ](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Title] [varchar](200) NOT NULL,
	[Description] [varchar](5000) NULL,
	[URL] [varchar](100) NULL
) ON [PRIMARY]

GO
CREATE TABLE [Video](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Title] [varchar](200) NOT NULL,
	[Description] [varchar](5000) NULL,
	[URL] [varchar](100) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

SELECT * FROM HelpOnlineLevel1 order by IndexTopic
go

/* create store procedure */

/****** Object:  StoredProcedure [sp_AddHelpOnlineLevel1]    Script Date: 27/11/2018 9:42:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Kate Ngo>
-- Create date: <27/11/2018>
-- Description:	<create a record in Help Online level 1 and return a new ID>
-- =============================================
CREATE PROCEDURE [dbo].[sp_AddHelpOnlineLevel1]
	-- Add the parameters for the stored procedure here
	@Title nvarchar(100), 
	@ImageFile nvarchar(100),
	@IndexTopic int,
	@NewId int output
AS
BEGIN
	INSERT INTO HelpOnlineLevel1 (Title, ImageFile, IndexTopic)
	VALUES 	(@Title, @ImageFile, @IndexTopic)
	SELECT @NewId = SCOPE_IDENTITY()
	return @NewId
END
GO
