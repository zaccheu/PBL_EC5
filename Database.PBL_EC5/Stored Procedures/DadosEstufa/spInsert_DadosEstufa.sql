CREATE PROCEDURE [dbo].[spInsert_DadosEstufa]
	@Id INT,
    @Id_Estufa INT,
    @Id_Temperatura VARCHAR(100),
    @Temperatura FLOAT,
    @Data DATETIME,
    @Tensao FLOAT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO DadosEstufa (Id, Id_Estufa, Id_Temperatura, Temperatura, Data, Tensao)
    VALUES (@Id, @Id_Estufa, @Id_Temperatura, @Temperatura, @Data, @Tensao);

END