--CREATE PROCEDURE spUpdate_Usuario
--	@id INT,
--    @nome NVARCHAR(50),
--    @email NVARCHAR(100),
--    @cpf NVARCHAR(15),
--	@senha VARCHAR(MAX),
--	@foto VARBINARY(MAX),
--    @administrador CHAR(1)
--AS
--BEGIN
--    SET NOCOUNT ON;

--    UPDATE Usuario
--    SET
--    nome = @nome,
--    email = @email, 
--    cpf = @cpf,
--	senha = @senha,
--	foto = @foto,
--    administrador = @administrador
--    WHERE id = @id;
--END


CREATE PROCEDURE spUpdate_Usuario
	@id INT,
    @nome NVARCHAR(50) = NULL,
    @email NVARCHAR(100) = NULL,
    @cpf NVARCHAR(15) = NULL,
	@senha VARCHAR(MAX) = NULL,
	@foto VARBINARY(MAX) = NULL,
    @administrador CHAR(1) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Atualiza apenas os campos que foram passados (não nulos)
    IF @nome IS NOT NULL
        UPDATE Usuario SET nome = @nome WHERE id = @id;

    IF @email IS NOT NULL
        UPDATE Usuario SET email = @email WHERE id = @id;

    IF @cpf IS NOT NULL
        UPDATE Usuario SET cpf = @cpf WHERE id = @id;

    IF @senha IS NOT NULL
        UPDATE Usuario SET senha = @senha WHERE id = @id;

    IF @foto IS NOT NULL
        UPDATE Usuario SET foto = @foto WHERE id = @id;

    IF @administrador IS NOT NULL
        UPDATE Usuario SET administrador = @administrador WHERE id = @id;
END