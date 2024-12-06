# ReserveAqui-API

## Apresentação da Aplicação

ReserveAqui é uma aplicação para gerenciar reservas de quartos de hotel. A aplicação permite que os usuários façam reservas, gerenciem seus dados pessoais e adicionem serviços adicionais às suas reservas.

## Funcionalidades

- Gerenciamento de hóspedes
- Gerenciamento de quartos
- Gerenciamento de reservas
- Adição de serviços adicionais às reservas
- Check-in e check-out de reservas

## Tecnologias Utilizadas

- C#
- ASP.NET Core
- Entity Framework Core

## Instruções para Execução da Aplicação

### Pré-requisitos

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Passos para Configuração

1. Clone o repositório:

    ```bash
    git clone https://github.com/ana-bmm0623/ReserveAqui-API.git
    cd ReserveAqui-API
    ```

### Endpoints da API

A seguir estão os principais endpoints disponíveis na aplicação:

#### Hóspedes

- `GET /reserveAqui/hospedes`: Retorna todos os hóspedes
- `GET /reserveAqui/hospedes/{id}`: Retorna um hóspede específico
- `POST /reserveAqui/hospedes`: Cria um novo hóspede
- `PUT /reserveAqui/hospedes/{id}`: Atualiza um hóspede existente
- `DELETE /reserveAqui/hospedes/{id}`: Exclui um hóspede

#### Quartos

- `GET /reserveAqui/quartos`: Retorna todos os quartos disponíveis
- `GET /reserveAqui/quartos/{id}`: Retorna um quarto específico
- `POST /reserveAqui/quartos`: Cria um novo quarto
- `PUT /reserveAqui/quartos/{id}`: Atualiza um quarto existente
- `DELETE /reserveAqui/quartos/{id}`: Exclui um quarto

#### Reservas

- `GET /reserveAqui/reserva`: Retorna todas as reservas
- `GET /reserveAqui/reserva/{id}`: Retorna uma reserva específica
- `POST /reserveAqui/reserva`: Cria uma nova reserva
- `PUT /reserveAqui/reserva/{id}`: Atualiza uma reserva existente
- `DELETE /reserveAqui/reserva/{id}`: Cancela uma reserva
- `POST /reserveAqui/reserva/{id}/check-in`: Realiza o check-in de uma reserva
- `POST /reserveAqui/reserva/{id}/check-out`: Realiza o check-out de uma reserva

#### Serviços Adicionais

- `GET /reserveAqui/servicos-adicionais`: Retorna todos os serviços adicionais
- `GET /reserveAqui/servicos-adicionais/{id}`: Retorna um serviço adicional específico
- `POST /reserveAqui/servicos-adicionais`: Cria um novo serviço adicional
- `PUT /reserveAqui/servicos-adicionais/{id}`: Atualiza um serviço adicional existente
- `DELETE /reserveAqui/servicos-adicionais/{id}`: Exclui um serviço adicional
- `POST /reserveAqui/servicos-adicionais/{servicoId}/add-to-reserva/{reservaId}`: Adiciona um serviço adicional a uma reserva
