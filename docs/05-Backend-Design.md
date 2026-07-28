# 1. Objetivo

Este documento define el diseño técnico del backend de Smart Workforce AI.

Su propósito es establecer la estructura de la solución, las convenciones de desarrollo y los componentes reutilizables que servirán como base para la implementación del sistema.

El diseño descrito en este documento materializa las decisiones arquitectónicas definidas en `04-Architecture.md`, garantizando que la implementación permanezca alineada con los principios de Clean Architecture, Domain-Driven Design y Arquitectura Orientada a Eventos.

Este documento constituye la guía técnica para todos los equipos que participen en el desarrollo del backend, proporcionando una estructura consistente, escalable y mantenible.

## 2. Objetivos de Diseño

El backend deberá cumplir los siguientes objetivos:

- Mantener el dominio completamente independiente de frameworks y tecnologías externas.
- Implementar una arquitectura modular basada en Bounded Contexts.
- Favorecer el bajo acoplamiento y la alta cohesión entre componentes.
- Facilitar la evolución progresiva desde un Monolito Modular hacia microservicios.
- Garantizar la consistencia de las reglas de negocio mediante Aggregates y Domain Events.
- Centralizar las convenciones de desarrollo para reducir la variabilidad entre equipos.
- Proporcionar una base reutilizable para futuros módulos del sistema.
- Simplificar las pruebas unitarias e integración mediante una clara separación de responsabilidades.

## 3. Stack Tecnológico

El backend de Smart Workforce AI será implementado sobre el ecosistema .NET, utilizando tecnologías estables y ampliamente adoptadas para el desarrollo de aplicaciones empresariales.

La selección tecnológica prioriza la mantenibilidad, el rendimiento, la escalabilidad y la independencia del dominio respecto a frameworks externos.

| Categoría         | Tecnología                                |
| ----------------- | ----------------------------------------- |
| Runtime           | .NET 10 LTS                               |
| Framework Web     | ASP.NET Core                              |
| Arquitectura      | Clean Architecture + Domain-Driven Design |
| Patrón            | CQRS                                      |
| Persistencia      | Entity Framework Core                     |
| Base de Datos     | PostgreSQL                                |
| Migraciones       | EF Core Migrations                        |
| Validaciones      | FluentValidation                          |
| Autenticación     | JWT + Identity Provider                   |
| Documentación API | OpenAPI / Swagger                         |
| Logging           | Serilog                                   |
| Testing           | xUnit                                     |
| Mocking           | NSubstitute                               |
| Contenedores      | Docker                                    |

### Decisiones de implementación

- Se implementará CQRS mediante Commands y Queries propios del proyecto, evitando dependencias innecesarias como MediatR.
- Se utilizarán Controllers para exponer la API REST.
- Los mapeos entre objetos serán explícitos; no se utilizará AutoMapper.
- Entity Framework Core será utilizado exclusivamente en la capa de Infraestructura.
- El dominio permanecerá completamente desacoplado de cualquier framework.

### Criterios para incorporar nuevas tecnologías

Toda nueva dependencia deberá:

- Resolver un problema concreto del sistema.
- Aportar un beneficio superior al costo de mantenimiento.
- Ser compatible con la arquitectura definida.
- No introducir dependencias sobre la capa de Dominio.

## 4. Estructura General de la Solution

La solución se organizará siguiendo los principios de Clean Architecture y Domain-Driven Design.

Cada Bounded Context mantendrá independencia lógica y una estructura consistente, facilitando su evolución hacia un servicio independiente en el futuro.

### Estructura de la Solution

```text
src/

├── Api/
│   └── SmartWorkforce.Api

├── BuildingBlocks/

│   ├── SmartWorkforce.BuildingBlocks.Application
│   ├── SmartWorkforce.BuildingBlocks.Domain
│   ├── SmartWorkforce.BuildingBlocks.Infrastructure
│   └── SmartWorkforce.BuildingBlocks.Shared

├── Modules/

│   ├── Conciliation/
│   │
│   ├── SmartWorkforce.Conciliation.Domain
│   ├── SmartWorkforce.Conciliation.Application
│   ├── SmartWorkforce.Conciliation.Infrastructure
│   └── SmartWorkforce.Conciliation.Api
│
│   ├── Administration/
│   │
│   ├── SmartWorkforce.Administration.Domain
│   ├── SmartWorkforce.Administration.Application
│   ├── SmartWorkforce.Administration.Infrastructure
│   └── SmartWorkforce.Administration.Api
│
│   ├── Reporting/
│   ├── Importing/
│   ├── Notifications/
│   └── AI/

tests/

├── UnitTests/
├── IntegrationTests/
└── ArchitectureTests/

docs/
```

