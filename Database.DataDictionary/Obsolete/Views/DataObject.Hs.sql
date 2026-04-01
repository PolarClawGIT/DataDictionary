CREATE VIEW [Obsolete].[DataObjectHs]As
-- Temporal View
With [Data] As (
	Select	[ObjectNameId],
			[ParentNameId],
			[AppGeneral].[funcCreatePath]([ObjectMember], Null) As [ObjectPath],
			[SysStart],
			[SysEnd]
	From	[Obsolete].[DataObjectName]
	Union All
	Select	D.[ObjectNameId],
			NullIf(P.[ParentNameId], D.[ObjectNameId]) As [ParentNameId],
			[AppGeneral].[funcCreatePath](P.[ObjectMember], D.[ObjectPath]) As [ObjectPath],
			Greatest(D.[SysStart], P.[SysStart]) As [SysStart],
			Least(D.[SysEnd], P.[SysEnd]) As [SysEnd]
	From	[Data] D
			Inner Join [Obsolete].[DataObjectName] P
			On	D.[ParentNameId] = P.[ObjectNameId] And
				-- Temporal, multiple rows could be returned. Do not have confidence in this.
				((D.[SysStart] >= P.[SysStart] And D.[SysStart] < P.[SysEnd]) Or
				 (P.[SysStart] >= D.[SysStart] And P.[SysStart] < P.[SysEnd]))),
	[Dates] As (
		Select	[DataObjectId],
				[SysStart],
				[SysEnd]
		From	[Obsolete].[DataObject]
		/*Union -- TODO: Temporal not yet implemented
		Select	[DataObjectId],
				[SysStart],
				[SysEnd]
		From	[HsScript].[DataObject]
		Where	[SysStart] != [SysEnd]*/)
Select	D.[DataSourceId],
		D.[DataObjectId],
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
From	[Obsolete].[DataObject] D
		Inner Join [Data] H
		On	D.[ObjectNameId] = H.[ObjectNameId] And
			H.[ParentNameId] is Null And
			-- Temporal, multiple rows could be returned. Do not have confidence in this.
			((D.[SysStart] >= H.[SysStart] And D.[SysStart] < D.[SysEnd]) Or
			 (H.[SysStart] >= D.[SysStart] And H.[SysStart] < H.[SysEnd]))	
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[DataSourceId] = D.[DataSourceId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[DataSourceId] = D.[DataSourceId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
GO
