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

CREATE PROCEDURE sp_obtener_traslados
AS
BEGIN
	SELECT 
	  t.id_traslado,
	  t.motivo_traslado,
	  t.fecha_traslado,
	  t.codigo_estudiante,
	  c.centro AS centro,
	  p.periodo AS periodo,
	  e.nombres AS estudiante
	  FROM traslados t
	  LEFT JOIN centros c ON c.centro = t.id_centro
	  LEFT JOIN periodos p ON p.id_periodo = t.id_periodo
	  LEFT JOIN estudiantes e ON e.id_estudiante = t.id_estudiante;
END;

-- procedure para obtener traslados por estudiante
CREATE PROCEDURE sp_obtener_traslados_id
 @id INT
AS
BEGIN
	SELECT 
	  t.id_traslado,
	  t.motivo_traslado,
	  t.fecha_traslado,
	  t.codigo_estudiante,
	  c.centro AS centro,
	  p.periodo AS periodo,
	  e.nombres AS estudiante
	  FROM traslados t
	  LEFT JOIN centros c ON c.centro = t.id_centro
	  LEFT JOIN periodos p ON p.id_periodo = t.id_periodo
	  LEFT JOIN estudiantes e ON e.id_estudiante = t.id_estudiante
	  WHERE id_traslado = @id;
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