CREATE TABLE [dbo].[SecurityExample]
(
	[SecuirtyId] INT NOT NULL,
    [SecuirtyTitle] NVARCHAR (100) NOT NULL,
	[SecuirtyData]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_SecurityExample] PRIMARY KEY CLUSTERED ([SecuirtyId] ASC)
)
