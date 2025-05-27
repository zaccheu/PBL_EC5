CREATE PROCEDURE [dbo].[spConsultaLogin] 
( 
  @email VARCHAR(50), 
  @senha VARCHAR(MAX) 
) 
AS 
BEGIN 
    SELECT * 
    FROM Usuario
    WHERE Email = @email AND Senha = @senha
END