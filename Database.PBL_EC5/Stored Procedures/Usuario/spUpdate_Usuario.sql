CREATE PROCEDURE spUpdate_Usuario
	@id INT,
    @nome NVARCHAR(50),
    @email NVARCHAR(100),
    @cpf NVARCHAR(15),
	@senha VARCHAR(MAX),
	@foto VARBINARY(MAX),
    @administrador CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuario
    SET
    nome = @nome,
    email = @email, 
    cpf = @cpf,
	senha = @senha,
	foto = @foto,
    administrador = @administrador
    WHERE id = @id;
END
