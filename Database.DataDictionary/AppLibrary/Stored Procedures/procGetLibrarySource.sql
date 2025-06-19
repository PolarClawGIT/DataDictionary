CREATE PROCEDURE [AppLibrary].[procGetLibrarySource]
		@ModelId UniqueIdentifier = Null,
		@LibraryId UniqueIdentifier = Null,
		@AssemblyName SysName = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on LibrarySource.
*/
Select	D.[LibraryId],
		D.[LibraryTitle],
		D.[LibraryDescription],
		D.[AssemblyName],
		D.[SourceFile],
		D.[SourceDate],
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


From	[AppLibrary].[LibrarySource] D
		Left Join [AppLibrary].[LibraryModel] A
		On	D.[LibraryId] = A.[LibraryId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@LibraryId is Null or @LibraryId = D.[LibraryId]) And
		(@AssemblyName is Null or @AssemblyName = D.[AssemblyName])
GO