CREATE TABLE Motores (
    Id INT PRIMARY KEY,
    IdEstufa INT NOT NULL,
    Nome VARCHAR(100) NOT NULL,
    Tipo VARCHAR(50),
    Status CHAR(1) NOT NULL DEFAULT ('0'),
    Descricao VARCHAR(MAX),
    DataAtualizacao DATETIME NOT NULL DEFAULT (GETDATE()),

    CONSTRAINT FK_Estufa
        FOREIGN KEY (IdEstufa)
        REFERENCES Estufas(IdEstufa)
        ON DELETE CASCADE
);
