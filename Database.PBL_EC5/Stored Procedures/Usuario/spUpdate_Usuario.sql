CREATE PROCEDURE spUpdate_Usuario
	@id INT,
    @email NVARCHAR(100),
	@foto VARBINARY(MAX),
	@senha VARCHAR(MAX),
    @administrador CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuario
    SET
    email = @email, 
	foto = @foto,
	senha = @senha,
    administrador = @administrador
    WHERE id = @id;
END
