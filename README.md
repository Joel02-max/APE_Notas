📘  de Gestión Académica - Notas

Aplicación Web para gestionar estudiantes, cursos, notas y promedios académicos. 
Desarrollado en ASP.NET Core Web API con un frontend en HTML, CSS y JavaScript

Tecnologías usadas
ASP.NET Core Web API

C# (Programación orientada a servicios)

Entity Framework Core

SQL Server

HTML, CSS, JavaScript (Frontend simple)

Swagger (para documentación y pruebas del API)

Estructura del Proyecto
Controladores: Definen los endpoints de la API RESTful.

Servicios: Contienen la lógica de negocio, conectan repositorios y controladores.

Repositorios: Gestionan el acceso a la base de datos.

Modelos (Models): Definen las entidades de dominio.

DTOs: Facilitan la transferencia de datos de forma estructurada y simplificada.

Middleware: Control global de excepciones para capturar errores en toda la aplicación.

Configuración: Define la conexión a la base de datos y el comportamiento general del proyecto.

Frontend: Ubicado en wwwroot, permite la interacción con la API.

Requisitos
Tener instalado SQL Server.

Contar con Visual Studio 2022 (o superior) con el SDK de .NET.

Disponer de .NET SDK 7.0 o superior.

Base de datos creada previamente (estructura entregada junto al proyecto).

Flujo general de trabajo
Estudiantes: Consultar, filtrar, registrar y listar estudiantes.

Notas: Asignar, modificar y consultar notas de los estudiantes por curso.

Promedios: Consultar promedios calculados para cada estudiante.

Control de excepciones: Ante cualquier error de base de datos, la aplicación captura la excepción específica y devuelve un mensaje controlado al usuario.

Ejecución de la aplicación
El backend se ejecuta como API RESTful en ASP.NET Core.

El frontend es estático, directamente accesible desde el navegador.

La API expone endpoints documentados a través de Swagger para facilitar las pruebas.

Control de excepciones implementado
En cada clase de acceso a datos (repositorios), los bloques try-catch capturan los errores de SQL Server usando SqlException.

Si ocurre un error en la base de datos (violación de clave, error de conexión, etc.), la excepción es lanzada hacia el middleware.

El middleware global captura la excepción, la registra (log) y devuelve un mensaje amigable al cliente.

Esto garantiza que la aplicación nunca expone trazas técnicas al usuario final.

Pruebas realizadas
Inserciones y actualizaciones válidas de estudiantes y notas.

Pruebas forzadas de errores (por ejemplo, cédulas duplicadas, claves inexistentes, violación de restricciones de integridad referencial).
