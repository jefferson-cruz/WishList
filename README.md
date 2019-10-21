# WishList (aplicação de lista de desejos)

Uma aplicação .NET Core 2.2, que utiliza conceitos como Domain Driven Design e CQRS. 

Tecnologias utilizadas:
 - .NET Core 2.2
 - SQL Server 2017 Linux
 - ElasticSearch 7.2.0
 - AspNetCoreRateLimit (Nuget)
 - Docker Compose

## Instalação

Para facilitar, você pode pode rodar o comando ```docker-compose``` do Docker para subir o SQL Server e o ElasticSearch. Caso você não tenha o Docker, instale os componentes separadamente e no arquivo ```appsettings.json``` edite as ```ConnectionStrings``` WishList e WishListIndex (SQL Server e ElasticSearch respectivamente)

```bash
docker-compose up
```

## Uso

Execute o projeto WishList no Visual Studio ou Visual Studio Code. E utilize o Postman ou outro parecido para interagir com a API.

A aplicação possui os seguintes endpoints:
- api/users: [GET|POST]
- api/products: [GET|POST]
- api/wishes: [GET|POST|DELETE]

Todos os métodos GET possuem paginaçãoo (exemplos):
- api/users?page=1&pageSize=3
- api/products?page=1&pageSize=3
- api/wishes?page=1&pageSize=3


## Links
https://github.com/stefanprodan/AspNetCoreRateLimit

https://www.elastic.co/pt/products/elasticsearch

https://www.microsoft.com/en-us/sql-server/sql-server-2017

## License
[MIT](https://choosealicense.com/licenses/mit/)
