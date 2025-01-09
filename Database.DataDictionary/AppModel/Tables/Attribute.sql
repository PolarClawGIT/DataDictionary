CREATE TABLE [AppModel].[Attribute]
(	-- An ER diagram attribute is is associated with a Column or Parameter.
	[AttributeId]			UniqueIdentifier Not Null CONSTRAINT [DF_AttributeId] DEFAULT (newid()),
	[AttributeTitle]		[App_DataDictionary].[typeTitle] Not Null,
	[AttributeDescription]	[App_DataDictionary].[typeDescription] Null,
	[AttributeName]			[AppModel].[typeQualifiedName]         Null,
	[DataType]				NVarChar(128) Null, -- Generic definition of the Data Type.
	[DataLength]			SmallInt Null, -- The Length/Maximum Size of the Data Type. Null generally = maximum possible.
	[DataPrecision]			TinyInt Null, -- The Precision of the Data Type. Generally only applies to numerics.
	[IsSingleValue]			Bit Null, -- A Simple Valued attribute has a distinct value (not Multi Valued)
--	[IsMultiValue]			As (convert(bit, case when [IsSingleValue]=(0) then (1) when [IsSingleValue]=(1) then (0) end)),
	[IsSimpleType]			Bit Null, -- A Simple attribute (not Composite)
--	[IsCompositeType]		As (convert(bit, case when [IsSimpleType]=(0) then (1) when [IsSimpleType]=(1) then (0) end)),
--  [IsDerived]				AS (convert(Bit, case when [IsIntegral]=(1) then (0) when [IsDerived]=(0) then (1) end)),
	[IsIntegral]			Bit Null, -- An Integral attribute is not-computed from other attribute(s) (not Derived) 
	[IsNullable]			Bit Null, -- A Null-able attribute can contain a Null Value
--	[IsValued]				AS (convert(Bit, case when [IsNullable]=(0) then (1) when [IsNullable]=(1) then (0) end)),
	[IsKey]					Bit Null, -- A Key attribute can identify an Entity
	-- Temporal History Support
	[SysStart]				DateTime2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Attribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]				DateTime2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Attribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Attribute] PRIMARY KEY CLUSTERED ([AttributeId] ASC),
	CONSTRAINT [CK_Attribute_DataLength] CHECK ([DataLength] is Null or [DataLength] >= 0)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[Attribute]))
GO

