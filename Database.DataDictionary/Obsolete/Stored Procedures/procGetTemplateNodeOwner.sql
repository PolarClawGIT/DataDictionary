CREATE PROCEDURE [Obsolete].[procGetTemplateNodeOwner]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
/* Description: Performs Get on TemplateNodeOwner.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDatetime())

Select	[NodeId],
		[NodeOwnerId],
		[TemplateId],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn], 
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[Obsolete].[TemplateNodeOwnerHs] D
Where	[NodeOwnerId] is Not Null And
		(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@TemplateId is Null Or @TemplateId = [TemplateId]) And
		(@ModelId is Null Or 
		 [TemplateId] In (
			Select	[TemplateId]
			From	[Obsolete].[ScriptingModel]
			Where	@ModelId = [ModelId]))
Go
