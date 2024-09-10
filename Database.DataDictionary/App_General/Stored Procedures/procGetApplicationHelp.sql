CREATE PROCEDURE [App_General].[procGetApplicationHelp]
		@HelpId UniqueIdentifier = Null, 
		@IncludeHistory Bit = 0
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ApplicationHelp.
*/
Select	A.[HelpId],
		A.[HelpSubject],
		A.[HelpToolTip],
		A.[HelpText],
		A.[NameSpace],
		A.[ModifiedBy],
		A.[SysStart] As [ModifiedOn],
		Convert(NVarChar(10),
		Case	When P.[HelpId] is Null Then 'Inserted'
				When N.[HelpId] is Null And A.[SysEnd] <= sysUtcDateTime() Then 'Deleted'
				Else 'Updated' End) As [Modification]
From	[App_General].[ApplicationHelp] For System_Time All A -- All values
		Left Join [App_General].[ApplicationHelp] For System_Time All P -- Prior
		On	A.[HelpId] = P.[HelpId] And
			A.[SysStart] = P.[SysEnd]
		Left Join [App_General].[ApplicationHelp] For System_Time All N -- Next
		On	A.[HelpId] = N.[HelpId] And
			A.[SysEnd] = N.[SysStart]
Where	(@HelpId is Null or @HelpId = A.[HelpId]) And
		(@IncludeHistory = 1 or sysUtcDateTime() Between A.[SysStart] And A.[SysEnd])
Order By A.[HelpId], A.[SysStart]
GO
