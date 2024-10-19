-- triggers para estudiantes

-- Trigger antes de insertar en la tabla estudiantes
CREATE TRIGGER trg_agregar_estudiante
ON estudiantes
AFTER INSERT
AS
BEGIN
    -- Verificamos cada fila que se intenta insertar
    DECLARE @nombres NVARCHAR(100), @cedula NVARCHAR(50), @fecha_nacimiento DATE, @direccion NVARCHAR(255),
            @partida_nacimiento NVARCHAR(50), @fecha_matricula DATE, @barrio NVARCHAR(100),
            @pais NVARCHAR(100), @departamento NVARCHAR(100), @municipio NVARCHAR(100);

    SELECT @nombres = nombres, @cedula = cedula, @fecha_nacimiento = fecha_nacimiento, 
           @direccion = direccion, @partida_nacimiento = partida_nacimiento,
           @fecha_matricula = fecha_matricula, @barrio = barrio, @pais = pais,
           @departamento = departamento, @municipio = municipio
    FROM INSERTED;

    -- Validar si algún campo es nulo y lanzar error personalizado
    IF @nombres IS NULL
    BEGIN
        THROW 50001, 'El nombre no puede ser nulo', 1;
    END;
    
    IF @cedula IS NULL OR @cedula NOT LIKE '[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][A-Z]'
    BEGIN
        THROW 50002, 'La cédula no puede ser nula y debe tener el formato 000-000000-0000X', 1;
    END;
    
    IF @fecha_nacimiento IS NULL
    BEGIN
        THROW 50003, 'La fecha de nacimiento no puede ser nula', 1;
    END;
    
    IF @direccion IS NULL
    BEGIN
        THROW 50004, 'La dirección no puede ser nula', 1;
    END;
    
    IF @partida_nacimiento IS NULL
    BEGIN
        THROW 50005, 'La partida de nacimiento no puede ser nula', 1;
    END;
    
    IF @fecha_matricula IS NULL
    BEGIN
        THROW 50006, 'La fecha de matrícula no puede ser nula', 1;
    END;
    
    IF @barrio IS NULL
    BEGIN
        THROW 50007, 'El barrio no puede ser nulo', 1;
    END;
    
    IF @pais IS NULL
    BEGIN
        THROW 50008, 'El país no puede ser nulo', 1;
    END;
    
    IF @departamento IS NULL
    BEGIN
        THROW 50009, 'El departamento no puede ser nulo', 1;
    END;
    
    IF @municipio IS NULL
    BEGIN
        THROW 50010, 'El municipio no puede ser nulo', 1;
    END;
END;

-- Trigger antes de actualizar en la tabla estudiantes
CREATE TRIGGER trg_actualizar_estudiante
ON estudiantes
AFTER UPDATE
AS
BEGIN
    -- Variables para almacenar los valores de la fila actualizada
    DECLARE @nombres NVARCHAR(100), @cedula NVARCHAR(50), @fecha_nacimiento DATE, @direccion NVARCHAR(255),
            @partida_nacimiento NVARCHAR(50), @fecha_matricula DATE, @barrio NVARCHAR(100),
            @pais NVARCHAR(100), @departamento NVARCHAR(100), @municipio NVARCHAR(100);

    -- Obtener los valores actualizados desde la tabla INSERTED
    SELECT @nombres = nombres, @cedula = cedula, @fecha_nacimiento = fecha_nacimiento, 
           @direccion = direccion, @partida_nacimiento = partida_nacimiento,
           @fecha_matricula = fecha_matricula, @barrio = barrio, @pais = pais,
           @departamento = departamento, @municipio = municipio
    FROM INSERTED;

    -- Validaciones de campos obligatorios y formato de cédula
    IF @nombres IS NULL
    BEGIN
        THROW 50001, 'El nombre no puede ser nulo', 1;
    END;

    IF @cedula IS NULL OR @cedula NOT LIKE '[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][A-Z]'
    BEGIN
        THROW 50002, 'La cédula no puede ser nula y debe tener el formato 000-000000-0000#', 1;
    END;

    IF @fecha_nacimiento IS NULL
    BEGIN
        THROW 50003, 'La fecha de nacimiento no puede ser nula', 1;
    END;

    IF @direccion IS NULL
    BEGIN
        THROW 50004, 'La dirección no puede ser nula', 1;
    END;

    IF @partida_nacimiento IS NULL
    BEGIN
        THROW 50005, 'La partida de nacimiento no puede ser nula', 1;
    END;

    IF @fecha_matricula IS NULL
    BEGIN
        THROW 50006, 'La fecha de matrícula no puede ser nula', 1;
    END;

    IF @barrio IS NULL
    BEGIN
        THROW 50007, 'El barrio no puede ser nulo', 1;
    END;

    IF @pais IS NULL
    BEGIN
        THROW 50008, 'El país no puede ser nulo', 1;
    END;

    IF @departamento IS NULL
    BEGIN
        THROW 50009, 'El departamento no puede ser nulo', 1;
    END;

    IF @municipio IS NULL
    BEGIN
        THROW 50010, 'El municipio no puede ser nulo', 1;
    END;
END;
