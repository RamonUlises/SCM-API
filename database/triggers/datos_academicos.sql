-- triggers para datos_academicos

CREATE TRIGGER trg_validar_codigo_estudiante
on datos_academicos
FOR INSERT, UPDATE
AS
BEGIN
    DECLARE @codigoestudiante VARCHAR(20)
    SELECT @codigoestudiante = codigo_estudiante
    FROM inserted;
	 IF @codigoestudiante IS NULL OR PATINDEX ('[A-Z][A-Z][A-Z][A-Z]-[0-9][0-9][0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][0-9][0-9][0-9]', @codigoestudiante) = 0
    BEGIN
        RAISERROR('El código del estudiante debe tener el formato AAAA-000000-0000000', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;