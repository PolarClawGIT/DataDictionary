CREATE TABLE [App_DataDictionary].[ApplicationScope]
(
	-- This represents Database Object Name and Name Space Level or Scope.
	-- This is a hierarchy that is expected to be static.
	-- Reference: https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16
	-- Reference: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/
	[ScopeId]             Int Not Null,
	[ScopeParentId]       Int Null,
	[ScopedElementName]   [App_DataDictionary].[typeNameSpaceElement] Not Null,
	[ScopeDescription]    [App_DataDictionary].[typeDescription] Null,
	[IsDatabaseScope]     Bit Not Null, -- Helper flag. Indicates that this is for Databases.
	[IsLibraryScope]      Bit Not Null, -- Helper Flag. Indicates that this is for Library (.Net code).
	-- Keys
	CONSTRAINT [PK_ApplicationScope] PRIMARY KEY CLUSTERED ([ScopeId] ASC),
	CONSTRAINT [FK_ApplicationScopeParent] FOREIGN KEY ([ScopeParentId]) REFERENCES [App_DataDictionary].[ApplicationScope] ([ScopeId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_ApplicationScope]
    ON [App_DataDictionary].[ApplicationScope]([ScopeParentId] ASC, [ScopedElementName] ASC);
GO