### Organización

Cada módulo implementará las siguientes capas:

- Domain
- Application
- Infrastructure
- Api

Cada módulo será responsable de su propio dominio y únicamente expondrá contratos públicos hacia otros módulos.

No existirán dependencias directas entre módulos a nivel de infraestructura.

La comunicación entre módulos deberá realizarse mediante:

- Casos de uso públicos.
- Eventos de dominio.
- Eventos de integración.

# 5. Organización de un Bounded Context

Cada Bounded Context constituye un módulo independiente del sistema y encapsula su propio dominio, casos de uso, persistencia y API.

Todos los módulos seguirán una estructura homogénea para facilitar el mantenimiento, reducir la curva de aprendizaje y permitir su evolución independiente.

## Estructura de un Bounded Context

```text
SmartWorkforce.<Module>.Domain
│
├── Aggregates/
├── Entities/
├── ValueObjects/
├── DomainEvents/
├── DomainServices/
├── Repositories/
├── Specifications/
├── Exceptions/
└── Enums/

SmartWorkforce.<Module>.Application
│
├── Commands/
├── Queries/
├── DTOs/
├── Interfaces/
├── Validators/
├── Behaviors/
└── Mappings/

SmartWorkforce.<Module>.Infrastructure
│
├── Persistence/
│   ├── Configurations/
│   ├── Repositories/
│   ├── Migrations/
│   └── Context/
│
├── Integrations/
├── Services/
└── DependencyInjection/

SmartWorkforce.<Module>.Api
│
├── Controllers/
├── Contracts/
├── Filters/
├── Middlewares/
└── DependencyInjection/
```

## Responsabilidades

### Domain

Contiene el modelo de negocio y las reglas invariantes del dominio.

Responsabilidades:

- Aggregates.
- Entities.
- Value Objects.
- Domain Events.
- Domain Services.
- Interfaces de Repositories.
- Excepciones del dominio.

El dominio no debe depender de ninguna tecnología externa.

---

### Application

Implementa los casos de uso del sistema.

Responsabilidades:

- Commands.
- Queries.
- Handlers.
- DTOs.
- Validaciones.
- Orquestación de operaciones.
- Publicación de eventos de aplicación.

La capa Application coordina el dominio, pero no implementa reglas de negocio.

---

### Infrastructure

Implementa los detalles técnicos requeridos por la aplicación.

Responsabilidades:

- Persistencia.
- Implementación de Repositories.
- Entity Framework Core.
- Integraciones externas.
- Consumo de APIs.
- Acceso a archivos.
- Mensajería.
- Configuración de dependencias.

Ninguna regla de negocio deberá implementarse en esta capa.

---

### Api

Expone las funcionalidades del módulo mediante HTTP.

Responsabilidades:

- Controllers.
- Endpoints.
- Model Binding.
- Autenticación.
- Autorización.
- Filtros.
- Configuración del módulo.

La API únicamente recibe solicitudes y delega la ejecución a la capa Application.

## Dependencias permitidas

Las dependencias entre capas seguirán la siguiente dirección:

```text
Api
    ↓
Application
    ↓
Domain

Infrastructure
    ↑
Application
```

El dominio nunca dependerá de Application, Infrastructure o Api.

La infraestructura implementará contratos definidos por el dominio o la aplicación.

## Principios

Todos los módulos deberán cumplir las siguientes reglas:

- Mantener independencia respecto a otros módulos.
- Proteger las reglas de negocio dentro del dominio.
- Implementar una estructura homogénea.
- Evitar dependencias circulares.
- Exponer únicamente contratos públicos necesarios para la integración.
- Preparar cada módulo para una futura extracción como microservicio sin afectar al resto del sistema.

