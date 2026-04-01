CREATE VIEW [ProofOfConcept].[viewDataObject] AS
-- Temporal View
With [Data] As (
	Select	[ObjectId],
			[ParentObjectId],
			[AppGeneral].[funcCreatePath]([ObjectMember], Null) As [ObjectPath],
			[SysStart],
			[SysEnd]
	From	[ProofOfConcept].[DataObject]
	Union All
	Select	D.[ObjectId],
			NullIf(P.[ParentObjectId], D.[ObjectId]) As [ParentObjectId],
			[AppGeneral].[funcCreatePath](P.[ObjectMember], D.[ObjectPath]) As [ObjectPath],
			Greatest(D.[SysStart], P.[SysStart]) As [SysStart],
			Least(D.[SysEnd], P.[SysEnd]) As [SysEnd]
	From	[Data] D
			Inner Join [ProofOfConcept].[DataObject] P
			On	D.[ParentObjectId] = P.[ObjectId] And
				-- Temporal, multiple rows could be returned. Do not have confidence in this.
				((D.[SysStart] >= P.[SysStart] And D.[SysStart] < P.[SysEnd]) Or
				 (P.[SysStart] >= D.[SysStart] And P.[SysStart] < P.[SysEnd]))),
	[Dates] As (
		Select	[ObjectId],
				[SysStart],
				[SysEnd]
		From	[ProofOfConcept].[DataObject]
		/*Union -- TODO: Temporal not yet implemented
		Select	[ObjectId],
				[SysStart],
				[SysEnd]
		From	[HsScript].[DataObject]
		Where	[SysStart] != [SysEnd]*/)
Select	D.[TemplateId],
		D.[ObjectId],
		D.[ObjectScope],
		H.[ObjectPath],
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
From	[ProofOfConcept].[DataObject] D
		Inner Join [Data] H
		On	D.[ObjectId] = H.[ObjectId] And
			H.[ParentObjectId] is Null And
			-- Temporal, multiple rows could be returned. Do not have confidence in this.
			((D.[SysStart] >= H.[SysStart] And D.[SysStart] < D.[SysEnd]) Or
			 (H.[SysStart] >= D.[SysStart] And H.[SysStart] < H.[SysEnd]))	
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[ObjectId] = D.[ObjectId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[ObjectId] = D.[ObjectId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
GO
GO