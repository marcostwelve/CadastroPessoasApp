<h1 align="center">CADASTRO DE PESSOAS</h1>


<img width="886" height="592" alt="image" src="https://github.com/user-attachments/assets/2a43c0b3-579e-4e6a-a2be-eefdc08794c8" />

 ## Cadastro é um projeto full-stack para cadastro de pessoas.

## 🔥 Introdução

APP foi criação com .net versão 8 e React versão 19

### ⚙️ Pré-requisitos
* .Net Core versão 8.0 [.Net Core 8.0 Download](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
* Entity Framework Core versão 8.0 [Documentação](https://learn.microsoft.com/pt-br/ef/)
* Visual studio 2022, ou IDE que tenha suporte ao .Net 8.0 [Visual Studio 2022 Download](https://visualstudio.microsoft.com/pt-br/downloads/)
* Sql Server versão 2022 [Sql Server Download](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
* Sql Server Management Studio (SSMS) [SSMS Download](https://learn.microsoft.com/pt-br/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
* React [Documentação] ([Learn React](https://react.dev/learn))
* Swagger [Documentação] ([Swagger](https://swagger.io/))


### 🔨 Guia de instalação

Para utilizar este projeto, necessário instalar o Entity Framework, e configurar o banco de dados no arquivo appsettings.Development.json, e instalar as migrations para conexão com o banco de dados

Etapas para instalar:

```bash
dotnet tool install --global dotnet-ef
```
Passo 2:
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```
Passo 3:
```bash
Install-Package Microsoft.EntityFrameworkCore.Design
```
Passo 4:
```bash
dotnet-ef migrations add (Nome da migration do projeto)
```

Passo 5:
```bash
dotnet-ef database update
```

# Executando a Aplicação 🌐

Para executar a aplicação App, digite o seguinte comando

```bash
npm start
```
Acessar o link http://localhost:3000 (Caso o navegador não abra automáticamente)

Para executar a aplicação back-end, digite o seguinte comando

```bash
dotnet run
```

# Endpoins 🚨

A API possui duas versões, e também autenticação e autorização Jwt para acesso aos endpoints
A API funciona, com os métodos HTTP GET, POST, PUT e DELETE.

## V1
<img width="1890" height="923" alt="image" src="https://github.com/user-attachments/assets/e1b59ca6-8292-4c0a-bee4-1c0ffb785e15" />

##V2
<img width="1898" height="918" alt="image" src="https://github.com/user-attachments/assets/8e09efeb-ec22-4125-bfce-098302d844ba" />


# Testes 👁️‍🗨️

Para testes na aplicação Back-End, foi utilizado a biblioteca XUnit.
<img width="886" height="394" alt="image" src="https://github.com/user-attachments/assets/db887f1a-701f-4167-bf7a-380eb7072878" />


