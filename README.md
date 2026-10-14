# Semana09.Caso1 - Middleware Personalizado

## Proyecto ASP.NET Core MVC que demuestra el uso de middleware en la canalización de solicitudes.

## Middlewares Implementados

### 1. RequestLogMiddleware
- Clase personalizada que registra información de cada solicitud HTTP
- Registra: método HTTP, ruta, código de estado y tiempo de respuesta
- Método de extensión: `UseRequestLog()`

### 2. Middleware en línea para /admin
- Bloquea rutas que empiezan con `/admin` sin clave
- Requiere query string `?key=secret123`
- Devuelve 403 cuando no se proporciona la clave (cortocircuito)

### 3. Middleware terminal /estado
- Ruta especial que responde texto simple
- Usa `app.Map()` para terminal middleware

## Instrucciones de Demostración

### Probar Middleware de Logging
1. Navegar a cualquier ruta (ej: `/`, `/Home/Index`)
2. Ver la consola para ver los logs con método, ruta, código y tiempo

### Probar Middleware de Protección /admin
1. Sin clave: `http://localhost:5155/admin` → 403 "Acceso denegado"
2. Con clave: `http://localhost:5155/admin?key=secret123` → Acceso concedido

### Probar Middleware Terminal /estado
1. Navegar a `http://localhost:5155/estado`
2. Ver respuesta: "Sistema operativo correctamente"

### Demo: Mover UseRequestLog() antes/después de UseStaticAssets()
**Estado actual:** UseRequestLog() está DESPUÉS de UseStaticAssets() - solo registra solicitudes MVC

**Para demostrar:**
1. Mover `app.UseRequestLog()` ANTES de `app.UseStaticAssets()` (línea 19)
2. Reiniciar la aplicación
3. Navegar a la página - verás que se registran también solicitudes de archivos estáticos (CSS, JS)
4. Mover `app.UseRequestLog()` DESPUÉS de `app.UseStaticAssets()` (línea 45)
5. Reiniciar - solo se registran las solicitudes de controladores, no archivos estáticos

## Compilar y Ejecutar
```bash
dotnet run
```
Aplicación disponible en: http://localhost:5155
"# Middleware_net" 
