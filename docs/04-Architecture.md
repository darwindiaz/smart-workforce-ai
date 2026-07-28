# Architecture

## 1. Objetivo

Este documento define la arquitectura de referencia para el sistema de conciliaciones bancarias, estableciendo los principios, estilos arquitectónicos, organización de la solución y decisiones técnicas que guiarán su implementación.

La arquitectura propuesta busca construir una plataforma mantenible, escalable y desacoplada, capaz de evolucionar conforme crezcan las necesidades del negocio sin comprometer la consistencia del dominio.

Este documento toma como entrada los artefactos generados durante el análisis del negocio (`Discovery.md`), el modelado estratégico (`Domain-Model.md`) y el modelado táctico (`Tactical-DDD.md`), utilizándolos como base para el diseño de la solución.

Su objetivo es servir como contrato arquitectónico para el equipo de desarrollo, definiendo las responsabilidades de cada componente y las reglas de interacción entre ellos.

## 2. Principios Arquitectónicos

La arquitectura del sistema se construirá siguiendo los siguientes principios:

### Dominio como núcleo del sistema

La lógica de negocio constituye el activo principal de la aplicación y permanecerá independiente de tecnologías, frameworks y mecanismos de infraestructura.

### Separación de responsabilidades

Cada componente deberá asumir una única responsabilidad claramente definida, favoreciendo alta cohesión y bajo acoplamiento.

### Independencia tecnológica

Las decisiones del dominio no dependerán de bases de datos, frameworks, servicios externos o proveedores específicos.

### Arquitectura evolutiva

La solución deberá permitir incorporar nuevas funcionalidades con el menor impacto posible sobre los componentes existentes.

### Consistencia del dominio

Todas las reglas invariantes serán protegidas por los Aggregates definidos en el modelo táctico.

### Integraciones desacopladas

La comunicación con sistemas externos se realizará mediante puertos, adaptadores y eventos de dominio, evitando dependencias directas.

### Configuración sobre implementación

Las reglas susceptibles de variar entre empresas deberán ser configurables sin modificar el código del dominio.

## 3. Estilo Arquitectónico

La solución adopta una arquitectura híbrida que combina distintos estilos arquitectónicos, seleccionados para resolver problemas específicos del dominio y facilitar la evolución del sistema.

### Domain-Driven Design (DDD)

DDD será utilizado para comprender, modelar y proteger el dominio de negocio. El diseño se basa en Bounded Contexts, Aggregates, Entidades, Value Objects, Domain Services y Domain Events, garantizando que las reglas del negocio permanezcan aisladas de detalles tecnológicos.

### Clean Architecture

La organización interna del código seguirá los principios de Clean Architecture, estableciendo una separación clara entre el dominio, los casos de uso, la infraestructura y las interfaces de entrada.

Las dependencias siempre apuntarán hacia el dominio, evitando que las reglas de negocio dependan de frameworks o tecnologías externas.

### Arquitectura Hexagonal (Ports & Adapters)

La interacción con bases de datos, servicios externos, bancos, ERP, sistemas de autenticación y mecanismos de mensajería se realizará mediante puertos y adaptadores.

Esto permitirá sustituir implementaciones de infraestructura sin afectar el dominio ni los casos de uso.

### Event-Driven Architecture

Los eventos de dominio permitirán desacoplar procesos secundarios del flujo principal del negocio.

Componentes como auditoría, notificaciones, generación de reportes, integraciones con ERP y futuras capacidades de inteligencia artificial reaccionarán a eventos publicados por el dominio sin introducir dependencias directas.

### Monolito Modular

La primera versión del sistema será implementada como un monolito modular.

Cada Bounded Context mantendrá independencia lógica, interfaces bien definidas y responsabilidades claramente delimitadas.

Esta estrategia reduce la complejidad inicial, facilita el desarrollo del MVP y prepara una evolución gradual hacia una arquitectura basada en microservicios cuando el crecimiento del negocio lo justifique.

## 4. Vista General de la Solución

El sistema de conciliaciones bancarias es una plataforma orientada a la automatización del proceso de conciliación entre los movimientos registrados por las entidades financieras y los registros contables provenientes del ERP de la organización.

