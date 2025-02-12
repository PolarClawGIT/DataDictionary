CREATE PROCEDURE [AppModel].[procGetAttribute]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Model Attribute.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDateTime())

;With [Events] As (
	Select	[AttributeId],
			[SysStart]
	From	[AppModel].[AttributeHs] For System_Time All
	Union
	Select	[AttributeId],
			[SysStart]
	From	[AppModel].[AttributeAliasHs] For System_Time All
	Union
	Select	[AttributeId],
			[SysStart]
	From	[AppModel].[AttributeDefinitionHs] For System_Time All
	Union
	Select	[AttributeId],
			[SysStart]
	From	[AppModel].[AttributePropertyHs] For System_Time All
	Union
	Select	[AttributeId],
			[SysStart]
	From	[AppModel].[AttributeSubjectAreaHs] For System_Time All),
[Dates] As (
	Select	[AttributeId],
			[SysStart],
			IsNull(Min([SysStart]) Over (
				Partition By [AttributeId]
				Order By [SysStart]
				Rows Between 1 Following and 1 Following),
				'9999-12-31 23:59:59.9999999')
			As [SysEnd]
	From	[Events])
Select	D.[AttributeId],
		D.[AttributeTitle],
		D.[AttributeDescription],
		D.[AttributeName],
		D.[DataType],
		D.[DataLength],
		D.[DataPrecision],
		D.[DataScale],
		D.[IsSingleValue],
		D.[IsMultiValue],
		D.[IsSimpleType],
		D.[IsCompositeType],
		D.[IsIntegral],
		D.[IsDerived],
		D.[IsValued],
		D.[IsNullable],
		D.[IsKey],
		D.[IsNonKey],
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
		Inner Join [AppModel].[AttributeHs] For System_Time All D
		On	T.[AttributeId] = D.[AttributeId] And
			T.[SysStart] >= D.[SysStart] And
			T.[SysStart] < D.[SysEnd]
Where	(@IncludeHistory = 1 Or (T.[SysStart] <= @AsOfUtcDate And Least(T.[SysEnd], D.[SysEnd]) > @AsOfUtcDate)) And
		(@AttributeId is Null Or @AttributeId = D.[AttributeId]) And
		(@ModelId is Null Or @ModelId In (
			Select	[ModelId]
			From	[AppModel].[ModelAttribute] For System_Time As of @AsOfUtcDate
			Where	D.[AttributeId] = [AttributeId]))
GO
