CREATE DATABASE [Blogging];
GO

USE [Blogging];
GO

CREATE TABLE [Blog] (
    [BlogId] int NOT NULL IDENTITY,
    [Url] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Blog] PRIMARY KEY ([BlogId])
);
GO

CREATE TABLE [Post] (
    [PostId] int NOT NULL IDENTITY,
    [BlogId] int NOT NULL,
    [Content] nvarchar(max),
    [Title] nvarchar(max),
    CONSTRAINT [PK_Post] PRIMARY KEY ([PostId]),
    CONSTRAINT [FK_Post_Blog_BlogId] FOREIGN KEY ([BlogId]) REFERENCES [Blog] ([BlogId]) ON DELETE CASCADE
);
GO

INSERT INTO [Blog] (Url) VALUES
('http://blogs.msdn.com/dotnet'),
('http://blogs.msdn.com/webdev'),
('http://blogs.msdn.com/visualstudio')
GO

INSERT INTO [Post] (BlogID, Content, Title) VALUES
(1,'Today, we are releasing the .NET Core May 2018 Update. This update includes .NET Core 2.1.200 SDK and ASP.NET Core 2.0.8. Security Microsoft is releasing this security advisory to provide information about a vulnerability in .NET Core and .NET native version 2.0.','.NET Core May 2018 Update'),
(1,'Today, we are excited to announce that the first release candidate of EF Core 2.1 is available, alongside .NET Core 2.1 RC 1 and ASP.NET Core 2.1 RC 1, for broad testing, and now also for production use! Go live support EF Core 2.1 RC1 is a “go live” release, which means once you test…','Announcing Entity Framework Core 2.1 RC 1')
GO