# 6. Shared Kernel

El Shared Kernel contiene componentes comunes utilizados por múltiples Bounded Contexts que representan conceptos transversales del dominio o contratos estables de la plataforma.

Su objetivo es promover la reutilización sin introducir acoplamientos innecesarios entre módulos.

## Principios

Todo componente incorporado al Shared Kernel deberá cumplir las siguientes condiciones:

- Ser utilizado por dos o más módulos.
- Tener una responsabilidad claramente definida.
- Mantener estabilidad a lo largo del tiempo.
- No contener lógica específica de un Bounded Context.
- No generar dependencias circulares.

## Componentes

La estructura inicial del Shared Kernel será la siguiente:

```text
SmartWorkforce.SharedKernel

├── ValueObjects/
├── DomainEvents/
├── Exceptions/
├── Constants/
├── Interfaces/
├── Results/
├── Authorization/
└── Abstractions/
```

### ValueObjects

Objetos de valor reutilizables por diferentes módulos.

Ejemplos:

- Money
- Email
- DocumentNumber
- DateRange

---

### DomainEvents

Contratos base para eventos del dominio.

Ejemplos:

- IDomainEvent
- DomainEvent

---

### Exceptions

Excepciones comunes utilizadas por el dominio.

Ejemplos:

- DomainException
- ValidationException
- BusinessRuleException

---

### Constants

Constantes compartidas entre módulos.

Ejemplos:

- Roles
- Policies
- ClaimTypes

---

### Interfaces

Contratos utilizados por múltiples módulos.

Ejemplos:

- ICurrentUser
- IDateTimeProvider
- IEventDispatcher

---

### Results

Objetos estandarizados para representar el resultado de operaciones.

Ejemplos:

- Result
- Result<T>
- Error

---

### Authorization

Elementos comunes relacionados con autorización.

Ejemplos:

- Permissions
- Roles
- Policies

---

### Abstractions

Clases base utilizadas por la plataforma.

Ejemplos:

- Entity
- AggregateRoot
- ValueObject

## Restricciones

El Shared Kernel no deberá contener:

- Casos de uso.
- Repositories concretos.
- Servicios de infraestructura.
- Integraciones externas.
- Controllers.
- DTOs.
- Commands.
- Queries.
- Configuración de Entity Framework.
- Lógica específica de un módulo.

## Evolución

La incorporación de nuevos componentes al Shared Kernel deberá justificarse técnicamente.

No se permitirá agregar clases únicamente por conveniencia o reutilización puntual.

Cuando un componente pertenezca exclusivamente a un Bounded Context, deberá permanecer dentro de dicho módulo.

# 7. Building Blocks

Los Building Blocks constituyen la base técnica reutilizable sobre la cual se implementarán todos los Bounded Contexts del sistema.

Su propósito es estandarizar patrones de implementación, reducir código repetitivo y garantizar consistencia entre los distintos módulos.

Los Building Blocks no contienen lógica específica del negocio y podrán evolucionar independientemente de cualquier módulo funcional.

## Objetivos

- Reducir duplicidad de código.
- Estandarizar la implementación del dominio.
- Facilitar el mantenimiento.
- Garantizar consistencia arquitectónica.
- Simplificar el desarrollo de nuevos módulos.

## Estructura

```text
SmartWorkforce.BuildingBlocks

├── Domain
│
├── Abstractions/
├── Entities/
├── Events/
├── Exceptions/
├── Repositories/
├── Results/
└── Specifications/

├── Application
│
├── Behaviors/
├── Commands/
├── Queries/
├── Interfaces/
└── Validation/

├── Infrastructure
│
├── Persistence/
├── Messaging/
├── Logging/
├── Security/
└── DependencyInjection/
```

---

## Domain

Contiene las clases base utilizadas por todos los Aggregates del sistema.

Ejemplos:

- Entity
- AggregateRoot
- ValueObject
- DomainEvent
- BusinessRule
- Specification

---

## Application

Contiene componentes reutilizables para implementar los casos de uso.

Ejemplos:

- ICommand
- ICommandHandler
- IQuery
- IQueryHandler
- Result
- Result<T>
- Error

---

## Infrastructure

