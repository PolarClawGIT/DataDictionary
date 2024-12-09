CREATE FUNCTION [AppSecurity].[funcModelAuthorization](@ModelId UniqueIdentifier, @OwnerOnly Bit)
Returns Table With SchemaBinding
As Return 
Select	Convert(Bit, 1) As [IsAllowed]
From	[AppModel].[Model] O
		Cross Apply [AppSecurity].[funcAuthorization](O.[ModelId]) F
Where	O.[ModelId] = @ModelId And
		([IsDbWriter] = 1 Or
		 [IsCatalogAdmin] = 1 Or
		 ([IsCatalogOwner] = 1 And [IsOwner] = 1) Or
		 ([IsGrant] = 1 And [IsDeny] = 0 And IsNull(@OwnerOnly,0) = 0))
GO