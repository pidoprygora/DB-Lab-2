SELECT 
    a.AlbumId,
    a.Tittle,
    COUNT(s.SongId) AS SongCount
FROM 
    Albums a
JOIN 
    Songs s ON a.AlbumId = s.AlbumId
GROUP BY 
    a.AlbumId, a.Tittle
HAVING 
    COUNT(s.SongId) >= @songCount;
