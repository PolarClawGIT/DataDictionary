CREATE FUNCTION [AppSecurity].[funcCatalogAuthorization](@ObjectId UniqueIdentifier, @OwnerOnly Bit)
Returns Table With SchemaBinding
As Return 
With [Object] As (
	Select	[CatalogId],
			[CatalogId] As [ObjectId]
	From	[AppCatalog].[Catalog]
	Where	[CatalogId] = @ObjectId
	Union
	Select	[CatalogId],
			[SchemaId] As [ObjectId]
	From	[App_DataDictionary].[DatabaseSchema]
	Where	[SchemaId] = @ObjectId
	-- TODO Add rest of tables
	)
Select	Convert(Bit, 1) As [IsAllowed]
From	[Object] O
		Cross Apply [AppSecurity].[funcAuthorization](O.[CatalogId]) F
Where	[IsDbWriter] = 1 Or
		[IsCatalogAdmin] = 1 Or
		([IsCatalogOwner] = 1 And [IsOwner] = 1) Or
		([IsGrant] = 1 And [IsDeny] = 0 And IsNull(@OwnerOnly,0) = 0)
GO