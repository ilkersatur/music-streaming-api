DECLARE @date DATE;
SET @date = '2016-08-10';

SELECT 
    DistinctSongCount AS DistinctPlayCount,
    COUNT(*) AS ClientCount
FROM (
    SELECT 
        ClientId,
        COUNT(DISTINCT SongId) AS DistinctSongCount
    FROM PlayLogs
    WHERE CAST(PlayTimeSpan AS DATE) = @date
    GROUP BY ClientId
) AS ClientDistinctCounts
GROUP BY DistinctSongCount
ORDER BY DistinctSongCount;