CREATE PROCEDURE [AppGeneral].[procThrowHelpSubject]
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Throw on HelpSubject.
** Used to supplement the an Exception.
** This procedure uses the ERROR functions to read HelpSubject [NameSpace]
** and attempt to find the a message associated with the exception.
** It then Throws the exception using the message found, if it exists.
**
** To Use: Call Throw with the exception number for the message to be returned.
** Then in the Catch Block, call this procedure instead of re-throwing the error.
**
** Example (inside Catch Block):
** 	If ERROR_NUMBER() >= 50000 Exec [AppGeneral].[procThrowHelpSubject]
**	Else If ERROR_SEVERITY() Not In (0, 11) Throw;
*/
Declare	@ObjectId Int = Object_Id(ERROR_PROCEDURE()),
		@Number Int = IsNull(ERROR_NUMBER(), 60000),
		@State TinyInt = IsNull(ERROR_STATE(), 0),
		@Message NVarChar(2048) = null

Declare @NameSpace Table (
		[RankIndex] TinyInt Not Null, -- Priority
		[NameSpace] NVarChar(1023) Not Null,
		Primary Key ([RankIndex]))

Insert Into @NameSpace
Values	-- Patterns to match to
		(1, FormatMessage('[Errors].[SqlException].[%s].[%s].[%i]', Object_Schema_Name(@ObjectId), Object_Name(@ObjectId), ERROR_NUMBER())), 
		(2, FormatMessage('[Errors].[SqlException].[%s].[%i]', ERROR_PROCEDURE(), ERROR_NUMBER())), 
		(3, FormatMessage('[Errors].[SqlException].[%s].[%i]', Object_Schema_Name(@ObjectId), ERROR_NUMBER())), 
		(4, FormatMessage('[Errors].[SqlException].[%i]', ERROR_NUMBER()))

Select	Top 1
		@Message = Coalesce([HelpToolTip], [HelpSubject])
From	[AppGeneral].[HelpSubject] H
		Cross Apply [AppGeneral].[funcParseName](H.[NameSpace]) N
		Inner Join @NameSpace M
		On	N.[QualifiedName] = M.[NameSpace]
Where	N.[IsBase] = 1
Order By M.[RankIndex]

If ERROR_NUMBER() is Not Null
-- This only works if the procedure is called within the Catch part of a Throw/Catch statement.
-- ERROR_NUMBER() < 50000 must be handled by the calling procedure. This will throw an error otherwise.
  Begin
	Print FormatMessage ('*** Error Report- %s', ERROR_PROCEDURE())
	--Print FormatMessage ('    Object Name [%s].[%s]', Object_Schema_Name(@ObjectId), Object_Name(@ObjectId))
	Print FormatMessage ('    Message- %s', ERROR_MESSAGE())
	Print FormatMessage ('    Help Subject- %s', @Message)
	Print FormatMessage ('    Number- %i', ERROR_NUMBER())
	Print FormatMessage ('    Severity- %i', ERROR_SEVERITY())
	Print FormatMessage ('    State- %i', ERROR_STATE())
	Print FormatMessage ('    Line- %i', ERROR_LINE())
	Print FormatMessage ('    @@TranCount - %i', @@TranCount)
	Print FormatMessage ('    @@NestLevel - %i', @@NestLevel)
	Print FormatMessage ('    Original_Login - %s', Original_Login())
	Print FormatMessage ('    Current_User - %s', Current_User)
	Print FormatMessage ('    XAct_State - %i', XAct_State())
	
	Set	@Message = IsNull(@Message, ERROR_MESSAGE());
	If ERROR_NUMBER() < 50000  Throw @Number, @Message, @State;
  End
GO
