CREATE TABLE [dbo].[Cliente](
	[Id] [int] NOT NULL,
	[Id_Usuario] [int] NULL,
	[Razao_Social] [varchar](255) NOT NULL,
	[CNPJ] [varchar](18) NULL,
	[CEP] [varchar](9) NULL,
	[Rua] [varchar](255) NULL,
	[Numero] [int] NULL,
	[Ativo] [char](1) NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Cliente] ADD  DEFAULT ('1') FOR [Ativo]
GO

ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD  CONSTRAINT [FK_Usuario] FOREIGN KEY([Id_Usuario])
REFERENCES [dbo].[Usuario] ([Id])
GO

ALTER TABLE [dbo].[Cliente] CHECK CONSTRAINT [FK_Usuario]
GO