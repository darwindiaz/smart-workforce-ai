# Problema

- "Las conciliaciones tardan demasiado."
- "Existen errores humanos."
- "La empresa pierde dinero."
- "Las auditorías toman mucho tiempo."
- "Los cierres contables se retrasan."

Problema de negocio ≠ Proceso de negocio

- Proceso → cómo trabaja hoy la empresa.
- Problema → por qué ese proceso ya no satisface al negocio.

---

# ACTORES

| Actor             | Responsabilidad                    |
| ----------------- | ---------------------------------- |
| Auxiliar contable | Ejecuta conciliaciones             |
| Contador          | Revisa y aprueba resultados        |
| Auditor           | Consulta historial y evidencia     |
| Administrador     | Configura reglas y usuarios        |
| Sistema bancario  | Proveedor de extractos             |
| ERP               | Proveedor de movimientos contables |
| IA                | Sugiere conciliaciones automáticas |

Actor
Cualquier persona o sistema externo que interactúe con el software.

---

# ALCANCE MPV

- Proceso 1
  Importar extractos bancarios.

- Proceso 2
  Importar movimientos contables.

- Proceso 3
  Ejecutar conciliación.

- Proceso 4
  Resolver diferencias.

- Proceso 5
  Generar reportes.

- Proceso 6
  Registrar auditoría.

- Proceso 7
  Aprobar conciliación.

# Proceso de negocio

Un proceso de negocio describe la secuencia de actividades necesarias para alcanzar un objetivo; no describe cómo se implementan técnicamente esas actividades.

## Conjunto de actividades que generan valor para el usuario o para la empresa.

# Roadmap

| Fase                                                      | Objetivo                                                                                        |    Estado    |
| --------------------------------------------------------- | ----------------------------------------------------------------------------------------------- | :----------: |
| **0. Discovery del Negocio**                              | Comprender el problema, procesos, actores, reglas y lenguaje del negocio.                       |      ✅      |
| **1. Modelado del Dominio (DDD Estratégico)**             | Construir el glosario, identificar subdominios, Bounded Contexts y relaciones.                  |      🟡      |
| **2. Arquitectura del Sistema**                           | Diseñar la arquitectura de alto nivel, comunicación entre servicios y decisiones estructurales. | ⏳ Pendiente |
| **3. ADR (Architecture Decision Records)**                | Documentar y justificar las decisiones arquitectónicas importantes.                             | ⏳ Pendiente |
| **4. Diseño Técnico del Backend**                         | Definir módulos, casos de uso, entidades, eventos, APIs y contratos.                            | ⏳ Pendiente |
| **5. Implementación Backend (NestJS)**                    | Construir los microservicios aplicando Clean Architecture y DDD.                                | ⏳ Pendiente |
| **6. Implementación Frontend (Angular + Microfrontends)** | Desarrollar la interfaz y la integración con el backend.                                        | ⏳ Pendiente |
| **7. IA Aplicada**                                        | Incorporar RAG, agentes, automatizaciones y asistentes inteligentes.                            | ⏳ Pendiente |
| **8. Observabilidad, DevOps y Despliegue**                | Logging, métricas, CI/CD, Docker, Kubernetes y monitoreo.                                       | ⏳ Pendiente |
| **9. Preparación para Entrevistas**                       | Defender el proyecto, explicar decisiones técnicas y practicar escenarios de Tech Lead.         | ⏳ Pendiente |

# Entregables por Fase

| Fase           | Entregables                                                                                        |
| -------------- | -------------------------------------------------------------------------------------------------- |
| Discovery      | Documento de conocimiento del negocio, glosario, actores, procesos y reglas de negocio.            |
| Modelado       | Bounded Contexts, Context Map, entidades, Value Objects, Aggregates y Domain Events identificados. |
| Arquitectura   | Diagramas C4 (Contexto, Contenedores y Componentes), flujo general y arquitectura objetivo.        |
| ADR            | Colección de ADR versionados (ADR-001, ADR-002, ...).                                              |
| Backend        | Especificación de APIs, contratos, casos de uso y estructura de microservicios.                    |
| Implementación | Código fuente, pruebas, documentación técnica y revisiones de arquitectura.                        |
| Frontend       | Microfrontends, integración y experiencia de usuario.                                              |
| IA             | Servicios de IA, RAG, embeddings y agentes integrados.                                             |
| DevOps         | Infraestructura como código, pipelines y monitoreo.                                                |
| Entrevistas    | Guía para defender el proyecto y responder preguntas técnicas.                                     |

---

# Preguntas Claves

- ¿Cómo trabaja un contador?
- ¿Qué documentos utiliza?
- ¿Qué reglas sigue?
- ¿Qué excepciones existen?
- ¿Qué significa conciliar?
- ¿Qué hace cuando falla la conciliacion?
- ¿Qué información necesita un auditor?

# ¿Qué es una conciliación bancaria desde el punto de vista del negocio?

Es el proceso de comparar los movimientos registrados por la empresa con los registrados por el banco para confirmar que ambos reflejan la misma realidad financiera.

Objetivo: detectar diferencias, encontrar su causa y asegurar que la información contable sea correcta.

# ¿Cuál es el flujo completo que sigue un auxiliar contable?

- Recibe el extracto bancario.
- Obtiene los registros contables de la empresa.
- Compara los saldos iniciales.
- Compara cada movimiento del banco con la contabilidad.
- Marca los movimientos que coinciden.
- Investiga las diferencias.
- Registra o solicita los ajustes necesarios.
- Verifica que los saldos finales sean correctos.
- Genera el informe de conciliación

