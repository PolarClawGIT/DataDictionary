-- This type is used to manage the NameSpace/NameSpacePath that is used by the Application to identify objects.
-- This can be used to pass that value between procedures that work with specific implementation of NameSpace.
-- Implementation: Subject Area, Domain Alias, Catalog (and children), Library (and children).
CREATE TYPE [App_DataDictionary].[typeNameSpace] AS TABLE
(
	[NameSpaceId]            UniqueIdentifier NULL,
	[NameSpace]             [App_DataDictionary].[typeNameSpacePath] Null
)
