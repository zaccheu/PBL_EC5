CREATE TABLE [Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Cpf] [varchar](15) NULL,
	[Email] [varchar](50) NOT NULL,
	[Administrador] char(1)  NOT NULL DEFAULT '0',
	[Salt] [varbinary](64) NOT NULL,
	[SenhaHash] [varbinary](64) NOT NULL,
	CONSTRAINT PK_Usuario PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY];
GO
CREATE UNIQUE INDEX UQ_Usuario_Email ON [Usuario](Email);
GO
/*nao por a unique key do cpf, para agilizar os testes, somente no ultimo commit*/
CREATE UNIQUE INDEX UQ_Usuario_Cpf ON [Usuario](Cpf);
