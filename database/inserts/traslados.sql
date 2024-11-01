
CREATE PROCEDURE sp_agregar_traslado
    @MotivoTraslado VARCHAR(255),
    @FechaTraslado DATE,
    @CodigoEstudiante VARCHAR(20),
    @IdCentro INT,
    @IdPeriodo INT,
    @IdEstudiante INT
AS
BEGIN
    INSERT INTO traslados (motivo_traslado, fecha_traslado, codigo_estudiante, id_centro, id_periodo, id_estudiante)
    VALUES (@MotivoTraslado, @FechaTraslado, @CodigoEstudiante, @IdCentro, @IdPeriodo, @IdEstudiante);

    PRINT 'Traslado agregado correctamente.';
END;

EXEC sp_agregar_traslado 'Traslado de prueba', '2021-01-01', 'AAAA-123456-1234567', 1, 1, 1; 