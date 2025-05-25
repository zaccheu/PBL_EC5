CREATE PROCEDURE spInsert_Estufa
    @id INT,
    @id_cliente INT,
    @numero_serie NVARCHAR(255),
    @marca NVARCHAR(255),
    @potencia FLOAT,
    @foto VARBINARY(MAX) = NULL,
    @tensao INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Estufa (id, id_cliente, numero_serie, marca, potencia, tensao)
    VALUES (@id, @id_cliente, @numero_serie, @marca, @potencia, @tensao);
END