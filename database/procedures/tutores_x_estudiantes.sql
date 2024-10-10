-- procedures para tutores_x_estudiantes

-- crear tabla tutores_x_estudiantes
CREATE PROCEDURE sp_agregar_tutor_x_estudiante
  @nombres VARCHAR(255),
  @apellidos VARCHAR(255),
  @cedula VARCHAR(16),
  @telefono VARCHAR(9)
AS
BEGIN
  INSERT INTO tutores_x_estudiantes(nombres, apellidos, cedula, telefono)
  VALUES(@nombres, @apellidos, @cedula, @telefono)
END;

-- editar tutor_x_estudiante
CREATE PROCEDURE sp_editar_tutor_x_estudiante
@id_tutor_x_estudiante INT,
@nombres VARCHAR(255),
@apellidos VARCHAR(255),
@cedula VARCHAR(16),
@telefono VARCHAR(9)
AS
BEGIN
  UPDATE tutores_x_estudiantes
  SET nombres = @nombres,
  apellidos = @apellidos,
  cedula = @cedula,
  telefono = @telefono
  WHERE id_tutor_x_estudiante = @id_tutor_x_estudiante
END;

-- borrar tutor_x_estudiante
CREATE PROCEDURE sp_borrar_tutor_x_estudiante
@id_tutor_x_estudiante INT
AS
BEGIN
    DELETE FROM tutores_x_estudiantes
    WHERE id_tutor_x_estudiante = @id_tutor_x_estudiante
END;

-- obtener todos los tutores_x_estudiantes
CREATE PROCEDURE sp_obtener_tutores_x_estudiantes
AS
BEGIN
  SELECT * FROM tutores_x_estudiantes
END;

-- obtener tutor_x_estudiante por id
CREATE PROCEDURE sp_obtener_tutor_x_estudiante
@id_tutor_x_estudiante INT
AS
BEGIN
    SELECT * FROM tutores_x_estudiantes
    WHERE id_tutor_x_estudiante = @id_tutor_x_estudiante
END;