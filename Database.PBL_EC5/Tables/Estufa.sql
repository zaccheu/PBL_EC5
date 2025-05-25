CREATE TABLE Estufa (
    Id INT PRIMARY KEY IDENTITY(1,1), -- Chave primária com auto incremento
    Id_Cliente INT NOT NULL, -- Relacionamento com a tabela Cliente
    Id_Estado INT NOT NULL, -- Relacionamento com a tabela Estado
    Numero_Serie VARCHAR(255) NOT NULL, -- Número de série da estufa
    Marca VARCHAR(255) NOT NULL, -- Marca da estufa
    Potencia FLOAT NOT NULL, -- Potência da estufa
    Tensao INT NOT NULL, -- Tensão elétrica da estufa


    CONSTRAINT FK_Estufa_Cliente FOREIGN KEY (Id_Cliente) REFERENCES Cliente(Id), -- Chave estrangeira
    CONSTRAINT FK_Estufa_Estado FOREIGN KEY (Id_Estado) REFERENCeS Estado(Id) -- Chave estrangeira
);