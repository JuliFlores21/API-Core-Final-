**API de Gestión de Estudiantes**

Backend del sistema de gestión de estudiantes desarrollado en ASP.NET Core con SQL Server. Proporciona una API RESTful para gestionar estudiantes con validaciones específicas.


Tabla de Contenidos
Requisitos
Instalación
Ejecución
Endpoints
Despliegue
Contribución
Requisitos
.NET SDK
SQL Server
Instalación
Clona el repositorio:

bash
Copiar código
git clone <URL_DEL_REPOSITORIO_BACKEND>
cd <NOMBRE_DEL_DIRECTORIO_BACKEND>
Configura la base de datos en appsettings.json:

json
Copiar código
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=GestionEstudiantes;User Id=tu_usuario;Password=tu_contraseña;"
    }
}
Ejecuta las migraciones:

bash
Copiar código
dotnet ef database update
Configura CORS en Program.cs para permitir el acceso desde el frontend.

Ejecución
Inicia el servidor:

bash
Copiar código
dotnet run
La API estará disponible en http://localhost:5000.

Endpoints
GET /api/estudiante - Listar estudiantes
POST /api/estudiante - Agregar estudiante
PUT /api/estudiante/{id} - Actualizar estudiante
DELETE /api/estudiante/{id} - Eliminar estudiante
