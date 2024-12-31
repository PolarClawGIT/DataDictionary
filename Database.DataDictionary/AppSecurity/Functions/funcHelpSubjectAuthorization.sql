CREATE FUNCTION [AppSecurity].[funcHelpSubjectAuthorization] (
	@HelpId UniqueIdentifier = null, 
	@IsAuthorized Bit = 1
		-- Null: return value only if [IsApplication] or [IsDbWriter] is true (for Security Policy).
		-- 1: return values only if [IsAuthorized] is true
		-- 0: return values only if [IsAuthorized] is false
	)
Returns Table With SchemaBinding as Return
-- Row Level Security. Returns zero (deny) or one row.
With [Authorization] As (
	Select	[PrincipalLogin],
			[PrincipalId],
			[IsApplication],
			[IsDbWriter],
			[IsHelpAdmin],
			[IsHelpOwner],
			[HasOwner],
			[IsOwner],
			[IsGrant],
			[IsDeny],
			Convert(Bit, Case
				When [IsApplication] = 0 And [IsDbWriter] = 1 Then 1
				When [IsApplication] = 1 And [IsHelpAdmin] = 1 Then 1
				When [IsApplication] = 1 And [IsHelpOwner] = 1 And [HasOwner] = 0 Then 1
				When @HelpId Not In (Select [HelpId] From [AppGeneral].[HelpSubject]) Then 0
				When [IsApplication] = 1 And [IsOwner] = 1 Then 1
				When [IsApplication] = 1 And [IsGrant] = 1 And [IsDeny] = 0 Then 1
				Else 0 End)
				As [IsAuthorized]
	From	[AppSecurity].[funcAuthorization](@HelpId))
Select	[PrincipalLogin],
		[PrincipalId],
		[IsApplication],
		[IsDbWriter],
		[IsHelpAdmin],
		[IsHelpOwner],
		[HasOwner],
		[IsOwner],
		[IsGrant],
		[IsDeny],
		[IsAuthorized]
From	[Authorization]
Where	(@HelpId is Null And @IsAuthorized is Null And ([IsApplication] = 1 or [IsDbWriter] = 1)) Or
		([IsAuthorized] = @IsAuthorized)
GO