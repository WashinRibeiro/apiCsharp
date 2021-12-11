## _AV2 - DESIGN E PROGRAMAÇÃO ORIENTADA A OBJETOS <br /> 2021/2 - 4º Período - Ciência da Computação_ :computer::nerd_face:

#### Proposta de avaliação:
#####  Elaboração de uma API com CRUD em C# com todos os recursos aplicados durante o período:

- Parsers para contratos (DTOs); <br />
- Persistência; <br />
- Autenticação; <br />
- Log; <br />
- Tratamento de erro. <br />

---

#### Conceitos solicitados:

> A **Persistência** de dados é um meio para que um aplicativo persista e recupere informações de um sistema de armazenamento não volátil. A persistência é vital para os aplicativos corporativos por causa do acesso necessário aos bancos de dados relacionais.


---

### Como criamos do nosso projeto em API Web com o ASP.NET Core do zero :interrobang: 
##### Para criação do projeto abrimos o cmd na pasta que ficará todos os arquivos e rodamos os seguintes comandos:

1. dotnet new sln -n apiCsharp
2. dotnet new webapi -n ApiCsharp.Api -o src/Api
3. dotnet sln add src\Api\ApiCsharp.Api.csproj

##### Comando para criar arquivo "gitignore":
4. dotnet new gitignore

##### Comando para abertura do projeto no VS Code
5. code .
