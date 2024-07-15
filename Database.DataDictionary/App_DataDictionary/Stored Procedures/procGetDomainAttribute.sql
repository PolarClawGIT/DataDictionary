CREATE PROCEDURE [App_DataDictionary].[procGetDomainAttribute]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@AttributeTitle NVarChar(100) = null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainAttribute.
*/

Select	D.[AttributeId],
		D.[AttributeTitle],
		D.[AttributeDescription],
		A.[MemberName],
		D.[TypeOfAttributeId],
		P.[AttributeTitle] As [TypeOfAttributeTitle],
		Convert(Bit,IIF(D.[IsSingleValue] = 1,1,0)) As [IsSingleValue],
		Convert(Bit,IIF(D.[IsSingleValue] = 0,1,0)) As [IsMultiValue],
		Convert(Bit,IIF(D.[IsSimpleType] = 1,1,0)) As [IsSimpleType],
		Convert(Bit,IIF(D.[IsSimpleType] = 0,1,0)) As [IsCompositeType],
		Convert(Bit,IIF(D.[IsDerived] = 0,1,0)) As [IsIntegral],
		Convert(Bit,IIF(D.[IsDerived] = 1,1,0)) As [IsDerived],
		Convert(Bit,IIF(D.[IsNullable] = 0,1,0)) As [IsValued],
		Convert(Bit,IIF(D.[IsNullable] = 1,1,0)) As [IsNullable],
		Convert(Bit,IIF(D.[IsKey] = 1,1,0)) As [IsKey],
		Convert(Bit,IIF(D.[IsKey] = 0,1,0)) As [IsNonKey]
From	[App_DataDictionary].[DomainAttribute] D
		Left Join [App_DataDictionary].[ModelAttribute] A
		On	D.[AttributeId] = A.[AttributeId]
		Left Join [App_DataDictionary].[DomainAttribute] P
		On	D.[TypeOfAttributeId] = P.[AttributeId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@AttributeId is Null or @AttributeId = D.[AttributeId]) And
		(@AttributeTitle is Null or @AttributeTitle = D.[AttributeTitle])
GO
