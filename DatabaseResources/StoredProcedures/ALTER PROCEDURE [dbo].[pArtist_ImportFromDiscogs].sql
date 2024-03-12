/****** Object:  StoredProcedure [dbo].[pRecordLabel_ImportFromDiscogs]    Script Date: 05-03-2024 13:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[pArtist_ImportFromDiscogs]
	@iXmlDocAsString NVARCHAR(MAX)
AS
DECLARE @lXmlPointer INT

BEGIN

	-- Load xml document on the server and get a pointer for referencing it
	-- Actually an old method but handles large documents well
	EXEC sp_xml_preparedocument @lXmlPointer OUTPUT, @iXmlDocAsString;

	-- Create table variable to extract xml data into
	DECLARE @lArtists TABLE (
		Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		DiscogsArtistsId INT NOT NULL,
		[Name] NVARCHAR(500) NOT NULL
	)

	-- Insert data from xml document into table variable
	INSERT INTO @lArtists (DiscogsArtistsId, [Name])
	SELECT
		Id AS DiscogsArtistsId,
		[Name]
	FROM OPENXML(@lXmlPointer, 'artists/artist')
	WITH 
	(
		Id INT 'id',
		[Name] NVARCHAR(500) 'name'
	)

	-- Cleanup the xml document
	EXEC sp_xml_removedocument @lXmlPointer

	-- Merge table variable into database table
	-- t and s refers to target and source
	-- t and s are matched on DiscogsLabelsId
	-- ROWLOCK hint to force locking the row, tries to avoid lock escalation to tablock or pagelock
	-- HOLDLOCK hint to hold the lock until transaction completes
	MERGE 
		[VinylXContext].[dbo].[Artist] WITH (ROWLOCK, HOLDLOCK) AS t 
	USING 
		(SELECT * FROM @lArtists) AS s
	ON 
		(t.DiscogArtistId = s.DiscogsArtistsId)
	WHEN MATCHED AND s.[Name] != t.ArtistName
		THEN UPDATE
			SET t.ArtistName = s.[Name]
	WHEN NOT MATCHED
		THEN INSERT (ArtistName, DiscogArtistId)
		VALUES (s.[Name], s.DiscogsArtistsId);


END