Contiene implementaciones técnicas reutilizables.

Ejemplos:

- BaseDbContext
- UnitOfWork
- RepositoryBase
- EventDispatcher
- Outbox
- AuditLogger

## Reglas

Los Building Blocks deberán cumplir las siguientes restricciones:

- No implementar reglas específicas del negocio.
- No depender de un Bounded Context.
- Mantener compatibilidad con toda la solución.
- Ser reutilizables por cualquier módulo.
- Evolucionar mediante versionado interno cuando sea necesario.

## Criterios de incorporación

Un componente podrá formar parte de los Building Blocks únicamente cuando:

- Sea utilizado por múltiples módulos.
- Resuelva un problema transversal.
- No pertenezca al dominio específico de un negocio.
- Su reutilización reduzca complejidad sin aumentar el acoplamiento.

# 8. Convenciones de Desarrollo

Con el fin de garantizar consistencia entre los equipos de desarrollo, todo el código deberá seguir las siguientes convenciones.

## Convenciones generales

- Utilizar inglés para nombres de proyectos, namespaces, clases, métodos y propiedades.
- Utilizar español únicamente en documentación funcional cuando sea necesario.
- Mantener nombres descriptivos y orientados al dominio.
- Evitar abreviaturas no estandarizadas.
- Mantener archivos pequeños y con una única responsabilidad.

## Organización

- Un Aggregate por carpeta.
- Un caso de uso por archivo.
- Un Controller por recurso.
- Una configuración de Entity Framework por entidad.

## Diseño

- Aplicar SOLID.
- Favorecer composición sobre herencia.
- Evitar lógica de negocio en Controllers e Infrastructure.
- Mantener el dominio independiente de frameworks.

## Calidad

- No utilizar código duplicado.
- Eliminar código muerto.
- Evitar comentarios innecesarios.
- Priorizar código legible sobre código complejo.

## Git

- Una funcionalidad por rama.
- Pull Request obligatorio.
- Commits pequeños y descriptivos.

# 9. Manejo de Errores

El sistema utilizará un manejo de errores centralizado para garantizar respuestas consistentes y facilitar la observabilidad.

## Principios

- No utilizar excepciones para controlar el flujo normal de la aplicación.
- Utilizar excepciones únicamente para situaciones inesperadas.
- Representar errores de negocio mediante objetos `Result`.
- Centralizar el tratamiento de excepciones mediante Middleware.

## Clasificación

- Errores de validación.
- Errores de negocio.
- Errores de autorización.
- Errores de infraestructura.
- Errores inesperados.

Todas las respuestas HTTP seguirán un formato uniforme definido por la API.

# 10. Logging y Observabilidad

El sistema implementará observabilidad desde el inicio del proyecto.

## Logging

Se utilizará Serilog como proveedor principal.

Se registrarán como mínimo:

- Inicio y fin de cada Request.
- Errores.
- Eventos de dominio relevantes.
- Integraciones externas.
- Operaciones críticas del negocio.

## Auditoría

Las acciones que afecten el dominio deberán registrarse mediante el módulo de Auditoría.

## Métricas

Se habilitarán métricas para monitorear:

- Tiempo de respuesta.
- Errores.
- Uso de recursos.
- Procesos automáticos.

# 11. Testing

El backend deberá garantizar un nivel adecuado de cobertura mediante pruebas automatizadas.

## Tipos de pruebas

- Unitarias.
- Integración.
- Arquitectura.

## Objetivos

- Validar reglas de negocio.
- Verificar casos de uso.
- Garantizar contratos entre capas.
- Detectar regresiones.

## Herramientas

- xUnit.
- NSubstitute.
- FluentAssertions.

# 12. Roadmap de Implementación

La implementación del backend seguirá el siguiente orden:

1. Crear la Solution.
2. Configurar la estructura de proyectos.
3. Implementar Building Blocks.
4. Implementar Shared Kernel.
5. Configurar Persistencia.
6. Implementar el módulo Conciliation.
7. Exponer la primera API.
8. Implementar autenticación y autorización.
9. Incorporar módulos restantes.
10. Preparar la evolución hacia microservicios.

Cada etapa deberá finalizar con pruebas automatizadas y documentación técnica actualizada.
