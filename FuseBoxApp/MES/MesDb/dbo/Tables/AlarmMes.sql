﻿CREATE TABLE [dbo].[AlarmMes]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [BreakerId] INT NOT NULL, 
    [Timestamp] DATETIME2 NOT NULL, 
    [Message] NVARCHAR(MAX) NOT NULL
)