# Discovery del Negocio

## Información del Documento

| Campo                | Valor                                                |
| -------------------- | ---------------------------------------------------- |
| Proyecto             | Smart Workforce AI – Módulo de Conciliación Bancaria |
| Versión              | 1.0                                                  |
| Estado               | En construcción                                      |
| Última actualización | Julio 2026                                           |
| Autor                | Darwin Díaz                                          |
| Arquitectura         | Clean Architecture + DDD (en definición)             |

---

# 1. Contexto del Negocio

## Propósito

Smart Workforce AI es una plataforma empresarial orientada a la automatización de procesos contables mediante Inteligencia Artificial.

La primera capacidad del sistema corresponde al módulo de Conciliación Bancaria, cuyo objetivo es asistir a los equipos contables en la comparación entre los movimientos registrados en la contabilidad de la empresa y los movimientos reportados por las entidades financieras.

El sistema busca reducir el trabajo operativo, minimizar errores humanos y proporcionar trazabilidad completa sobre todo el proceso de conciliación.

Su diseño permitirá evolucionar posteriormente hacia otros procesos de conciliación contable y automatización financiera sin comprometer la arquitectura del sistema.

---

## Problema del Negocio

Actualmente, la conciliación bancaria es un proceso altamente manual.

Los auxiliares y contadores deben consultar múltiples fuentes de información, comparar movimientos, investigar diferencias y registrar ajustes utilizando herramientas dispersas como ERP, extractos bancarios y hojas de cálculo.

Esta operación presenta múltiples desafíos:

- Alto consumo de tiempo operativo.
- Riesgo elevado de errores humanos.
- Procesos repetitivos.
- Baja trazabilidad.
- Dificultad para auditorías.
- Dependencia del conocimiento de cada contador.
- Escasa automatización para identificar diferencias complejas.

Como consecuencia, las empresas incrementan sus costos operativos y retrasan el cierre oportuno de sus procesos financieros.

---

## Objetivos del Sistema

El sistema deberá:

- Automatizar el proceso de conciliación bancaria.
- Detectar coincidencias entre movimientos bancarios y contables.
- Identificar diferencias y clasificarlas.
- Facilitar la investigación de partidas conciliatorias.
- Registrar evidencia y trazabilidad completa.
- Permitir la aprobación formal de las conciliaciones.
- Integrarse con sistemas externos (ERP, bancos y servicios corporativos).
- Servir como plataforma base para futuros procesos de conciliación contable.
- Manejo de roles y accesos configurables para cada proceso.

---

## Alcance del MVP

La primera versión del sistema estará enfocada exclusivamente en el proceso de conciliación bancaria.

El MVP incluirá:

- Importación de extractos bancarios.
- Importación de registros contables.
- Comparación automática de movimientos.
- Conciliaciones 1:1, 1:N, N:1 y N:N.
- Aplicación de reglas configurables.
- Gestión de partidas conciliatorias.
- Flujo de revisión y aprobación.
- Registro completo de auditoría.
- Generación de reportes.
- Integración inicial con ERP.

# 2. Modelo Operativo del Negocio

## Actores

El proceso de conciliación bancaria involucra cuatro actores principales, cada uno con responsabilidades claramente definidas para garantizar la segregación de funciones, la trazabilidad y el cumplimiento de los controles internos.

| Actor             | Responsabilidad                                                                                                      |
| ----------------- | -------------------------------------------------------------------------------------------------------------------- |
| Auxiliar Contable | Ejecutar el proceso de conciliación, analizar diferencias y preparar la conciliación para revisión.                  |
| Contador          | Revisar, aprobar o rechazar la conciliación y autorizar los ajustes contables cuando corresponda.                    |
| Auditor           | Verificar la integridad del proceso, revisar las evidencias y validar el cumplimiento de los controles establecidos. |
| Administrador     | Gestionar usuarios, permisos, parametrizaciones e integraciones del sistema.                                         |

Cada actor posee responsabilidades exclusivas que no deben superponerse, preservando el principio de segregación de funciones requerido en procesos financieros.

---

## Flujo Operativo

El proceso de conciliación bancaria sigue un flujo secuencial que inicia con la recepción de la información financiera y finaliza con la aprobación formal de la conciliación.

