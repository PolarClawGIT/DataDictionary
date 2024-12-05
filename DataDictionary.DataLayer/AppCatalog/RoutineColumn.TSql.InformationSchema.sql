-- Remember: Resource files do not update until they are saved directly.
Select	I.[TABLE_CATALOG] As [DatabaseName],
		I.[TABLE_SCHEMA] As [SchemaName],
		I.[TABLE_NAME] As [RoutineName],
		Case
		When [ROUTINE_TYPE] In ('PROCEDURE') Then 'Procedure'
		When [ROUTINE_TYPE] In ('FUNCTION') Then 'Function'
		Else [ROUTINE_TYPE] 
		End As [RoutineType],
		I.[COLUMN_NAME] As [ColumnName],
		I.[ORDINAL_POSITION] As [OrdinalPosition],
		iif(I.[IS_NULLABLE] In ('YES','TRUE','1'),1,0) As [IsNullable],
		I.[DATA_TYPE] As [DataType],
		I.[COLUMN_DEFAULT] As [ColumnDefault],
		I.[CHARACTER_MAXIMUM_LENGTH] As [CharacterMaximumLength],
		I.[CHARACTER_OCTET_LENGTH] As [CharacterOctetLength],
		I.[NUMERIC_PRECISION] As [NumericPrecision],
		I.[NUMERIC_PRECISION_RADIX] As [NumericPrecisionRadix],
		I.[NUMERIC_SCALE] As [NumericScale],
		I.[DATETIME_PRECISION] As [DateTimePrecision],
		I.[CHARACTER_SET_CATALOG] As [CharacterSetCatalog],
		I.[CHARACTER_SET_SCHEMA] As [CharacterSetSchema],
		I.[CHARACTER_SET_NAME] As [CharacterSetName],
		I.[COLLATION_CATALOG] As [CollationCatalog],
		I.[COLLATION_SCHEMA] As [CollationSchema],
		I.[COLLATION_NAME] As [CollationName],
		I.[DOMAIN_CATALOG] As [DomainCatalog],
		I.[DOMAIN_SCHEMA] As [DomainSchema],
		I.[DOMAIN_NAME] As [DomainName],
		C.[is_identity] As [IsIdentity],
		--C.[is_hidden] As [IsHidden],
		C.[is_computed] As [IsComputed],
		P.[definition] As [ComputedDefinition]
		--NullIf(C.[generated_always_type_desc],'NOT_APPLICABLE') As [GeneratedAlwayType],
From	[INFORMATION_SCHEMA].[ROUTINE_COLUMNS] I
		Left Join [INFORMATION_SCHEMA].[ROUTINES] T
		On	I.[TABLE_CATALOG] = T.[SPECIFIC_CATALOG] And
			I.[TABLE_SCHEMA] = T.[SPECIFIC_SCHEMA] And
			I.[TABLE_NAME] = T.[SPECIFIC_NAME]
		-- Some data does not exist in InfoSchema
		Left Join [sys].[Columns] C
		On	I.[TABLE_SCHEMA] = Object_Schema_Name(C.[object_id]) And
			I.[TABLE_NAME] = Object_Name(C.[object_id]) And
			I.[COLUMN_NAME] = C.[name]
		Left Join [sys].[computed_columns] P
		On	C.[object_id] = P.[object_id] And
			C.[column_id] = P.[column_id]
