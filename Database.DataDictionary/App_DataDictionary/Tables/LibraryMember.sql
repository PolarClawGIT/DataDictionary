CREATE TABLE [App_DataDictionary].[LibraryMember]
(	[MemberId]              UniqueIdentifier Not Null CONSTRAINT [DF_LibraryMemberId] DEFAULT (newid()),
	[LibraryId]             UniqueIdentifier Not Null,
	[MemberParentId]        UniqueIdentifier Null,
	[MemberName]            [App_DataDictionary].[typeNameSpaceMember] Not Null,
	[MemberType]            [App_DataDictionary].[typeObjectType] Null, -- .Net Documents produce a letter that is converted to a Member type. There may not be one.
	[MemberData]            XML Null, -- Raw data as XML
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_LibraryMember_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_LibraryMember_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_LibraryMember_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_LibraryMember] PRIMARY KEY CLUSTERED ([MemberId] ASC),
	CONSTRAINT [FK_LibraryMemberSource] FOREIGN KEY ([LibraryId]) REFERENCES [App_DataDictionary].[LibrarySource] ([LibraryId]),
	CONSTRAINT [FK_LibraryMemberParent] FOREIGN KEY ([MemberParentId]) REFERENCES [App_DataDictionary].[LibraryMember] ([MemberId]),
	CONSTRAINT [CK_LibraryMemberType] CHECK ([MemberType]='Parameter' OR [MemberType]='Event' OR [MemberType]='Method' OR [MemberType]='Property' OR [MemberType]='Field' OR [MemberType]='Type' OR [MemberType]='NameSpace' OR [MemberType] IS NULL),
)
GO
CREATE NONCLUSTERED INDEX [FK_LibraryMemberParent]
    ON [App_DataDictionary].[LibraryMember]([MemberParentId]);
GO