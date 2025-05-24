CREATE PROCEDURE spInsert_Usuario
    @id INT,
    @nome NVARCHAR(100),
    @cpf NVARCHAR(20) = NULL,
    @email NVARCHAR(100),
    @administrador CHAR(1),
    @salt VARBINARY(MAX) = NULL,
    @senhahash VARBINARY(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Usuario (id, nome, cpf, email, administrador, salt, senhahash)
    VALUES (@id, @nome, @cpf, @email, @administrador, @salt, @senhahash);
END