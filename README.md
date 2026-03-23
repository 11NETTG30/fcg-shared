# fcg-shared

Pacotes NuGet compartilhados do **FIAP Cloud Games (FCG)**.

Contém as abstrações, implementações de infraestrutura e contratos de eventos reutilizados por todos os microsserviços do ecossistema FCG. Publicados via GitHub Packages na organização [11NETTG30](https://github.com/11NETTG30).

---

## Bibliotecas

### `FCG.Shared.Domain`

Abstrações e primitivos de domínio para uso na camada Domain de qualquer microsserviço: classes base para entidades e value objects, interfaces de repositório, unidade de trabalho, log de domínio, auditoria e exceções de domínio tipadas (`DomainException`, `ValidationException`, `ConflictException`).

### `FCG.Shared.Infrastructure`

Implementações reutilizáveis de infraestrutura: middlewares de tratamento de erros (ProblemDetails RFC 7807), interceptor EF Core para auditoria automática, implementação de `IUnitOfWork` sobre `DbContext`, acesso ao usuário logado via `IHttpContextAccessor` e configuração de observabilidade.

### `FCG.Shared.Contracts`

Contratos de eventos de integração trafegados via RabbitMQ (MassTransit). Cada microsserviço referencia este pacote para publicar ou consumir eventos sem acoplamento direto.

> **Estratégia de versionamento**: campos novos são sempre `nullable`. Campos existentes nunca são removidos.

---

## Configuração do NuGet (GitHub Packages)

Este repositório publica no GitHub Packages. Como o GitHub Packages exige autenticação mesmo para pacotes públicos, é necessário configurar um **Personal Access Token (PAT)** antes de restaurar dependências.

### 1. Criar o PAT no GitHub

1. Acesse **GitHub → Settings → Developer settings → Personal access tokens → Tokens (classic)**
2. Clique em **Generate new token (classic)**
3. Dê um nome (ex: `fcg-nuget-read`) e selecione o escopo **`read:packages`**
4. Clique em **Generate token** e copie o valor gerado

### 2. Registrar a fonte NuGet localmente

Execute o comando abaixo substituindo `SEU_USUARIO` e `SEU_TOKEN`:

```bash
dotnet nuget add source "https://nuget.pkg.github.com/11NETTG30/index.json" \
  --name "github" \
  --username "SEU_USUARIO" \
  --password "SEU_TOKEN" \
  --store-password-in-clear-text \
  --configfile ~/.nuget/NuGet/NuGet.Config
```

> As credenciais ficam no arquivo global da sua máquina (`~/.nuget/NuGet/NuGet.Config`) e **nunca entram no repositório**.

### 3. Consumir os pacotes

Adicione as referências necessárias no `.csproj` do seu microsserviço:

```xml
<PackageReference Include="FCG.Shared.Domain"         Version="x.y.z" />
<PackageReference Include="FCG.Shared.Infrastructure"  Version="x.y.z" />
<PackageReference Include="FCG.Shared.Contracts"       Version="x.y.z" />
```

---

## Publicação (CI)

Cada biblioteca tem seu próprio workflow de publicação, acionado manualmente via **workflow_dispatch**:

| Workflow | Biblioteca |
|---|---|
| `publish-domain.yml` | `FCG.Shared.Domain` |
| `publish-infrastructure.yml` | `FCG.Shared.Infrastructure` |
| `publish-contracts.yml` | `FCG.Shared.Contracts` |

Para publicar, acesse **Actions → [nome do workflow] → Run workflow**.

---

## Desenvolvimento Local

Enquanto os pacotes não estão publicados, os microsserviços podem referenciar as bibliotecas diretamente via `ProjectReference`:

```xml
<ProjectReference Include="../../../fcg-shared/src/FCG.Shared.Domain/FCG.Shared.Domain.csproj" />
```

---

## Estrutura do Repositório

```
fcg-shared/
├── src/
│   ├── FCG.Shared.Domain/           ← abstrações de domínio
│   ├── FCG.Shared.Infrastructure/   ← implementações reutilizáveis
│   └── FCG.Shared.Contracts/        ← eventos de integração
└── FCG.Shared.slnx
```
