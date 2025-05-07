CREATE PROCEDURE [AppModel].[procGetRelationship]
		@ModelId UniqueIdentifier = Null,
		@RelationshipId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Model Relationship.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDateTime())

;With [Events] As (
	Select	[RelationshipId],
			[SysStart]
	From	[AppModel].[RelationshipHs] For System_Time All
	Union
	Select	[RelationshipId],
			[SysStart]
	From	[AppModel].[RelationshipAliasHs] For System_Time All
	Union
	Select	[RelationshipId],
			[SysStart]
	From	[AppModel].[RelationshipDefinitionHs] For System_Time All
	Union
	Select	[RelationshipId],
			[SysStart]
	From	[AppModel].[RelationshipPropertyHs] For System_Time All
	Union
	Select	[RelationshipId],
			[SysStart]
	From	[AppModel].[RelationshipSubjectAreaHs] For System_Time All
	Union
	Select	[RelationshipId],
			[SysStart]
	From	[AppModel].[RelationshipAttributeHs] For System_Time All),
[Dates] As (
	Select	[RelationshipId],
			[SysStart],
			IsNull(Min([SysStart]) Over (
				Partition By [RelationshipId]
				Order By [SysStart]
				Rows Between 1 Following and 1 Following),
				'9999-12-31 23:59:59.9999999')
			As [SysEnd]
	From	[Events]
	Group By [RelationshipId],
			[SysStart])
Select	D.[RelationshipId],
		D.[RelationshipTitle],
		D.[RelationshipDescription],
		D.[RelationshipName],
		D.[RelationshipType],
		D.[OwnerAliasPath],
		D.[RefrenceAliasPath],
		-- Temporal Data
		--T.[SysStart],
		--Least(T.[SysEnd], D.[SysEnd]) As [SysEnd],
		D.[CreatedOn],
		D.[CreatedBy],
		D.[RemovedOn],
		D.[RemovedBy],
		Convert(Bit, IIF(D.[IsInserted] = 1 And T.[SysStart] = D.[SysStart], 1,0)) As [IsInserted],
		Convert(Bit, IIF(D.[IsUpdated] = 1 Or T.[SysStart] <> D.[SysStart], 1,0)) As [IsUpdated],
		Convert(Bit, IIF(D.[IsDeleted] = 1 And T.[SysStart] = D.[SysStart], 1,0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= T.[SysStart] And SysUtcDateTime() < Least(T.[SysEnd], D.[SysEnd]), 1, 0)) As [IsCurrent]
From	[Dates] T
		Inner Join [AppModel].[RelationshipHs] For System_Time All D
		On	T.[RelationshipId] = D.[RelationshipId] And
			T.[SysStart] >= D.[SysStart] And
			T.[SysStart] < D.[SysEnd]
Where	(@IncludeHistory = 1 Or (T.[SysStart] <= @AsOfUtcDate And Least(T.[SysEnd], D.[SysEnd]) > @AsOfUtcDate)) And
		(@RelationshipId is Null Or @RelationshipId = D.[RelationshipId]) And
		(@ModelId is Null Or @ModelId In (
			Select	[ModelId]
			From	[AppModel].[ModelRelationship] For System_Time As of @AsOfUtcDate
			Where	D.[RelationshipId] = [RelationshipId]))
GO
