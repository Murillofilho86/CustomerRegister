# Customer Register

Este projeto é uma aplicação web desenvolvida com Angular 16 e .NET 7.

## Pré-requisitos

- Node.js LTS ou superior
- Angular CLI
- .NET SDK 7.0 ou superior
- Docker (opcional)
- SQL Server

## Instalação

1. Clone o repositório:

    ```bash
    git clone https://github.com/seu-usuario/customer-register.git
    ```

2. Instale as dependências do Node.js:

    ```bash
    npm install
    ```

3. Instale as dependências do .NET:

    ```bash
    dotnet restore
    ```

4. Crie o banco de dados:

    ```sql
    CREATE DATABASE CustomerRegisterDB;
    GO
    ```

5. Execute os scripts para criar as tabelas:

    ```sql
    USE CustomerRegisterDB
    GO

    CREATE TABLE [dbo].[Address](
        [AddressId] [uniqueidentifier] NOT NULL,
        [Street] [nvarchar](100) NOT NULL,
        [Number] [nvarchar](10) NULL,
        [Complement] [nvarchar](20) NULL,
        [Neighborhood] [nvarchar](50) NULL,
        [City] [nvarchar](50) NOT NULL,
        [State] [nvarchar](50) NOT NULL,
        [ZipCode] [nvarchar](10) NOT NULL,
    PRIMARY KEY CLUSTERED 
    (
        [AddressId] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO

    CREATE TABLE [dbo].[Customer](
        [CustomerId] [uniqueidentifier] NOT NULL,
        [FirstName] [nvarchar](50) NOT NULL,
        [LastName] [nvarchar](50) NOT NULL,
        [Email] [nvarchar](100) NOT NULL,
        [Phone] [nvarchar](20) NOT NULL,
        [Cpf] [nvarchar](11) NULL,
        [CreatedAt] [datetime] NOT NULL,
        [ModifiedIn] [datetime] NULL,
        [AddressId] [uniqueidentifier] NULL,
        [RowVersion] [timestamp] NOT NULL,
    PRIMARY KEY CLUSTERED 
    (
        [CustomerId] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO

    ALTER TABLE [dbo].[Customer]  WITH CHECK ADD FOREIGN KEY([AddressId])
    REFERENCES [dbo].[Address] ([AddressId])
    GO
    ```

## Execução

1. Execute o backend:

    ```bash
    dotnet run
    ```

2. Anote a porta em que o backend está rodando.

3. Abra o arquivo `environment.ts` do Angular e substitua `{PORTA}` pela porta em que o backend está rodando.

4. Execute o Angular:

    ```bash
    ng serve
    ```

5. Acesse a aplicação em [http://localhost:4200](http://localhost:4200).

## Docker

Para executar a aplicação em container, execute o seguinte comando:

```bash
docker-compose up
