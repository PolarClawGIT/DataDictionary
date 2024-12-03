Select	[CONSTRAINT_CATALOG] As [DatabaseName],
		[CONSTRAINT_SCHEMA] As [SchemaName],
		[CONSTRAINT_NAME] As [ConstraintName],
		[TABLE_NAME] As [TableName],
		[CONSTRAINT_TYPE] As [ConstraintType],*
From	[INFORMATION_SCHEMA].[TABLE_CONSTRAINTS]