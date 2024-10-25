# Calculadora de CDB

- Sistema para calculo de CDB (Certificado de Depósito Bancário). <br />
- Desenvolvido usando Docker, Angular e .Net 8. <br />
- Arquitetura backend baseada em [Clean Architecture](https://www.amazon.com/Clean-Architecture-Craftsmans-Software-Structure/dp/0134494164) <br />
- Tests unitários <br />
- Tests funcionais (Specflow ainda não suporta .net 8)

## Patterns utilizados

- Fail fast validation <br />
- Dependency Injection <br />
- Mediator <br />
- Command <br />
- Retry Policy <br />
- Circuit Breaker (A utilizar) <br />
- Repository (A utilizar)

## Como rodar o projeto

Você pode rodar o projeto direto da sua máquina.

> Pré-requisitos

1 - [Node.js](https://nodejs.org/en/download/package-manager/) superior a 12 instalada. <br />
2 - [Docker](https://www.docker.com/products/docker-desktop/) <br/>
3 - [.Net 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)

> Rodar

1 - Abra o prompt de comando e faça o clone do projeto no github <br />
```sh
git clone https://github.com/ulissesxavier2015/app-calculo-cbd.git
```

2 - Acesse a pasta do projeto <br />
```sh
cd app-calculo-cdb
```

3 - Digite o comando <br />
```sh
docker compose up -d --build 
```

4 - Acesse a aplicação através do endereço http://localhost:8090 
