Select	P.[SPECIFIC_CATALOG] As [DatabaseName],
		P.[SPECIFIC_SCHEMA] As [SchemaName],
		P.[SPECIFIC_NAME] As [RoutineName],
		Case
		When [ROUTINE_TYPE] In ('PROCEDURE') Then 'Procedure'
		When [ROUTINE_TYPE] In ('FUNCTION') Then 'Function'
		Else [ROUTINE_TYPE] 
		End As [RoutineType],
		IIF(R.[ROUTINE_TYPE] IN ('FUNCTION') AND P.[ORDINAL_POSITION] = 0,'RETURN',P.[PARAMETER_NAME]) As [ParameterName],
		P.[ORDINAL_POSITION] As [OrdinalPosition],
		P.[DATA_TYPE] As [DataType],
		P.[CHARACTER_MAXIMUM_LENGTH] As [CharacterMaximumLength],
		P.[CHARACTER_OCTET_LENGTH] As [CharacterOctetLength],
		P.[NUMERIC_PRECISION] As [NumericPrecision],
		P.[NUMERIC_PRECISION_RADIX] As [NumericPrecisionRadix],
		P.[NUMERIC_SCALE] As [NumericScale],
		P.[DATETIME_PRECISION] As [DateTimePrecision],
		P.[CHARACTER_SET_CATALOG] As [CharacterSetCatalog],
		P.[CHARACTER_SET_SCHEMA] As [CharacterSetSchema],
		P.[CHARACTER_SET_NAME] As [CharacterSetName],
		P.[COLLATION_CATALOG] As [CollationCatalog],
		P.[COLLATION_SCHEMA] As [CollationSchema],
		P.[COLLATION_NAME] As [CollationName],
		P.[USER_DEFINED_TYPE_CATALOG] As [DomainCatalog],
		P.[USER_DEFINED_TYPE_SCHEMA] As [DomainSchema],
		P.[USER_DEFINED_TYPE_NAME] As [DomainName]
From	[INFORMATION_SCHEMA].[PARAMETERS] P
		Inner Join [INFORMATION_SCHEMA].[ROUTINES] R
		On	P.[SPECIFIC_CATALOG] = R.[SPECIFIC_CATALOG] And
			P.[SPECIFIC_SCHEMA] = R.[SPECIFIC_SCHEMA] And
			P.[SPECIFIC_NAME] = R.[SPECIFIC_NAME]