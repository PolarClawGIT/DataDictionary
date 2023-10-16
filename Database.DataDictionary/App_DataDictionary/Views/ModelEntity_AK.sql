CREATE VIEW [App_DataDictionary].[ModelEntity_AK]
WITH SCHEMABINDING AS
-- Enforces rule: EntityTitle must be unique within the Model.
Select	M.[ModelId],
		C.[EntityId],
		C.[EntityTitle]
From	[App_DataDictionary].[DomainEntity] C
		Inner Join [App_DataDictionary].[ModelEntity] M
		On	C.[EntityId] = M.[EntityId]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_ModelEntity]
    ON [App_DataDictionary].[ModelEntity_AK]([ModelId] ASC, [EntityTitle] ASC)
GO