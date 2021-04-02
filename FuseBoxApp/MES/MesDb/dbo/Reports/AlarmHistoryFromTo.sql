CREATE PROCEDURE [dbo].[AlarmHistoryFromTo]
    @param1 datetime2,
    @param2 datetime2
AS
    SELECT Id, BreakerId, Timestamp, Message
    FROM AlarmMes
    WHERE Timestamp BETWEEN @param1 AND @param2
RETURN 0
