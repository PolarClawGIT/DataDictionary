CREATE TYPE [AppModel].[typeSubjectArea] AS TABLE (
    [SubjectAreaId]          UNIQUEIDENTIFIER                           NULL,
    [SubjectAreaTitle]       [App_DataDictionary].[typeTitle]           NULL,
    [SubjectAreaDescription] [App_DataDictionary].[typeDescription]     NULL,
    [SubjectName]            [App_DataDictionary].[typeNameSpacePath]   NULL,
	-- Temporal Data
	[CreatedOn]              DateTime2 (7) Null,
	[CreatedBy]              NVarChar(4000) Null,
	[RemovedOn]              DateTime2 (7) Null,
	[RemovedBy]              NVarChar(4000) Null,
	[IsInserted]             Bit Null,
	[IsUpdated]              Bit Null,
	[IsDeleted]              Bit Null,
	[IsCurrent]              Bit Null);