1. Importar el extracto bancario correspondiente al período.
2. Obtener los movimientos contables registrados en el ERP.
3. Validar la consistencia de la información recibida.
4. Ejecutar el proceso automático de conciliación aplicando las reglas configuradas.
5. Clasificar los movimientos en conciliados y partidas conciliatorias.
6. Analizar las diferencias que no pudieron resolverse automáticamente.
7. Registrar ajustes o documentar las diferencias pendientes cuando aplique.
8. Generar el resultado preliminar de la conciliación.
9. Enviar la conciliación para revisión.
10. Aprobar o rechazar la conciliación.
11. Registrar la evidencia completa del proceso para efectos de auditoría.

El sistema deberá automatizar todas las actividades susceptibles de ejecutarse mediante reglas de negocio, reservando la intervención humana para el análisis de excepciones y la toma de decisiones contables.

---

## Ciclo de Vida de la Conciliación

Una conciliación evoluciona a través de un conjunto de estados definidos por el negocio.

| Estado                | Descripción                                                                                              |
| --------------------- | -------------------------------------------------------------------------------------------------------- |
| Borrador o En Proceso | La conciliación fue creada, El sistema o el auxiliar ejecutan las actividades de conciliación.           |
| Pendiente de Revisión | El análisis terminó y espera validación por parte del responsable.                                       |
| Finalizada            | La conciliación fue completada y cumple las condiciones para su aprobación.                              |
| Aprobada              | La conciliación fue validada por un usuario autorizado y queda protegida contra modificaciones directas. |

La transición entre estados deberá respetar las reglas de autorización definidas por el negocio y registrar evidencia completa de cada cambio realizado.

Una conciliación aprobada constituye un hecho de negocio inmutable. Cualquier corrección posterior deberá realizarse mediante un nuevo proceso que preserve el historial y la trazabilidad de la información.

# 3. Modelo del Dominio del Negocio

## Lenguaje Ubicuo

El sistema de conciliación bancaria utiliza los siguientes conceptos principales definidos desde el dominio financiero.

---

## Conciliación Bancaria

Representa el proceso mediante el cual una empresa compara los movimientos registrados en su sistema contable contra los movimientos reportados por una entidad bancaria, con el objetivo de validar que ambas fuentes representan la misma realidad financiera.

Una conciliación permite identificar:

- movimientos coincidentes;
- diferencias;
- ajustes requeridos;
- partidas pendientes de explicación.

La conciliación representa un proceso controlado que debe mantener trazabilidad completa desde su creación hasta su aprobación.

---

## Movimiento Bancario

Representa una transacción financiera registrada por una entidad bancaria.

Ejemplos:

- transferencia recibida;
- pago realizado;
- comisión bancaria;
- interés generado;
- débito automático.

Información relevante:

- fecha de movimiento;
- valor;
- tipo de operación;
- referencia bancaria;
- cuenta bancaria asociada.

---

## Movimiento Contable

Representa un registro financiero generado dentro del sistema contable de la empresa.

Puede corresponder a:

- ingresos;
- egresos;
- pagos;
- facturas;
- ajustes contables.

Su propósito dentro de la conciliación es permitir la comparación contra los movimientos bancarios.

---

## Coincidencia

Representa la relación encontrada entre uno o varios movimientos bancarios y uno o varios movimientos contables cuando cumplen las reglas de conciliación definidas.

Una coincidencia puede tener diferentes cardinalidades:

- Uno a Uno.
- Uno a Muchos.
- Muchos a Uno.
- Muchos a Muchos.

La identificación de coincidencias puede realizarse automáticamente mediante reglas o requerir validación humana.

---

## Partida Conciliatoria

Representa una diferencia identificada entre la información bancaria y contable que aún no ha sido resuelta.

Ejemplos:

- movimiento bancario sin registro contable;
- registro contable pendiente de reflejarse en banco;
- diferencia de valores;
- comisión bancaria no registrada.

Una partida conciliatoria debe conservar:

- motivo de la diferencia;
- evidencia asociada;
- responsable de revisión;
- estado actual.

---

## Regla de Conciliación

Representa la lógica utilizada para determinar cuándo dos movimientos pueden considerarse equivalentes.

Ejemplos:

- igualdad de valor;
- coincidencia de referencia;
- tolerancia de fechas;
- coincidencia del tercero;
- tipo de movimiento.

