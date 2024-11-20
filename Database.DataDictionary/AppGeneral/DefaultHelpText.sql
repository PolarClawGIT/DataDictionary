Begin Try;
	Begin Transaction;
	Set NoCount On;

	Declare @Data [AppGeneral].[typeHelpSubject]

	Insert Into @Data ([HelpId], [HelpSubject], [NameSpace], [HelpToolTip], [HelpText])
	Values	('00000000-0000-0000-0010-100000000000','Setup','[Setup]', Null, '{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang1033{\fonttbl{\f0\fnil\fcharset0 Segoe UI;}{\f1\fswiss\fprq2\fcharset0 Segoe UI;}{\f2\fnil Segoe UI;}{\f3\fnil\fcharset2 Symbol;}}  {\*\listtable   {\list\listhybrid  {\listlevel\levelnfc23\leveljc0\levelstartat1{\leveltext\''01\''B7;}{\levelnumbers;}\f3\jclisttab\tx0}  {\listlevel\levelnfc23\leveljc0\levelstartat1{\leveltext\''01\''B7;}{\levelnumbers;}\f3\jclisttab\tx0}\listid1 }}  {\*\listoverridetable{\listoverride\listid1\listoverridecount0\ls1}}  {\*\generator Riched20 10.0.19041}\viewkind4\uc1   \pard\widctlpar\b\f0\fs18 This page contains information on how to get this application up and running.\b0\f1\par  \par  This application is written with a C# WinForms Application with a MS SQL Server as a back-end. The application can run without the back-end database. However, the application will be limited to a \ldblquote offline\rdblquote  mode supporting only a single user. The data then can be saved to the local drive and loaded back to the shared database at a later date.\par  \par  For development, use Microsoft SQL Local Db. This can be found on Microsoft\rquote s web site at no charge for personal use. In a production environment, you will want to use a full version of Microsoft SQL Server.\par  \par  To setup the database, you will need Microsoft Visual Studio and the Database Project included with the application source code. Before using the project:\par    \pard{\pntext\f3\''B7\tab}{\*\pn\pnlvlblt\pnf3\pnindent0{\pntxtb\''B7}}\fi-360\li720 Establish the database on the target server\line The project is setup to use the name [DataDictionary]. This can be changed without a lot of effort.\par  {\pntext\f3\''B7\tab}Make sure the user that is using the Visual Studio project has db_owner rights for the database.\par  {\pntext\f3\''B7\tab}Make sure the user can connect to the target database and server\par    \pard\widctlpar\par  Once this is complete, Visual Studio (or something that can be used to deploy a Database Projects) can deploy the database using the Database Project. If you are using the default instance of Microsoft SQL Local Db and the default name of the database, the project is already pointing to the correct location. If you are using another instance of Local Db or a full SQL Server or a database with a different name, you will need to adjust the project to match your environment.\par  \par  When the Project is deployed it will do a few things:\par    \pard   {\listtext\f1\u10625?\tab}\ls1\fi-360\li720 Create a Db Schema for [App_DataDictionary] and [History_DataDictionary]\par    \pard   {\listtext\f1\u10625?\tab}\ls1\ilvl1\fi-360\li1440 [App_DataDictionary] is the primary set of tables and most of the code resides\par  {\listtext\f1 1\tab}[History_DataDictionary] contain the temporal component (in development) component of the database. This will allow for tracking of changes to the data over time. \par    \pard   {\listtext\f1\u10625?\tab}\ls1\fi-360\li720 Create an Application Role called [DataDictionaryApp].\line Application roles is a security mechanism to limit access to database component to an application. It does require a Password that must be setup on the SQL Server and be stored in the database. For security purposes, change the password I have set and keep a copy. Please note, having the Application Role and Password does not give a person access to the SQL Server. They also require a valid Account as well as access to the database. Additional information about setting up users will be supplied later.\par  {\listtext\f1 1\tab}Create all the schema elements such as Tables, Views, Function, Table Data Types, and stored procedures.\par  {\listtext\f1 2\tab}It will not create users, as is.\par    \pard\widctlpar\par  There is also a script that builds the bare-bones set of data called: [ExtendedProperty Data]. This same set of data is also stored in an XML file. Loading the data using the application XML file is preferred but using the script is an option.\par  \par  If the server, database or application role where changed, then the changes need to be reflected in the Application Settings. A developer should be able to easily find the settings and make the adjustments. \i Do this before the application is first run.\i0\par  \par  Once the application is configured, it should be ready to run.\par  \par  When the application first starts, it will attempt to connect to the database and download the application data. This includes the help documentation. The first time, the database will likely be empty and no data will be downloaded. If the application fails to connect, the application will default and attempt to load the application data from an XML file called AppData.XML. This puts the application in off-line mode.\par  \par  If the database is empty, this can be addressed by using the application. Under the \ldblquote Tools\rdblquote  menu is an entry for \ldblquote Options\rdblquote . This will bring up a screen showing the current state (on-line vs off-line) as well as tools that transfer the Application data to/from the Application Database or the Application Data file.\par    \pard\f2\par  }'),
			('00000000-0000-0000-0020-100000000000','About Application', '[About]', Null,'{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang1033{\fonttbl{\f0\fnil\fcharset0 Segoe UI;}{\f1\fnil Segoe UI;}}  {\*\generator Riched20 10.0.19041}\viewkind4\uc1   \pard\b\f0\fs18 Data Dictionary Manager\b0\f1\par  \f0\''a9 2023 William Howard, All Rights Reserved\par  \par  \i Purpose\i0 : This application is intended as an editor for the Extended Properties within Microsoft SQL Server. The primary property of interest is the \ldblquote MS_Description\rdblquote  property. This is the property Microsoft and other products uses to populate meta data information.\par  \par  This is a tool I had wished we had during my professional carrier. Yes, Entity Relationship diagram (ERD) tools have this built in. But they are expensive and required every user to be licensed just to edit the data dictionary. Something my organization was not interested in paying for. We tried to maintain this within Visual Studio Database projects. It requires a lot of attention to detail and was not an interface that business users could deal with.\par  \par  This tool is intended to fill the gap. Something easy enough for Business to use to help maintain but technical enough that developers and designers can use to actually apply the descriptions to the extended properties.\par  \f1\par  }'),
			-- Exceptions, shared
			('00000000-0000-0000-1000-100000000000','Errors', '[Errors]','Describes various errors known errors that the application generates and any addtional information that can be provided.',null),
			('00000000-0000-0000-1010-100000000000','Error: Catalog not correct', '[Errors].[SqlException].[601010]','The Catalog passed to the method does not match the Catalog belonging to the Model.',null),
			('00000000-0000-0000-1020-100000000000','Error: Catalog invalid for Model', '[Errors].[SqlException].[601020]','The CatalogId or @CatalogId passed is not contained in the Model specified.',null),
			-- Exceptions, procedure specfic
			('00000000-0000-0000-2010-100000000000','Error: Duplicate Catalog', '[Errors].[SqlException].[AppCatalog].[procSetCatalog].[602010]','Duplicate CatalogId in @Data are not allowed.',null),
			('00000000-0000-0000-2020-100000000000','Error: Duplicate Database Name for Model', '[Errors].[SqlException].[AppCatalog].[procSetCatalog].[602020]','Duplicate Database Name are not allowed for a Model.',null),
			-- Exceptions, SQL Exceptions
			('00000000-0000-0000-3010-100000000000','Error: Permission Denied by Policy (Row Level Security)', '[Errors].[SqlException].[33504]','Row Level Security Violation',null)

	Merge @Data T
		Using [AppGeneral].[HelpSubject] S
		On	T.[HelpId] = S.[HelpId]
	When Matched Then Update
		Set	[HelpSubject] = T.[HelpSubject],
			[HelpToolTip] = T.[HelpToolTip],
			[HelpText] = S.[HelpText], -- Keep existing text, if any
			[NameSpace] = T.[NameSpace]
	When Not Matched By Target Then
		Insert ([HelpId], [HelpSubject], [HelpToolTip], [HelpText], [NameSpace])
		Values ([HelpId], [HelpSubject], [HelpToolTip], [HelpText], [NameSpace])
	;

	Exec [AppGeneral].[procSetHelpSubject] @Data = @Data

	Select	*
	From	[AppGeneral].[HelpSubject]

	-- By default, throw and error and exit without committing
;	Throw 50000, 'Abort process, comment out this line when ready to actual Commit the transaction',255;
	
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
