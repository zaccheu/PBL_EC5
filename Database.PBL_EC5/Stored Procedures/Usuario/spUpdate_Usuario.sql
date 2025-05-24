CREATE PROCEDURE spUpdate_Usuario
	@id INT,
    @email NVARCHAR(100),
    @administrador CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuario
    SET
    email = @email, 
    administrador = @administrador
    WHERE id = @id;
END
