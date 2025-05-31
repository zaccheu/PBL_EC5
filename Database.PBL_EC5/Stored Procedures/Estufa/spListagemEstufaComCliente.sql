CREATE PROCEDURE spListagemEstufaComCliente
AS
BEGIN
    SELECT 
        e.Id,
        e.Id_Cliente,
        c.Razao_Social AS NomeCliente,
        e.Numero_Serie,
        e.Marca,
        e.Potencia,
        e.Tensao,
        e.Ativo
    FROM Estufa e
    INNER JOIN Cliente c ON e.Id_Cliente = c.Id
END