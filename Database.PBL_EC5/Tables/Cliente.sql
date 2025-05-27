CREATE TABLE Cliente (
    [Id] INT NOT NULL,
    [Id_Usuario] INT NULL,
    Razao_Social VARCHAR(255) NOT NULL,
    [CNPJ] VARCHAR(14) NOT NULL,
    CEP VARCHAR(9) NULL,
    Rua VARCHAR(255) NULL,
    Numero INT,
    Ativo CHAR(1) NOT NULL DEFAULT '1',
    CONSTRAINT PK_Cliente PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY];
GO

-- Adiciona a constraint de chave estrangeira
ALTER TABLE Cliente
ADD CONSTRAINT FK_Usuario
FOREIGN KEY (Id_Usuario) REFERENCES [Usuario](Id);
GO

-- Cria índice único no CNPJ
CREATE UNIQUE INDEX UQ_Cliente_CNPJ ON Cliente(CNPJ);
GO