La solución se estructura alrededor del dominio de conciliación, complementado por módulos de soporte e integración que permiten automatizar el proceso, preservar la trazabilidad de las operaciones y facilitar la evolución funcional del sistema.

### Actores principales

- Auxiliar Contable
- Contador
- Auditor
- Administrador

### Sistemas externos

- ERP
- Entidades Bancarias
- Proveedor de Identidad
- Servicio de Correo
- Almacenamiento de Documentos
- Plataforma de Mensajería/Eventos

### Módulos principales

#### Administración

Responsable de la gestión de usuarios, autenticación, autorización, empresas, parámetros y configuración general del sistema.

#### Importación de Datos

Gestiona la incorporación de extractos bancarios, movimientos contables y demás fuentes de información requeridas para iniciar una conciliación.

#### Motor de Conciliación

Constituye el núcleo funcional del sistema.

Aplica las reglas de conciliación, identifica coincidencias, detecta diferencias y genera las partidas conciliatorias correspondientes.

#### Gestión de Ajustes

Permite registrar, justificar y controlar las partidas conciliatorias identificadas durante el proceso.

#### Reportes

Genera reportes operativos, históricos y de auditoría, proporcionando información para la toma de decisiones y el cumplimiento normativo.

#### Auditoría

Registra todas las acciones relevantes ejecutadas sobre el sistema, garantizando trazabilidad e integridad de la información.

### Flujo funcional de alto nivel

1. El usuario inicia una conciliación.
2. Se importan los movimientos bancarios.
3. Se obtienen los movimientos contables desde el ERP.
4. El motor ejecuta el proceso automático de conciliación.
5. Se generan las partidas conciliatorias.
6. El usuario analiza y resuelve las diferencias.
7. La conciliación se envía a revisión.
8. El contador aprueba la conciliación.
9. Se publican los eventos del dominio.
10. Los módulos de soporte reaccionan a dichos eventos (auditoría, reportes, notificaciones e integraciones).

### Diagrama de flujo

                 Usuarios
                     │
                     ▼
        ┌──────────────────────────┐
        │      Administración      │
        └──────────────────────────┘
                     │
                     ▼
        ┌──────────────────────────┐
        │  Importación de Datos    │
        └──────────────────────────┘
                     │
                     ▼
        ╔══════════════════════════╗
        ║   Motor de Conciliación  ║
        ╚══════════════════════════╝
                     │
          ┌──────────┴──────────┐
          ▼                     ▼
    ╔═════════════╗     ╔═════════════╗
    ║ Gestion de  ║     ║   Domain    ║
    ║ Ajustes     ║     ║   Events    ║
    ╚═════════════╝     ╚═════════════╝
        │                     │
        ├──────────┬──────────┤
        ▼          ▼          ▼
    Auditoría Reportes Integraciones

## 5. Estructura de la Solución

La solución se organizará siguiendo una estructura orientada a Bounded Contexts. Cada contexto encapsulará su propio dominio, casos de uso, infraestructura e interfaces de entrada, manteniendo alta cohesión y bajo acoplamiento.

Esta organización permite evolucionar el sistema desde un monolito modular hacia una arquitectura distribuida sin requerir cambios significativos en el dominio.

### Organización de alto nivel

```text
src/

├── SharedKernel/
│
├── Administracion/
│   ├── Domain/
│   ├── Application/
│   ├── Infrastructure/
│   └── API/
│
├── Conciliacion/
│   ├── Domain/
│   ├── Application/
│   ├── Infrastructure/
│   └── API/
│
├── Importacion/
│   ├── Domain/
│   ├── Application/
│   ├── Infrastructure/
│   └── API/
│
├── Ajustes/
│   ├── Domain/
│   ├── Application/
│   ├── Infrastructure/
│   └── API/
│
├── Reportes/
│   ├── Domain/
│   ├── Application/
│   ├── Infrastructure/
│   └── API/
│
└── BuildingBlocks/
```

### Shared Kernel

Contendrá elementos compartidos por todos los contextos cuando representen conceptos estables del dominio y no introduzcan acoplamiento innecesario.

Ejemplos:

