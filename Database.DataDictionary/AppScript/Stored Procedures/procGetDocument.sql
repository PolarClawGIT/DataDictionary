CREATE PROCEDURE [AppScript].[procGetDocument]
		@ModelId UniqueIdentifier = Null,
		@DocumentId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
/* Description: Performs Get on Document.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDatetime())

Select	[DocumentId],
		[DocumentTitle],
		[TemplateId],
		[TransformScript],
		[RootFolder],
		[InputDirectory],
		[InputFile],
		[OutputDirectory],
		[OutputFile],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn], 
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppScript].[DocumentHs] D
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@DocumentId is Null Or @DocumentId = [DocumentId]) And
		(@ModelId is Null Or @ModelId = [ModelId])
Go
