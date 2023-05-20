CREATE TABLE [dbo].[Games]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[FPlayer] int not null,
	[SPlayer] int,
	[Board] nvarchar(75) null,
	[Status] nvarchar(10) not null, 
    [Rating] INT NOT NULL DEFAULT 100, 
    [Duration] INT NOT NULL DEFAULT 10, 
    [CreatedTime] DATETIME NULL
)
