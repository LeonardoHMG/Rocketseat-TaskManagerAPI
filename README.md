# ✅  Gerenciador de tarefas simples  – API REST em .NET

![.NET](https://img.shields.io/badge/.NET-8-purple)  
![C#](https://img.shields.io/badge/C%23-API-green)  
![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-blue)  

Este projeto é uma API REST completa desenvolvida em .NET, criada como parte da formação em C# da Rocketseat.
A aplicação utiliza uma arquitetura em camadas, aplicando boas práticas ao separar claramente a Camada de Comunicação da Camada de Regras de Negócio.

A API foi projetada para gerenciar tarefas, oferecendo um CRUD completo, com validações robustas, tratamento adequado de status codes e documentação integrada via Swagger, garantindo organização, consistência e facilidade de uso.
---

## 🚀 Funcionalidades

A API possibilita:

1. **📝 Criar uma nova tarefa**
2. **📋 Listar todas as tarefas registradas**
3. **🔍 Buscar tarefa por ID**
4. **✏️ Atualizar dados de uma tarefa**
5. **🗑️ Excluir uma tarefa**
---

## 📌 Regras e Validações

### 🧾 Campos Obrigatórios

| Campo         | Tipo     | Obrigatório | Validações                                     |
|---------------|----------|-------------|------------------------------------------------|
| `id`          | GUID     | Sim         | Gerado automaticamente; único para cada tarefa |
| `name`        | string   | Sim         | Máximo de 100 caracteres                       |
| `description` | string   | Não         | Máximo de 500 caracteres                       |
| `priority`    | string   | Sim         | high, medium ou low                            |
| `dueDate`     | DateTime | Sim         | Data futura para conclusão da tarefa           |
| `status`      | status   | Sim         | pending, inProgress ou completed               |

### 🧠 Regras de Negócio

✔️ 1. Criação de Tarefa
-----------------------
Ao criar uma tarefa:

*   O **nome é obrigatório** e deve ter **no máximo 100 caracteres**.
*   A **data limite (dueDate) deve ser futura** — não é permitido criar tarefas com data no passado. 
*   O campo **priority** deve aceitar **somente**:
    *   high 
    *   medium 
    *   low       
*   O campo **status** deve aceitar **somente**:
    *   pending 
    *   inProgress   
    *   completed
        
*   O campo **description** é opcional, mas se informado, deve ter **até 500 caracteres**.
*   O id deve ser gerado automaticamente e ser único para cada tarefa.
    
✔️ 2. Atualização de Tarefa
---------------------------
Ao atualizar uma tarefa:
*   É permitido atualizar: **nome**, **descrição**, **prioridade**, **data limite** e **status**. 
*   O nome continua respeitando o limite de 100 caracteres.
*   dueDate deve ser futura **ou igual à data atual**. 
*   Os valores aceitos de priority e status continuam restritos aos mesmos conjuntos.
*   A tarefa **deve existir** — caso contrário → erro **404 Not Found**.
    
✔️ 3. Exclusão de Tarefa
------------------------
*   Só é permitido excluir tarefas **existentes**. 
*   Se o ID não existir → deve retornar **404 Not Found**.
    
✔️ 4. Consulta de Dados
-----------------------
*   Deve ser possível listar todas as tarefas existentes. 
*   Ao buscar por ID:
    *   Se a tarefa não for encontrada → **404 Not Found**.
        
✔️ 5. Integridade e Consistência
--------------------------------

*   Nenhum campo obrigatório pode ser nulo.
*   Se qualquer dado estiver inválido → **400 Bad Request**.
*   Conflitos de operação devem retornar **409 Conflict**.  
*   Erros inesperados devem retornar **500 Internal Server Error**.
---

🏗️ Arquitetura em Camadas
--------------------------

O projeto segue uma divisão clara de responsabilidades:

### **📡 TaskManager.API** — Camada de Comunicação

Responsável por:
*   Controllers
*   Mapeamento HTTP
*   Middlewares
*   Exposição do Swagger 

### **🧠 TaskManager.Application** — Casos de Uso e Regras de Negócio

Responsável por:
*   UseCases: Register, GetAll, GetById, Update, Delete
*   Validações
*   Exceptions personalizadas 
*   Serviços de aplicação
    
### **💬 TaskManager.Communication** — DTOs

Responsável por:
*   Requests (entrada)  
*   Responses (saída)
    
### **📦 TaskManager.Domain** — Entidades

Responsável por:
*   Modelos da regra de negócio 
*   A entidade TaskEntity

---

## 📂 Estrutura do Projeto 
```
TaskManager/
│
├── TaskManager.API/
│   ├── Controllers/
│   │   └── TaskController.cs
│   ├── Extensions/
│   │   └── ExceptionMiddlewareExtensions.cs
│   ├── Middleware/
│   │   └── ExceptionHandlingMiddleware.cs
│   ├── appsettings.json
│   └── Program.cs
│
├── TaskManager.Application/
│   ├── AppServices/
│   │   └── TaskAppService.cs
│   ├── Exceptions/
│   │   ├── AppException.cs
│   │   ├── ConflictException.cs
│   │   ├── NotFoundException.cs
│   │   └── ValidationException.cs
│   ├── UseCases/
│   │   ├── Register/
│   │   │   └── RegisterTaskUseCase.cs
│   │   ├── GetAll/
│   │   │   └── GetAllTasksUseCase.cs
│   │   ├── GetById/
│   │   │   └── GetTaskByIdUseCase.cs
│   │   ├── Update/
│   │   │   └── UpdateTaskUseCase.cs
│   │   └── Delete/
│   │       └── DeleteTaskByIdUseCase.cs
│   └── Validation/
│       ├── TaskValidator.cs
│       └── ValidationError.cs
│
├── TaskManager.Communication/
│   ├── Requests/
│   │   └── RequestTaskJson.cs
│   ├── Responses/
│   │   ├── ErrorResponse.cs
│   │   ├── ResponseShortTaskJson.cs
│   │   ├── ResponseTaskJson.cs
│   │   └── ResponseRegisteredTaskJson.cs
│
└── TaskManager.Domain/
    └── Entities/
        └── TaskEntity.cs

```

---

## 🔗 Endpoints

| Método     | Rota              | Descrição               |
| ---------- | ----------------- | ----------------------- |
| **POST**   | `/api/tasks`      | Criar nova tarefa       |
| **GET**    | `/api/tasks`      | Listar todas as tarefas |
| **GET**    | `/api/tasks/{id}` | Buscar tarefa pelo ID   |
| **PUT**    | `/api/tasks/{id}` | Atualizar tarefa        |
| **DELETE** | `/api/tasks/{id}` | Excluir tarefa          |

---

## 🔄 Status Codes

| Código                        | Uso                                         |
| ----------------------------- | ------------------------------------------- |
| **200 OK**                    | Consultas e atualizações                    |
| **201 Created**               | Criação de tarefas                          |
| **204 No Content**            | Exclusão                                    |
| **400 Bad Request**           | Dados inválidos                             |
| **404 Not Found**             | Tarefa não encontrada                       |
| **409 Conflict**              | Conflito (duplicidade)                      |
| **500 Internal Server Error** | Erro inesperado                             |

---

🛠️ Tecnologias Utilizadas
==========================

*   **.NET 8**    
*   **C#**    
*   **ASP.NET Web API**   
*   **Swagger / OpenAPI**  
*   **Arquitetura em Camadas (API / Application / Domain / Communication)**   
*   **Middleware de tratamento de erros**  
*   **Objetos de Request e Response para entrada e saída de dados**   
*   **Validações customizadas**
---

## 💻 Como Executar

1. Clone o repositório:

```bash
git clone https://github.com/LeonardoHMG/Rocketseat-BookstoreManagerAPI.git
```

2. Navegue até o diretório do projeto:

```bash
cd Rocketseat-BookstoreManagerAPI
```

3. Execute o projeto no terminal ou no Visual Studio:

```bash
dotnet run
```

---

## ✨ Contato

**Desenvolvedor:** Leonardo Henrique Martucci Gussi

**GitHub:** [https://github.com/LeonardoHMG](https://github.com/LeonardoHMG)


