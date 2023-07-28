CREATE PROCEDURE [App_DataDictionary].[procGetApplicationHelp]
		@HelpId UniqueIdentifier = Null,
		@HelpSubject NVarChar(100) = Null,
		@NameSpace NVarChar(1000) = null,
		@Obsolete Bit = 0
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ApplicationHelp.
*/

Select	[HelpId],
		[HelpParentId],
		[HelpSubject],
		[HelpText],
		[NameSpace],
		[Obsolete],
		[SysStart]
From	[App_DataDictionary].[ApplicationHelp]
Where	(@HelpId is Null or @HelpId = [HelpId]) And
		(@HelpSubject is Null or @HelpSubject = [HelpSubject]) And
		(@NameSpace is Null or @NameSpace = [NameSpace]) And
		(@Obsolete is Null or @Obsolete = 1 or @Obsolete = [Obsolete])
GO
-- Provide System Documentation
EXEC sp_addextendedproperty @name = N'MS_Description',
	@level0type = N'SCHEMA', @level0name = N'App_DataDictionary',
    @level1type = N'PROCEDURE', @level1name = N'procGetApplicationHelp',
	@value = N'Performs Get on ApplicationHelp.'
GO
