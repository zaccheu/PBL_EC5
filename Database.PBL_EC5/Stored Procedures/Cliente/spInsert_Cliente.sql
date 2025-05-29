create PROCEDURE spInsert_Cliente
    @id INT,
    @id_usuario INT = NULL,
    @razao_social VARCHAR(255),
    @cnpj VARCHAR(14),
    @cep VARCHAR(9) = NULL,
    @rua VARCHAR(255) = NULL,
    @numero INT = NULL,
    @ativo CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Cliente (id_usuario, razao_social, cnpj, cep, rua, numero, ativo)
    VALUES (@id_usuario, @razao_social, @cnpj, @cep, @rua, @numero, @ativo);
END