
CREATE PROCEDURE [dbo].[spUpdate_Cliente]
    @id INT,
    @id_usuario INT = NULL,
    @razao_social VARCHAR(255),
    @cnpj VARCHAR(18),
    @cep VARCHAR(9) = NULL,
    @rua VARCHAR(255) = NULL,
    @numero INT = NULL,
    @ativo CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Cliente
    SET
        id_usuario = @id_usuario,
        razao_social = @razao_social,
        cnpj = @cnpj,
        cep = @cep,
        rua = @rua,
        numero = @numero,
        ativo = @ativo
    WHERE id = @id;
END