- Result
- Domain Event base
- Entity base
- Aggregate Root base
- Value Object base
- Interfaces comunes
- Excepciones del dominio

### Building Blocks

Agrupará componentes técnicos reutilizables que no forman parte del dominio de negocio.

Ejemplos:

- Logging
- Autenticación
- Autorización
- Persistencia común
- Observabilidad
- Configuración
- Utilidades
- Mensajería

## 6. Arquitectura Interna de un Bounded Context

Todos los Bounded Contexts del sistema seguirán la misma estructura interna, basada en los principios de Clean Architecture y Domain-Driven Design.

Cada contexto encapsula completamente su lógica de negocio y expone únicamente los contratos necesarios para interactuar con otros módulos.

```text
BoundedContext/

├── Domain/
├── Application/
├── Infrastructure/
└── API/
```

---

### Domain

Contiene el núcleo del negocio y no posee dependencias hacia otras capas.

#### Responsabilidades

- Aggregates
- Aggregate Roots
- Entities
- Value Objects
- Domain Services
- Domain Events
- Repositories (Interfaces)
- Factories
- Especificaciones del dominio
- Excepciones del dominio

#### Restricciones

- No depende de frameworks.
- No conoce bases de datos.
- No conoce APIs externas.
- No conoce HTTP.
- No conoce mecanismos de persistencia.

---

### Application

Orquesta los casos de uso del sistema.

Coordina el dominio, invoca repositorios mediante interfaces y publica eventos cuando corresponde.

#### Responsabilidades

- Use Cases
- Commands
- Queries
- DTOs
- Mappers
- Validaciones de aplicación
- Interfaces de servicios externos
- Coordinación de transacciones

#### Restricciones

- No contiene reglas de negocio.
- No implementa persistencia.
- No implementa integraciones.

---

### Infrastructure

Implementa todos los detalles técnicos requeridos por la aplicación.

#### Responsabilidades

- Implementación de Repositories
- Entity Framework / JPA
- Persistencia
- Integraciones con ERP
- Integraciones bancarias
- Mensajería
- Cache
- Archivos
- Email
- Logging
- Configuración

#### Restricciones

- Nunca contiene reglas del negocio.
- Implementa únicamente contratos definidos por Application o Domain.

---

### API

Representa los puntos de entrada al sistema.

#### Responsabilidades

- Controllers
- Endpoints REST
- Middleware
- Autenticación
- Autorización
- Versionado de APIs
- Documentación OpenAPI

#### Restricciones

- No implementa reglas del negocio.
- No consulta directamente la base de datos.
- Toda operación debe ejecutarse mediante un Use Case.

---

### Regla de dependencias

Las dependencias siempre apuntan hacia el dominio.

```text
API
      │
      ▼
Application
      │
      ▼
Domain
      ▲
      │
Infrastructure
```

El dominio nunca dependerá de ninguna otra capa.

## 7. Comunicación y Dependencias entre Bounded Contexts

Cada Bounded Context constituye un módulo autónomo responsable de su propio modelo de dominio.

La comunicación entre contextos deberá realizarse mediante contratos explícitos o eventos, evitando referencias directas a entidades internas de otros módulos.

### Principios

- Cada contexto es propietario de su información.
- Ningún contexto accederá directamente a la persistencia de otro.
- Ningún contexto modificará el estado interno de otro contexto.
- Toda comunicación deberá realizarse mediante interfaces bien definidas.
- Los eventos de dominio serán el mecanismo preferido para desacoplar procesos secundarios.

### Comunicación síncrona

Se utilizará cuando un caso de uso requiera una respuesta inmediata.

Ejemplos:

- Consultar información de usuarios.
- Obtener parámetros de configuración.
- Validar permisos.
- Consultar reglas configurables.

Estas operaciones se realizarán mediante contratos definidos en la capa Application.

### Comunicación asíncrona

Se utilizará cuando la operación receptora no afecte directamente el resultado del caso de uso que originó el evento.

Ejemplos:

- Registrar auditoría.
- Enviar notificaciones.
- Generar reportes.
- Actualizar indicadores.
- Integraciones con sistemas externos.

Estas operaciones reaccionarán a Domain Events publicados por el contexto propietario.

### Dependencias permitidas