# ¿Qué documentos utiliza en cada paso?

| Paso                         | Documento principal                                                                                |
| ---------------------------- | -------------------------------------------------------------------------------------------------- |
| Recibir movimientos          | Extracto bancario (reporte del banco con todos los movimientos de la cuenta).                      |
| Consultar registros internos | Libro auxiliar de bancos (registro contable de ingresos y egresos de la empresa).                  |
| Comparar movimientos         | Extracto bancario + Libro auxiliar.                                                                |
| Validar diferencias          | Comprobantes (facturas, recibos, consignaciones, transferencias, cheques, notas débito y crédito). |
| Entregar resultado           | Informe o formato de conciliación bancaria. ([Gobierno de Colombia][1])                            |

# ¿Qué reglas aplica para decidir si dos movimientos coinciden?

Dos movimientos normalmente coinciden cuando tienen:

- El mismo valor.
- La misma fecha o una fecha cercana.
- El mismo tipo de movimiento (ingreso o egreso).
- La misma referencia o número de documento.
- El mismo tercero (persona o empresa relacionada con la transacción).

# ¿Qué hace cuando no encuentra coincidencias?

Primero intenta identificar la causa de la diferencia.

Las causas más comunes son:

- El movimiento existe en el banco, pero no en la contabilidad.
- El movimiento existe en la contabilidad, pero aún no aparece en el banco.
- El valor fue registrado incorrectamente.
- La fecha es diferente.
- Existe un movimiento duplicado.
- El banco aplicó un cobro o un abono que aún no fue registrado.

Con la información que recopilaste, ya podemos construir el primer modelo del ciclo de vida de una conciliación.

Ciclo de vida
Borrador o En Proceso
│
▼
Pendiente de Revisión
│
▼
Finalizada
│
▼
Aprobada

Regla de negocio

Una conciliación puede finalizar con partidas conciliatorias pendientes, siempre que estén justificadas.

Motor de Conciliación (versión funcional 0.1)

Sin escribir una línea de código, ya sabemos qué deberá hacer el núcleo del sistema.

Tipos de conciliación soportados
Tipo Debe soportarlo el sistema
1 ↔ 1 ✅
1 ↔ N ✅
N ↔ 1 ✅
N ↔ N ✅

Este descubrimiento tiene un impacto enorme: el motor no puede asumir que siempre existe una relación uno a uno. Eso condicionará el modelo del dominio y los algoritmos.

# Regla determinística

- Una regla que siempre produce el mismo resultado con la misma entrada.

Heurística
-Regla aproximada utilizada cuando no existe una coincidencia exacta.

Trazabilidad
-Capacidad de reconstruir completamente qué ocurrió, quién lo hizo, cuándo ocurrió y cuál fue el resultado.

Inmutabilidad
-Una vez que un dato representa un hecho de negocio confirmado, no se modifica; cualquier cambio posterior se registra como un nuevo hecho.

Invariante del Dominio
-Una condición que siempre debe cumplirse para mantener la consistencia del modelo de negocio.

Integridad del Dominio
-Capacidad del sistema para impedir que las reglas fundamentales del negocio sean violadas.

Descubrimiento importante
-Los Aggregates no se diseñan primero; normalmente se descubren analizando las reglas de negocio que deben proteger.

# Modelos

1. Modelo de Roles
   Rol Responsabilidad principal
   Auxiliar Contable Ejecuta la conciliación.
   Contador Valida y aprueba.
   Auditor Verifica y audita.
   Administrador Configura el sistema.

Esto nos servirá más adelante para definir autorización y casos de uso.

2. Modelo de Auditoría

Ya sabemos que el sistema deberá registrar, como mínimo:

Creación.
Modificación.
Conciliación de movimientos.
Registro de ajustes.
Cambios de estado.
Aprobaciones.
Rechazos.
Usuario.
Fecha y hora.
📝 Apunte

Trazabilidad

Capacidad de reconstruir completamente qué ocurrió, quién lo hizo, cuándo ocurrió y cuál fue el resultado.

En sistemas financieros, la trazabilidad es un requisito funcional y, en muchos contextos, regulatorio.

3. Modelo de Eventos del Negocio

Este descubrimiento es enorme.

Hasta ahora identificamos eventos como:

Conciliación creada.
Conciliación finalizada.
Conciliación aprobada.
Conciliación rechazada.
Ajuste registrado.
Extracto importado.
Diferencia crítica detectada.

# Flujo del proceso

Conciliación creada

↓

Extracto bancario importado

↓

Movimientos contables importados

↓

Proceso automático de conciliación ejecutado

↓

Movimientos conciliados

↓

Partidas conciliatorias detectadas

↓

Ajustes contables registrados

↓

Partidas conciliatorias justificadas

↓

Conciliación enviada para revisión

↓

Conciliación aprobada

# Comandos del negocio

Crear conciliación

↓

Importar extracto

↓

Importar movimientos

↓

Iniciar conciliación

↓

Registrar ajuste

↓

Enviar revisión

↓

Aprobar conciliación

# DOMINIOS DEL NEGOCIO

                  Smart Workforce AI

                          │
        ┌─────────────────┼──────────────────┐
        │                 │                  │

Core Domain Supporting Domains Generic Domains

        │                 │                  │

Conciliación Integraciones Identidad

                   Ajustes              Autenticación

                   Auditoría            Autorización

                   Configuración        Notificaciones

                                        Reportes
