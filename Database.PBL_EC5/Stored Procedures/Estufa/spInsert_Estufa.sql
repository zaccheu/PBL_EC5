CREATE PROCEDURE [dbo].[spInsert_Estufa]
    @id INT,
    @id_cliente INT,
    @numero_serie NVARCHAR(255),
    @marca NVARCHAR(255),
    @potencia FLOAT,
    @tensao INT,
	@ativo CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Estufa (id, id_cliente, numero_serie, marca, potencia, tensao, ativo)
    VALUES (@id, @id_cliente, @numero_serie, @marca, @potencia, @tensao, @ativo);
END