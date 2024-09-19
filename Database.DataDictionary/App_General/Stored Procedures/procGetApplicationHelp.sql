﻿CREATE PROCEDURE [App_General].[procGetApplicationHelp]
		@HelpId UniqueIdentifier = Null, 
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0, -- History is included, @AsOfUtcDate and @IncludeDeleted is ignored
		@IncludeDeleted Bit = 0  -- Include Deleted rows. @AsOfUtcDate is ignored
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ApplicationHelp.
*/
;With [Data] As (
	Select	A.[HelpId],
			A.[HelpSubject],
			A.[HelpToolTip],
			A.[HelpText],
			A.[NameSpace],
			A.[ModifiedBy],
			A.[SysStart] As [ModifiedOn],
			Convert(NVarChar(10), Case
				When P.[HelpId] is Null Then 'Inserted'
				Else 'Updated'
				End) As [Modification]
	From	[App_General].[ApplicationHelp] For System_Time All A -- All Values
			Left Join [App_General].[ApplicationHelp] For System_Time All P -- Prior
			On	A.[HelpId] = P.[HelpId] And
				A.[SysStart] = P.[SysEnd]
	Where	(@HelpId is Null or @HelpId = A.[HelpId]) And
			(@IncludeHistory = 1 Or
				(IsNull(@AsOfUtcDate,sysUtcDateTime()) >= A.[SysStart] And IsNull(@AsOfUtcDate,sysUtcDateTime()) < A.[SysEnd]))
	Union -- Handle Deleted Rows
	Select	A.[HelpId],
			A.[HelpSubject],
			A.[HelpToolTip],
			A.[HelpText],
			A.[NameSpace],
			Null As [ModifiedBy], -- The account deleting the row is not recorded
			A.[SysEnd] As [ModifiedOn],
			Convert(NVarChar(10), 'Deleted') As [Modification]
	From	[App_General].[ApplicationHelp] For System_Time All A -- All Values
			Left Join [App_General].[ApplicationHelp] For System_Time All N -- Next
			On	A.[HelpId] = N.[HelpId] And
				A.[SysEnd] = N.[SysStart]
	Where	(@HelpId is Null or @HelpId = A.[HelpId]) And
			@IncludeDeleted = 1 And
			N.[HelpId] is Null And
			IsNull(@AsOfUtcDate,sysUtcDateTime()) > A.[SysStart] And
			A.[SysEnd] <= sysUtcDateTime())
Select	[HelpId],
		[HelpSubject],
		[HelpToolTip],
		[HelpText],
		[NameSpace],
		[ModifiedBy],
		[ModifiedOn],
		[Modification]
From	[Data]
Order By Last_Value ([HelpSubject]) Over (
				Partition By [HelpId]
				Order By [ModifiedOn]
				Rows Between Unbounded Preceding and Unbounded Following),
		[ModifiedOn]