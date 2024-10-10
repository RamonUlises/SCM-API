-- triggers para traslados

CREATE TRIGGER trg_agregar_traslado
ON traslados
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @MotivoTraslado VARCHAR(255), @FechaTraslado DATE, @CodigoEstudiante VARCHAR(20),
            @IdCentro INT, @IdPeriodo INT, @IdEstudiante INT;

    SELECT 
        @MotivoTraslado = MotivoTraslado,
        @FechaTraslado = FechaTraslado,
        @CodigoEstudiante = CodigoEstudiante,
        @IdCentro = IdCentro,
        @IdPeriodo = IdPeriodo,
        @IdEstudiante = IdEstudiante
    FROM inserted;

    -- Validar que todos los campos requeridos no sean NULL
    IF @MotivoTraslado IS NULL OR @FechaTraslado IS NULL OR @CodigoEstudiante IS NULL OR 
       @IdCentro IS NULL OR @IdPeriodo IS NULL OR @IdEstudiante IS NULL
    BEGIN
        RAISERROR('Todos los campos son obligatorios.', 16, 1);
        RETURN;
    END

    -- Validar que la fecha no sea futura
    IF @FechaTraslado > GETDATE()
    BEGIN
        RAISERROR('La fecha del traslado no puede ser una fecha futura.', 16, 1);
        RETURN;
    END

    -- Validar que el estudiante exista
    IF NOT EXISTS (SELECT 1 FROM estudiantes WHERE id_estudiante = @IdEstudiante)
    BEGIN
        RAISERROR('El estudiante especificado no existe.', 16, 1);
        RETURN;
    END

    -- Validar que el centro exista
    IF NOT EXISTS (SELECT 1 FROM centros WHERE id_centro = @IdCentro)
    BEGIN
        RAISERROR('El centro especificado no existe.', 16, 1);
        RETURN;
    END

    -- Validar que el per�odo exista
    IF NOT EXISTS (SELECT 1 FROM periodos WHERE id_periodo = @IdPeriodo)
    BEGIN
        RAISERROR('El per�odo especificado no existe.', 16, 1);
        RETURN;
    END

    -- Insertar el traslado si pasa las validaciones
    INSERT INTO traslados (motivo_traslado, fecha_traslado, codigo_estudiante, id_centro, id_periodo, id_estudiante)
    VALUES (@MotivoTraslado, @FechaTraslado, @CodigoEstudiante, @IdCentro, @IdPeriodo, @IdEstudiante);
END;

CREATE TRIGGER trg_actualizar_traslado
ON traslados
INSTEAD OF UPDATE
AS
BEGIN
    DECLARE @IdTraslado INT, @MotivoTraslado VARCHAR(255), @FechaTraslado DATE,
            @CodigoEstudiante VARCHAR(20), @IdCentro INT, @IdPeriodo INT, @IdEstudiante INT;

    SELECT 
        @IdTraslado = id_traslado,
        @MotivoTraslado = MotivoTraslado,
        @FechaTraslado = FechaTraslado,
        @CodigoEstudiante = CodigoEstudiante,
        @IdCentro = IdCentro,
        @IdPeriodo = IdPeriodo,
        @IdEstudiante = IdEstudiante
    FROM inserted;

    -- Validar que el traslado existe
    IF NOT EXISTS (SELECT 1 FROM traslados WHERE id_traslado = @IdTraslado)
    BEGIN
        RAISERROR('El traslado especificado no existe.', 16, 1);
        RETURN;
    END

    -- Validar que todos los campos requeridos no sean NULL
    IF @MotivoTraslado IS NULL OR @FechaTraslado IS NULL OR @CodigoEstudiante IS NULL OR 
       @IdCentro IS NULL OR @IdPeriodo IS NULL OR @IdEstudiante IS NULL
    BEGIN
        RAISERROR('Todos los campos son obligatorios.', 16, 1);
        RETURN;
    END

    -- Validar que la fecha no sea futura
    IF @FechaTraslado > GETDATE()
    BEGIN
        RAISERROR('La fecha del traslado no puede ser una fecha futura.', 16, 1);
        RETURN;
    END

    -- Validar que el estudiante exista
    IF NOT EXISTS (SELECT 1 FROM estudiantes WHERE id_estudiante = @IdEstudiante)
    BEGIN
        RAISERROR('El estudiante especificado no existe.', 16, 1);
        RETURN;
    END

    -- Validar que el centro exista
    IF NOT EXISTS (SELECT 1 FROM centros WHERE id_centro = @IdCentro)
    BEGIN
        RAISERROR('El centro especificado no existe.', 16, 1);
        RETURN;
    END

    -- Validar que el per�odo exista
    IF NOT EXISTS (SELECT 1 FROM periodos WHERE id_periodo = @IdPeriodo)
    BEGIN
        RAISERROR('El per�odo especificado no existe.', 16, 1);
        RETURN;
    END

    -- Actualizar el traslado si pasa todas las validaciones
    UPDATE traslados
    SET 
        motivo_traslado = @MotivoTraslado,
        fecha_traslado = @FechaTraslado,
        codigo_estudiante = @CodigoEstudiante,
        id_centro = @IdCentro,
        id_periodo = @IdPeriodo,
        id_estudiante = @IdEstudiante
    WHERE id_traslado = @IdTraslado;
END;