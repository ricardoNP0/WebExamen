# Sistema Web de Reservas de Hoteles

Proyecto desarrollado con **ASP.NET Core MVC** para la gestión de hoteles y reservas, incorporando **autenticación con Identity**, **manejo de roles**, **dashboard administrativo** y **generación de reportes en PDF**.

---

## Descripción del proyecto

El sistema permite administrar hoteles y realizar reservas mediante una aplicación web moderna, organizada y responsiva.

Se implementaron dos tipos de usuarios:

- **Administrador**
- **Cliente**

El administrador puede gestionar hoteles, visualizar estadísticas y generar reportes.  
El cliente puede registrarse, iniciar sesión, visualizar hoteles y realizar reservas.

---

## Objetivo

Desarrollar una aplicación web utilizando **ASP.NET Core MVC** que permita:

- gestionar hoteles
- registrar y controlar reservas
- manejar usuarios con autenticación segura
- restringir funcionalidades según roles
- visualizar información en un dashboard
- generar reportes en PDF

---

## Tecnologías utilizadas

- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server / LocalDB**
- **ASP.NET Core Identity**
- **Bootstrap**
- **Chart.js**
- **QuestPDF**

---

## Funcionalidades implementadas

### Autenticación e identidad
- Registro de usuarios
- Inicio de sesión
- Cierre de sesión
- Gestión de usuarios mediante Identity
- Persistencia de usuarios en base de datos
- Asignación de roles

### Roles del sistema
- **Administrador**
  - CRUD completo de hoteles
  - Visualización de todas las reservas
  - Acceso al dashboard
  - Acceso a reportes PDF
- **Cliente**
  - Visualización de hoteles
  - Registro de reservas
  - Consulta de sus propias reservas

### Gestión de hoteles
- Listado de hoteles
- Registro de nuevos hoteles
- Edición de hoteles
- Eliminación de hoteles
- Visualización de detalle

### Gestión de reservas
- Registro de reservas
- Validación de fechas
- Restricción de fechas pasadas
- Validación de fecha fin mayor a fecha inicio
- Visualización de reservas por usuario
- Visualización total para administrador

### Dashboard
- Total de hoteles
- Total de usuarios
- Total de reservas
- Gráfico de reservas por mes
- Gráfico de hoteles más reservados

### Reportes
- Reporte general de reservas
- Exportación de reservas en PDF

---

## Estructura del proyecto

```text
WebExamen/
│
├── Controllers/
│   ├── HomeController.cs
│   ├── HotelesController.cs
│   ├── ReservasController.cs
│   ├── DashboardController.cs
│   └── ReportesController.cs
│
├── Models/
│   ├── ApplicationUser.cs
│   ├── Hotel.cs
│   ├── Reserva.cs
│   └── ErrorViewModel.cs
│
├── Data/
│   ├── ApplicationDbContext.cs
│   └── DbInitializer.cs
│
├── ViewModels/
│   ├── ReservaCreateViewModel.cs
│   ├── DashboardViewModel.cs
│   └── ReporteReservaItemViewModel.cs
│
├── Services/
│   └── PdfService.cs
│
├── Views/
│   ├── Home/
│   ├── Hoteles/
│   ├── Reservas/
│   ├── Dashboard/
│   ├── Reportes/
│   └── Shared/
│
├── Areas/
│   └── Identity/
│
├── wwwroot/
│
├── Program.cs
├── appsettings.json
└── README.md











Modelo de datos
Hotel
Id
Nombre
Dirección
Precio por noche
Descripción
ApplicationUser
Id
NombreCompleto
Email
PasswordHash
demás campos de Identity
Reserva
Id
FechaInicio
FechaFin
UsuarioId
HotelId
Relaciones
Un usuario puede tener muchas reservas
Un hotel puede tener muchas reservas
Validaciones implementadas

Se utilizaron Data Annotations para asegurar la integridad de los datos:

[Required]
[StringLength]
[Range]
[DataType(DataType.Date)]

También se implementaron validaciones funcionales para reservas:

no se permiten reservas en fechas pasadas
la fecha de fin debe ser mayor a la fecha de inicio
Roles y usuario administrador inicial

El sistema crea automáticamente los roles:

Administrador
Cliente

También se genera un usuario administrador inicial mediante DbInitializer.

Credenciales de administrador
Correo: admin@hotel.com
Contraseña: Admin123*
Flujo general del sistema
Cliente
Se registra en el sistema
Inicia sesión
Visualiza hoteles disponibles
Realiza reservas
Consulta sus reservas
Administrador
Inicia sesión
Gestiona hoteles
Visualiza todas las reservas
Consulta el dashboard
Descarga reportes en PDF
Instalación y ejecución
1. Clonar el repositorio
git clone <URL_DEL_REPOSITORIO>
2. Ingresar al proyecto
cd WebExamen
3. Restaurar paquetes
dotnet restore
4. Aplicar migraciones
dotnet ef database update
5. Ejecutar el proyecto
dotnet run
Paquetes utilizados
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Identity.UI
dotnet add package QuestPDF
Interfaz

La interfaz fue desarrollada con Bootstrap, buscando que el sistema sea:

claro
ordenado
responsivo
visualmente profesional

Se personalizó el layout principal en español con navegación para:

Inicio
Hoteles
Reservas
Dashboard
Reportes
Inicio/Cierre de sesión
Dashboard y reportes
Dashboard

El panel administrativo muestra:

cantidad total de hoteles
cantidad total de usuarios
cantidad total de reservas
reservas por mes
hoteles más reservados
Reportes PDF

Se implementó exportación de reservas a PDF utilizando QuestPDF.

Trabajo en equipo

El proyecto fue dividido en módulos para facilitar el trabajo colaborativo.

Ricardo

Encargado de:

estructura base del proyecto
configuración de ASP.NET Core MVC
implementación de Identity
manejo de roles
creación del usuario administrador inicial
CRUD de hoteles
layout general del sistema
Vania

Encargada de:

módulo de reservas
dashboard administrativo
reportes PDF
vistas complementarias del sistema
Organización en GitHub

Se trabajó con:

rama principal main
ramas individuales por integrante
integración final del proyecto en una sola versión

Ejemplo de ramas:

main
ricardo
vania
Estado actual del proyecto

El sistema cuenta con:

autenticación funcional
roles implementados
hoteles funcionales
reservas funcionales
dashboard funcional
reportes PDF funcionales
interfaz en español
estructura lista para demostración académica
Posibles mejoras futuras
validación de disponibilidad real por fechas
edición y cancelación de reservas
búsqueda avanzada de hoteles
carga de imágenes de hoteles
filtros por precio y ubicación
reportes PDF más detallados
envío de confirmaciones por correo
Conclusión

Este proyecto permitió aplicar conocimientos de:

desarrollo web con ASP.NET Core MVC
persistencia de datos con Entity Framework Core
autenticación y autorización con Identity
diseño de interfaces con Bootstrap
generación de reportes
trabajo colaborativo con GitHub

El resultado es un sistema funcional, organizado y escalable para la gestión de hoteles y reservas.

Autoría

Proyecto académico desarrollado por:

Ricardo
Vania