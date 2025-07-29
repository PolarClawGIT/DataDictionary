CREATE PROCEDURE [AppScript].[procSetTemplateAttributeOwner]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [AppScript].[udttTemplateAttributeOwner] ReadOnly
AS

GO
