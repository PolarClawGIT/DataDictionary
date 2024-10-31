Begin Try;
	Begin Transaction;
	Set NoCount On;

	Declare @Roles [AppSecurity].[typeSecurityRole]
	Insert Into @Roles (
		[RoleId],
		[RoleName],
		[RoleDescription],
		[IsSecurityAdmin],
		[IsHelpAdmin],
		[IsHelpOwner],
		[IsCatalogAdmin],
		[IsCatalogOwner],
		[IsLibraryAdmin],
		[IsLibraryOwner],
		[IsModelAdmin],
		[IsModelOwner],
		[IsScriptAdmin],
		[IsScriptOwner])
	Values
		('00000000-0000-0000-0010-200000000000','Security Admin','Security Administrator (modify any security table)',
			1,0,0,0,0,0,0,0,0,0,0),
		('00000000-0000-0000-0020-200000000000','Help Admin','Help Subject Administrator (modify any Help Subject)',
			0,1,0,0,0,0,0,0,0,0,0),
		('00000000-0000-0000-0030-200000000000','Application Admin','Application Administrator (modify any Catalog, Library, Model or Script)',
			0,0,0,1,0,1,0,1,0,1,0),
		('00000000-0000-0000-0040-200000000000','Help Owner','Help Subject Owner (Can Create new Help Subject and modify any Help Subject they own)',
			0,0,1,0,0,0,0,0,0,0,0),
		('00000000-0000-0000-0050-200000000000','Application Owner','Application Owner (Can Create new Catalogs, Libraries, Models or Scripts and modify any that they own)',
			0,0,0,0,1,0,1,0,1,0,1)

	Exec [AppSecurity].[procSetRole] @Data = @Roles

	Select	*
	From	[AppSecurity].[Role]

	-- By default, throw and error and exit without committing
--;	Throw 50000, 'Abort process, comment out this line when ready to actual Commit the transaction',255;
	
	Commit Transaction;
	Print 'Commit Issued';
End Try
Begin Catch
	Print FormatMessage ('*** Error Report: %s ***', Object_Name(@@ProcID));
	Print FormatMessage (' Message- %s', ERROR_MESSAGE());
	Print FormatMessage (' Number- %i', ERROR_NUMBER());
	Print FormatMessage (' Severity- %i', ERROR_SEVERITY());
	Print FormatMessage (' State- %i', ERROR_STATE());
	Print FormatMessage (' Procedure- %s', ERROR_PROCEDURE());
	Print FormatMessage (' Line- %i', ERROR_LINE());
	Print FormatMessage (' @@TranCount - %i', @@TranCount);
	Print FormatMessage (' @@NestLevel - %i', @@NestLevel);
	Print FormatMessage (' Original_Login - %s', Original_Login());
	Print FormatMessage (' Current_User - %s', Current_User);
	Print FormatMessage (' XAct_State - %i', XAct_State());
	Print '--- Debug Data ---';

	-- Rollback Transaction
	Print 'Rollback Issued';
	Rollback Transaction;
	Throw;
End Catch;
