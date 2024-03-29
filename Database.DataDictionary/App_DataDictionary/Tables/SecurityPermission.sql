﻿CREATE TABLE [App_DataDictionary].[SecurityPermission]
(
	-- Proof of Concept code.
	[SecurityPermissionId]  UniqueIdentifier Not Null CONSTRAINT [DF_SecurityPermissionId] DEFAULT (newsequentialid()),
	[SecurityRoleId] Int Not Null,
	-- Items that can have Security.
	-- This is a Sub-Type demoralization.
	-- RI is not enforced at this point.
	[ModelId] UniqueIdentifier Null,
	[SubjectAreaId] UniqueIdentifier Null,
	[CatalogId] UniqueIdentifier Null,

	-- Permission Set
	[IsRead] Bit Not Null CONSTRAINT [DF_SecurityPermission_IsRead] DEFAULT (1), -- Role Members can Read this Entity or Attribute as well as the children
	[IsWrite] Bit Not Null CONSTRAINT [DF_SecurityPermission_IsWrite] DEFAULT (0), -- Role Members can Write this Entity or Attribute as well as the children
	[IsGrant] Bit Not Null CONSTRAINT [DF_SecurityPermission_IsGrant] DEFAULT (0), -- Role Members can Grant Read or Write to other Roles
	[IsRevoke] Bit Not Null CONSTRAINT [DF_SecurityPermission_IsRevoke] DEFAULT (0), -- Role Members are Revoked privileges on this Entity or Attribute as well as the children
	[IsAdministrator] Bit Not Null CONSTRAINT [DF_SecurityPermission_IsAdministrator] DEFAULT (0), -- Role Member is the Application Administrator. All Other security settings are ignored.
	-- Sub-type to cover each of the types of items security can be applied to. Check constraint enforce rules.
	-- Keys
	CONSTRAINT [PK_SecurityPermission] PRIMARY KEY CLUSTERED ([SecurityPermissionId] ASC),
	CONSTRAINT [FK_SecurityPermission_SecurityRole] FOREIGN KEY ([SecurityRoleId]) REFERENCES [App_DataDictionary].[SecurityRole] ([SecurityRoleId]),
)
