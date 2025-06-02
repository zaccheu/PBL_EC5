CREATE PROCEDURE [dbo].[spConsultaIdTemperatura] 
( 
  @id_Temperatura VARCHAR(100)
) 
AS 
BEGIN 
    SELECT * 
    FROM DadosEstufa
    WHERE Id_Temperatura = @id_Temperatura
END