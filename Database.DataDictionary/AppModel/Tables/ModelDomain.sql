CREATE TABLE [AppModel].[ModelDomain] (
    [ModelId]       UniqueIdentifier                                   NOT NULL,
    [DomainId]      UniqueIdentifier                                   NOT NULL,
    [SysStart]      DateTime2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ModelDomain_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]        DateTime2 (7) GENERATED ALWAYS AS ROW END HIDDEN CONSTRAINT [DF_ModelDomain_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    CONSTRAINT [PK_ModelDomain] PRIMARY KEY CLUSTERED ([ModelId] ASC, [DomainId] ASC),
    CONSTRAINT [FK_ModelDomain_Domain] FOREIGN KEY ([DomainId]) REFERENCES [AppModel].[Domain] ([DomainId]),
    CONSTRAINT [FK_ModelDomain_Model] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd])
)
GO
