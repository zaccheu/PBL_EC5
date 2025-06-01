CREATE PROCEDURE [dbo].[spInsert_DadosEstufa]
    @Id_Estufa INT,
    @Temperatura FLOAT,
    @Data DATETIME,
    @Tensao FLOAT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO DadosEstufa (Id_Estufa, Temperatura, Data, Tensao)
    VALUES (@Id_Estufa, @Temperatura, @Data, @Tensao);

END