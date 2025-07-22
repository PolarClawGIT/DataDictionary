CREATE FUNCTION [AppSecurity].[funcScriptingAuthorization] (
	@TemplateId UniqueIdentifier, 
	@IsAuthorized Bit = 1
		-- Null: return value only if [IsApplication] or [IsDbWriter] is true (for Security Policy).
		-- 1: return values only if [IsAuthorized] is true
		-- 0: return values only if [IsAuthorized] is false
	)
Returns Table With SchemaBinding as Return
-- Row Level Security. Returns zero or one row.
With [Authorization] As (
	Select	[PrincipalLogin],
			[PrincipalId],
			[IsApplication],
			[IsDbWriter],
			[IsCatalogAdmin],
			[IsCatalogOwner],
			[HasOwner],
			[IsOwner],
			[IsGrant],
			[IsDeny],
			Convert(Bit, Case
				When [IsApplication] = 0 And [IsDbWriter] = 1 Then 1
				When [IsApplication] = 1 And [IsScriptAdmin] = 1 Then 1
				When [IsApplication] = 1 And [IsScriptOwner] = 1 And [HasOwner] = 0 Then 1
				When @TemplateId Not In (Select [TemplateId] From [AppScript].[Template]) Then 0
				When [IsApplication] = 1 And [IsOwner] = 1 Then 1
				When [IsApplication] = 1 And [IsGrant] = 1 And [IsDeny] = 0 Then 1
				Else 0 End)
				As [IsAuthorized]
	From	[AppSecurity].[funcAuthorization](@TemplateId))
Select	[PrincipalLogin],
		[PrincipalId],
		[IsApplication],
		[IsDbWriter],
		[IsCatalogAdmin],
		[IsCatalogOwner],
		[HasOwner],
		[IsOwner],
		[IsGrant],
		[IsDeny],
		[IsAuthorized]
From	[Authorization]
Where	(@TemplateId is Null And @IsAuthorized is Null And ([IsApplication] = 1 or [IsDbWriter] = 1)) Or
		([IsAuthorized] = @IsAuthorized)
GO