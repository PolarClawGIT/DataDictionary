CREATE TABLE [AppModel].[Domain]
(	-- A Domain represents a data structure.
	-- Implementation varies based on language.
	-- SQL: Type
	-- C#: varies
	[DomainId]			UniqueIdentifier Not Null CONSTRAINT [DF_DomainId] DEFAULT (newid()),
	[DomainTitle]		[App_DataDictionary].[typeTitle] Not Null,
	[DomainDescription]	[App_DataDictionary].[typeDescription] Null,
	[DomainName]        [AppModel].[typeQualifiedName]         Null,
	[IsCommon]          Bit Not Null CONSTRAINT [DF_DomainIsCommon] DEFAULT(0), -- Common Domain are shared by all Models.
	[IsPrimitive]		Bit Not Null CONSTRAINT [DF_DomainPrimitive] DEFAULT (0), -- Does this represent a primitive types that are foundations for all other types.
	-- Data Definition: 
	-- These values are expected to be used by the scripting engine to build a native DataType.
	-- TODO: Consider moving flags from Attribute into this table then Attribute specifies the Domain.
	-- TODO: Consider allowing Entity to specify a Domain.
	-- TODO: Consider reversing the concept. Entity & Process has the Implement while Attribute has the Data Type.
	[DataType]			NVarChar(128) Null, -- Generic definition of the Data Type.
	[DataLength]		Int Null, -- The Length/Maximum Size of the Data Type. Null generally = maximum possible.
	[DataPrecision]		Int Null, -- The Precision of the Data Type. Generally only applies to numerics.
	-- TODO: Add System Version later once the schema is locked down
	[SysStart]			DateTime2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Domain_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]			DateTime2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Domain_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Domain] PRIMARY KEY CLUSTERED ([DomainId] ASC),
)
