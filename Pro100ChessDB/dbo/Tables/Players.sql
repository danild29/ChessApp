CREATE TABLE [dbo].[Players]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [NickName] NVARCHAR(50) NULL DEFAULT 'Anonymous' ,
    [Email] NVARCHAR(50) NULL ,
    [Pasword] NVARCHAR(50) NULL, 
    [Rating] INT NOT NULL DEFAULT 100, 
    [IsPremium] BIT NULL DEFAULT 0, 
    [Status] NVARCHAR(100) NULL
)
