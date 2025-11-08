# Sprint-2025

![image](https://github.com/user-attachments/assets/6335eded-1ce5-41f1-8fbd-7921804f3f67)

## 👥 Integrantes

- **Gabriel Camargo** – RM557879
- **Kauan Felipe** – RM557954
- **Vinicius Alves** – RM551939

---

## 📋 Descrição do Projeto

A **Mottu** enfrenta desafios operacionais na gestão e localização de motos nos pátios de suas filiais. Este projeto resolve esse problema fornecendo:

✅ Localização precisa de cada moto nos pátios  
✅ Visualização em tempo real da disposição das motos  
✅ Modelo digital interativo e adaptável para diferentes pátios  
✅ Interface web intuitiva para acesso rápido  
✅ Escalabilidade para múltiplas filiais  

---

## 🎯 Funcionalidades Implementadas (CP5)

### ✨ Arquitetura
- ✅ **Clean Architecture** com 4 camadas bem definidas
- ✅ **Domain-Driven Design (DDD)** com Agregados Raiz e Value Objects
- ✅ **Clean Code** aplicando princípios SRP, DRY, KISS, YAGNI

### 🗄️ Banco de Dados
- ✅ **Integração com MongoDB** (local ou Atlas)
- ✅ **CRUD completo** para 3 collections: Motos, Pátios, Usuários
- ✅ **DTOs e validações** em todas as requisições
- ✅ **Índices únicos** para garantir integridade de dados

### 💚 Monitoramento
- ✅ **Health Check** monitorando aplicação e MongoDB
- ✅ **Endpoint `/health`** com respostas estruturadas
- ✅ **Verificações adicionais** de conectividade

### 📚 Documentação
- ✅ **Swagger** configurado com versionamento (v1, v2)
- ✅ **Endpoints documentados** com exemplos e responses
- ✅ **Dois ambientes**: Oracle (v1) e MongoDB (v2)

---

## 🏗️ Estrutura do Projeto

```
📦 Mottu.Fleet
 ┣ 📂 Mottu.Fleet.API
 ┃ ┣ 📂 Controllers
 ┃ ┃ ┣ MotosMongoController.cs
 ┃ ┃ ┣ PatiosMongoController.cs
 ┃ ┃ ┗ UsuariosMongoController.cs
 ┃ ┣ Program.cs
 ┃ ┣ appsettings.json
 ┃ ┗ Mottu.Fleet.API.csproj
 ┃
 ┣ 📂 Mottu.Fleet.Application
 ┃ ┣ 📂 DTOs
 ┃ ┗ 📂 Handlers
 ┃
 ┣ 📂 Mottu.Fleet.Domain
 ┃ ┣ 📂 Entities
 ┃ ┃ ┣ MotoMongo.cs
 ┃ ┃ ┣ PatioMongo.cs
 ┃ ┃ ┗ UsuarioMongo.cs
 ┃ ┣ 📂 ValueObjects
 ┃ ┃ ┗ Placa.cs (validação de placa)
 ┃ ┣ 📂 Aggregates
 ┃ ┃ ┗ FrotaAggregate.cs
 ┃ ┗ 📂 Interfaces
 ┃
 ┗ 📂 Mottu.Fleet.Infrastructure
   ┣ 📂 Configuration
   ┃ ┗ MongoDbSettings.cs
   ┣ 📂 Data
   ┃ ┗ MongoDbContext.cs
   ┣ 📂 Repositories
   ┃ ┣ MotoMongoRepository.cs
   ┃ ┣ PatioMongoRepository.cs
   ┃ ┗ UsuarioMongoRepository.cs
   ┗ 📂 HealthChecks
```

---

## 🔧 Configuração do Ambiente

### Pré-requisitos

- **.NET 8.0 SDK** ou superior  
- **MongoDB 6.0** ou superior (local ou [MongoDB Atlas](https://www.mongodb.com/cloud/atlas))
- **Visual Studio 2022** ou **Visual Studio Code**
- **Git** para versionamento

### Instalação do MongoDB

#### Windows
1. Baixar [MongoDB Community Edition](https://www.mongodb.com/try/download/community)
2. Executar o instalador
3. Selecionar "Run service as Network Service user" (recomendado)
4. MongoDB estará disponível em `mongodb://localhost:27017`

#### macOS
```bash
brew install mongodb-community@7.0
brew services start mongodb-community@7.0
```

#### Linux (Ubuntu)
```bash
sudo apt-get install -y mongodb
sudo systemctl start mongodb
```

### Clonar o Repositório

```bash
git clone https://github.com/Vinicius-A-Siqueira/Sprint-1-2025.git
cd Sprint-1-2025/.Net
```

### Configurar a Connection String

No arquivo `Mottu.Fleet.API/appsettings.json`, configure:

```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "MottuFleetDB",
    "Collections": {
      "Motos": "Motos",
      "Patios": "Patios",
      "Usuarios": "Usuarios"
    }
  }
}
```

**Para MongoDB Atlas (nuvem):**
```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb+srv://usuario:senha@cluster.mongodb.net/?retryWrites=true&w=majority",
    "DatabaseName": "MottuFleetDB",
    "Collections": {
      "Motos": "Motos",
      "Patios": "Patios",
      "Usuarios": "Usuarios"
    }
  }
}
```

### Instalar Dependências

```bash
cd Mottu.Fleet.API
dotnet restore
```

### Executar a Aplicação

```bash
dotnet run
```

A aplicação estará disponível em: `https://localhost:7000` (ou porta informada no console)

---

## 🧪 Testando a API

### Swagger UI
Acesse: `https://localhost:7000/swagger`

Aqui você pode:
- ✅ Visualizar todos os endpoints
- ✅ Testar requisições diretamente
- ✅ Ver exemplos de responses
- ✅ Alternar entre v1 e v2 da API

### Health Check
```bash
curl https://localhost:7000/health
```

Resposta esperada:
```json
{
  "status": "Healthy",
  "totalDuration": "00:00:00.1234567",
  "entries": {
    "mongodb": {
      "status": "Healthy",
      "duration": "00:00:00.0987654",
      "tags": ["db", "mongodb"]
    }
  }
}
```

---

## 📡 Endpoints Disponíveis

### Motos (v2 - MongoDB)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/v2/motos` | Listar todas as motos |
| GET | `/api/v2/motos/{id}` | Obter moto por ID |
| GET | `/api/v2/motos/placa/{placa}` | Obter moto por placa |
| GET | `/api/v2/motos/patio/{patioId}` | Listar motos de um pátio |
| GET | `/api/v2/motos/status/{status}` | Listar motos por status |
| POST | `/api/v2/motos` | Criar nova moto |
| PUT | `/api/v2/motos/{id}` | Atualizar moto |
| DELETE | `/api/v2/motos/{id}` | Deletar moto |

### Pátios (v2 - MongoDB)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/v2/patios` | Listar todos os pátios |
| GET | `/api/v2/patios/{id}` | Obter pátio por ID |
| GET | `/api/v2/patios/ativos` | Listar pátios ativos |
| GET | `/api/v2/patios/disponiveis` | Listar pátios com vagas |
| POST | `/api/v2/patios` | Criar novo pátio |
| PUT | `/api/v2/patios/{id}` | Atualizar pátio |
| PATCH | `/api/v2/patios/{id}/desativar` | Desativar pátio |
| DELETE | `/api/v2/patios/{id}` | Deletar pátio |

### Usuários (v2 - MongoDB)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/v2/usuarios` | Listar todos os usuários |
| GET | `/api/v2/usuarios/{id}` | Obter usuário por ID |
| GET | `/api/v2/usuarios/username/{username}` | Obter usuário por username |
| GET | `/api/v2/usuarios/perfil/{perfil}` | Listar usuários por perfil |
| GET | `/api/v2/usuarios/ativos` | Listar usuários ativos |
| POST | `/api/v2/usuarios` | Criar novo usuário |
| PUT | `/api/v2/usuarios/{id}` | Atualizar usuário |
| PATCH | `/api/v2/usuarios/{id}/ultimo-acesso` | Atualizar último acesso |
| PATCH | `/api/v2/usuarios/{id}/desativar` | Desativar usuário |
| DELETE | `/api/v2/usuarios/{id}` | Deletar usuário |

### Monitoramento

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/health` | Status de saúde da aplicação |

---

## 📊 Exemplos de Uso

### Criar uma Moto
```bash
curl -X POST https://localhost:7000/api/v2/motos \
  -H "Content-Type: application/json" \
  -d '{
    "placa": "ABC1234",
    "modelo": "Honda CG 160",
    "ano": 2023,
    "cor": "Vermelha",
    "status": "Disponivel",
    "patioId": "id_do_patio"
  }'
```

### Criar um Pátio
```bash
curl -X POST https://localhost:7000/api/v2/patios \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Pátio Central",
    "endereco": "Av. Principal, 1000",
    "cidade": "São Paulo",
    "estado": "SP",
    "cep": "01000-000",
    "capacidade": 100,
    "telefone": "(11) 4000-5000"
  }'
```

### Criar um Usuário
```bash
curl -X POST https://localhost:7000/api/v2/usuarios \
  -H "Content-Type: application/json" \
  -d '{
    "username": "vinicius",
    "password": "senhaSegura123",
    "perfil": "Admin",
    "fullName": "Vinicius Alves",
    "email": "vinicius@mottu.com",
    "phone": "(11) 99999-9999"
  }'
```

---

## 🏗️ Arquitetura e Design

### Clean Architecture

A aplicação está organizada em 4 camadas:

1. **API Layer** - Controllers, validações e Swagger
2. **Application Layer** - DTOs e casos de uso
3. **Domain Layer** - Entidades, Value Objects e regras de negócio
4. **Infrastructure Layer** - Acesso a dados, MongoDB e Health Checks

### Domain-Driven Design (DDD)

- **Entities**: `MotoMongo`, `PatioMongo`, `UsuarioMongo`
- **Value Object**: `Placa` (valida formato de placa brasileira)
- **Aggregate Root**: `FrotaAggregate` (gerencia coleção de motos)

### Princípios de Clean Code

- **SRP** (Single Responsibility): Cada classe tem uma única responsabilidade
- **DRY** (Don't Repeat Yourself): Código reutilizável e sem duplicação
- **KISS** (Keep It Simple, Stupid): Soluções simples e objetivas
- **YAGNI** (You Aren't Gonna Need It): Apenas funcionalidades necessárias

---

## 🔐 Segurança

Recomendações de segurança:

- [ ] Usar HTTPS em produção
- [ ] Implementar autenticação (JWT/OAuth2)
- [ ] Validar e sanitizar entradas
- [ ] Usar variáveis de ambiente para secrets
- [ ] Implementar rate limiting
- [ ] Adicionar CORS quando necessário

---

## 📈 Métricas de Código

| Métrica | Valor |
|---------|-------|
| Linhas de Código | ~2000+ |
| Repositórios | 3 (Motos, Pátios, Usuários) |
| Controllers | 3 |
| Endpoints | 24+ |
| Testes | Em desenvolvimento |

---

## 🐛 Troubleshooting

### MongoDB não conecta
```bash
# Verificar se o MongoDB está rodando
mongosh
# ou
mongo
```

### Porta 7000 já em uso
```bash
# Usar uma porta diferente
dotnet run --urls="https://localhost:7001"
```

### Erro ao restaurar pacotes
```bash
dotnet clean
dotnet restore
```

---

## 📚 Tecnologias Utilizadas

- **Runtime**: .NET 8.0
- **Framework**: ASP.NET Core
- **Banco de Dados**: MongoDB
- **ORM**: MongoDB.Driver
- **Documentação**: Swagger/OpenAPI
- **Health Checks**: AspNetCore.HealthChecks
- **Versionamento**: API versioning com Swagger

---

## 📝 Commits Semânticos

O projeto utiliza commits semânticos:

```bash
git commit -m "feat: adiciona integração com MongoDB"
git commit -m "feat: implementa CRUD completo para Motos"
git commit -m "fix: corrige validação de placa"
git commit -m "refactor: simplifica lógica de repositório"
git commit -m "docs: atualiza README com instruções"
```

---

## 🤝 Contribuindo

Para contribuir:

1. Fork o repositório
2. Crie uma branch (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'feat: add AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

---

## 📞 Contato

Para dúvidas ou sugestões:

- **Vinicius Alves** (RM551939)
- **Gabriel Camargo** (RM557879)
- **Kauan Felipe** (RM557954)

---

## 📜 Licença

Este projeto é parte do desafio **CP5 - FIAP 2TDS 2025**.

---

## 🎓 Referências

- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)
- [MongoDB C# Driver](https://www.mongodb.com/docs/drivers/csharp/)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Domain-Driven Design](https://martinfowler.com/bliki/DomainDrivenDesign.html)

---

**Desenvolvido com ❤️ durante o CP5 - FIAP 2TDS 2025**
