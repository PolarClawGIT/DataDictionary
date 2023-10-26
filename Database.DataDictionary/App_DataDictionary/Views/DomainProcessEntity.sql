CREATE VIEW [App_DataDictionary].[DomainProcessEntity]
WITH SCHEMABINDING AS
Select	D.[ProcessId],
		D.[ProcessDependencyId],
		E.[EntityId]
From	[App_DataDictionary].[DomainProcessDependency] D
		Inner Join [App_DataDictionary].[DomainEntityAlias] E
		ON	D.[DatabaseName] = E.[DatabaseName] And
			D.[SchemaName] = E.[SchemaName] And
			D.[ObjectName] = E.[ObjectName] And
			D.[ElementName] is Null
GO
CREATE UNIQUE CLUSTERED INDEX [PK_DomainProcessEntity]
    ON [App_DataDictionary].[DomainProcessEntity]([ProcessId] ASC, [ProcessDependencyId] ASC, [EntityId])
GO