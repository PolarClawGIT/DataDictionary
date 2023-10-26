CREATE VIEW [App_DataDictionary].[DomainProcessAttribute]
WITH SCHEMABINDING AS
Select	D.[ProcessId],
		D.[ProcessDependencyId],
		A.[AttributeId]
From	[App_DataDictionary].[DomainProcessDependency] D
		Inner Join [App_DataDictionary].[DomainAttributeAlias] A
		ON	D.[DatabaseName] = A.[DatabaseName] And
			D.[SchemaName] = A.[SchemaName] And
			D.[ObjectName] = A.[ObjectName] And
			D.[ElementName] = A.[ElementName]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_DomainProcessAttribute]
    ON [App_DataDictionary].[DomainProcessAttribute]([ProcessId] ASC, [ProcessDependencyId] ASC, [AttributeId] ASC)
GO