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

Select	M.[SubjectAreaId],
		M.[SubjectAreaTitle],
		M.[SubjectAreaDescription],
		M.[SubjectName],
		-- Temporal Data
		M.[CreatedOn],
		M.[CreatedBy],
		M.[RemovedOn],
		M.[RemovedBy],
		M.[IsInserted],
		M.[IsUpdated],
		M.[IsDeleted],
		M.[IsCurrent]
From	[AppModel].[SubjectAreaHs] For System_Time All M
Where	(@IncludeHistory = 1 Or (M.[SysStart] <= @AsOfUtcDate And M.[SysEnd] > @AsOfUtcDate)) And
		(@ModelId is Null Or @ModelId = M.[ModelId]) And
		(@SubjectAreaId is Null Or @SubjectAreaId = M.[SubjectAreaId])
Print FormatMessage ('Select: %i, %s', @@RowCount, Convert(VarChar,GetDate()));
GO