CREATE TABLE [dbo].[BreakerMes]
(
    [Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [CurrentState] BIT NOT NULL, 
    [LastState] BIT NOT NULL
)
