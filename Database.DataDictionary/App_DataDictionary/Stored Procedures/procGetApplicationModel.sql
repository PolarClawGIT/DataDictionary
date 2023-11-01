CREATE PROCEDURE [App_DataDictionary].[procGetApplicationModel]
		@ModelId UniqueIdentifier = Null,
		@ModelTitle NVarChar(100) = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ApplicationModel.
*/

Select	[ModelId],
		[ModelTitle],
		[ModelDescription]
From	[App_DataDictionary].[Model]
Where	(@ModelId is Null or @ModelId = [ModelId]) And
		(@ModelTitle is Null or @ModelTitle = [ModelTitle])
GO
