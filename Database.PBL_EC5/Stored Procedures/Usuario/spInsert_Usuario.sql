CREATE PROCEDURE spInsert_Usuario
    @id INT,
    @nome NVARCHAR(100),
    @cpf NVARCHAR(20) = NULL,
    @email NVARCHAR(100),
    @administrador CHAR(1),
    @senha VARCHAR(MAX) = NULL,
    @foto VARBINARY(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Usuario (id, nome, cpf, email, administrador, senha, foto)
    VALUES (@id, @nome, @cpf, @email, @administrador, @senha, @foto);
END