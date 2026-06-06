# Hospital API

API REST para gestión clínica/hospitalaria en **.NET 9** con Clean Architecture, JWT, SQL Server y formularios dinámicos.

## Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (SQL Server local)
- Opcional: `make`

## Inicio rápido

### 0. Configuración local (primera vez)

```bash
cp Hospital.Api/appsettings.Development.example.json Hospital.Api/appsettings.Development.json
cp Hospital.Admin/.env.example Hospital.Admin/.env
```

Edita los valores sensibles en `appsettings.Development.json` o usa [User Secrets](#configuración-y-secretos).

### 1. Base de datos

```bash
make docker-up
# o: docker compose up -d
```

Contraseña SA configurable con variable de entorno:

```bash
export SA_PASSWORD="TuPasswordSeguro123*"
docker compose up -d
```

### 2. Migraciones y seed

Al arrancar la API en Development se ejecutan migraciones y seed automáticamente.

Manual:

```bash
make update
make run
```

### 3. Credenciales iniciales (seed)

| Campo | Valor por defecto |
|-------|-------------------|
| Usuario | `admin` |
| Contraseña | `Admin123!` |
| Rol | `Admin` |

Configurable en `Seed` de `appsettings.Development.json`.

### 4. Swagger y JWT

1. Abre `https://localhost:7xxx/swagger` (puerto según `launchSettings.json`)
2. `POST /api/auth/login` con `admin` / `Admin123!`
3. **Authorize** → `Bearer {token}`
4. Prueba los endpoints protegidos

## Configuración y secretos

No subas claves reales al repositorio. En desarrollo usa **User Secrets**:

```bash
cd Hospital.Api

dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=HospitalDb;User Id=sa;Password=TU_PASSWORD;TrustServerCertificate=True;"
dotnet user-secrets set "Jwt:Key" "TuClaveSecretaDeAlMenos32Caracteres!!"
```

Variables de entorno (producción):

| Variable | Descripción |
|----------|-------------|
| `ConnectionStrings__DefaultConnection` | Cadena SQL Server |
| `Jwt__Key` | Clave JWT (mín. 32 caracteres) |
| `Jwt__Issuer` | Emisor |
| `Jwt__Audience` | Audiencia |
| `Cors__AllowedOrigins__0` | Origen frontend permitido |
| `Seed__AdminPassword` | Contraseña del admin inicial |

## CORS por entorno

En `appsettings.Development.json`:

```json
"Cors": {
  "AllowedOrigins": ["http://localhost:5173", "http://localhost:3000"]
}
```

En producción define orígenes explícitos. Sin orígenes configurados, la API permite cualquier origen (solo recomendado para pruebas locales).

## Autenticación y roles

| Rol | Acceso |
|-----|--------|
| **Admin** | Usuarios, roles y todo el sistema |
| **Medico** | Pacientes, atenciones, formularios, catálogos |
| **Recepcion** | Igual que Medico (staff clínico) |

Políticas:

- `Staff` → Admin, Medico, Recepcion
- `AdminOnly` → solo Admin

Endpoints públicos: `POST /api/auth/login`, `POST /api/auth/refresh`.

## Paginación

Los listados usan query params:

```
GET /api/pacientes?page=1&pageSize=20&search=garcia
```

Respuesta:

```json
{
  "items": [],
  "totalCount": 100,
  "page": 1,
  "pageSize": 20,
  "totalPages": 5
}
```

`pageSize` máximo: **100**.

## Auditoría

Entidades `AuditableEntity` registran automáticamente:

- **Create:** `CreatedAt`, `CreatedBy`, `Activo = true`
- **Update:** `UpdatedAt`, `UpdatedBy`

Con JWT: `CreatedBy` / `UpdatedBy` = nombre de usuario. Sin login: `"Sistema"`.

## Estructura del proyecto

```
Hospital.Domain/          Entidades y reglas
Hospital.Application/     DTOs, interfaces, validadores
Hospital.Infrastructure/  EF Core, servicios, seed
Hospital.Api/               Controllers, middleware, JWT
```

## Comandos Make

| Comando | Descripción |
|---------|-------------|
| `make docker-up` | Levanta SQL Server |
| `make docker-down` | Detiene contenedores |
| `make migration name=Nombre` | Nueva migración EF |
| `make update` | Aplica migraciones |
| `make run` | Ejecuta la API |
| `make watch` | Hot reload |

## API principal

| Recurso | Ruta |
|---------|------|
| Auth | `/api/auth` |
| Usuarios | `/api/users` (Admin) |
| Roles | `/api/roles` (Admin) |
| Pacientes | `/api/pacientes` |
| Tipos atención | `/api/tipos-atencion` |
| Formularios | `/api/formularios` |
| Campos formulario | `/api/formulario-campos` |
| Atenciones | `/api/atenciones` |
| Valores atención | `/api/atencion-valores` |
| Catálogos | `/api/catalogo-grupos`, `/api/catalogo-items` |

## Códigos HTTP (middleware)

| Excepción | HTTP |
|-----------|------|
| `BadRequestException` | 400 |
| `UnauthorizedException` | 401 |
| `NotFoundException` | 404 |
| `ConflictException` | 409 |
