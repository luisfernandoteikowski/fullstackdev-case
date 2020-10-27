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

### `cd src/GerenciadorEscolar.Api/`

Execute a aplicação

### `dotnet run`

Para acesso a documentação da API
Acesse no navegador https://localhost:5001/swagger

O frontend para este aplicativo está disponível [aqui](https://github.com/luisfernandoteikowski/fullstackdev-case-angularapp).

## Testes

Navegue até diretório

### `cd /src/GerenciadorEscolar.Test`

Execute a suite de testes

### `dotnet test`

## Executando a aplicação via Docker

### Pre-requisitos

1. Docker
2. Docker Compose

### Comandos

```sh
git clone https://github.com/luisfernandoteikowski/fullstackdev-case.git
cd src/GerenciadorEscolar.Api/
docker build -t gerenciadorescolar-api .
docker-compose up -d
```

- Feito isto a API da aplicação sobe em localhost porta 8095, caso queira alterar a porta, alterar o docker-compose.yml.
- Para acessar a documentaçao da API acesse: http://localhost:8095/swagger
- Para executar toda aplicação junto com o frontend utilize o Docker Compose do [frontend](https://github.com/luisfernandoteikowski/fullstackdev-case-angularapp).
