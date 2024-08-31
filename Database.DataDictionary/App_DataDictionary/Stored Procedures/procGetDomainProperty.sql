CREATE PROCEDURE [App_DataDictionary].[procGetDomainProperty]
		@ModelId UniqueIdentifier = Null,
		@PropertyId UniqueIdentifier = Null,
		@PropertyTitle NVarChar(100) = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainProperty.
*/
Select	D.[PropertyId],
		D.[PropertyTitle],
		D.[PropertyDescription],
		D.[DataType],
		D.[PropertyData]
From	[App_DataDictionary].[DomainProperty] D
		Left Join [App_DataDictionary].[ModelProperty] M
		On	D.[PropertyId] = M.[PropertyId] And
			M.[ModelId] = @ModelId
Where	(D.[IsCommon] = 1 Or @ModelId is Null Or @ModelId = M.[ModelId]) And
		(@PropertyId is Null Or @PropertyId = D.[PropertyId]) And
		(@PropertyTitle is Null Or @PropertyTitle = D.[PropertyTitle])
GO