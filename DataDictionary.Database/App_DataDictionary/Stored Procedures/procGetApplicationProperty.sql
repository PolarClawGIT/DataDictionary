CREATE PROCEDURE [App_DataDictionary].[procGetApplicationProperty]
		@PropertyId UniqueIdentifier = Null,
		@PropertyTitle NVarChar(100) = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ApplicationProperty.
*/
Select	[PropertyId],
		[PropertyTitle],
		[PropertyDescription],
		[IsExtendedProperty],
		[IsDefinition],
		[IsChoice],
		[ExtendedProperty],
		[ChoiceList],
		[Obsolete],
		[SysStart]
From	[App_DataDictionary].[ApplicationProperty]
Where	(@PropertyId is Null Or @PropertyId = [PropertyId]) And
		(@PropertyTitle is Null Or @PropertyTitle = [PropertyTitle])
GO
