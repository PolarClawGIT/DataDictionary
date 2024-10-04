CREATE VIEW [AppSecurity].[SecurityObject] As
-- DO Not think this is needed. Maybe for UI purposes.

Select	Convert(UniqueIdentifier, Null) As [ObjectId], -- The GUID of an application object used as the Object/Security ID. Not Null
		Convert(UniqueIdentifier, Null) As [ContextId], -- The GUID of an application object that is the owner of the base object. Null
		Convert(NVarchar(100), Null) As [ObjectTitle] -- Title/Name of the Object. Not Null.
Where	1=2 -- Sets up the column names and types
Union
Select	[HelpId] As [ObjectId],
		Convert(UniqueIdentifier, Null) As [ContextId],
		[HelpSubject] As [ObjectTitle]
From	[AppGeneral].[HelpSubject]
-- TODO: Add each object that can be secured
-- Do not use this in the Security Functions.
-- Its
GO
