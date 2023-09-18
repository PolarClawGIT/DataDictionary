﻿CREATE TYPE [App_DataDictionary].[typeDomainEntityProperty] AS TABLE (
	-- TIP: This matches the C# DataTable structure
    [EntityId]              UNIQUEIDENTIFIER NULL,
	[PropertyId]            UNIQUEIDENTIFIER NULL,
	[PropertyValue]         NVarChar(4000)  NULL,
	[DefinitionText]        NVarChar(Max) Null,
    [SysStart]              DATETIME2 (7) NULL);