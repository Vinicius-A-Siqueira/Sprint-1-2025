# Sprint-2025

![image](https://github.com/user-attachments/assets/6335eded-1ce5-41f1-8fbd-7921804f3f67)

## 👥 Integrantes

- **Gabriel Camargo** – RM557879  
- **Kauan Felipe** – RM557954  
- **Vinicius Alves** – RM551939  

## Video

- **Link do Video:**
---
# Mottu Application - Guia de Uso

## Descrição

Aplicação Mottu API desenvolvida com Spring Boot em Java, com banco de dados Azure SQL.  
Permite operações CRUD para entidades: Usuário, Pátio e Moto.  
Publicado no Azure App Service com armazenamento do banco na nuvem.

## Pré-requisitos

- Conta Azure com App Service e Azure SQL configurados.
- Java 17 e Maven instalados para execução local.
- Ferramenta para SQL: Azure Portal Query editor, Azure Data Studio, ou SSMS.

## Passo a Passo para Uso

### 1. Clonar repositório

```
git clone [<url-do-repositório>](https://github.com/Vinicius-A-Siqueira/Sprint-1-2025)
cd Devops/mottuapi
```

### 2. Configurar banco Azure SQL

No portal Azure:

- Crie o banco Azure SQL e configure regras de firewall para seu IP e do App Service.
- Utilize o Query Editor ou Azure Data Studio para executar os scripts SQL:

```
-- Drop tabelas se necessário
IF OBJECT_ID('dbo.MOTO', 'U') IS NOT NULL DROP TABLE dbo.MOTO;
IF OBJECT_ID('dbo.USUARIO', 'U') IS NOT NULL DROP TABLE dbo.USUARIO;
IF OBJECT_ID('dbo.PATIO', 'U') IS NOT NULL DROP TABLE dbo.PATIO;

-- Criação das tabelas
CREATE TABLE USUARIO (
ID INT IDENTITY(1,1) PRIMARY KEY,
USERNAME NVARCHAR(100) NOT NULL UNIQUE,
PASSWORD NVARCHAR(100) NOT NULL,
PERFIL NVARCHAR(50) NOT NULL
);

CREATE TABLE PATIO (
ID INT IDENTITY(1,1) PRIMARY KEY,
NOME NVARCHAR(100) NOT NULL,
ENDERECO NVARCHAR(255) NOT NULL
);

CREATE TABLE MOTO (
ID INT IDENTITY(1,1) PRIMARY KEY,
PLACA NVARCHAR(20) NOT NULL UNIQUE,
MODELO NVARCHAR(100) NOT NULL,
PATIO_ID INT NOT NULL,
FOREIGN KEY (PATIO_ID) REFERENCES PATIO(ID)
);

-- Inserir dados
INSERT INTO USUARIO (USERNAME, PASSWORD, PERFIL) VALUES ('admin', '{noop}admin123', 'ROLE_ADMIN');
INSERT INTO USUARIO (USERNAME, PASSWORD, PERFIL) VALUES ('user', '{noop}user123', 'ROLE_FUNCIONARIO');

INSERT INTO PATIO (NOME, ENDERECO) VALUES ('Patio Norte', 'Rua XPTO, 123');
INSERT INTO PATIO (NOME, ENDERECO) VALUES ('Patio Sul', 'Avenida ABC, 456');

INSERT INTO MOTO (PLACA, MODELO, PATIO_ID) VALUES ('ABC1D23', 'Mottu Sport 110i', 1);
INSERT INTO MOTO (PLACA, MODELO, PATIO_ID) VALUES ('DEF4G56', 'Mottu Delivery 2023', 2);
```
### 3. Buildar aplicação localmente
```
mvn clean package
```
### 4. Deploy no Azure App Service
```
git add target/mottu-app-1.0.0.jar
git commit -m "Deploy jar"
git push azure master
```
### 5. Acessar aplicação

Abra a URL pública do App Service, ex.: `https://mottu-api-unique-1001.azurewebsites.net`

### 6. Logs e Diagnóstico

Monitorar logs com:
```
az webapp log tail --resource-group grupomottu --name mottu-api-unique-1001
```
### 7. Reiniciar App Service

No portal Azure > Seu App Service > Overview > Restart, ou via CLI:
```
az webapp restart --resource-group grupomottu --name mottu-api-unique-1001
```


