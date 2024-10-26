# Documentación SCM-API

### Instalación
Para instalar las dependencias necesarias, ejecute el siguiente comando:
```bash
dotnet restore
```

### Uso
Para iniciar la API:
```bash
cd SCM-API
dotnet run --urls "http://localhost:3000"
```

Puedes remplazar el **3000** por el puerto que desees usar.

### Tecnologías
- C#
- .NET
- SQL Server

### Autentificación
La API implementa autenticación básica `(Basic Auth)` utilizando un `middleware` que se ejecuta antes de que cualquier ruta sea accedida. Este middleware intercepta cada solicitud y verifica la presencia de las credenciales en la cabecera `Authorization`, las cuales deben estar codificadas en **Base64** siguiendo el formato `username:password`. Si las credenciales son válidas, la solicitud procede hacia la ruta solicitada; de lo contrario, el middleware devuelve una respuesta con el código HTTP `401` **Unauthorized**, solicitando autenticación. Para garantizar la seguridad de las credenciales transmitidas, es obligatorio que las solicitudes se realicen a través de HTTPS, protegiendo así la información sensible durante su tránsito.

```bash
curl -u username:password https://api.example.com/protected-resource
```

La `API` necesita un archivo que no esta en GitHub, que es el que guarda las credenciales válidas.

#### authsettings.json
```json
{
  "username": "credencial-usuario",
  "password": "credencial-password"
}
```

### Organización de carpetas

- **SCM-API/**:
  - **Clase/**: Guarda las clases para las entidades de la base de datos.
  - **Connection/**: Contiene una clase que se encarga de abrir y cerrar la conexión con la base de datos.
  - **Controllers/**: Contiene los controladores que manejan las rutas a las que se dirigen las peticiones, además realiza las validaciones de los datos que espera recibir.
  - **Lib/**: Contiene scripts del proyecto que automatizan los procesos que realiza el servidor como el manejo de errores, validación, entre otras.
  - **Middlewares/**: Contiene los middlewares que se ejecutan antes de que los controladores reciban las peticiones.
  - **Models/**: Contiene toda la lógica de negocio, es el encargado de las validaciones del negocio y de realizar las peticiones a la base de datos.
- **database/**: 
  - **inserts/**: Contiene registros para pruebas de procedimientos y tablas.
  - **procedures/**: Contiene los procedimientos almacenados de todas las tablas que lo vayan a necesitar.
  - **triggers/**: Contiene los disparadores que se ejecutaran para cada tabla que necesite.
  - **create.sql**: Contiene las querys para crear la base de datos y tablas necesarias para la API.
  - **insert.sql**: Contiene las inserciones a las tablas cátalogos.

La `API` sigue la arquitectura **Modelo-Vista-Controlador** `(MVC)`, que organiza el código en tres componentes principales: el `Modelo`, que gestiona la lógica de datos y las interacciones con la base de datos; el `Controlador`, que actúa como intermediario entre el modelo y la vista, procesando las solicitudes HTTP y determinando qué acciones tomar en función de la lógica de negocio; y la `Vista`, que se encarga de formatear los datos que se envían de vuelta al cliente. Este patrón permite separar las responsabilidades del código, lo que mejora la mantenibilidad, escalabilidad y facilita la evolución de la API sin comprometer su funcionalidad central.

### Endpoints

Cada uno de estos endpoind corresponde a una `url` específica y un método http o https `(GET, POST, PUT, DELETE)`, que define la acción que se realizará.

Si se realiza una petición que tenga cuerpo, debe ser en formato `json` ya que el servidor solo recibe este. En todo caso las respuestas también serán en `json`.

En caso de que algo en la petición falle o ejecute correctamente mostrá un status y un mensaje con el error o aceptación.

#### Estudiantes

`GET` **/api/estudiantes**
- Obtiene todos los registros de estudiantes que se guardan en la base de datos.

`GET` **/api/estudiantes/:id**
- Obtiene los datos de un estudiante en específico, dependiendo el **id** que se le pase. 

`POST` **/api/estudiantes**
- Crea un estudiante con los datos que se le envían en el cuerpo de la petición. 

`PUT` **/api/estudiantes/:id**
- Actualiza un estudiante con los nuevos datos que se obtienen del cuerpo de la petición.

