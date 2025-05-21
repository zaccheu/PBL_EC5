CREATE TABLE [dbo].[Usuario](
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Nome]            VARCHAR (50)  NULL,
    [Telefone]        VARCHAR (20)  NULL,
    [Cpf]             VARCHAR (15)  NULL,
    [Cep]             VARCHAR (10)  NULL,
    [Data_Nascimento] DATE          NULL,
    [Email]           VARCHAR (50)  NOT NULL,
    [SenhaHash]           VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


