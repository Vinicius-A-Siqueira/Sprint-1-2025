# Sprint-1-2025

![image](https://github.com/user-attachments/assets/6335eded-1ce5-41f1-8fbd-7921804f3f67)

## 👥 Integrantes

- **Gabriel Camargo** – RM557879  
- **Kauan Felipe** – RM557954  
- **Vinicius Alves** – RM551939

## 🏗️ Justificativa da Arquitetura

A arquitetura foi baseada na Clean Architecture para garantir escalabilidade, manutenibilidade e independência tecnológica do software. Esse padrão separa o domínio central do sistema (regras de negócio e entidades) das camadas externas de infraestrutura (banco de dados, frameworks, web/API), permitindo que mudanças em tecnologia ou ferramentas não afetem a lógica de negócio.

- **Independência de tecnologia**: O domínio não depende de frameworks, bancos ou bibliotecas externas, facilitando upgrades e migrações tecnológicas sem afetar as regras de negócio.
- **Alta testabilidade:** É possível testar as regras de negócio isoladamente, sem dependências externas, aumentando a confiabilidade dos testes.
- **Adaptabilidade::** Novos requisitos e integrações podem ser implementados com menor impacto no código existente.
- **Facilidade de manutenção:** Alterações, correções de bugs e adaptações a novas necessidades são feitas de forma localizada, reduzindo risco de efeitos colaterais.

Com Clean Architecture, evoluir e dar manutenção na aplicação torna-se mais ágil, estável e seguro, promovendo um crescimento sustentável do projeto sem criar “gambiarra” ou débitos técnicos sérios no longo prazo.

---

### Clean Architecture (Arquitetura Limpa)

A aplicação foi desenvolvida seguindo os princípios da **Clean Architecture**, garantindo:

- **Separação de Responsabilidades**: Cada camada tem uma responsabilidade específica
- **Independência de Frameworks**: O domínio não depende de tecnologias externas
- **Testabilidade**: Facilita a criação de testes unitários e de integração
- **Manutenibilidade**: Código organizando e fácil de manter

### Estrutura das Camadas

```
Mottu.Fleet.Domain/          # Entidades, Enums, Interfaces do domínio
Mottu.Fleet.Application/     # DTOs, Services, Validações, Regras de negócio
Mottu.Fleet.Infrastructure/  # Repositórios, DbContext, Implementações externas
Mottu.Fleet.API/            # Controllers, Configurações, Startup
```

### Tecnologias Utilizadas

- **.NET 8**: Framework principal
- **Entity Framework Core**: ORM para acesso aos dados
- **Oracle Database**: Banco de dados relacional
- **AutoMapper**: Mapeamento entre entidades e DTOs
- **Swagger/OpenAPI**: Documentação interativa da API
- **Dependency Injection**: Injeção de dependência nativa do .NET

### Padrões Implementados

- **Repository Pattern**: Abstração da camada de dados
- **Unit of Work**: Gerenciamento de transações
- **DTO Pattern**: Transferência de dados entre camadas
- **Dependency Inversion**: Inversão de dependências

## 🚀 Instruções de Execução da API

### Pré-requisitos

- .NET 8 SDK
- Oracle Database (local ou remoto)
- Visual Studio 2022 ou VS Code

### 1. Clone o Repositório

```bash
git clone https://github.com/Vinicius-A-Siqueira/Sprint-1-2025
cd Sprint-1-2025\Devops\mottuapi
```

### 2. Configure a String de Conexão

Edite o arquivo `appsettings.json` na pasta `Mottu.Fleet.API`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost:1521/orcl;User Id=seu_usuario;Password=sua_senha;"
  }
}
```

### 3. Execute os Scripts do Banco

Execute os scripts SQL disponíveis na pasta `Database/`:

```sql
-- Execute na seguinte ordem:
-- 1. create_tables.sql (criação das tabelas)
-- 2. seed_data.sql (dados iniciais)
```

### 4. Instale as Dependências

```bash
dotnet restore
```

### 5. Execute a Aplicação

```bash
cd Mottu.Fleet.API
dotnet run
```

A API estará disponível em:
- **HTTP**: `http://localhost:5010`
- **HTTPS**: `https://localhost:5011`
- **Swagger**: `http://localhost:5010/swagger`

## 📚 Exemplos de Uso dos Endpoints

### Autenticação
A API utiliza autenticação baseada em tokens JWT (caso implementada).

### 🏍️ Motos

#### Listar Motos
```http
GET /api/motos?page=1&pageSize=10&search=honda
```

