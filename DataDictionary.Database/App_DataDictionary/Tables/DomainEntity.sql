CREATE TABLE [App_DataDictionary].[DomainEntity]
(
	[DomainEntityId] Int Not Null,
	[DomainEntityTitle] NVarChar(100) Not Null,
	[DomainEntityParentId] Int Null,
	[DomainText] NVarChar(2000) Null,
	[ModfiedBy] SysName Not Null CONSTRAINT [Df_DomainEntity_ModfiedBy] DEFAULT (ORIGINAL_LOGIN()),
	-- TODO: Add System Versioning later once the schema is locked down
	CONSTRAINT [PK_DomainEntity] PRIMARY KEY CLUSTERED ([DomainEntityId] ASC),
	CONSTRAINT [FK_DomainEntity_Parent] FOREIGN KEY ([DomainEntityParentId]) REFERENCES [App_DataDictionary].[DomainEntity] ([DomainEntityId])
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainEntity]
    ON [App_DataDictionary].[DomainEntity]([DomainEntityTitle] ASC, [DomainEntityParentId] ASC);
GO