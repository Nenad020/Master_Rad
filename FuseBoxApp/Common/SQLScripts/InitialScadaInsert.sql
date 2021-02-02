USE [ScadaDb]
GO

DECLARE @first AS INT = 1
DECLARE @last AS INT = 65535

WHILE(@first <= @last)
BEGIN
    INSERT INTO dbo.CoilsAddress VALUES(@first, 0, 0, 0)
	INSERT INTO dbo.HoldingRegistersAddress VALUES(@first, 0, 0, 0)
    SET @first += 1
END
