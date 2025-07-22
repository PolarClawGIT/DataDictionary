/* Obsolete
CREATE TYPE [AppScript].[udttScriptingPath] AS TABLE
(
	[TemplateId]            UniqueIdentifier NULL,
	[NameSpace]             [AppGeneral].[uddtNameSpacePath] NULL,
	[NameSpaceScope]        [AppGeneral].[uddtScopeName] NULL,
	-- Temporal Data
	[CreatedOn]             DateTime2 (7) Null,
	[CreatedBy]             NVarChar(4000) Null,
	[RemovedOn]             DateTime2 (7) Null,
	[RemovedBy]             NVarChar(4000) Null,
	[IsInserted]            Bit Null,
	[IsUpdated]             Bit Null,
	[IsDeleted]             Bit Null,
	[IsCurrent]             Bit Null
);
*/