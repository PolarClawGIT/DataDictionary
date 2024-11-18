CREATE FUNCTION [AppSecurity].[funcCatalogReferenceAuthorization](@ReferenceId UniqueIdentifier, @OwnerOnly Bit)
Returns Table With SchemaBinding
As Return 
Select	Convert(Bit, 1) As [IsAllowed]
From	[AppCatalog].[Catalog] O
		Inner Join [AppCatalog].[Reference] S
		On	O.[CatalogId] = S.[CatalogId]
		Cross Apply [AppSecurity].[funcAuthorization](O.[CatalogId]) F
Where	S.[ReferenceId] = @ReferenceId And
		([IsDbWriter] = 1 Or
		 [IsCatalogAdmin] = 1 Or
		 ([IsCatalogOwner] = 1 And [IsOwner] = 1) Or
		 ([IsGrant] = 1 And [IsDeny] = 0 And IsNull(@OwnerOnly,0) = 0))
GO