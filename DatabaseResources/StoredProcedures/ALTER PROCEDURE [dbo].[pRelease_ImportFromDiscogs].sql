/****** Object:  StoredProcedure [dbo].[pRelease_ImportFromDiscogs]    Script Date: 12-03-2024 07:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[pRelease_ImportFromDiscogs]
	@iXmlDocAsString NVARCHAR(MAX)
AS
DECLARE @lXmlPointer INT

BEGIN

	DECLARE @lReleases TABLE (
		DiscogReleaseId INT NOT NULL PRIMARY KEY,
		ReleaseDate NVARCHAR(100) NULL,
		CategoryNumber NVARCHAR(MAX) NULL,
		Edition NVARCHAR(500) NOT NULL,
		Genre NVARCHAR(200) NULL,
		MasterReleaseId INT NOT NULL,
		MediaID INT NOT NULL,
		RecordLabelId INT NULL,
		BarcodeNumber NVARCHAR(MAX)
	)

	EXEC sp_xml_preparedocument @lXmlPointer OUTPUT, @iXmlDocAsString;

	INSERT INTO @lReleases (DiscogReleaseId,ReleaseDate,CategoryNumber,Edition,Genre,MasterReleaseId,MediaID,RecordLabelId,BarcodeNumber)
	SELECT
		x2.DiscogsReleasesId,
		x2.Released,
		x2.CatNo,
		x2.Title,
		x2.Genre,
		masterRelease.[MasterReleaseId],	
		COALESCE(media.MediaId,(SELECT m.MediaId FROM [dbo].[Media] m WHERE m.MediaName = 'Other'))	,
		(SELECT MIN([RecordLabelId]) FROM [dbo].[RecordLabel] l WHERE l.[LabelName] = x2.LabelName), -- Not pretty but there is no label id in files 
		Barcode
	FROM
		(
			SELECT
				x.Id AS DiscogsReleasesId,
				x.Released,
				x.CatNo,
				x.Title,
				x.Genre,
				x.MasterId AS DiscogsMasterId,
				COALESCE(x.BarcodeDirectlyScanned,x.BarcodeScanned,x.Barcode) AS Barcode,
				CASE 
					WHEN x.FormatName = 'Vinyl' AND x.FormatDescription IN('12"','10"','7"') THEN x.FormatName + ' ' +x.FormatDescription
					ELSE x.FormatName
				END AS FormatName,
				LabelName
			FROM OPENXML(@lXmlPointer, 'releases/release')
			WITH 
			(
				Id INT '@id',
				Title NVARCHAR(500) 'title',
				CatNo NVARCHAR(MAX) 'labels/label[1]/@catno',
				Released NVARCHAR(200) 'released',
				MasterId INT 'master_id',
				Barcode NVARCHAR(MAX) 'identifiers/identifier[@type="Barcode"]/@value',
				BarcodeDirectlyScanned NVARCHAR(MAX) 'identifiers/identifier[@type="Barcode"][@description="Directly scanned"]/@value',
				BarcodeScanned NVARCHAR(MAX) 'identifiers/identifier[@type="Barcode"][@description="Scanned"]/@value',
				FormatName NVARCHAR(200) 'formats/format[1]/@name',
				FormatQty NVARCHAR(200) 'formats/format[1]/@qty',
				FormatDescription NVARCHAR(200) 'formats/format[1]/descriptions/description[1]',	
				Genre NVARCHAR(200) 'genres/genre[1]',
				LabelName NVARCHAR(200) 'labels/label/@name'
			) AS x
		) AS x2
	JOIN [dbo].[MasterRelease] masterRelease ON masterRelease.[DiscogMasterReleaseId] = x2.DiscogsMasterId
	JOIN dbo.Media media ON media.MediaName = x2.FormatName

	delete @lReleases where RecordLabelId is null

	MERGE 
		[VinylXContext].[dbo].[Release] WITH (ROWLOCK, HOLDLOCK) AS t 
	USING 
		(SELECT * FROM @lReleases) AS s
	ON 
		(t.[DiscogReleaseId] = s.[DiscogReleaseId])
	WHEN MATCHED 
		THEN UPDATE
			SET t.DiscogReleaseId = s.DiscogReleaseId,
			t.ReleaseDate = s.ReleaseDate,
			t.CategoryNumber = s.CategoryNumber,
			t.Edition = s.Edition,
			t.Genre = s.Genre,
			t.MasterReleaseId = s.MasterReleaseId,
			t.MediaID = s.MediaID,
			t.RecordLabelId = s.RecordLabelId,
			t.BarcodeNumber = s.BarcodeNumber
	WHEN NOT MATCHED
		THEN INSERT (DiscogReleaseId,ReleaseDate,CategoryNumber,Edition,Genre,MasterReleaseId,MediaID,RecordLabelId,BarcodeNumber)
		VALUES (DiscogReleaseId,ReleaseDate,CategoryNumber,Edition,Genre,MasterReleaseId,MediaID,RecordLabelId,BarcodeNumber);
END