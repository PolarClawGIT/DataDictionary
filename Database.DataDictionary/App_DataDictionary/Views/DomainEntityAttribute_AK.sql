CREATE VIEW [App_DataDictionary].[DomainEntityAttribute_AK]
WITH SCHEMABINDING AS
-- Enforces Natural Key for the Domain Entity Attribute
Select	E.[EntityAttributeId],
		E.[EntityId],
		--E.[AttributeId],
		E.[AttributeName] --IsNull(E.[AttributeName], A.[AttributeTitle]) As [AttributeName]
From	[AppModel].[EntityAttribute] E
		--Inner Join [AppModel].[Attribute] A
		--On	E.[AttributeId] = A.[AttributeId]
GO
