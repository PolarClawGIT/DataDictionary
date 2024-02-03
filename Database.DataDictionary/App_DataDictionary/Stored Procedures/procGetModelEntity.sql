CREATE PROCEDURE [App_DataDictionary].[procGetModelEntity]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null,
		@SubjectAreaId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ModelEntity.
*/

Select	M.[EntityId],
		A.[EntityTitle],
		M.[SubjectAreaId],
		S.[SubjectAreaTitle]
From	[App_DataDictionary].[ModelEntity] M
		Inner Join [App_DataDictionary].[DomainEntity] A
		On	M.[EntityId] = A.[EntityId]
		Left Join [App_DataDictionary].[ModelSubjectArea] S
		On	M.[SubjectAreaId] = S.[SubjectAreaId]
Where	(@ModelId is Null or @ModelId = M.[ModelId]) And
		(@EntityId is Null or @EntityId = M.[EntityId]) And
		(@SubjectAreaId is Null or @SubjectAreaId = M.[SubjectAreaId])
		
GO