USE [ScadaDb]
GO

DECLARE @first AS INT = 1
DECLARE @last AS INT = 15

WHILE(@first <= @last)
BEGIN
    INSERT INTO dbo.CoilsAddress VALUES(@first, 0, 1)
	INSERT INTO dbo.HoldingRegistersAddress VALUES(@first, 0, 1)
    SET @first += 1
END
