# Arquitetura do Sistema ERP (Híbrido)

## 1. Visão Geral
Sistema com arquitetura **Edge-to-Cloud**.
- **Edge (Local):** Banco PostgreSQL + API .NET 8 (Operação em tempo real, suporte offline).
- **Cloud (Nuvem):** Banco PostgreSQL + API .NET 8 + Metabase (Gestão e BI).

Objetivos:
- **Operação resiliente** no PDV (mesmo sem internet).
- **Centralização** de gestão/BI na nuvem (fonte oficial para relatórios).
- **Sincronização incremental** com rastreabilidade e tolerância a falhas.

Diagrama (alto nível):

```mermaid
flowchart LR
  subgraph Edge[Edge (Loja / PDV)]
    EdgeApi[API .NET 8]
    EdgeDb[(PostgreSQL)]
    EdgeUi[UI/Operação]
    EdgeUi --> EdgeApi --> EdgeDb
  end

  subgraph Cloud[Cloud (Admin / BI)]
    CloudApi[API .NET 8]
    CloudDb[(PostgreSQL)]
    Metabase[Metabase]
    CloudApi --> CloudDb --> Metabase
  end

  EdgeApi <--> Sync[Sync: Pull/Push incremental] <--> CloudApi
```

## 2. Padrão de Dados (Sync & Consistência)
Toda entidade (tabela) deve seguir a estrutura mínima de campos de controle:
- `created_at` (timestamp)
- `updated_at` (timestamp)
- `is_synced` (boolean)
- `version` (int)

Regras de negócio:
- **Single Source of Truth:** O BI e Painel Admin consultam exclusivamente a Nuvem.
- **D-1 Analítico:** Relatórios de BI operam com fechamento diário (D-1), garantindo dados imutáveis.
- **Handshake:** O caixa só marca como `is_synced = true` após confirmação (200 OK) da API Cloud.

Decisões de sync (recomendadas):
- **IDs imutáveis**: preferir `uuid` (ou `bigint` com estratégia que evite colisão entre lojas) para permitir criação offline.
- **Soft delete**: incluir `deleted_at` (timestamp, nullable) para propagar deleções sem perder histórico.
- **Concorrência**: `version` deve ser incrementado a cada alteração; a Cloud valida concorrência e decide conflitos.
- **Conflitos**: regra padrão “última gravação vence” é aceitável apenas para entidades não-críticas; para entidades críticas (ex.: fechamento, pagamento) exigir fluxo explícito (ex.: rejeitar e solicitar resolução).
- **Auditoria**: eventos relevantes (ex.: fechamento de caixa) devem ser persistidos como “append-only” (não editáveis) na Cloud.

## 3. Diretrizes de Performance (Hardware Limitado)
- **Paginação:** Listagens devem ter limite padrão de 50 registros.
- **Dapper:** Utilizar Dapper para consultas de leitura intensa (evitar EF Core em relatórios).
- **Assincronismo:** Uso obrigatório de `async/await` para evitar travamento da UI.
- **Background Jobs:** Sincronização e relatórios devem rodar em segundo plano (Hangfire/BackgroundService).

Diretrizes adicionais:
- **Índices**: garantir índices em (`updated_at`), (`is_synced`), e chaves de busca usuais por entidade.
- **Queries previsíveis**: evitar `SELECT *` em endpoints de listagem; projetar apenas colunas necessárias.
- **Timeouts/Retry**: toda chamada Cloud deve ter timeout e política de retry com backoff (sem travar o fluxo do caixa).

## 4. Estrutura de Pastas Esperada
/src
  /Core           -> Entidades e Regras de Negócio
  /Infrastructure -> DbContext, Dapper, Mappings
  /API            -> Controllers e Middleware
  /SyncService    -> Jobs de Background (Sincronização)
  /AdminPanel     -> Frontend Web

## 5. Segurança e Observabilidade (mínimo)
- **Autenticação**: tokens por loja/caixa (evitar credenciais fixas em cliente).
- **Autorização**: perfis separados (PDV vs Admin) com escopos claros.
- **Logs**: logs estruturados com `correlation_id` por request e por lote de sincronização.
- **Métricas**: filas de sync (pendências), latência de push/pull, falhas por endpoint, tempo desde última sync.

## 6. Próximos Passos (Prioridades)
1. Refatorar entidades existentes para herdar de `BaseEntity`.
2. Implementar `BaseEntity` com os campos de controle de Sync.
3. Criar Job de sincronização incremental.
4. Implementar sistema de "Fechamento de Caixa" para snapshot D-1.