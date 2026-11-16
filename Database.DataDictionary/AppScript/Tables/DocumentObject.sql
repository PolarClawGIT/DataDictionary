CREATE TABLE [AppScript].[DocumentObject]
(
	[DocumentId]		UniqueIdentifier Not Null,
	[ObjectId]			UniqueIdentifier Not Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DocumentObject_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DocumentObject_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DocumentObject] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
	CONSTRAINT [FK_DocumentObjectDocument] FOREIGN KEY ([DocumentId]) REFERENCES [AppScript].[Document] ([DocumentId]),
	CONSTRAINT [FK_DocumentObjectObject] FOREIGN KEY ([ObjectId]) REFERENCES [AppScript].[TemplateObject] ([ObjectId]),
)
