CREATE TABLE [dbo].[Estufa]
(
    IdEstufa INT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Localizacao VARCHAR(255),
    Descricao VARCHAR(MAX),
    AreaEstufaM2 FLOAT,              
    AlturaM FLOAT,                   
    DataAtualizacao DATETIME NOT NULL DEFAULT (GETDATE())
);