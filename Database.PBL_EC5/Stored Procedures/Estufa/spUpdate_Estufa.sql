CREATE PROCEDURE [dbo].[spUpdate_Estufa]
    @id INT,
    @id_cliente INT,
    @numero_serie VARCHAR(255),
    @marca VARCHAR(255),
    @potencia FLOAT,
    @tensao INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Estufa
    SET
        id_cliente = @id_cliente,
        numero_serie = @numero_serie,
        marca = @marca,
        potencia = @potencia,
        tensao = @tensao
    WHERE id = @id;
END
GO