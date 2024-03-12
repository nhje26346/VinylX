/****** Object:  StoredProcedure [dbo].[pArtist_ImportFromDiscogs]    Script Date: 09-03-2024 08:21:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[pMasterRelease_ImportFromDiscogs]
	@iXmlDocAsString NVARCHAR(MAX)
AS
DECLARE @lXmlPointer INT

BEGIN
	EXEC sp_xml_preparedocument @lXmlPointer OUTPUT, @iXmlDocAsString;

	DECLARE @lMasters TABLE (
		DiscogsMastersId INT NOT NULL PRIMARY KEY,
		DiscogsArtistsId INT NOT NULL,
		ArtistId INT NOT NULL,
		Genre NVARCHAR(500) NOT NULL,
		Title NVARCHAR(500) NOT NULL
	)
	
	INSERT INTO @lMasters (DiscogsMastersId,DiscogsArtistsId,ArtistId,Genre,Title)
	SELECT
		x.Id AS DiscogsMastersId,
		x.DiscogsArtistsId,
		artist.ArtistId,
		x.Genre,
		x.Title
	FROM OPENXML(@lXmlPointer, 'masters/master')
	WITH 
	(
		Id INT '@id',
		main_release NVARCHAR(500) 'main_release',
		DiscogsArtistsId INT 'artists/artist[1]/id',
		Genre NVARCHAR(500) 'genres/genre[1]',
		Title NVARCHAR(500) 'title[1]'
	) as x
	JOIN dbo.Artist artist ON artist.DiscogArtistId = x.DiscogsArtistsId

	EXEC sp_xml_removedocument @lXmlPointer

	MERGE 
		[VinylXContext].[dbo].MasterRelease WITH (ROWLOCK, HOLDLOCK) AS t 
	USING 
		(SELECT * FROM @lMasters) AS s
	ON 
		(t.DiscogMasterReleaseId = s.DiscogsMastersId)
	WHEN MATCHED AND (s.ArtistId != t.ArtistId OR s.Title != t.AlbumName)
		THEN UPDATE
			SET t.ArtistId = s.ArtistId,
				t.AlbumName = s.Title
	WHEN NOT MATCHED
		THEN INSERT (ArtistId, AlbumName, DiscogMasterReleaseId)
		VALUES (s.ArtistId, s.title, s.DiscogsMastersId);


END