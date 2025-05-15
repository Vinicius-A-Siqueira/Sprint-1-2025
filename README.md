# Sprint-1-2025

![image](https://github.com/user-attachments/assets/6335eded-1ce5-41f1-8fbd-7921804f3f67)

## 👥 Integrantes

- **Gabriel Camargo** – RM557879  
- **Kauan Felipe** – RM557954  
- **Vinicius Alves** – RM551939  

---

## 📌 Escopo do Projeto

### ❗ Descrição Detalhada do Problema

A Mottu enfrenta desafios operacionais na **gestão e localização de motos** nos pátios de mais de 100 filiais. Atualmente, esse controle é feito **manualmente**, o que resulta em:

- Ineficiência no gerenciamento.
- Alta propensão a erros humanos.
- Dificuldade de escalabilidade operacional.

---

### 🎯 Objetivos da Solução Idealizada

- 📍 **Identificar com precisão** a localização de cada moto nos pátios.
- 🗺️ Fornecer **visualização em tempo real** da disposição das motos.
- 🧩 Criar um **modelo digital interativo e adaptável** para diferentes pátios.
- 📱 Desenvolver **interface web/mobile** de acesso rápido e intuitivo.
- 📡 **Integrar sensores IoT** às motos para coleta de dados adicionais (posição, status, alertas).
- 🌎 Permitir **escalabilidade** da solução para todas as filiais no Brasil e México.

---

## 🚀 Instruções de Execução

### 🛠️ Pré-requisitos

- Java 17 ou superior instalado
- Maven instalado
- Oracle Database configurado e em execução
- Postman ou `curl` para testes (opcional)
- IDE (IntelliJ, Eclipse ou VSCode com suporte Java)

---

### 🔧 Setup do Projeto

1. **Clone o repositório:**

```bash
git clone https://github.com/seu-usuario/projeto-mottu.git
cd projeto-mottu
```

2. **Configure o banco Oracle:**

- Execute os scripts SQL disponíveis na pasta `/database` para criar as tabelas e inserir os dados de exemplo.
- Certifique-se de que o Oracle esteja acessível via JDBC com as credenciais corretas.

3. **Configure o `application.properties`:**

```properties
spring.datasource.url=jdbc:oracle:thin:@//oracle.fiap.com.br:1521/orcl
spring.datasource.username=rm551939
spring.datasource.password=270399
spring.datasource.driver-class-name=oracle.jdbc.OracleDriver
spring.jpa.database-platform=org.hibernate.dialect.OracleDialect
spring.jpa.hibernate.ddl-auto=update
spring.jpa.show-sql=true
spring.jpa.properties.hibernate.format_sql=true
spring.jpa.properties.hibernate.use_sql_comments=true

```

4. **Execute o projeto:**

```bash
./mvnw spring-boot:run
```

> O servidor será iniciado em `http://localhost:8080`.

---

### 📫 Endpoints Principais

- `GET /api/filiais` – Lista todas as filiais  
- `POST /api/filiais` – Cria uma nova filial  
- `PUT /api/filiais/{id}` – Atualiza os dados de uma filial  
- `DELETE /api/filiais/{id}` – Remove uma filial  

(Endpoints de motos também estão disponíveis se implementados.)

---

### 📎 Observações

- Autenticação básica está ativada (`admin:admin123` por padrão).
- Use o Postman ou `curl` com as credenciais acima para consumir a API.
