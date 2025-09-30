# Sprint-1-2025

![image](https://github.com/user-attachments/assets/6335eded-1ce5-41f1-8fbd-7921804f3f67)

## 👥 Integrantes

- **Gabriel Camargo** – RM557879  
- **Kauan Felipe** – RM557954  
- **Vinicius Alves** – RM551939  

## Video

- **Link do Video:** 
---

# Mottu API

## Descrição

Aplicação web desenvolvida em Spring Boot para gerenciamento de pátios e motos, com sistema de usuários autenticados para controle de acesso. Utiliza Oracle Database para persistência e Thymeleaf para renderização de páginas web.

---

## Funcionalidades

- Cadastro, listagem, edição e exclusão de pátios.
- Cadastro, listagem, edição e exclusão de motos vinculadas a pátios.
- Cadastro e autenticação de usuários com perfis administrativos e funcionais.
- Controle de acesso baseado em roles via Spring Security.
- Validação de entradas com Bean Validation.
- Migrações automáticas de banco com Flyway.

---

## Tecnologias Utilizadas

- Java 18
- Spring Boot 3.0.5
- Spring Security
- Thymeleaf 3.1.1
- Oracle Database 19c
- Flyway para migrações de banco
- Maven para build e gerenciamento de dependências

---

## Requisitos

- Oracle Database ativo e acessível
- JDK 18 instalado
- Maven instalado

---

## Configuração

Ajuste o arquivo `src/main/resources/application.properties` com os dados da sua base Oracle:
```
spring.datasource.url=jdbc:oracle:thin:@//oracle.fiap.com.br:1521/orcl
spring.datasource.username=rm551939
spring.datasource.password=sua_senha
spring.jpa.hibernate.ddl-auto=none
spring.jpa.show-sql=true
```
---

## Como executar

Na raiz do projeto, execute:
```
mvn clean package
mvn spring-boot:run
```

---

## Endpoints Principais

- `/login` - Página de login.
- `/usuario/novo` - Formulário para cadastro de novos usuários.
- `/patio` - Gerenciamento de pátios.
- `/moto` - Gerenciamento de motos.

---

## Estrutura do Projeto

- `src/main/java` - Código-fonte Java.
- `src/main/resources/templates` - Templates Thymeleaf.
- `src/main/resources/db/migration` - Scripts Flyway.
- `src/main/resources/application.properties` - Configurações de ambiente.

---

## Contato

Para dúvidas e suporte, contate o desenvolvedor.

