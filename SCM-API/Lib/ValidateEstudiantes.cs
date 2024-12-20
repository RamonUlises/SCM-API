﻿using SCM_API.Clase;

namespace SCM_API.Lib
{
    public class ValidateEstudiantes
    {
        public Errors ValidarEstudiantes(EstudiantesClass estudiante)
        {
            try
            {
                new Validates().NotNull(estudiante.Nombres);
                new Validates().NotNull(estudiante.Apellidos);
                new Validates().NotNull(estudiante.FechaNacimiento);
                new Validates().NotNull(estudiante.Direccion);
                new Validates().NotNull(estudiante.Telefono);
                new Validates().NotNull(estudiante.PartidaNacimiento);
                new Validates().NotNull(estudiante.FechaMatricula);
                new Validates().NotNull(estudiante.Barrio);
                new Validates().NotNull(estudiante.Peso);
                new Validates().NotNull(estudiante.Talla);
                new Validates().NotNull(estudiante.Pais);
                new Validates().NotNull(estudiante.Departamento);
                new Validates().NotNull(estudiante.Municipio);
                new Validates().NotNull(estudiante.Nacionalidad);
                new Validates().String(estudiante.Nombres, "Nombres");
                new Validates().String(estudiante.Apellidos, "Apellidos");
                if(estudiante.Cedula.Length > 0)
                {
                    new Validates().Cedula(estudiante.Cedula);  
                }
                new Validates().Fecha(estudiante.FechaNacimiento);
                new Validates().String(estudiante.Direccion, "Dirección");
                new Validates().Telefono(estudiante.Telefono);
                new Validates().NotNull(estudiante.PartidaNacimiento);
                new Validates().Fecha(estudiante.FechaMatricula);
                new Validates().String(estudiante.Barrio, "Barrio");
                new Validates().Number(estudiante.Peso.ToString());
                new Validates().Number(estudiante.Talla.ToString());
                if(estudiante.TerritorioIndigena.Length > 0)
                {
                    new Validates().String(estudiante.TerritorioIndigena, "Territorio Indigena");
                }
                if(estudiante.ComunidadIndigena.Length > 0)
                {
                    new Validates().String(estudiante.ComunidadIndigena, "Comunidad Indigena");
                }
                new Validates().String(estudiante.Pais, "Pais");
                new Validates().String(estudiante.Departamento, "Departamento");
                new Validates().String(estudiante.Municipio, "Municipio");
                new Validates().String(estudiante.Nacionalidad, "Nacionalidad");
                new Validates().String(estudiante.Sexo, "Sexo");
                new Validates().String(estudiante.Etnia, "Etnia");
                new Validates().String(estudiante.Lengua, "Lengua");
                new Validates().String(estudiante.Discapacidad, "Discapaciadad");
                new Validates().Cedula(estudiante.TutorEstudiante);

                return new Errors { message = "Estudiante creado", status = true };
            } catch (Exception ex)
            {
                return new Errors { message = ex.Message, status = false };
            }
        }
    }
}
