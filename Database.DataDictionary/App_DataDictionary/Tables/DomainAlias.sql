CREATE TABLE [App_DataDictionary].[DomainAlias]
(   -- This is used to store the Alias data for Attributes and Entities.
	-- The Alias Name can be a Database Object (Database, Schema, Object, Element) or Library NameSpace (NameSpace, Class, Method, Property).
	-- Element Name lengths are driven by TSQL Indexing limitations.
	-- This is shorter then what is allowed in .Net languages.
	-- Each row holds an element of the name.
	-- The design "shares" the alias naming across the entire application, not just a Model.
	-- This allows related objects to be identified across Models.
	-- The design assume that the UI does not work with the IDs but the full name.
	-- The database is responsible for assigning and maintains the Ids.
	-- The full name is combined with a delimiter. Databases and Library use a period delimiter.
	-- This structure is a hierarchy.
	[AliasId]          UniqueIdentifier Not Null CONSTRAINT [DF_DomainAliasId] DEFAULT (newid()),
	[AliasParentId]    UniqueIdentifier Null,
	[AliasElementName] [App_DataDictionary].[typeNameSpaceElement] Not Null,
	-- Keys
	CONSTRAINT [PK_DomainAlias] PRIMARY KEY CLUSTERED ([AliasId] ASC),
	CONSTRAINT [FK_DomainAliasParent] FOREIGN KEY ([AliasParentId]) REFERENCES [App_DataDictionary].[DomainAlias] ([AliasId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainAlias]
    ON [App_DataDictionary].[DomainAlias]([AliasElementName] ASC, [AliasParentId] ASC);
GO