**Resposta:**
```json
{
  "items": [
    {
      "id": 1,
      "placa": "ABC1D23",
      "modelo": "Mottu Sport 110i",
      "patioId": 1,
      "patioNome": "Patio Norte",
      "ano": 2023,
      "cor": "Branca",
      "quilometragem": 0,
      "status": 1,
      "statusDescricao": "Disponivel"
    }
  ],
  "totalItems": 1,
  "page": 1,
  "pageSize": 10,
  "totalPages": 1
}
```

#### Buscar Moto por ID
```http
GET /api/motos/1
```

#### Criar Nova Moto
```http
POST /api/motos
Content-Type: application/json

{
  "placa": "XYZ9876",
  "modelo": "Mottu Delivery",
  "patioId": 1,
  "ano": 2023,
  "cor": "Vermelha",
  "quilometragem": 0,
  "status": 1,
  "chassi": "9BD12345678901236",
  "numeroMotor": "MT110XYZ123456"
}
```

#### Atualizar Moto
```http
PUT /api/motos/1
Content-Type: application/json

{
  "id": 1,
  "placa": "ABC1D23",
  "modelo": "Mottu Sport 110i",
  "patioId": 1,
  "ano": 2023,
  "cor": "Azul",
  "quilometragem": 1500,
  "status": 2
}
```

#### Deletar Moto
```http
DELETE /api/motos/1
```

### 🏢 Pátios

#### Listar Pátios
```http
GET /api/patios?page=1&pageSize=10
```

#### Buscar Pátio por ID
```http
GET /api/patios/1
```

#### Criar Novo Pátio
```http
POST /api/patios
Content-Type: application/json

{
  "nome": "Patio Oeste",
  "endereco": "Rua das Flores, 456",
  "cidade": "São Paulo",
  "estado": "SP",
  "cep": "01234-567",
  "capacidade": 120,
  "telefone": "(11) 9999-9999"
}
```

### 👤 Usuários

#### Listar Usuários
```http
GET /api/usuarios?page=1&pageSize=10
```

#### Buscar Usuário por ID
```http
GET /api/usuarios/1
```

#### Criar Novo Usuário
```http
POST /api/usuarios
Content-Type: application/json

{
  "username": "operador2",
  "password": "senha123",
  "perfil": "ROLE_OPERADOR",
  "fullName": "João Silva",
  "email": "joao@mottu.com",
  "phone": "(11) 98765-4321",
  "status": 1
}
```

## 🧪 Comando para Rodar os Testes

### Testes Unitários

```bash
# Executar todos os testes
dotnet test

# Executar testes com relatório de cobertura
dotnet test --collect:"XPlat Code Coverage"

# Executar testes de um projeto específico
dotnet test Mottu.Fleet.Tests.Unit

# Executar testes com verbose
dotnet test --verbosity normal
```

### Testes de Integração

```bash
# Executar apenas testes de integração
dotnet test Mottu.Fleet.Tests.Integration

# Executar testes com filtro
dotnet test --filter Category=Integration
```

### Gerar Relatório de Cobertura

```bash
# Instalar ferramenta de relatório
dotnet tool install -g dotnet-reportgenerator-globaltool

# Gerar relatório
reportgenerator -reports:"coverage.cobertura.xml" -targetdir:"coveragereport"
```

## 📊 Status dos Enums

### MotoStatus
- `1` - Disponível
- `2` - Em Uso
- `3` - Em Manutenção
- `4` - Fora de Serviço
- `5` - Reservada

### UserStatus
- `1` - Ativo
- `2` - Inativo
- `3` - Bloqueado

## 🔧 Configurações Adicionais

### Variáveis de Ambiente

```bash
# Ambiente de desenvolvimento
ASPNETCORE_ENVIRONMENT=Development

# String de conexão (alternativa ao appsettings.json)
ConnectionStrings__DefaultConnection="Data Source=localhost:1521/XE;User Id=usuario;Password=senha;"
```

## 📝 Observações

- A API utiliza Entity Framework InMemory em desenvolvimento para facilitar os testes
- Em produção, configure corretamente a string de conexão do Oracle
- Os dados são inicializados automaticamente através do `DbInitializer`
- A documentação completa está disponível via Swagger UI

## 🤝 Contribuições

Para contribuir com o projeto:

1. Faça fork do repositório
2. Crie uma branch para sua feature (`git checkout -b feature/nova-funcionalidade`)
3. Commit suas mudanças (`git commit -am 'Adiciona nova funcionalidade'`)
4. Push para a branch (`git push origin feature/nova-funcionalidade`)
5. Crie um Pull Request