CREATE PROCEDURE spInsert_Log
    @Dados_Enviados VARCHAR(MAX),
    @Dados_Recebidos VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Log(Dados_Enviados, Dados_Recebidos)
    VALUES (@Dados_Enviados, @Dados_Recebidos);
END