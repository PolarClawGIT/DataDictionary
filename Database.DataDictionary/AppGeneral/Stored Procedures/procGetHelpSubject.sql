CREATE PROCEDURE [AppGeneral].[procGetHelpSubject]
		@HelpId UniqueIdentifier = Null, 
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0, -- History is included, @AsOfUtcDate and @IncludeDeleted is ignored
		@IncludeDeleted Bit = 0  -- Include Deleted rows. @AsOfUtcDate is ignored
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on HelpSubject.
*/
;With [Data] As (
	Select	A.[HelpId],
			A.[HelpSubject],
			A.[HelpToolTip],
			A.[HelpText],
			A.[NameSpace],
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
	From	[AppGeneral].[HelpSubject] For System_Time All A 
			Outer Apply (
				-- Prior Row
				Select	Max([SysEnd]) As [PriorDate]
				From	[AppGeneral].[HelpSubject] For System_Time All
				Where	[HelpId] = A.[HelpId] And
						[SysEnd] <= A.[SysStart]) P
			Outer Apply (
				-- Next Row
				Select	Min([SysStart]) As [NextDate]
				From	[AppGeneral].[HelpSubject] For System_Time All
				Where	[HelpId] = A.[HelpId] And
						[SysStart] >= A.[SysEnd]) N)
Select	[HelpId],
		[HelpSubject],
		[HelpToolTip],
		[HelpText],
		[NameSpace],
		[ModifiedBy],
		[ModifiedOn],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[Data]
Where	(@HelpId is Null or @HelpId = [HelpId]) And
		(@AsOfUtcDate is Null Or [IsCurrent] = 1) And
		(@IncludeHistory = 1 Or (@IncludeHistory = 0 And [IsCurrent] = 1)) And
		(@IncludeHistory = 1 Or @IncludeDeleted = 1 Or (@IncludeDeleted = 0 And [IsDeleted] = 0))
Order By Last_Value ([HelpSubject]) Over (
				Partition By [HelpId]
				Order By [ModifiedOn]
				Rows Between Unbounded Preceding and Unbounded Following),
		[ModifiedOn]
GO