# 📦 StockMaster Elite - Gestión de Inventario Energético
**Desarrollado por Uriel Zagada Dominguez**

Sistema integral de gestión de inventario profesional diseñado con arquitectura de software robusta y persistencia de datos en la nube.

## 🚀 Tecnologías y Skills
- **Backend:** .NET 8 (C#) Web API.
- **Arquitectura:** N-Capas (Entidades, Datos, Negocio, Web).
- **Base de Datos:** PostgreSQL hospedado en Supabase Cloud.
- **Frontend:** HTML5, JavaScript Moderno, Bootstrap 5 y Chart.js.
- **Integraciones:** Manejo de archivos Excel (ClosedXML) e Inyección de Dependencias.
- **Herramientas:** VS Code, Git, Swagger.

## 🛠️ Funcionalidades Pro
- **Dashboard Estadístico:** Gráficos dinámicos con Chart.js sincronizados con la base de datos.
- **Ciclo CRUD Completo:** Creación manual vía Modales, lectura, actualización y eliminación.
- **Gestión de Archivos:** Importación masiva y exportación de reportes en Microsoft Excel.
- **Resiliencia de Red:** Implementación de Connection Pooling y reintentos automáticos.

## 🏗️ Estructura N-Layers
- `StockMaster.Entidades`: Modelos de datos POCO.
- `StockMaster.Data`: Persistencia con Entity Framework Core y Fluent API.
- `StockMaster.Negocio`: Lógica de servicios e integraciones externas.
- `StockMaster.Web`: Controladores REST y UI (wwwroot).
