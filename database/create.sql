-- BASE DE DATOS EN SQL SERVER

CREATE DATABASE sistema_control_matricula;
USE sistema_gestion_matricula;

-- TABLES

CREATE TABLE sexos(
    id_sexo INT PRIMARY KEY IDENTITY(1,1),
    sexo VARCHAR(20) NOT NULL
);

CREATE TABLE etnias(
    id_etnia INT PRIMARY KEY IDENTITY(1,1),
    etnia VARCHAR(50) NOT NULL
);

CREATE TABLE lenguas(
    id_lengua INT PRIMARY KEY IDENTITY(1,1),
    lengua VARCHAR(50) NOT NULL
);

CREATE TABLE discapacidades(
    id_discapacidad INT PRIMARY KEY IDENTITY(1,1),
    discapacidad VARCHAR(50) NOT NULL
);

CREATE TABLE turnos(
    id_turno INT PRIMARY KEY IDENTITY(1,1),
    turno VARCHAR(100) NOT NULL
);

CREATE TABLE periodos (
    id_periodo INT PRIMARY KEY IDENTITY(1,1),
    periodo VARCHAR(100) NOT NULL
);

CREATE TABLE centros (
    id_centro INT PRIMARY KEY IDENTITY(1,1),
    centro VARCHAR(150) NOT NULL
);

CREATE TABLE grados (
    id_grado INT PRIMARY KEY IDENTITY(1,1),
    grado INT NOT NULL
);

CREATE TABLE secciones (
    id_seccion INT PRIMARY KEY IDENTITY(1,1),
    seccion CHAR(1) NOT NULL
);

CREATE TABLE modalidades (
    id_modalidad INT PRIMARY KEY IDENTITY(1,1),
    modalidad VARCHAR(100) NOT NULL
);

CREATE TABLE tutores_x_estudiantes(
    id_tutor_x_estudiante INT PRIMARY KEY IDENTITY(1,1),
    nombres VARCHAR(255) NOT NULL,
    apellidos VARCHAR(255) NOT NULL,
    cedula VARCHAR(16) NOT NULL,
    telefono VARCHAR(9) NOT NULL
);

CREATE TABLE estudiantes(
    id_estudiante INT PRIMARY KEY IDENTITY(1,1),
    nombres VARCHAR(255) NOT NULL,
    apellidos VARCHAR(255) NOT NULL,
    cedula VARCHAR(16),
    fecha_nacimiento DATE NOT NULL,
    direccion VARCHAR(255) NOT NULL, 
    telefono VARCHAR(9),
    partida_nacimiento BIT NOT NULL,
    fecha_matricula DATE NOT NULL,
    barrio VARCHAR(255) NOT NULL,
    peso DECIMAL(18, 2) DEFAULT 0,
    talla DECIMAL(18, 2) DEFAULT 0,
    territorio_indigena VARCHAR(255),
    comunidad_indigena VARCHAR(255),
    pais VARCHAR(100) NOT NULL,
    departamento VARCHAR(100) NOT NULL,
    municipio VARCHAR(100) NOT NULL,
    nacionalidad VARCHAR(100) NOT NULL,
    id_sexo INT,
    id_etnia INT,
    id_lengua INT,
    id_discapacidad INT,
    id_tutor_x_estudiante INT,
    FOREIGN KEY (id_sexo) REFERENCES sexos(id_sexo),
    FOREIGN KEY (id_etnia) REFERENCES etnias(id_etnia),
    FOREIGN KEY (id_lengua) REFERENCES lenguas(id_lengua),
    FOREIGN KEY (id_discapacidad) REFERENCES discapacidades(id_discapacidad),
    FOREIGN KEY (id_tutor_x_estudiante) REFERENCES tutores_x_estudiantes(id_tutor_x_estudiante)
);

CREATE TABLE datos_academicos (
    codigo_estudiante VARCHAR(20) PRIMARY KEY,
    fecha_matricula DATE NOT NULL,
    nivel_educativo VARCHAR(255) NOT NULL,
    repitente BIT DEFAULT 0,
    id_modalidad INT,
    id_grado INT,
    id_seccion INT,
    id_turno INT,
    id_centro INT,
    id_estudiante INT,
    FOREIGN KEY (id_modalidad) REFERENCES modalidades(id_modalidad),
    FOREIGN KEY (id_grado) REFERENCES grados(id_grado),
    FOREIGN KEY (id_seccion) REFERENCES secciones(id_seccion),
    FOREIGN KEY (id_turno) REFERENCES turnos(id_turno),
    FOREIGN KEY (id_centro) REFERENCES centros(id_centro),
    FOREIGN KEY (id_estudiante) REFERENCES estudiantes(id_estudiante)
);

CREATE TABLE traslados (
    id_traslado INT PRIMARY KEY IDENTITY(1,1),
    motivo_traslado VARCHAR(255) NOT NULL,
    fecha_traslado DATE NOT NULL,
    codigo_estudiante VARCHAR(20),
    id_centro INT,
    id_periodo INT,
    id_estudiante INT,
    FOREIGN KEY (codigo_estudiante) REFERENCES datos_academicos(codigo_estudiante),
    FOREIGN KEY (id_centro) REFERENCES centros(id_centro),
    FOREIGN KEY (id_periodo) REFERENCES periodos(id_periodo),
    FOREIGN KEY (id_estudiante) REFERENCES estudiantes(id_estudiante)
);