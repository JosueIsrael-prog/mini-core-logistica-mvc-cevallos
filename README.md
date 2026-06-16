# Mini Core de Logística — Sistema de Reporte de Envíos

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![React 19](https://img.shields.io/badge/React-19-61DAFB?style=for-the-badge&logo=react&logoColor=black)
![Supabase](https://img.shields.io/badge/Supabase-PostgreSQL-3ECF8E?style=for-the-badge&logo=supabase&logoColor=white)
![Vite](https://img.shields.io/badge/Vite-8-646CFF?style=for-the-badge&logo=vite&logoColor=white)
![pnpm](https://img.shields.io/badge/pnpm-10-F69220?style=for-the-badge&logo=pnpm&logoColor=white)
![Render](https://img.shields.io/badge/Render-Deploy-46E3B7?style=for-the-badge&logo=render&logoColor=white)

---

## Contexto del Proyecto

Aplicación web funcional desarrollada bajo el patrón de arquitectura **Modelo-Vista-Controlador (MVC)** con un stack desacoplado: backend en ASP.NET Core Web API (.NET 8) y frontend en React 19.

El sistema resuelve un problema logístico concreto: dado un conjunto de repartidores, zonas de entrega con tarifas fijas por kilogramo y registros de envíos, la aplicación **filtra los envíos dentro de un rango de fechas** seleccionado por el usuario y **calcula de forma automatizada el costo total** generado por cada repartidor. El cálculo se basa en el peso en kg de los paquetes enviados multiplicado por la tarifa de la zona de entrega asignada, desglosando los resultados por cada combinación repartidor-zona.

---

## Arquitectura de la Solución (MVC)

```
┌─────────────────────────────────────────────────────────────────┐
│                        CLIENTE (Vista)                          │
│              React 19 · Vite · Fetch nativo                     │
│                   http://localhost:5173                          │
└────────────────────────────┬────────────────────────────────────┘
                             │  HTTP GET /api/logistica/reporte
                             ▼
┌─────────────────────────────────────────────────────────────────┐
│                    API REST (Controlador)                        │
│          ASP.NET Core Web API · .NET 8 · CORS                   │
│                   http://localhost:5122                          │
└────────────────────────────┬────────────────────────────────────┘
                             │  Entity Framework Core · Npgsql
                             ▼
┌─────────────────────────────────────────────────────────────────┐
│                   BASE DE DATOS (Modelo)                        │
│         Supabase · PostgreSQL · Seed Data                       │
│          Tablas: repartidores, zonas, envios                    │
└─────────────────────────────────────────────────────────────────┘
```

### Modelo (M) — Capa de Datos

El mapeo objeto-relacional se implementa con **Entity Framework Core** y el proveedor **Npgsql** apuntando a una instancia de PostgreSQL alojada en Supabase. Se definen tres entidades principales:

| Tabla | Campos clave | Propósito |
|-------|-------------|-----------|
| `repartidores` | `id_repartidor`, `nombre`, `email` | Catálogo de repartidores del sistema |
| `zonas` | `id_zona`, `nombre_zona`, `tarifa_por_kg` | Zonas geográficas con tarifa fija por kilogramo |
| `envios` | `id_envio`, `id_repartidor` (FK), `id_zona` (FK), `peso_kg`, `fecha_envio` | Registro de cada envío realizado |

Los datos semilla se inyectan mediante `HasData()` en el `DbContext` y se aplican automáticamente a través de migraciones de EF Core al iniciar la aplicación.

### Controlador (C) — Lógica de Negocio

Un único endpoint `GET /api/logistica/reporte` recibe los parámetros `fechaInicio` y `fechaFin` y ejecuta la siguiente lógica:

1. Consulta todos los repartidores registrados.
2. Filtra los envíos cuya `fecha_envio` se encuentre dentro del rango inclusivo proporcionado.
3. Agrupa los envíos de cada repartidor por zona de entrega.
4. Calcula el costo total por grupo: `totalKg × tarifaPorKg`.
5. Incluye a los repartidores sin envíos en el período con valores en cero.
6. Retorna la lista ordenada alfabéticamente por nombre del repartidor.

### Vista (V) — Interfaz de Usuario

Interfaz SPA construida en **React 19** con **Vite** como bundler. Utiliza hooks reactivos (`useState`) para gestionar el estado del formulario, los resultados y los indicadores de carga. La comunicación con la API se realiza mediante `fetch` nativo sin dependencias externas adicionales.

La vista presenta:
- Formulario con selectores de fecha de inicio y fin.
- Tabla de resultados con las columnas: Repartidor, Envíos, Total Kg, Zona, Tarifa Aplicada y Costo Total.
- Estados de carga, error y mensaje cuando no se encuentran resultados.

---

## Guía de Ejecución Local

### Prerrequisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (v18+)
- [pnpm](https://pnpm.io/) (`npm install -g pnpm`)

### Backend (API)

```bash
cd Logistica.API
dotnet run
```

La API se levanta en `http://localhost:5122`. Swagger disponible en `/swagger` en modo Development.

### Frontend (Cliente)

```bash
cd Logistica.Client
pnpm install
pnpm dev
```

El cliente se levanta en `http://localhost:5173` y consume la API automáticamente.

> **Nota:** Al iniciar la API por primera vez, las migraciones de EF Core se aplican automáticamente y las tablas con datos semilla se crean en Supabase.

---

## Entregables

| Entregable | Enlace |
|------------|--------|
| 📹 Video Explicativo (Loom/YouTube) | [Insertar link del video aquí] |
| 🌐 Proyecto Deployado (Render) | [Insertar link de Render aquí] |
| 📦 Repositorio GitHub | [github.com/JosueIsrael-prog/mini-core-logistica-mvc-cevallos](https://github.com/JosueIsrael-prog/mini-core-logistica-mvc-cevallos) |

### Documentación de Referencia

- [Documentación oficial de ASP.NET Core](https://learn.microsoft.com/aspnet/core/)
- [Documentación oficial de React](https://react.dev/)
- [Entity Framework Core — Getting Started](https://learn.microsoft.com/ef/core/)
- [Supabase — Database](https://supabase.com/docs/guides/database/overview)

---

## Información Institucional

| Campo | Detalle |
|-------|---------|
| **Autor** | Josue Israel Cevallos Barbero |
| **Institución** | Universidad de las Américas (UDLA) |
| **Carrera** | Ingeniería de Software |
| **Semestre** | Séptimo Semestre |
| **Año** | 2026 |
| **Correo institucional** | josue.cevallos@udla.edu.ec |
