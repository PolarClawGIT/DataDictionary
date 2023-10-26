CREATE VIEW [App_DataDictionary].[DomainEntityAttribute]
WITH SCHEMABINDING AS
Select	E.[EntityId],
		A.[AttributeId]
From	[App_DataDictionary].[DomainEntityAlias] E
		Inner Join [App_DataDictionary].[DomainAttributeAlias] A
		ON	E.[DatabaseName] = A.[DatabaseName] And
			E.[SchemaName] = A.[SchemaName] And
			E.[ObjectName] = A.[ObjectName]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_DomainEntityAttribute]
    ON [App_DataDictionary].[DomainEntityAttribute]([EntityId] ASC, [AttributeId] ASC)
GO