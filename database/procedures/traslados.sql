-- procedures para traslados

-- procedure para agregar traslado
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

-- procedure para actualizar traslado
CREATE PROCEDURE sp_actualizar_traslado
    @IdTraslado INT,
    @MotivoTraslado VARCHAR(255),
    @FechaTraslado DATE,
    @CodigoEstudiante VARCHAR(20),
    @IdCentro INT,
    @IdPeriodo INT,
    @IdEstudiante INT
AS
BEGIN
    UPDATE traslados
    SET 
        motivo_traslado = @MotivoTraslado,
        fecha_traslado = @FechaTraslado,
        codigo_estudiante = @CodigoEstudiante,
        id_centro = @IdCentro,
        id_periodo = @IdPeriodo,
        id_estudiante = @IdEstudiante
    WHERE id_traslado = @IdTraslado;

    PRINT 'Traslado actualizado correctamente.';
END;

-- procedure para obtener traslados por estudiante
CREATE PROCEDURE sp_obtener_traslados_por_estudiante
    @IdEstudiante INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM estudiantes WHERE id_estudiante = @IdEstudiante)
    BEGIN
        RAISERROR('El estudiante especificado no existe.', 16, 1);
        RETURN;
    END

    SELECT * FROM traslados WHERE id_estudiante = @IdEstudiante;
END;

-- procedure para eliminar traslado
CREATE PROCEDURE sp_eliminar_traslado
    @IdTraslado INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM traslados WHERE id_traslado = @IdTraslado)
    BEGIN
        RAISERROR('El traslado especificado no existe.', 16, 1);
        RETURN;
    END

    DELETE FROM traslados WHERE id_traslado = @IdTraslado;

    PRINT 'Traslado eliminado correctamente.';
END;