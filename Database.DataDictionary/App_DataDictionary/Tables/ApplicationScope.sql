CREATE TABLE [App_DataDictionary].[ApplicationScope]
(
	-- This represents Database Object Name and Name Space Level or Scope.
	-- This is a hierarchy that is expected to be static.
	-- Reference: https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16
	-- Reference: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/
	[ScopeId]             Int Not Null,
	[ScopeParentId]       Int Null,	
	[ScopeElement]        [App_DataDictionary].[typeScopeElement] Not Null,
	[ScopeDescription]    [App_DataDictionary].[typeDescription] Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ApplicationScopeModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ApplicationScopeSysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ApplicationScopeSysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ApplicationScope] PRIMARY KEY CLUSTERED ([ScopeId] ASC),
	CONSTRAINT [FK_ApplicationScopeParent] FOREIGN KEY ([ScopeParentId]) REFERENCES [App_DataDictionary].[ApplicationScope] ([ScopeId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_ApplicationScope]
    ON [App_DataDictionary].[ApplicationScope]([ScopeParentId] ASC, [ScopeElement] ASC)
GO