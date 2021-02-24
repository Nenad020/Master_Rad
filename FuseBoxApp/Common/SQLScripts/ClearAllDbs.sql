--USE [ScadaDb]
--GO
--	UPDATE dbo.CoilsAddress 
--	SET Id = 0, Used = 0

--	UPDATE dbo.HoldingRegistersAddress
--	SET Id = 0, Used = 0

USE [MesDb]
GO
    UPDATE dbo.ElectricityMeterMes
    SET Value = 0

    UPDATE dbo.BreakerMes
    SET CurrentState = 'False', LastState = 'False'

    TRUNCATE TABLE dbo.AlarmMes