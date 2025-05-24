CREATE PROCEDURE spDelete_Usuario
    @id INT
AS
BEGIN
    DELETE FROM Usuario
    WHERE id = @id;
END