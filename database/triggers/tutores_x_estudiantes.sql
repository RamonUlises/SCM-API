-- triggers para tutores_x_estudiantes

CREATE TRIGGER tr_validar_cedula_tutor_x_estudiante
ON tutores_x_estudiantes
FOR INSERT, UPDATE
AS
BEGIN
    IF EXISTS(
        SELECT cedula FROM tutores_x_estudiantes
        WHERE cedula = (SELECT cedula FROM INSERTED)
    )
    BEGIN
        RAISERROR('La cedula ya existe en la base de datos', 16, 1)
        ROLLBACK TRANSACTION
    END
END;

CREATE TRIGGER tr_update_tutores_x_estudiantes
ON tutores_x_estudiantes
FOR UPDATE
AS
BEGIN
    UPDATE tutores_x_estudiantes
    SET nombres = (SELECT nombres FROM INSERTED),
    apellidos = (SELECT apellidos FROM INSERTED),
    cedula = (SELECT cedula FROM INSERTED),
    telefono = (SELECT telefono FROM INSERTED)
    WHERE id_tutor_x_estudiante = (SELECT id_tutor_x_estudiante FROM INSERTED)
END;