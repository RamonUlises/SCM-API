--  procedures de estudiantes

CREATE PROCEDURE sp_insertar_estudiante
    @nombres VARCHAR(255),
    @apellidos VARCHAR(255),
    @cedula VARCHAR(16),
    @fecha_nacimiento DATE,
    @direccion VARCHAR(255),
    @telefono VARCHAR(9),
    @partida_nacimiento BIT,
    @fecha_matricula DATE,
    @barrio VARCHAR(255),
    @peso DECIMAL(18, 2),
    @talla DECIMAL(18, 2),
    @territorio_indigena VARCHAR(255),
    @comunidad_indigena VARCHAR(255),
    @pais VARCHAR(100),
    @departamento VARCHAR(100),
    @municipio VARCHAR(100),
    @nacionalidad VARCHAR(100),
    @id_sexo INT,
    @id_etnia INT,
    @id_lengua INT,
    @id_discapacidad INT,
    @id_tutor_x_estudiante INT
AS
BEGIN
    INSERT INTO estudiantes(
        nombres,
        apellidos,
        cedula,
        fecha_nacimiento,
        direccion,
        telefono,
        partida_nacimiento,
        fecha_matricula,
        barrio,
        peso,
        talla,
        territorio_indigena,
        comunidad_indigena,
        pais,
        departamento,
        municipio,
        nacionalidad,
        id_sexo,
        id_etnia,
        id_lengua,
        id_discapacidad,
        id_tutor_x_estudiante
    )
    VALUES(
        @nombres,
        @apellidos,
        @cedula,
        @fecha_nacimiento,
        @direccion,
        @telefono,
        @partida_nacimiento,
        @fecha_matricula,
        @barrio,
        @peso,
        @talla,
        @territorio_indigena,
        @comunidad_indigena,
        @pais,
        @departamento,
        @municipio,
        @nacionalidad,
        @id_sexo,
        @id_etnia,
        @id_lengua,
        @id_discapacidad,
        @id_tutor_x_estudiante
    );
END;

CREATE PROCEDURE sp_editar_estudiante
    @id_estudiante INT,
    @nombres VARCHAR(255),
    @apellidos VARCHAR(255),
    @cedula VARCHAR(16),
    @fecha_nacimiento DATE,
    @direccion VARCHAR(255),
    @telefono VARCHAR(9),
    @partida_nacimiento BIT,
    @fecha_matricula DATE,
    @barrio VARCHAR(255),
    @peso DECIMAL(18, 2),
    @talla DECIMAL(18, 2),
    @territorio_indigena VARCHAR(255),
    @comunidad_indigena VARCHAR(255),
    @pais VARCHAR(100),
    @departamento VARCHAR(100),
    @municipio VARCHAR(100),
    @nacionalidad VARCHAR(100),
    @id_sexo INT,
    @id_etnia INT,
    @id_lengua INT,
    @id_discapacidad INT,
    @id_tutor_x_estudiante INT
AS
BEGIN
  IF EXISTS (SELECT * FROM estudiantes WHERE id_estudiante = @id_estudiante)
  BEGIN
    UPDATE estudiantes
    SET
        nombres = @nombres,
        apellidos = @apellidos,
        cedula = @cedula,
        fecha_nacimiento = @fecha_nacimiento,
        direccion = @direccion,
        telefono = @telefono,
        partida_nacimiento = @partida_nacimiento,
        fecha_matricula = @fecha_matricula,
        barrio = @barrio,
        peso = @peso,
        talla = @talla,
        territorio_indigena = @territorio_indigena,
        comunidad_indigena = @comunidad_indigena,
        pais = @pais,
        departamento = @departamento,
        municipio = @municipio,
        nacionalidad = @nacionalidad,
        id_sexo = @id_sexo,
        id_etnia = @id_etnia,
        id_lengua = @id_lengua,
        id_discapacidad = @id_discapacidad,
        id_tutor_x_estudiante = @id_tutor_x_estudiante
    WHERE id_estudiante = @id_estudiante;
  END
  ELSE 
  BEGIN
        RAISERROR('El estudiante no existe', 16, 1);
  END
END;

