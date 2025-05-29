CREATE PROCEDURE [dbo].[spInsert_Estufa]
    @id_cliente INT,
    @numero_serie VARCHAR(255),
    @marca VARCHAR(255),
    @potencia FLOAT,
    @tensao INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Estufa (id_cliente, numero_serie, marca, potencia, tensao)
    VALUES (@id_cliente, @numero_serie, @marca, @potencia, @tensao);
END
GO
