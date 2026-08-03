# Tactical DDD

## 1. Objetivo

Este documento define el modelo táctico del dominio para el sistema de conciliaciones bancarias. Su propósito es establecer la estructura interna del dominio y los patrones de diseño que garantizan la consistencia de las reglas de negocio durante la implementación.

A partir de los modelos definidos en `Discovery.md` y `Domain-Model.md`, este documento especifica los principales elementos del modelo táctico, incluyendo Aggregates, Entidades, Value Objects, Domain Services, Domain Events, Repositories y Factories.

Este modelo constituye el contrato de diseño para la implementación del dominio y servirá como referencia durante el desarrollo basado en Clean Architecture, manteniendo una separación clara entre el dominio y la infraestructura.

---

# 2. Aggregate Root

## 2.1 Aggregate Root: Conciliación

El Aggregate Root **Conciliación** representa la unidad de consistencia del dominio. Todas las modificaciones que afecten el resultado de una conciliación deben realizarse exclusivamente a través de este Aggregate.

Es el responsable de proteger las reglas invariantes del negocio, garantizar la consistencia de los movimientos conciliados y controlar el ciclo de vida completo de la conciliación.

### Responsabilidades

- Crear una conciliación válida.
- Administrar el estado de la conciliación.
- Gestionar los movimientos asociados.
- Registrar partidas conciliatorias.
- Validar las reglas del negocio.
- Aprobar la conciliación.
- Publicar los eventos de dominio correspondientes.

### Límites del Aggregate

Dentro del Aggregate:

- Conciliación
- Movimientos
- Partidas conciliatorias
- Estado
- Reglas aplicadas

Fuera del Aggregate:

- Reportes
- Auditoría
- Notificaciones
- ERP
- Bancos
- IA
- Integraciones externas

### Invariantes

Las siguientes reglas nunca pueden romperse:

- Una conciliación pertenece a una única empresa.
- Una conciliación pertenece a una única cuenta bancaria.
- Una conciliación pertenece a un único período.
- Un movimiento solo puede conciliarse una vez.
- No pueden agregarse movimientos de otra cuenta bancaria.
- No pueden mezclarse movimientos de diferentes períodos.
- Una conciliación aprobada no puede modificarse.
- Toda diferencia debe quedar justificada antes del cierre.
- Toda modificación debe conservar trazabilidad.

---

# 3. Entidades

## 3.1 Conciliación

Representa el proceso completo de conciliación bancaria.

### Identidad

- ConciliacionId

### Atributos principales

- Número
- Empresa
- Cuenta Bancaria
- Período
- Estado
- Usuario creador
- Fecha de creación

---

## 3.2 Movimiento

Representa un movimiento financiero participante en una conciliación.

### Identidad

- MovimientoId

### Atributos principales

- Fecha
- Valor
- Tipo
- Referencia
- Tercero
- Estado de conciliación

---

## 3.3 Partida Conciliatoria

Representa una diferencia identificada durante el proceso de conciliación.

### Identidad

- PartidaConciliatoriaId

### Atributos principales

- Tipo
- Descripción
- Estado
- Justificación
- Fecha de registro

---

# 4. Value Objects

## Período

Representa el rango de fechas sobre el cual se ejecuta una conciliación.

---

## EstadoConciliacion

Representa el estado actual del proceso.

Estados definidos:

- Borrador
- En Proceso
- Pendiente de Revisión
- Aprobada

---

## ReglaConciliacion

Representa la configuración utilizada por el motor de conciliación.

Ejemplos:

- Tolerancia de fechas
- Tolerancia de valores
- Comparación por referencia
- Comparación por tercero

---

# 5. Domain Services

Los Domain Services encapsulan lógica del negocio que involucra múltiples Aggregates o que no pertenece naturalmente a una Entidad.

## ConciliacionService

Responsabilidades:

- Ejecutar el algoritmo de conciliación.
- Aplicar reglas configurables.
- Detectar coincidencias.
- Detectar partidas conciliatorias.
- Validar reglas complejas del dominio.

---

# 6. Domain Events

Los Domain Events representan hechos relevantes ocurridos dentro del dominio.

Eventos identificados:

- ConciliacionCreada
- ExtractoImportado
- MovimientosContablesImportados
- ConciliacionAutomaticaEjecutada
- MovimientosConciliados
- PartidasConciliatoriasDetectadas
- AjustesContablesRegistrados
- PartidasJustificadas
- ConciliacionEnviadaRevision
- ConciliacionAprobada

Los consumidores de estos eventos podrán ser:

- Auditoría
- Reportes
- ERP
- Notificaciones
- IA
- Power BI
- Kafka
- Microsoft Teams
- WhatsApp

El Aggregate únicamente publica los eventos; nunca conoce sus consumidores.

---

# 7. Repositories

Los Repositories abstraen el mecanismo de persistencia del dominio.

El dominio únicamente conoce el contrato.

Ejemplo:

- Obtener una conciliación por Id.
- Guardar una conciliación.
- Consultar conciliaciones abiertas.
- Verificar existencia de conciliaciones por período y cuenta.

La implementación concreta pertenece a la infraestructura.

---

# 8. Factories

Las Factories encapsulan el proceso de creación de Aggregates cuando dicho proceso requiere aplicar reglas del negocio.

## ConciliacionFactory

Responsabilidades:

- Validar la empresa.
- Validar la cuenta bancaria.
- Verificar conciliaciones existentes.
- Obtener reglas configuradas.
- Generar número consecutivo.
- Inicializar estado.
- Inicializar colecciones.
- Crear el Aggregate válido.
- Publicar el evento ConciliacionCreada.

---

# 9. Resultado del Modelo Táctico

El modelo táctico establece la estructura interna del dominio y define los componentes responsables de proteger las reglas de negocio.

Este modelo servirá como base para la implementación del sistema utilizando Clean Architecture, donde:

- Los Use Cases orquestarán el flujo de aplicación.
- Los Aggregates protegerán las reglas del negocio.
- Los Repositories actuarán como puertos de persistencia.
- Los Domain Events permitirán desacoplar procesos secundarios.
- La infraestructura implementará los adaptadores necesarios sin afectar el dominio.