```text
API
      │
      ▼
Application
      │
      ▼
Domain

Infrastructure
      │
      ▼
Application
```

Los Bounded Contexts nunca establecerán dependencias directas entre sus capas Domain.

La interacción siempre deberá producirse mediante contratos o eventos definidos en la capa Application.+

## 8. Arquitectura de Integraciones

El sistema interactúa con múltiples plataformas externas necesarias para ejecutar el proceso de conciliación bancaria. Estas integraciones se implementarán siguiendo los principios de Arquitectura Hexagonal (Ports & Adapters), garantizando que el dominio permanezca independiente de tecnologías y proveedores específicos.

### Principios

- El dominio no conocerá sistemas externos.
- Toda integración será abstraída mediante interfaces (Ports).
- Las implementaciones concretas residirán en la capa Infrastructure.
- Cada integración podrá reemplazarse sin afectar la lógica de negocio.
- Las fallas de un sistema externo no deberán comprometer la consistencia del dominio.

### Sistemas externos

| Sistema                      | Propósito                                                      |
| ---------------------------- | -------------------------------------------------------------- |
| ERP                          | Consultar movimientos contables y registrar ajustes aprobados. |
| Banco                        | Importar extractos bancarios y movimientos financieros.        |
| Proveedor de Identidad       | Autenticación y autorización de usuarios.                      |
| Servicio de Correo           | Envío de notificaciones y alertas.                             |
| Almacenamiento de Documentos | Gestión de extractos, comprobantes y evidencias.               |
| Plataforma de Mensajería     | Publicación y consumo de eventos de integración.               |

### Modelo de integración

Cada integración seguirá el siguiente esquema:

```text
Application
        │
        ▼
Port (Interface)
        │
        ▼
Infrastructure
        │
        ▼
Sistema Externo
```

La capa Application definirá los contratos necesarios para interactuar con sistemas externos.

La capa Infrastructure implementará dichos contratos utilizando el protocolo o tecnología correspondiente (REST, SOAP, SFTP, mensajería, SDK, entre otros).

### Resiliencia

Todas las integraciones deberán incorporar mecanismos que aumenten la confiabilidad del sistema frente a fallos temporales.

Entre ellos:

- Timeouts.
- Reintentos controlados.
- Circuit Breaker.
- Validación de respuestas.
- Registro de errores.
- Idempotencia cuando el proceso lo requiera.

### Sustitución de proveedores

El diseño permitirá sustituir un proveedor por otro sin modificar el dominio.

Ejemplos:

- Cambiar de SAP a Odoo.
- Cambiar de un banco a otro.
- Reemplazar SMTP por Microsoft Graph.
- Migrar de RabbitMQ a Kafka.

Estas modificaciones deberán limitarse a la implementación de los adaptadores correspondientes en Infrastructure.

## 9. Arquitectura de Eventos

La arquitectura del sistema adopta un modelo orientado a eventos para desacoplar procesos secundarios del flujo principal del negocio y facilitar la evolución hacia arquitecturas distribuidas.

### Objetivos

- Reducir el acoplamiento entre módulos.
- Facilitar la incorporación de nuevas funcionalidades.
- Mantener el dominio independiente de procesos técnicos.
- Permitir integraciones asincrónicas con otros sistemas.

### Domain Events

Los Domain Events representan hechos relevantes ocurridos dentro del dominio.

Su objetivo es comunicar que una regla de negocio ha sido ejecutada correctamente.

Ejemplos:

- ConciliationCreated
- BankStatementImported
- AccountingEntriesImported
- AutomaticReconciliationCompleted
- ReconciliationApproved
- AdjustmentRegistered

Los Domain Events únicamente expresan hechos del negocio.

No contienen lógica de infraestructura ni referencias a sistemas externos.

### Integration Events

Los Integration Events representan información que debe ser consumida por otros sistemas o plataformas externas.

Su publicación será responsabilidad de la capa Application o Infrastructure, nunca del dominio.

Ejemplos:

- NotifyAccountantRequested
- ERPAdjustmentRequested
- AuditLogRequested
- ReconciliationReportRequested

### Flujo de publicación

