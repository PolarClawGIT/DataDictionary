CREATE PROCEDURE [AppModel].[procGetSubjectArea]
		@ModelId UniqueIdentifier = Null,
		@SubjectAreaId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included

As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Model SubjectArea.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDateTime())

Select	[SubjectAreaId],
		[SubjectAreaTitle],
		[SubjectAreaDescription],
		N.[NameSpace] As [SubjectName],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn],
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppModel].[SubjectAreaHs] For System_Time All M
		Cross Apply [AppModel].[funcGetNameSpaceById](M.[NameSpaceId]) N
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		--(@ModelId is Null Or @ModelId = [ModelId]) And
		(@SubjectAreaId is Null Or @SubjectAreaId = [SubjectAreaId])
GO