CREATE PROCEDURE [AppGeneral].[procGetHelpSubject]
		@HelpId UniqueIdentifier = Null, 
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included, @AsOfUtcDate and @IncludeDeleted is ignored
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on HelpSubject.
*/

Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysDatetime())

Select	[HelpId],
		[HelpSubject],
		[HelpToolTip],
		[HelpText],
		[NameSpace],
		[CreatedBy],
		[SysStart] As [CreatedOn],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppGeneral].[HelpSubjectAK] For System_Time All
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@HelpId is Null Or @HelpId = [HelpId])
GO