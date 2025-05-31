CREATE PROCEDURE [dbo].[spDesvinculaCliente]
    @id_usuario INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Cliente SET Id_Usuario = NULL WHERE Id_Usuario = @id_usuario
END