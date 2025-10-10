CREATE VIEW [AppScript].[TemplateNodeOwnerHs] AS
-- Temporal View
With [Data] As (
		Select	N.[NodeId],
				P.[NodeOwnerId],
				N.[TemplateId],
				N.[NodeName],
				Greatest(N.[SysStart], P.[SysStart]) As [SysStart],
				Least(N.[SysEnd], P.[SysEnd]) As [SysEnd]
		From	[AppScript].[TemplateNode] N
				Left Join [AppScript].[TemplateNodeOwner] P
				On	N.[NodeId] = P.[NodeId] And
					N.[TemplateId] = P.[TemplateId]),
	[Tree] As (
		Select	[NodeId],
				Convert(UNIQUEIDENTIFIER, 0x0) As[NodeOwnerId],
				[TemplateId],
				[NodeName],
				[AppGeneral].[funcCreatePath]([NodeName], Null) As [NodePath],
				[AppGeneral].[funcCreatePath](Null, Null) As [NodeOwnerPath],
				Convert(NVarChar(Max),
					FormatMessage('/%I64d/', -- Under documented BigInt. See C++ PrintF
					Dense_Rank() Over (Order By [NodeName])))
					As [HierarchyId],
				[SysStart],
				[SysEnd]
		From	[Data]
		Where	[NodeOwnerId] is Null
		Union All
		Select	N.[NodeId],
				N.[NodeOwnerId],
				N.[TemplateId],
				N.[NodeName],
				[AppGeneral].[funcCreatePath](D.[NodePath], N.[NodeName]) As [NodePath],
				D.[NodePath] As [NodeOwnerPath],
				Convert(NVarChar(Max), FormatMessage('%s%I64d/', D.[HierarchyId],
					Row_Number() Over (Partition By D.[NodeId] Order By N.[NodeName])))
					As [HierarchyId],
				Greatest(D.[SysStart], N.[SysStart]) As [SysStart],
				Least(D.[SysEnd], N.[SysEnd]) As [SysEnd]
		From	[Tree] D
				Inner Join [Data] N
				On	D.[NodeId] = N.[NodeOwnerId] And
					D.[TemplateId] = N.[TemplateId]
					
					),
	[Dates] As (
		Select	[NodeId],
				[NodeOwnerId],
				[TemplateId],
				[SysStart],
				[SysEnd]
		From	[Data]
		-- TODO Query for Temporal
	)
Select	D.[NodeId],
		NullIf(D.[NodeOwnerId],Convert(UNIQUEIDENTIFIER, 0x0)) As [NodeOwnerId],
		D.[TemplateId],
		D.[NodeName],
		D.[NodePath],
		D.[NodeOwnerPath],
		D.[HierarchyId],
		-- Temporal Status
		D.[SysStart], -- AK, PK
		D.[SysEnd],
		IsNull(C.[ModifiedOn], D.[SysStart]) As [CreatedOn],
		C.[ModifiedBy] As [CreatedBy],
		IsNull(R.[ModifiedOn], NullIf(D.[SysEnd],'9999-12-31 23:59:59.9999999')) As [RemovedOn],
		R.[ModifiedBy] As [RemovedBy],
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] != D.[SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = D.[SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] is Null And D.[SysEnd] < SysUtcDateTime(), 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= D.[SysStart] And SysUtcDateTime() < D.[SysEnd], 1, 0)) As [IsCurrent]
From	[Tree] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[NodeId] = D.[NodeId] And
					[NodeOwnerId] = D.[NodeOwnerId] And
					[TemplateId] = D.[TemplateId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[NodeId] = D.[NodeId] And
					[NodeOwnerId] = D.[NodeOwnerId] And
					[TemplateId] = D.[TemplateId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
Go