-- triggers para tutores_x_estudiantes

CREATE TRIGGER trg_validar_cedula_tutor_x_estudiante
ON tutores_x_estudiantes
FOR INSERT, UPDATE
AS
BEGIN
    IF EXISTS(
        SELECT 1
        FROM tutores_x_estudiantes AS te
        INNER JOIN INSERTED AS i ON te.cedula = i.cedula 
        WHERE te.id_tutor_x_estudiante <> i.id_tutor_x_estudiante
    )
    BEGIN
        RAISERROR('La cédula ya existe en la base de datos', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;