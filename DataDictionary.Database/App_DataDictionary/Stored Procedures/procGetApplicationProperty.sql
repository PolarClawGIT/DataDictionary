CREATE PROCEDURE [App_DataDictionary].[procGetApplicationProperty]
		@ModelId UniqueIdentifier = Null,
		@PropertyId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ApplicationProperty.
*/
Select	[PropertyId],
		[PropertyTitle],
		[ModelId]
From	[App_DataDictionary].[ApplicationProperty]
Where	(@ModelId is Null or @ModelId = IsNull([ModelId],@ModelId)) And
		(@PropertyId is Null Or @PropertyId = [PropertyId])
GO
