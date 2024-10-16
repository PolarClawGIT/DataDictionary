﻿CREATE PROCEDURE [App_DataDictionary].[procGetDomainAttributeProperty]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@PropertyId UniqueIdentifier = Null

As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainAttributeProperty.
*/
Select	D.[AttributeId],
		D.[PropertyId],
		D.[PropertyValue]
From	[App_DataDictionary].[DomainAttributeProperty] D
		Left Join [App_DataDictionary].[ModelAttribute] A
		On	D.[AttributeId] = A.[AttributeId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@AttributeId is Null or @AttributeId = D.[AttributeId])  And
		(@PropertyId is Null or @PropertyId = D.[PropertyId])
GO