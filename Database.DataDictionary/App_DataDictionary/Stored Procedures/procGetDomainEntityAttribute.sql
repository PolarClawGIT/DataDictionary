CREATE PROCEDURE [App_DataDictionary].[procGetDomainEntityAttribute]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null
AS
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainEntityAttribute.
*/
Select	D.[EntityId],
		D.[AttributeId],
		IsNull(D.[AttributeName], B.[AttributeTitle]) As [AttributeName],
		D.[IsNullable],
		D.[OrdinalPosition]
From	[App_DataDictionary].[DomainEntityAttribute] D
		Left Join [App_DataDictionary].[ModelEntity] E
		On	D.[EntityId] = E.[EntityId]
		Left Join [App_DataDictionary].[ModelAttribute] A
		On	D.[AttributeId] = A.[AttributeId] And
			E.[ModelId] = A.[ModelId]
		Left Join [App_DataDictionary].[DomainAttribute] B
		On	D.[AttributeId] = B.[AttributeId]
Where	(@ModelId is Null or (@ModelId = A.[ModelId] and @ModelId = E.[ModelId])) And
		(@EntityId is Null or @AttributeId = D.[EntityId]) And
		(@AttributeId is Null or @AttributeId = D.[AttributeId])
GO