CREATE FUNCTION [AppSecurity].[funcCatalogAuthorization] (@CatalogId UniqueIdentifier)
-- Checks Authorization for a Catalog. 1 = Grant, 0 = Deny
RETURNS Int AS
BEGIN
	RETURN (
		Select	IsNull(Max(1), 0) As [Result]
		From	[AppSecurity].[funcAuthorization](@CatalogId)
		Where	([IsApplication] = 1 And [IsCatalogAdmin] = 1) or
				([IsApplication] = 1 And [IsCatalogOwner] = 1 And [HasOwner] = 0) Or -- New Catalog Only
				([IsApplication] = 1 And [IsOwner] = 1) Or
				([IsApplication] = 1 And [IsGrant] = 1 And [IsDeny] = 0) Or
				([IsApplication] = 0 And [IsDbWriter]  = 1)
		)
END
