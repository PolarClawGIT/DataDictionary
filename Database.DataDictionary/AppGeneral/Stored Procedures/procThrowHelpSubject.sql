CREATE PROCEDURE [AppGeneral].[procThrowHelpSubject]
		@ProcId Int = null,
		@Number Int = 50000,
		@Message NVarChar(2048) = Null,
		@State TinyInt = 0
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Throw on HelpSubject.
** Used as an alternative to calling Throw directly.
** This allows the stored procedures to call the Help System to return messages stored there.
*/
Select	@Number = IsNull(@Number, 60000),
		@State = IsNull(@State,0)

Select	@Message = IsNull(@Message,
			FormatMessage('Unknown Message: [Errors].[%s].[%s].[Number %i][State %i]', Object_Schema_Name(@ProcId), Object_Name(@ProcId), @Number, @State))

Declare @NameSpace Table (
		[RankIndex] TinyInt Not Null,
		[NameSpace] NVarChar(1023) Not Null )

Insert Into @NameSpace
Values	-- Preferred format
		(1, FormatMessage('[Errors].[SqlException].[%s].[%s].[%i]', Object_Schema_Name(@ProcId), Object_Name(@ProcId), @Number)), 
		(2, FormatMessage('[Errors].[SqlException].[%s].[%i]', Object_Schema_Name(@ProcId), @Number)), 
		(3, FormatMessage('[Errors].[SqlException].[%i]', @Number)), 
		-- Default
		(255, '[Errors]')

Select	Top 1
		@Message = Coalesce([HelpToolTip], [HelpSubject])
From	[AppGeneral].[HelpSubject] H
		Cross Apply [App_DataDictionary].[funcSplitNameSpace](H.[NameSpace]) N
		Inner Join @NameSpace M
		On	N.[NameSpace] = M.[NameSpace]
Where	N.[IsBase] = 1
Order By M.[RankIndex]

Print FormatMessage('SqlException: Caller- [%s].[%s], Number- %i, State- %1',Object_Schema_Name(@ProcId), Object_Name(@ProcId), @Number, @State)
Print FormatMessage('Message- %s', @Message)

;Throw @Number, @Message, @State;
GO
