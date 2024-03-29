/****** Object:  StoredProcedure [dbo].[pRecordLabel_ImportFromDiscogs]    Script Date: 25-02-2024 13:23:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[pRecordLabel_ImportFromDiscogs]
	@iXmlDocAsString NVARCHAR(MAX)
AS
DECLARE @lXmlPointer INT

BEGIN

	-- Load xml document on the server and get a pointer for referencing it
	EXEC sp_xml_preparedocument @lXmlPointer OUTPUT, @iXmlDocAsString;

	-- Create table variable to extract xml data into
	DECLARE @lLabels TABLE (
		Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		DiscogsLabelsId INT NOT NULL,
		[Name] NVARCHAR(500) NOT NULL
	)

	-- Insert data from xml document into table variable
	INSERT INTO @lLabels (DiscogsLabelsId, [Name])
	SELECT
		Id AS DiscogsLabelsId,
		[Name]
	FROM OPENXML(@lXmlPointer, 'labels/label')
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
	MERGE 
		[VinylXContext].[dbo].[RecordLabel] WITH (ROWLOCK, HOLDLOCK) AS t 
	USING 
		(SELECT * FROM @lLabels) AS s
	ON 
		(t.DiscogLabelId = s.DiscogsLabelsId)
	WHEN MATCHED AND s.[Name] != t.LabelName
		THEN UPDATE
			SET t.LabelName = s.[Name]
	WHEN NOT MATCHED
		THEN INSERT (LabelName, DiscogLabelId)
		VALUES (s.[Name], s.DiscogsLabelsId);

	/*
	SELECT
		Id AS DiscogsLabelsId,
		SubLabelsId AS DiscogsSubLabelsId
	FROM OPENXML(@lXmlPointer, 'labels/label')
	WITH 
	(
		Id INT 'id',
		SubLabelsId INT 'sublabels/label/@id'
	)
	*/

END