CREATE PROCEDURE [dbo].[spListagemDadosComEstufa]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        de.Id,
        de.Id_Estufa,
        de.Id_Temperatura,
        de.Temperatura,
        de.Data,
        de.Tensao,
        e.Numero_Serie
    FROM 
        DadosEstufa de
    INNER JOIN 
        Estufa e ON de.Id_Estufa = e.Id
    ORDER BY 
        de.Data DESC; -- Ordena pelos registros mais recentes
END


