CREATE TABLE [AppModel].[DomainImplement]
(
	-- Domains Implement other domains. This can take a variety of forms.
	-- This describes the relationship between Domains.
	-- SQL: A Table Type implements one or more columns (other domains).
	-- SQL: A non-Table Type implements a base (often a primitive) type.
	-- .Net: A Class that inherits from another class implements that base class.
	-- .Net: A Class can Implement an Interface. 
	-- TODO: This can end up being a circular relationship. That maybe not be an issue, I only care about what is immediately implemented by the domain.
	[DomainId]			UniqueIdentifier Not Null,
	[ImplementedId]		UniqueIdentifier Not Null,
	-- OOP Polymorphism
	[IsInheritance]		Bit Not Null CONSTRAINT [Df_DomainImplement_IsInheritance] DEFAULT (0), -- Is the Implemented domain a base type/class. Most languages allow only one other type/class to be the base type/class. (.Net inheritance. SQL a System type)
	[IsAbstraction]		Bit Not Null CONSTRAINT [Df_DomainImplement_IsAbstraction] DEFAULT (0), -- Is the Implemented domain Abstracted within this Domain. (.Net usually using Abstract class. SQL views may be abstractions.)
	[IsEncapsulation]	Bit Not Null CONSTRAINT [Df_DomainImplement_IsEncapsulation] DEFAULT (0),-- Is the Implemented domain Encapsulated within this Domain. (.Net class contains the implemented class but may not expose it or implements it explicitly. SQL views may be encapsulation.)
	-- OOP Relationship  
	--[IsAssociation]		Bit Not Null CONSTRAINT [Df_DomainImplement_IsAssociation] DEFAULT (0),
	--[IsAggregation]		Bit Not Null CONSTRAINT [Df_DomainImplement_IsAggregation] DEFAULT (0),
	--[IsComposition]		Bit Not Null CONSTRAINT [Df_DomainImplement_IsComposition] DEFAULT (0),
	-- Temporal History Support
	[SysStart]			DateTime2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainImplement_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]			DateTime2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainImplement_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainImplement] PRIMARY KEY CLUSTERED ([DomainId] ASC, [ImplementedId] ASC),
	CONSTRAINT [FK_DomainImplementDomain] FOREIGN KEY ([DomainId]) REFERENCES [AppModel].[Domain] ([DomainId]),
	CONSTRAINT [FK_DomainImplemented] FOREIGN KEY ([ImplementedId]) REFERENCES [AppModel].[Domain] ([DomainId]),
	CONSTRAINT [CK_DomainImplemented] CHECK ([DomainId] <> [ImplementedId])

)
