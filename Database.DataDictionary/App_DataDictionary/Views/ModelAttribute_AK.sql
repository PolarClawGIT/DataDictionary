CREATE VIEW [App_DataDictionary].[ModelAttribute_AK]
WITH SCHEMABINDING AS
-- Enforces rule: AttributeTitle must be unique within the Model.
Select	M.[ModelId],
		C.[AttributeId],
		C.[AttributeTitle]
From	[App_DataDictionary].[DomainAttribute] C
		Inner Join [App_DataDictionary].[ModelAttribute] M
		On	C.[AttributeId] = M.[AttributeId]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_ModelAttribute]
    ON [App_DataDictionary].[ModelAttribute_AK]([ModelId] ASC, [AttributeTitle] ASC)
GO