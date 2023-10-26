CREATE VIEW [App_DataDictionary].[ModelEntityAlias_AK]
WITH SCHEMABINDING AS
-- Enforces rule: Entity Alias must be unique within the Model.
Select	M.[ModelId],
		C.[EntityId],
		C.[DatabaseName],
		C.[SchemaName],
		C.[ObjectName],
		-- Key would be longer then the 900 characters allowed. Binary Checksum is used as a work-around.
		BINARY_CHECKSUM(C.[DatabaseName], C.[SchemaName], C.[ObjectName]) As [ObjectChecksum]
From	[App_DataDictionary].[DomainEntityAlias] C
		Inner Join [App_DataDictionary].[ModelEntity] M
		On	C.[EntityId] = M.[EntityId]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_ModelEntityAlias]
    ON [App_DataDictionary].[ModelEntityAlias_AK]([ModelId] ASC, [DatabaseName] ASC, [SchemaName] ASC, [ObjectName] ASC)
GO
CREATE UNIQUE INDEX [UK_ModelEntityAlias]
    ON [App_DataDictionary].[ModelEntityAlias_AK]([ModelId] ASC, [ObjectChecksum] ASC)
GO