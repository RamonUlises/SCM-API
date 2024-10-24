-- procedures para datos_academicos

CREATE PROCEDURE sp_obtener_datos_academicos
AS
BEGIN
  SELECT 
    e.codigo_estudiante,
    e.fecha_matricula,
    e.nivel_educativo,
    e.repitente,
    m.modalidad AS modalidad,
    g.grado AS grado,
    s.seccion AS seccion,
    t.turno AS turno,
    c.centro AS centro,
    ie.nombres AS estudiante
  FROM
    datos_academicos e
  LEFT JOIN modalidades m ON e.id_modalidad = m.id_modalidad
  LEFT JOIN grados g ON e.id_grado = g.id_grado
  LEFT JOIN secciones s ON e.id_seccion = s.id_seccion
  LEFT JOIN turnos t ON e.id_turno = t.id_turno
  LEFT JOIN centros c ON e.id_centro = c.id_centro
  LEFT JOIN estudiantes ie ON e.id_estudiante = ie.id_estudiante;
END;

CREATE PROCEDURE sp_obtener_datos_academicos_codigo
 @codigo INT
AS
BEGIN
	SELECT 
		e.codigo_estudiante,
		e.fecha_matricula,
		e.nivel_educativo,
		e.repitente,
		m.modalidad AS modalidad,
		g.grado AS grado,
		s.seccion AS seccion,
		t.turno AS turno,
		c.centro AS centro,
		ie.nombres AS estudiante
	FROM
		datos_academicos e
	LEFT JOIN modalidades m ON e.id_modalidad = m.id_modalidad
	LEFT JOIN grados g ON e.id_grado = g.id_grado
	LEFT JOIN secciones s ON e.id_seccion = s.id_seccion
	LEFT JOIN turnos t ON e.id_turno = t.id_turno
	LEFT JOIN centros c ON e.id_centro = c.id_centro
	LEFT JOIN estudiantes ie ON e.id_estudiante = ie.id_estudiante
  WHERE e.codigo_estudiante = @codigo;
END;

CREATE PROCEDURE sp_agregar_datos_academicos
    @codigo_estudiante VARCHAR(20),
    @fecha_matricula DATE,
    @nivel_educativo VARCHAR(255),
    @repitente BIT = 0,
    @id_modalidad INT,
    @id_grado INT,
    @id_seccion INT,
    @id_turno INT,
    @id_centro INT,
    @id_estudiante INT
AS
BEGIN
    INSERT INTO datos_academicos (codigo_estudiante, fecha_matricula, nivel_educativo, repitente, id_modalidad, id_grado, id_seccion, id_turno, id_centro, id_estudiante)
    VALUES (@codigo_estudiante, @fecha_matricula, @nivel_educativo, @repitente, @id_modalidad, @id_grado, @id_seccion, @id_turno, @id_centro, @id_estudiante);
END;

CREATE PROCEDURE sp_actualizar_datos_academicos
    @codigo_estudiante VARCHAR(20),
    @fecha_matricula DATE,
    @nivel_educativo VARCHAR(255),
    @repitente BIT,
    @id_modalidad INT,
    @id_grado INT,
    @id_seccion INT,
    @id_turno INT,
    @id_centro INT,
    @id_estudiante INT
AS
BEGIN
    UPDATE datos_academicos
    SET fecha_matricula = @fecha_matricula,
        nivel_educativo = @nivel_educativo,
        repitente = @repitente,
        id_modalidad = @id_modalidad,
        id_grado = @id_grado,
        id_seccion = @id_seccion,
        id_turno = @id_turno,
        id_centro = @id_centro,
        id_estudiante = @id_estudiante
    WHERE codigo_estudiante = @codigo_estudiante;
END;

CREATE PROCEDURE sp_eliminar_datos_academicos
    @codigo_estudiante VARCHAR(20)
AS
BEGIN
    DELETE FROM datos_academicos
    WHERE codigo_estudiante = @codigo_estudiante;
END;