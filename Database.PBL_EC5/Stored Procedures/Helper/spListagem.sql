CREATE PROCEDURE [dbo].[spListagem]
    @Tabela SYSNAME,
    @Ordem NVARCHAR(128) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SQL NVARCHAR(MAX);

    SET @SQL = N'SELECT * FROM ' + QUOTENAME(@Tabela);

    IF @Ordem IS NOT NULL AND LEN(@Ordem) > 0
    BEGIN
        -- Se for um número, não usar QUOTENAME
        IF ISNUMERIC(@Ordem) = 1
            SET @SQL += ' ORDER BY ' + @Ordem;
        ELSE
            SET @SQL += ' ORDER BY ' + QUOTENAME(@Ordem);
    END

    EXEC sp_executesql @SQL;
END
GO