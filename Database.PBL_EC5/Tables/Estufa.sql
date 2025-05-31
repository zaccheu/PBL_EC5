CREATE TABLE [dbo].[Estufa](
	[Id] [int] NOT NULL,
	[Id_Cliente] [int] NOT NULL,
	[Numero_Serie] [varchar](255) NOT NULL,
	[Marca] [varchar](255) NOT NULL,
	[Potencia] [float] NOT NULL,
	[Tensao] [int] NOT NULL,
	[Ativo] [char](1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Estufa] ADD  DEFAULT ('1') FOR [Ativo]
GO

ALTER TABLE [dbo].[Estufa]  WITH CHECK ADD  CONSTRAINT [FK_Estufa_Cliente] FOREIGN KEY([Id_Cliente])
REFERENCES [dbo].[Cliente] ([Id])
GO

ALTER TABLE [dbo].[Estufa] CHECK CONSTRAINT [FK_Estufa_Cliente]
GO
