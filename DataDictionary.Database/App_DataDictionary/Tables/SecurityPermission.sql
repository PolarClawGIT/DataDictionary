CREATE TABLE [App_DataDictionary].[SecurityPermission]
(
	[SecurityPermissionId] Int Not Null,
	[SecurityRoleId] Int Not Null,
	[IsRead] Bit Not Null CONSTRAINT [DF_SecurityPermission_IsRead] DEFAULT (1), -- Role Members can Read this Entity or Attribute as well as the children
	[IsWrite] Bit Not Null CONSTRAINT [DF_SecurityPermission_IsWrite] DEFAULT (0), -- Role Members can Write this Entity or Attribute as well as the children
	[IsGrant] Bit Not Null CONSTRAINT [DF_SecurityPermission_IsGrant] DEFAULT (0), -- Role Members can Grant Read or Write to other Roles
	[IsRevoke] Bit Not Null CONSTRAINT [DF_SecurityPermission_IsRevoke] DEFAULT (0), -- Role Members are Revoked privlages on this Entity or Attribute as well as the children
	[IsAdministrator] Bit Not Null CONSTRAINT [DF_SecurityPermission_IsAdministrator] DEFAULT (0), -- Role Member is the Application Adminisstrator. All Other security settings are ignored.
	-- Sub-type to cover each of the types of items security can be applied to. Check constraint enforce rules.
	[AttributeId] Int Null,
	[EntityId] Int Null,
	CONSTRAINT [PK_SecurityPermission] PRIMARY KEY CLUSTERED ([SecurityPermissionId] ASC),
	CONSTRAINT [FK_SecurityPermission_SecurityRole] FOREIGN KEY ([SecurityRoleId]) REFERENCES [App_DataDictionary].[SecurityRole] ([SecurityRoleId]),
	CONSTRAINT [FK_SecurityPermission_DomainAttribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
	CONSTRAINT [FK_SecurityPermission_DomainEntity] FOREIGN KEY ([EntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
	CONSTRAINT [CK_SecurityPermission_AttributeEntity] CHECK (([AttributeId] is Null And [EntityId] is Not Null) Or ([AttributeId] is Not Null And [EntityId] is Null) Or ([IsAdministrator] = 1 And [AttributeId] is Null And [EntityId] is Null))
)