`DELETE` **/api/estudiantes/:id**
- Elimina un estudiante en específico, en este caso el **id** que se le pase como párametro.

Información que necesita para crear o actualizar registros de estudiantes:

```json
{
  "Nombres": "string",
  "Apellidos": "string",
  "Cedula": "000-000000-0000X",
  "FechaNacimiento": "yyyy-mm-dd",
  "Telefono": "0000-0000", ?
  "Direccion": "string",
  "PartidaNacimiento": "boleano",
  "FechaMatricula": "yyyy-mm-dd",
  "Pais": "string",
  "Nacionalidad": "string",
  "Departamento": "string",
  "Municipio": "string",
  "Barrio": "string",
  "Peso": "number",
  "Talla": "number",
  "TerritorioIndigena": "string", ?
  "ComunidadIndigena": "string", ?
  "Sexo": "Masculino/Femenino",
  "Etnia": "string",
  "Lengua": "string",
  "Discapacidad": "string",
  "Tutor": "000-000000-0000X"
}
```

Los campos que poseen un signo de interrogación no son necesarios a la hora de crear un registro.

#### Datos académicos

`GET` **/api/datos-academicos**
- Obtiene los registros académicos de todos los estudiantes de la base de datos.

`GET` **/api/datos-academicos/:codigo**
- Obtiene los datos académicos de un estudiante en específico, dependiendo el **codigo estudiantil** que se le pase. 

`POST` **/api/datos-academicos**
- Crea un registro académico con los datos que se le envían en el cuerpo de la petición. 

`PUT` **/api/datos-academicos/:codigo**
- Actualiza los datos académicos con los nuevos datos que se obtienen del cuerpo de la petición.

`DELETE` **/api/datos-academicos/:codigo**
- Elimina un registro en específico, en este caso el **codigo** que se le pase como párametro.

Información que necesita para crear o actualizar registros académicos:

```json
{
  "CodigoEstudiante": "código del estudiante",
  "FechaMatricula": "yyyy-mm-dd",
  "NivelEducativo": "string",
  "Repitente": "boleano",
  "Modalidad": "string",
  "Grado": "Number",
  "Seccion": "string",
  "Turno": "string",
  "centro": "string",
  "IdEstudiante": "Number"
}
```

#### Traslados

`GET` **/api/traslados**
- Obtiene todos los traslados que se han realizado.

`GET` **/api/traslados/:id**
- Obtiene un traslado en específico, dependiendo el **id** que se le pase. 

`POST` **/api/traslados**
- Crea un registro de traslado con los datos que se le envían en el cuerpo de la petición. 

`PUT` **/api/traslados/:id**
- Actualiza los datos de un tralado con los nuevos datos que se obtienen del cuerpo de la petición.

`DELETE` **/api/traslados/:id**
- Elimina un traslado en específico, en este caso el **id** que se le pase como párametro.

Información que necesita para crear o actualizar traslados:

```json
{
  "MotivoTraslado": "string",
  "FechaTraslado": "yyyy-mm-dd",
  "CodigoEstudiante": "codigo del estudiante",
  "Centro": "string (centro del que viene)",
  "Periodo": "string"
}
```

#### Tutores de estudiantes

`GET` **/api/tutores-estudiantes**
- Obtiene los datos de todos los tutores de los estudiantes que están registrados.

`GET` **/api/tutores-estudiantes/:id**
- Obtiene un tutor en específico, dependiendo el **id** que se le pase. 

`POST` **/api/tutores-estudiantes**
- Crea un registro para un nuevo tutor con los datos que se le envían en el cuerpo de la petición. 

`PUT` **/api/tutores-estudiantes/:id**
- Actualiza los datos de un tutor con los nuevos datos que se obtienen del cuerpo de la petición.

`DELETE` **/api/tutores-estudiantes/:id**
- Elimina un tutor en específico, en este caso el **id** que se le pase como párametro.

Información que necesita para crear o actualizar tutores de estudiantes:

```json
{
  "Nombres": "string",
  "Apellidos": "yyyy-mm-dd",
  "Cedula": "000-000000-0000X",
  "Telefono": "####-####"
}
```