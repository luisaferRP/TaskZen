# 💻 TaskZen - Administrador de tareas 💻

Este repositorio contiene el desarrollo de un **Administrador de Tareas** con un diseño práctico, fácil de usar y enfocado en la experiencia del usuario. Presenta un estilo **Kanban**, donde el usuario puede gestionar visualmente sus tareas según su estado.

---

## 👩‍💻 Funcionalidades

- ✅ Crear una tarea
- 📋 Listar tareas en columnas según su estado
- 🔍 Buscar tareas
- ✏️ Actualizar tareas
- ❌ Eliminar tareas
- 👤 Registrar y gestionar usuarios

---

## 🚀 Tecnologías utilizadas

- C#
- Entity Framework Core
- SQL Server
- Visual Studio

---

## 🧰 Características

- Conexión directa a la base de datos usando Entity Framework Core
- CRUD completo: Create, Read, Update, Delete
- Manejo de errores mediante `try-catch`
- Sistema de autenticación con JWT
- Separación de tareas por estado (Kanban)

---

## ▶️ Ejecutar el proyecto localmente

### 1️⃣ Clona el repositorio

```bash
git clone https://github.com/luisaferRP/TaskZen
``` 

2️⃣ Abre el proyecto
Abre la solución en Visual Studio o Visual Studio Code.

3️⃣ Instala las dependencias necesarias
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design

``` 

4️⃣ Configura la cadena de conexión
Crea un archivo .env en la raíz del proyecto y agrega tu cadena de conexión y configuración JWT. Ejemplo:

```bash
CONNECTION_STRING=Server=localhost\SQLEXPRESS;Database=TaskDB;Trusted_Connection=True;TrustServerCertificate=True;

JWT_KEY=79gK6ueP6g35cAUV20OgpsnGwrKQt7ayhWi
JWT_ISSUER=http://localhost:5025
JWT_AUDIENCE=public
JWT_EXPIRATION_IN=60
```

✅ Resultado
Una vez ejecutado el proyecto, podrás:

👤 Crear tu usuario

📝 Crear tareas y gestionarlas por estado (Kanban)

✏️ Editar o eliminar tareas fácilmente
