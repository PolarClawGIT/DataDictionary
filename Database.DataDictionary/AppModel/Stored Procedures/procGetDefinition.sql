CREATE PROCEDURE [AppModel].[procGetDefinition]
		@ModelId UniqueIdentifier = Null,
		@DefinitionId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainDefinition.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDateTime())

Select	[DefinitionId],
		[DefinitionTitle],
		[DefinitionDescription],
		[IsCommon],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn],
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppModel].[DefinitionHs]
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@DefinitionId is Null Or @DefinitionId = [DefinitionId]) And
		(@ModelId is Null Or 
		 [IsCommon] = 1 Or
		 [DefinitionId] In (
			Select	[DefinitionId]
			From	[AppModel].[ModelDefinitionHs]
			Where	@ModelId = [ModelId]))
Print FormatMessage ('Select: %i, %s', @@RowCount, Convert(VarChar,GetDate()));
GO


