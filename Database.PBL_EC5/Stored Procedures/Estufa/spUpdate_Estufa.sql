CREATE PROCEDURE spUpdate_Estufa
    @id INT,
    @id_cliente INT,
    @numero_serie NVARCHAR(255),
    @marca NVARCHAR(255),
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