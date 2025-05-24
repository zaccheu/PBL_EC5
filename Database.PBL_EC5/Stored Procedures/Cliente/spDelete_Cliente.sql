CREATE PROCEDURE spDelete_Cliente
    @id INT
AS
BEGIN
    DELETE FROM Cliente
    WHERE id = @id;
END