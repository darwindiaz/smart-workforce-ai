# Domain Model

## Objetivo

Definir el modelo estratégico del dominio de la plataforma de conciliación bancaria, identificando sus capacidades de negocio, límites de contexto y relaciones de alto nivel. Este documento sirve como base para el diseño táctico del dominio y la implementación de la arquitectura.

---

# Core Domain

## Conciliación Bancaria

Es el núcleo funcional del sistema y representa el principal diferencial de negocio.

### Responsabilidades

- Gestionar el ciclo de vida de una conciliación.
- Ejecutar el proceso de conciliación.
- Aplicar reglas de conciliación.
- Determinar coincidencias entre movimientos.
- Detectar partidas conciliatorias.
- Gestionar el estado de la conciliación.
- Garantizar las reglas críticas del dominio.

---

# Supporting Domains

## Integraciones

Responsable de obtener y enviar información hacia sistemas externos.

### Responsabilidades

- Importar extractos bancarios.
- Importar movimientos contables desde ERP.
- Exportar ajustes contables.
- Integrarse con servicios externos.

---

## Ajustes Contables

Responsable de administrar las diferencias identificadas durante la conciliación.

### Responsabilidades

- Registrar ajustes.
- Gestionar partidas conciliatorias.
- Registrar justificaciones.
- Dar seguimiento a diferencias pendientes.

---

## Configuración de Reglas

Permite adaptar el motor de conciliación a diferentes empresas.

### Responsabilidades

- Administrar reglas de conciliación.
- Configurar tolerancias.
- Configurar estrategias de coincidencia.
- Parametrizar el proceso de conciliación.

---

## Auditoría

Garantiza la trazabilidad completa del proceso.

### Responsabilidades

- Registrar eventos relevantes.
- Mantener evidencia de cambios.
- Proveer información para auditoría.

---

# Generic Domains

## Identidad y Acceso

- Autenticación.
- Autorización.
- Usuarios.
- Roles.
- Permisos.

---

## Notificaciones

- Correos electrónicos.
- Alertas.
- Recordatorios.

---

## Reportes

- Reportes operativos.
- Reportes de conciliación.
- Reportes para auditoría.

---

# Bounded Contexts

| Contexto       | Responsabilidad                                    |
| -------------- | -------------------------------------------------- |
| Conciliación   | Ejecutar y controlar el proceso de conciliación.   |
| Integraciones  | Comunicación con ERP, bancos y servicios externos. |
| Ajustes        | Gestión de diferencias y ajustes contables.        |
| Configuración  | Administración de reglas y parámetros.             |
| Auditoría      | Registro de eventos y trazabilidad.                |
| Identidad      | Gestión de usuarios y permisos.                    |
| Notificaciones | Comunicación con usuarios.                         |
| Reportes       | Generación de informes.                            |

---

# Relaciones entre Contextos

- Conciliación consume información proveniente de Integraciones.
- Conciliación utiliza reglas administradas por Configuración.
- Conciliación genera eventos consumidos por Auditoría.
- Conciliación genera eventos consumidos por Notificaciones.
- Conciliación genera información utilizada por Reportes.
- Ajustes interactúa con Conciliación para resolver diferencias.

---

# Principios del Modelo

- El dominio no depende de tecnologías concretas.
- Cada contexto posee responsabilidades claramente delimitadas.
- La comunicación entre contextos se realiza mediante eventos o contratos.
- Las reglas de negocio pertenecen exclusivamente al dominio.
- El Core Domain concentra la mayor inversión de diseño.
