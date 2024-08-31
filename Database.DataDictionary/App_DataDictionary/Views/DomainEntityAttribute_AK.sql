CREATE VIEW [App_DataDictionary].[DomainEntityAttribute_AK]
WITH SCHEMABINDING AS
-- Enforces Natural Key for the Domain Entity Attribute
Select	E.[EntityAttributeId],
		E.[EntityId],
		E.[AttributeId],
		IsNull(E.[AttributeName], A.[AttributeTitle]) As [AttributeName]
From	[App_DataDictionary].[DomainEntityAttribute] E
		Inner Join [App_DataDictionary].[DomainAttribute] A
		On	E.[AttributeId] = A.[AttributeId]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_DomainEntityAttribute]
    ON [App_DataDictionary].[DomainEntityAttribute_AK]([EntityAttributeId])
GO
CREATE UNIQUE INDEX [AK_DomainEntityAttribute]
    ON [App_DataDictionary].[DomainEntityAttribute_AK]([EntityId], [AttributeName])
GO