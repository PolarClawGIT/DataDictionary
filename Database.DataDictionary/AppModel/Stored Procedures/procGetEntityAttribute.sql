CREATE PROCEDURE [AppModel].[procGetEntityAttribute]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null
AS
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainEntityAttribute.
*/
;Throw 50000,'Not Implemented',1;
/*
Select	D.[EntityId],
		D.[AttributeId],
		IsNull(D.[AttributeName], B.[AttributeTitle]) As [AttributeName],
		D.[IsNullable],
		D.[OrdinalPosition]
From	[AppModel].[EntityAttribute] D
		Left Join [AppModel].[ModelEntity] E
		On	D.[EntityId] = E.[EntityId]
		Left Join [AppModel].[ModelAttribute] A
		On	D.[AttributeId] = A.[AttributeId] And
			E.[ModelId] = A.[ModelId]
		Left Join [AppModel].[Attribute] B
		On	D.[AttributeId] = B.[AttributeId]
Where	(@ModelId is Null or (@ModelId = A.[ModelId] and @ModelId = E.[ModelId])) And
		(@EntityId is Null or @AttributeId = D.[EntityId]) And
		(@AttributeId is Null or @AttributeId = D.[AttributeId])*/
GO