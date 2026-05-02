CREATE PROCEDURE [AppScript].[procGetSchemaNode]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
AS
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDatetime())

Select	[NodeId],
		[SchemaId],
		[TemplateId],
		[NodeName],
		[NodeOrder],
		[RenderValueAs],
		[FixedValue],
		[ObjectScope],
		[ObjectProperty],
		[ModelPropertyId],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn], 
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppScript].[SchemaNodeHS] For System_Time All D
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@TemplateId is Null Or @TemplateId = [TemplateId]) And
		(@ModelId is Null Or @ModelId In (
			Select	[ModelId]
			From	[AppScript].[TemplateModel] For System_Time As of @AsOfUtcDate
			Where	D.[TemplateId] = [TemplateId]))
Print FormatMessage ('Select: %i, %s', @@RowCount, Convert(VarChar,GetDate()));
GO
