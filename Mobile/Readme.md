# Sprint-1-2025

![image](https://github.com/user-attachments/assets/6335eded-1ce5-41f1-8fbd-7921804f3f67)

## 👥 Integrantes
- Gabriel Camargo – RM557879
- Link do GIithub: https://github.com/GabrielCamargoo
- Kauan Felipe – RM557954 
- Link do GIithub: https://github.com/KauanSouz
- Vinicius Alves – RM551939  
- Link do GIithub: https://github.com/Vinicius-A-Siqueira

## Video

- **Link do video:** 


---

# APP

Aplicativo mobile desenvolvido em **React Native (Expo)** para gerenciar **tasks** e **notes**.  
Entrega intermediária da disciplina **Mobile Application Development**.
---

## ✨ Funcionalidades
- 🔐 **Autenticação** (Login, Cadastro, Logout) com validação, mensagens de erro e loaders.  
- 🎨 **Tema Claro/Escuro** com persistência e alternância em tempo real.  
- ✅ **Tasks (CRUD completo)**  
 - Criar, Listar, Detalhar, Editar, Excluir  
 - Indicadores de status (“Pendente”, “Concluída”)  
- 📝 **Notes (CRUD completo)**  
 - Criar, Listar, Detalhar, Editar, Excluir  
- 📋 **Formulários com validação** usando Formik + Yup.  
- ⏳ **Indicadores de carregamento** em todas as chamadas de rede.  
- 🎨 **UI baseada em Material Design** (React Native Paper).  

---

## 🧱 Arquitetura do Projeto

src/
App.tsx
routes/ (AuthRoutes, AppRoutes)
screens/
LoginScreen.tsx
RegisterScreen.tsx
HomeScreen.tsx
tasks/
TaskListScreen.tsx
TaskFormScreen.tsx
TaskDetailScreen.tsx
notes/
NoteListScreen.tsx
NoteFormScreen.tsx
NoteDetailScreen.tsx
services/ (api.ts, taskService.ts, noteService.ts)
contexts/ (AuthContext.tsx, ThemeContext.tsx)
components/ (Loading, ErrorMessage, ThemeToggle)
theme/ (index.tsx)
utils/ (validators.ts)

## 🔧 Requisitos
- Node.js 18+  
- Expo CLI  
- Backend disponível (.NET) com endpoints:  
 - `POST /auth/register`, `POST /auth/login` → retorna `{ token, user }`  
 - `GET/POST/PUT/DELETE /tasks`  
 - `GET/POST/PUT/DELETE /notes`  
Configuração do `.env`:

> 📌 Para celular físico use o IP da sua máquina (ex: `192.168.1.5`).

---

## ▶️ Como Rodar
### Passos

1. Clone o repositório

```bash
git clone https://github.com/Vinicius-A-Siqueira/Sprint-1-2025.git
```

2. Navegue até a pasta do projeto Mobile

```bash
cd Sprint-1-2025/Mobile/mobile-app-intermediate
```

3. Instale as dependências

```bash
npm install
# ou
yarn install
```
4. Inicie o servidor Expo

```bash
npx expo start
```

5. Abra o aplicativo em um dispositivo físico (Android/iOS) usando o app Expo Go, ou em um emulador.

---


4. Inicie o servidor Expo

npx expo start
