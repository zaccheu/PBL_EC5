CREATE PROCEDURE spDelete_Estufa
    @id INT
AS
BEGIN
    DELETE FROM Estufa
    WHERE id = @id;
END