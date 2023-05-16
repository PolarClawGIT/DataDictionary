EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sample Child Table', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SampleChild';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sample Parent Table.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SampleParent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary Key column, Sample Parent', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SampleParent', @level2type = N'COLUMN', @level2name = N'SampleParentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary Key column, Sample Parent', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SampleChild', @level2type = N'COLUMN', @level2name = N'SampleParentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary Key', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SampleParent', @level2type = N'CONSTRAINT', @level2name = N'PK_SampleParent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK: SampleChild is a child of SampleParent', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SampleChild', @level2type = N'CONSTRAINT', @level2name = N'FK_SampleChild_SampleParent';
GO