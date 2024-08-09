CREATE VIEW [App_DataDictionary].[DatabaseObject] As
-- This represent the logical parent to the Database Objects (Table, Routine, Domain, Constraint)
-- Because of Union clause, these Keys cannot be enforced. Column [IsUnique] checks this.
-- PK: [ObjectId]
-- AK: [CatalogId], [DatabaseName], [SchemaName], [ObjectName]
With [Object] As (
	Select	[CatalogId],
			[DatabaseName],
			[SchemaName],
			[ConstraintName] As [ObjectName],
			[ConstraintId] As [ObjectId]
	From	[App_DataDictionary].[DatabaseConstraint_AK]
	Union
	Select	[CatalogId],
			[DatabaseName],
			[SchemaName],
			[DomainName] As [ObjectName],
			[DomainId] As [ObjectId]
	From	[App_DataDictionary].[DatabaseDomain_AK]
	Union
	Select	[CatalogId],
			[DatabaseName],
			[SchemaName],
			[RoutineName] As [ObjectName],
			[RoutineId] As [ObjectId]
	From	[App_DataDictionary].[DatabaseRoutine_AK]
	Union
	Select	[CatalogId],
			[DatabaseName],
			[SchemaName],
			[TableName] As [ObjectName],
			[TableId] As [ObjectId]
	From	[App_DataDictionary].[DatabaseTable_AK])
Select	[CatalogId],
		[DatabaseName],
		[SchemaName],
		[ObjectName],
		[ObjectId],
		-- Row is a Valid Unique Key.
		Convert(Bit, IIF(
			Count([CatalogId]) Over (Partition By [ObjectId]) = 1 And
			Count([CatalogId]) Over (Partition By [CatalogId], [DatabaseName], [SchemaName], [ObjectName]) = 1,
			1,0)) As [IsUnique]
From	[Object]

GO


