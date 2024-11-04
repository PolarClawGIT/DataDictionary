CREATE PROCEDURE [AppCatalog].[procGetCatalog]
		@ModelId UniqueIdentifier = Null,
		@CatalogId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0, -- History is included, @AsOfUtcDate and @IncludeDeleted is ignored
		@IncludeDeleted Bit = 0  -- Include Deleted rows. @AsOfUtcDate is ignored
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DatabaseCatalog.
*/
;With [Data] As (
	Select	A.[CatalogId],
			A.[CatalogTitle],
			A.[CatalogDescription],
			A.[SourceServerName],
			A.[SourceDatabaseName],
			A.[SourceDate],
			A.[ModifiedBy],
			A.[SysStart] As [ModifiedOn],
			P.[PriorDate],
			N.[NextDate],
			Convert(Bit, IIF(P.[PriorDate] is Null Or P.[PriorDate] <> A.[SysStart],1,0)) As [IsInserted],
			Convert(Bit, IIF(P.[PriorDate] = A.[SysStart],1,0)) As [IsUpdated],
			Convert(Bit, IIF(N.[NextDate] <> A.[SysEnd],1,0)) As [IsDeleted],
				Convert(Bit, IIF(
					IsNull(@AsOfUtcDate,sysUtcDateTime()) >= A.[SysStart] And
					IsNull(@AsOfUtcDate,sysUtcDateTime()) < A.[SysEnd], 1,0))
					As [IsCurrent]
	From	[AppCatalog].[Catalog] For System_Time All A 
			Cross Apply (
				-- Prior Row
				Select	Max([SysEnd]) As [PriorDate]
				From	[AppCatalog].[Catalog] For System_Time All
				Where	[CatalogId] = A.[CatalogId] And
						[SysEnd] <= A.[SysStart]) P
			Cross Apply (
				-- Next Row
				Select	Min([SysStart]) As [NextDate]
				From	[AppCatalog].[Catalog] For System_Time All
				Where	[CatalogId] = A.[CatalogId] And
						[SysStart] >= A.[SysEnd]) N)
Select	[CatalogId],
		[CatalogTitle],
		[CatalogDescription],
		[SourceServerName],
		[SourceDatabaseName],
		[SourceDate],
		[ModifiedBy],
		[ModifiedOn],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[Data] D
Where	(@CatalogId is Null or @CatalogId = [CatalogId]) And
		(@ModelId is Null or @ModelId in (
			Select	[ModelId]
			From	[App_DataDictionary].[ModelCatalog]
			Where	[CatalogId] = D.[CatalogId])) And
		(@AsOfUtcDate is Null Or [IsCurrent] = 1) And
		(@IncludeHistory = 1 Or (@IncludeHistory = 0 And [IsCurrent] = 1)) And
		(@IncludeHistory = 1 Or @IncludeDeleted = 1 Or (@IncludeDeleted = 0 And [IsDeleted] = 0))
Order By Last_Value ([CatalogTitle]) Over (
				Partition By [CatalogId]
				Order By [ModifiedOn]
				Rows Between Unbounded Preceding and Unbounded Following),
		[ModifiedOn]
GO