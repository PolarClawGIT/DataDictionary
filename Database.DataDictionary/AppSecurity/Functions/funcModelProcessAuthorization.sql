CREATE FUNCTION [AppSecurity].[funcModelProcessAuthorization]
(
	@ModelId UniqueIdentifier,
	@ProcessId UniqueIdentifier,
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
			[IsModelAdmin],
			[IsModelOwner],
			[HasOwner],
			[IsOwner],
			[IsGrant],
			[IsDeny],
			Convert(Bit, Case
				When [IsApplication] = 0 And [IsDbWriter] = 1 Then 1
				When [IsApplication] = 1 And [IsModelAdmin] = 1 Then 1
				When [IsApplication] = 1 And [IsModelOwner] = 1 And [HasOwner] = 0 Then 1
				When @ModelId Not In (Select [ModelId] From [AppModel].[Model]) Then 0
				When [IsApplication] = 1 And [IsOwner] = 1 Then 1
				When [IsApplication] = 1 And [IsGrant] = 1 And [IsDeny] = 0 Then 1
				Else 0 End)
				As [IsAuthorized]
	From	[AppModel].[Model] M
			Left Join [AppModel].[ModelProcess] A
			On	M.[ModelId] = A.[ModelId] And
				A.[ProcessId] = @ProcessId
			Cross Apply [AppSecurity].[funcAuthorization](IsNull(M.[ModelId], @ModelId))
			)
Select	[PrincipalLogin],
		[PrincipalId],
		Convert(Bit, Max(Convert(Int, [IsApplication]))) As [IsApplication],
		Convert(Bit, Max(Convert(Int, [IsDbWriter]))) As [IsDbWriter],
		Convert(Bit, Max(Convert(Int, [IsModelAdmin]))) As [IsModelAdmin],
		Convert(Bit, Max(Convert(Int, [IsModelOwner]))) As [IsModelOwner],
		Convert(Bit, Max(Convert(Int, [HasOwner]))) As [HasOwner],
		Convert(Bit, Min(Convert(Int, [IsOwner]))) As [IsOwner],
		Convert(Bit, Min(Convert(Int, [IsGrant]))) As [IsGrant],
		Convert(Bit, Max(Convert(Int, [IsDeny]))) As [IsDeny],
		Convert(Bit, Min(Convert(Int, [IsAuthorized]))) As [IsAuthorized]
From	[Authorization]
Where	(@ProcessId is Null And @IsAuthorized is Null And ([IsApplication] = 1 or [IsDbWriter] = 1)) Or
		([IsAuthorized] = @IsAuthorized)
Group By [PrincipalLogin],
		[PrincipalId]
Go