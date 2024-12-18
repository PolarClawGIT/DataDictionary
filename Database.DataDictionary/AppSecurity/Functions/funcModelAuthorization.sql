CREATE FUNCTION [AppSecurity].[funcModelAuthorization](@ModelId UniqueIdentifier, @OwnerOnly Bit)
Returns Table With SchemaBinding
As Return 
Select	Convert(Bit, 1) As [IsAllowed]
From	[AppSecurity].[funcAuthorization](@ModelId) F
Where	([IsDbWriter] = 1 Or
		 [IsModelAdmin] = 1 Or
		 ([IsModelOwner] = 1 And [IsOwner] = 1) Or
		 ([IsGrant] = 1 And [IsDeny] = 0 And IsNull(@OwnerOnly,0) = 0))
GO