```text
Aggregate
        │
        ▼
Domain Event
        │
        ▼
Application
        │
        ▼
Integration Event
        │
        ▼
Infrastructure
        │
        ▼
Consumidores Externos
```

### Consumidores

Los eventos podrán ser consumidos por distintos módulos o plataformas.

Ejemplos:

- Auditoría
- Reportes
- ERP
- Correo electrónico
- Plataforma de IA
- Dashboards
- Sistemas de monitoreo

Cada consumidor será independiente del Aggregate que originó el evento.

### Beneficios

- Bajo acoplamiento.
- Alta cohesión.
- Extensibilidad.
- Escalabilidad.
- Facilita la evolución hacia microservicios.
- Reduce el impacto de incorporar nuevas integraciones.

## 10. Arquitectura de Persistencia

La persistencia del sistema seguirá los principios de Clean Architecture y Domain-Driven Design, garantizando que el dominio permanezca completamente desacoplado de la tecnología de almacenamiento.

### Principios

- El dominio no conocerá mecanismos de persistencia.
- Toda operación de acceso a datos se realizará mediante Repositories.
- Las implementaciones concretas residirán en Infrastructure.
- La persistencia deberá preservar la consistencia del Aggregate.

### Organización

Durante el MVP se utilizará una única instancia de base de datos PostgreSQL.

Cada Bounded Context dispondrá de su propio esquema lógico, evitando dependencias directas entre modelos de datos.

Ejemplo:

```text
PostgreSQL

├── administracion
├── conciliacion
├── importacion
├── ajustes
└── reportes
```

Esta organización facilita una futura separación física hacia bases de datos independientes sin modificar el dominio.

### Repositories

Los Repositories representan el mecanismo oficial para recuperar y persistir Aggregates.

Sus contratos serán definidos en Domain y sus implementaciones en Infrastructure.

Los casos de uso accederán al dominio exclusivamente mediante estos contratos.

### Unidad de Trabajo

Cada caso de uso ejecutará una única unidad transaccional consistente.

Las modificaciones realizadas sobre un Aggregate deberán persistirse de forma atómica.

### Control de concurrencia

El sistema adoptará **Optimistic Concurrency** como estrategia principal para proteger la consistencia de los Aggregates.

Esta estrategia permitirá detectar modificaciones concurrentes evitando bloqueos innecesarios sobre la base de datos.

En caso de conflicto, la operación será rechazada y el usuario deberá recargar la información antes de intentar nuevamente la acción.

### Evolución

La estrategia de persistencia permitirá evolucionar hacia:

- Bases de datos independientes por Bounded Context.
- Replicación de lectura.
- Eventual Consistency entre contextos.
- Outbox Pattern para publicación confiable de eventos.

Estas evoluciones no requerirán modificaciones en el dominio.

## 11. Arquitectura de Seguridad

La seguridad constituye un atributo transversal de la arquitectura y será aplicada de forma consistente en todos los Bounded Contexts.

El diseño busca proteger la confidencialidad, integridad y disponibilidad de la información, garantizando que únicamente usuarios autorizados puedan ejecutar operaciones sobre el dominio.

### Principios

- La autenticación será delegada a un proveedor de identidad.
- La autorización será responsabilidad de la aplicación.
- Toda operación crítica deberá validar permisos antes de ejecutarse.
- El dominio nunca confiará únicamente en la interfaz de usuario para proteger reglas de negocio.
- Todas las acciones sensibles deberán generar trazabilidad.

### Autenticación

La autenticación será realizada mediante un Identity Provider externo.

El sistema consumirá la identidad del usuario autenticado sin administrar directamente credenciales.

Esta decisión permite desacoplar la autenticación del dominio y facilita futuras integraciones con distintos proveedores.

Ejemplos:

- Microsoft Entra ID
- Keycloak
- Auth0
- OAuth2/OpenID Connect

### Autorización

La autorización se implementará mediante un modelo basado en Roles y Permisos (RBAC).

Cada operación validará explícitamente que el usuario posea los permisos requeridos antes de ejecutar el caso de uso.

Ejemplos:

- Crear conciliación.
- Importar extractos.
- Registrar ajustes.
- Aprobar conciliaciones.
- Consultar auditoría.

### Protección del dominio