Las reglas deben ser configurables porque pueden variar dependiendo de cada empresa.

---

## Evidencia

Representa la información que permite justificar una operación o decisión dentro del proceso.

Puede incluir:

- extractos bancarios;
- facturas;
- comprobantes;
- documentos soporte;
- comentarios de revisión.

La evidencia permite garantizar trazabilidad durante auditorías.

---

## Auditoría

Representa el registro histórico de las acciones realizadas sobre la información del sistema.

Debe permitir responder:

- ¿Qué ocurrió?
- ¿Quién realizó la acción?
- ¿Cuándo ocurrió?
- ¿Qué información cambió?

---

## Aprobación

Representa la confirmación formal realizada por un usuario autorizado indicando que una conciliación cumple las condiciones definidas por el negocio.

Una conciliación aprobada representa un hecho financiero confirmado y no debe modificarse directamente.

# 4. Reglas de Negocio e Invariantes del Dominio

Las siguientes reglas representan restricciones fundamentales del negocio y deberán preservarse independientemente de la tecnología, arquitectura o implementación utilizada.

## RN-001 — Un movimiento solo puede conciliarse una vez

Un movimiento bancario o contable no podrá participar simultáneamente en múltiples conciliaciones para el mismo período.

**Objetivo**

Garantizar la integridad del proceso de conciliación y evitar inconsistencias en los resultados.

---

## RN-002 — Toda conciliación pertenece a un período y una cuenta bancaria

Cada conciliación deberá estar asociada de forma obligatoria a una cuenta bancaria y a un período contable determinado.

**Objetivo**

Garantizar la trazabilidad y evitar conciliaciones ambiguas.

---

## RN-003 — Toda acción relevante debe generar auditoría

Toda operación que modifique el estado o la información de una conciliación deberá registrar como mínimo:

- Usuario responsable.
- Fecha y hora.
- Acción ejecutada.
- Información modificada.

**Objetivo**

Garantizar trazabilidad completa para auditorías internas y externas.

---

## RN-004 — Una conciliación aprobada es inmutable

Una vez aprobada, una conciliación no podrá modificarse directamente.

Cualquier corrección deberá realizarse mediante un nuevo proceso que preserve el historial de la conciliación original.

**Objetivo**

Proteger la integridad histórica de la información financiera.

---

## RN-005 — Solo usuarios autorizados pueden aprobar conciliaciones

La aprobación constituye una decisión de negocio y únicamente podrá ser ejecutada por usuarios con los permisos correspondientes.

**Objetivo**

Garantizar el cumplimiento de las políticas de control interno.

---

## RN-006 — Toda diferencia debe quedar clasificada

Todo movimiento que no pueda conciliarse automáticamente deberá quedar registrado como una partida conciliatoria con su respectivo estado y justificación.

**Objetivo**

Evitar diferencias sin seguimiento o sin responsable asignado.

---

## RN-007 — Las reglas de conciliación son configurables

Cada organización podrá definir sus propios criterios de conciliación, incluyendo tolerancias, prioridades y reglas de coincidencia.

Estas configuraciones no deberán requerir modificaciones sobre el núcleo del dominio.

**Objetivo**

Permitir la adaptación del sistema a diferentes organizaciones preservando la estabilidad del modelo de negocio.

---

## RN-008 — Toda conciliación debe conservar su evidencia

Cada conciliación deberá mantener la información necesaria para reconstruir completamente el proceso realizado.

La evidencia podrá incluir:

- Extractos bancarios.
- Movimientos contables.
- Documentos soporte.
- Comentarios.
- Aprobaciones.
- Historial de cambios.

**Objetivo**

Garantizar verificabilidad y cumplimiento de auditorías.

---

## RN-009 — El sistema automatiza; el responsable decide

El sistema podrá ejecutar automáticamente las reglas de conciliación y proponer resultados.

Las decisiones contables relacionadas con diferencias complejas, ajustes o aprobaciones finales siempre corresponderán a un usuario autorizado.

**Objetivo**

Mantener el control humano sobre las decisiones financieras críticas.

# 5. Contexto Operativo del Sistema

