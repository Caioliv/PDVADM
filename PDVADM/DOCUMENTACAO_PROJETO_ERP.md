## Documentação do Projeto ERP/PDV (PDVADM)

Este documento descreve a arquitetura de dados e sincronização entre **PDV local (Edge)** e **Cloud**, além de regras de performance e boas práticas para manter consistência e previsibilidade operacional.

## 1) Arquitetura de Dados (Single Source of Truth)
- **Servidor Local (Edge)**: opera PDV/Comandas com foco em **disponibilidade offline**, **baixa latência** e **robustez**.
- **Servidor Cloud**: fonte oficial para **Administração**, **BI** e auditoria.
- **Consistência (Handshake)**: o caixa **só marca como sincronizado** após confirmação do recebimento/commit no Cloud.
- **IDs globais**: vendas e documentos críticos usam **GUID/UUID** para evitar colisões e divergências.

## 2) Níveis de Visualização de Dados
- **Painel Administrativo (near real-time)**: consulta a **API Cloud** para monitorar:
  - vendas em andamento
  - comandas abertas
  - estoque imediato (se aplicável ao fluxo)
- **BI (Metabase – D-1)**: consulta **tabelas consolidadas** geradas pelo processo de **Fechamento de Caixa**.
  - objetivo: garantir que o dono veja dados **auditados** e **imutáveis** (pós-fechamento).

## 3) Padrão de Sincronização (Workflow)
### 3.1 Upload (Caixa → Nuvem)
- **Fila de envio**: vendas e eventos são persistidos localmente e publicados via fila.
- **Retry automático**: reenvio em falhas de rede/timeouts com backoff.
- **Idempotência (recomendado)**: operações devem ser reprocessáveis sem duplicar registros (por exemplo, via `sale_id` GUID + chaves naturais).

### 3.2 Download (Nuvem → Caixa)
- **Sincronização incremental**: baseada em `updated_at` (ou `version`/`rowversion`).
- **Marcadores (cursor)**: manter um cursor local do “último update aplicado” por entidade/aggregate para evitar full sync.

### 3.3 Consolidação (Fechamento)
- **Fechamento de Caixa**: o fechamento do dia (físico/operacional) dispara a criação de um **snapshot D-1** para BI.
- **Imutabilidade**: após o fechamento, os dados consolidados devem ser tratados como **auditáveis** (alterações posteriores exigem trilha/lançamentos de ajuste).

## 4) Regras de Performance (Hardware Limitado)
- **Paginação**: todas as listagens limitadas a **50 itens** por padrão (com paginação/cursor).
- **Micro-ORM (Dapper)**: para leituras intensas e consultas críticas visando menor overhead e consumo de RAM.
- **Assíncrono**: tudo que envolve rede ou banco usa `async/await` para não travar UI.
- **Background jobs**: sincronização e relatórios fora da thread principal (ex.: **Hangfire** ou serviços de background embutidos).

## 5) Checklist de Boas Práticas (convenções do projeto)
- **SyncStatus por entidade**: garantir colunas/campos de controle como:
  - `is_synced`
  - `updated_at`
  - `version` (ou equivalente)
- **BaseEntity**: priorizar uma `BaseEntity` com campos comuns de controle/auditoria.
- **Consultas enxutas**: evitar `SELECT *`; selecionar apenas colunas necessárias.
- **Domínio na API**: manter regras de negócio na API/serviço de aplicação, não na camada de persistência.

## 6) Glossário (rápido)
- **Edge**: PDV local (opera offline e sincroniza com Cloud).
- **Handshake**: confirmação explícita de recebimento/commit no Cloud antes de marcar como sincronizado.
- **D-1**: visão do dia anterior (ou “após fechamento”), consolidada e auditável.