
<h1> FullStack Developer Case - Backend</h1>

Este é uma aplicação REST API para um simples sistema de controle de escolas e suas turmas 

## Requerimentos
- Postgresql
- Dotnet core
- Asp net core 3.1

## Configurações
Este projeto utiliza banco de dados PostgreSQL, para criar as tabelas necessárias siga os seguintes passos.
- Informe Connection string em `src/GerenciadorEscolar.Api/appsettings.Development.json`
- Navegue até o diretório `src/GerenciadorEscolar.Api/`
- Execute migrations `dotnet ef database update`. Alternativamente, você pode executar a migração manualmente executando o arquivo SQL `src/atualizar_para_ultimaversao.sql` diretamente no banco de dados.

## Executando a aplicação
Navegue até o diretório
### `src/GerenciadorEscolar.Api/`
Execute a aplicação
### `dotnet run`
O frontend para este aplicativo está disponível [aqui](https://github.com/luisfernandoteikowski/fullstackdev-case-angularapp).

## Testes
Navegue até diretório
### `cd /src/GerenciadorEscolar.Test`
Execute a suite de testes
### `dotnet test`