Las reglas críticas del negocio deberán validarse tanto en la capa Application como dentro del Aggregate cuando corresponda.

Ejemplos:

- Una conciliación aprobada no puede modificarse.
- Solo usuarios autorizados pueden aprobar una conciliación.
- No pueden agregarse movimientos de otra cuenta bancaria.
- Un movimiento no puede conciliarse más de una vez.

La interfaz de usuario nunca será considerada un mecanismo de seguridad.

### Auditoría

Toda operación que modifique el estado del negocio deberá generar un registro de auditoría.

Como mínimo se registrará:

- Usuario.
- Fecha y hora.
- Operación ejecutada.
- Recurso afectado.
- Resultado de la operación.

La auditoría se implementará mediante eventos, evitando acoplar el dominio con mecanismos de almacenamiento.

### Aislamiento de información

El sistema garantizará el aislamiento lógico de la información entre empresas.

Todo acceso a datos deberá validar el contexto organizacional del usuario autenticado, evitando el acceso a información perteneciente a otras organizaciones.

### Principio de mínimo privilegio

Los usuarios únicamente dispondrán de los permisos estrictamente necesarios para desempeñar sus funciones.

Ningún rol dispondrá de privilegios superiores a los requeridos por el proceso de negocio.

## 12. Estrategia de Evolución y Escalabilidad

La arquitectura del sistema ha sido diseñada siguiendo un enfoque evolutivo.

El objetivo no es implementar una arquitectura distribuida desde el inicio, sino construir un Monolito Modular que permita evolucionar progresivamente hacia microservicios conforme aumenten las necesidades del negocio.

### Principios

- La complejidad arquitectónica debe responder a necesidades reales del negocio.
- Los Bounded Contexts representan los candidatos naturales para una futura extracción como microservicios.
- El dominio permanecerá independiente del modelo de despliegue.
- La evolución tecnológica no deberá requerir modificaciones sobre las reglas de negocio.

### Etapas de evolución

#### Etapa 1 - Monolito Modular

Características:

- Un único despliegue.
- Una única base de datos.
- Esquemas separados por Bounded Context.
- Comunicación interna mediante casos de uso y eventos.

Objetivo:

Maximizar la velocidad de desarrollo y reducir la complejidad operacional durante el MVP.

---

#### Etapa 2 - Modularización avanzada

Características:

- Mayor desacoplamiento entre contextos.
- Contratos claramente definidos.
- Mayor uso de eventos.
- Preparación para separación física.

Objetivo:

Reducir dependencias y facilitar la extracción de módulos.

---

#### Etapa 3 - Microservicios

Cada Bounded Context podrá evolucionar hacia un servicio independiente.

Posibles candidatos:

- Conciliación
- Administración
- Importación
- Reportes
- Notificaciones
- Inteligencia Artificial

Cada servicio podrá utilizar:

- Lenguaje de programación independiente.
- Base de datos propia.
- Estrategia de despliegue independiente.
- Escalamiento independiente.

### Principios de escalabilidad

La arquitectura permitirá escalar horizontalmente los componentes con mayor demanda sin afectar el resto del sistema.

La comunicación entre servicios evolucionará desde eventos internos hacia mensajería distribuida cuando el contexto del negocio lo requiera.

### Independencia tecnológica

Cada microservicio podrá adoptar la tecnología más adecuada para resolver su problema específico.

Ejemplos:

- .NET para procesos transaccionales.
- Python para modelos de Inteligencia Artificial.
- Go para procesamiento de alto rendimiento.
- Node.js para servicios orientados a integración y mensajería.

La elección tecnológica no afectará el modelo de dominio ni los contratos establecidos entre contextos.

### Criterios para extraer un Bounded Context

Un Bounded Context podrá evolucionar a un microservicio cuando se cumpla uno o varios de los siguientes criterios:

- Requiere escalar de forma independiente.
- Tiene un ciclo de despliegue diferente al resto del sistema.
- Es mantenido por un equipo distinto.
- Presenta una carga significativamente superior a otros módulos.
- Necesita utilizar una tecnología diferente.
- Requiere un modelo de persistencia especializado.
- Posee un alto grado de autonomía funcional.
