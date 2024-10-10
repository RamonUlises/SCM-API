-- triggers para estudiantes

-- Trigger antes de insertar en la tabla estudiantes
CREATE TRIGGER trg_agregar_estudiante
BEFORE INSERT ON estudiantes
FOR EACH ROW
BEGIN
    IF NEW.nombres IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El nombre no puede ser nulo';
    END IF;
    IF NEW.cedula IS NULL OR NEW.cedula NOT REGEXP '^[0-9]{3}-[0-9]{6}-[0-9]{4}#$' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'La cédula no puede ser nula y debe tener el formato 000-000000-0000#';
    END IF;
    IF NEW.fecha_nacimiento IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'La fecha de nacimiento no puede ser nula';
    END IF;
    IF NEW.direccion IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'La dirección no puede ser nula';
    END IF;
    IF NEW.partida_nacimiento IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'La partida de nacimiento no puede ser nula';
    END IF;
    IF NEW.fecha_matricula IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'La fecha de matrícula no puede ser nula';
    END IF;
    IF NEW.barrio IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El barrio no puede ser nulo';
    END IF;
    IF NEW.pais IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El país no puede ser nulo';
    END IF;
    IF NEW.departamento IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El departamento no puede ser nulo';
    END IF;
    IF NEW.municipio IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El municipio no puede ser nulo';
    END IF;
END;

-- Trigger antes de actualizar en la tabla estudiantes
CREATE TRIGGER trg_actualizar_estudiante
BEFORE UPDATE ON estudiantes
FOR EACH ROW
BEGIN
    -- Validar que los campos no sean nulos
    IF NEW.nombres IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El nombre no puede ser nulo';
    END IF;
    IF NEW.cedula IS NULL OR NEW.cedula NOT REGEXP '^[0-9]{3}-[0-9]{6}-[0-9]{4}#$' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'La cédula no puede ser nula y debe tener el formato 000-000000-0000#';
    END IF;
    IF NEW.fecha_nacimiento IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'La fecha de nacimiento no puede ser nula';
    END IF;
    IF NEW.direccion IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'La dirección no puede ser nula';
    END IF;
    IF NEW.partida_nacimiento IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'La partida de nacimiento no puede ser nula';
    END IF;
    IF NEW.fecha_matricula IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'La fecha de matrícula no puede ser nula';
    END IF;
    IF NEW.barrio IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El barrio no puede ser nulo';
    END IF;
    IF NEW.pais IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El país no puede ser nulo';
    END IF;
    IF NEW.departamento IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El departamento no puede ser nulo';
    END IF;
    IF NEW.municipio IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El municipio no puede ser nulo';
    END IF;
END;
