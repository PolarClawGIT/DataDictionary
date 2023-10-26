CREATE VIEW [App_DataDictionary].[ModelAttributeAlias_AK]
WITH SCHEMABINDING AS
-- Enforces rule: Attribute Alias must be unique within the Model.
Select	M.[ModelId],
		C.[AttributeId],
		C.[DatabaseName],
		C.[SchemaName],
		C.[ObjectName],
		C.[ElementName],
		-- Key would be longer then the 900 characters allowed. Binary Checksum is used as a work-around.
		BINARY_CHECKSUM(C.[DatabaseName], C.[SchemaName], C.[ObjectName]) As [ObjectChecksum]
From	[App_DataDictionary].[DomainAttributeAlias] C
		Inner Join [App_DataDictionary].[ModelAttribute] M
		On	C.[AttributeId] = M.[AttributeId]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_ModelAttributeAlias]
    ON [App_DataDictionary].[ModelAttributeAlias_AK]([ModelId] ASC, [ObjectChecksum] ASC, [ElementName] ASC)
GO