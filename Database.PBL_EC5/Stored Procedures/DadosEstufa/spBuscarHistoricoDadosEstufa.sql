CREATE PROCEDURE spBuscarHistoricoDadosEstufa
    @id_estufa INT,
    @dataInicial DATETIME,
    @dataFinal DATETIME
AS
BEGIN
    SELECT
        Id,
        Id_Estufa,
        Id_Temperatura,
        Temperatura,
        Data,
        Tensao
    FROM DadosEstufa
    WHERE Id_Estufa = @id_estufa
      AND Data >= @dataInicial
      AND Data <= @dataFinal
    ORDER BY Data
END