Esta sección describe las interacciones del dominio de conciliación bancaria con actores, sistemas externos y procesos organizacionales. Su objetivo es delimitar el contexto operativo del sistema, identificando las fronteras de integración, los flujos de información y los eventos relevantes para el negocio.

---

## Sistemas Externos

La plataforma deberá interoperar con sistemas corporativos y servicios de terceros para obtener información, ejecutar procesos y distribuir resultados.

| Sistema                                   | Propósito                                                      |
| ----------------------------------------- | -------------------------------------------------------------- |
| ERP                                       | Consultar movimientos contables y registrar ajustes aprobados. |
| Entidades Bancarias                       | Importar extractos y movimientos bancarios.                    |
| Proveedor de Identidad (LDAP, OAuth, SSO) | Autenticar y autorizar usuarios.                               |
| Servicio de Correo                        | Enviar notificaciones y alertas operativas.                    |
| Almacenamiento Documental                 | Conservar extractos, comprobantes y evidencias.                |
| APIs Corporativas                         | Intercambiar información con aplicaciones internas o externas. |

Las integraciones deberán diseñarse de forma desacoplada para facilitar la incorporación de nuevos proveedores sin afectar el dominio del negocio.

---

## Flujos de Información

### Entradas

El sistema recibirá principalmente:

- Extractos bancarios.
- Movimientos contables provenientes del ERP.
- Documentos soporte.
- Configuración de reglas de conciliación.
- Información de usuarios y permisos.

### Salidas

El sistema generará:

- Conciliaciones bancarias.
- Reportes de diferencias.
- Ajustes contables.
- Reportes de auditoría.
- Alertas y notificaciones.
- Información de retorno hacia el ERP.

---

## Automatización del Proceso

El sistema deberá maximizar la automatización de actividades repetitivas y de bajo riesgo, manteniendo la intervención humana sobre las decisiones contables que requieren criterio profesional.

### Procesos automatizables

- Importación de información.
- Validación del formato de archivos.
- Comparación de movimientos.
- Aplicación de reglas de conciliación.
- Identificación de coincidencias.
- Clasificación inicial de diferencias.
- Generación de reportes.
- Envío de notificaciones.

### Procesos con intervención humana

- Investigación de diferencias complejas.
- Validación de documentos soporte.
- Registro de ajustes contables.
- Resolución de excepciones.
- Revisión y aprobación de conciliaciones.

El sistema actúa como un mecanismo de apoyo a la decisión, sin reemplazar la responsabilidad profesional del contador.

---

## Eventos de Negocio

Los siguientes eventos representan hechos relevantes dentro del dominio y podrán ser utilizados para iniciar procesos internos o integraciones con otros sistemas.

| Evento                            | Acción esperada                                            |
| --------------------------------- | ---------------------------------------------------------- |
| Conciliación creada               | Notificar al responsable del proceso.                      |
| Conciliación finalizada           | Solicitar revisión del contador.                           |
| Conciliación aprobada             | Registrar auditoría e informar a los interesados.          |
| Conciliación rechazada            | Notificar al auxiliar contable para realizar correcciones. |
| Diferencias críticas detectadas   | Generar alertas operativas.                                |
| Ajuste contable registrado        | Sincronizar la información con el ERP.                     |
| Nuevo extracto bancario importado | Iniciar el proceso automático de conciliación.             |

La publicación de estos eventos deberá preservar el desacoplamiento entre módulos y facilitar la evolución futura hacia arquitecturas distribuidas o basadas en eventos.

---

## Variabilidad del Negocio

La plataforma será utilizada por organizaciones con procesos y políticas de conciliación diferentes. Por esta razón, el sistema deberá permitir la configuración de determinados comportamientos sin alterar el núcleo del dominio.

### Elementos configurables

- Reglas de conciliación.
- Tolerancias de fecha y valor.
- Flujo de aprobación.
- Estados de la conciliación.
- Roles y permisos.
- Formatos de importación y exportación.
- Integraciones con ERP y entidades bancarias.

### Elementos invariantes

Los siguientes conceptos constituyen la base del dominio y deberán mantenerse consistentes independientemente de la organización usuaria:

- El proceso de conciliación bancaria.
- La trazabilidad de las operaciones.
- El registro de auditoría.
- La gestión de usuarios y permisos.
- Los controles de seguridad.
- Las reglas críticas del negocio definidas en este documento.
