# WishList (aplicação de lista de desejos)

Uma aplicação .NET Core 2.2, que utiliza conceitos como Domain Driven Design e CQRS. 

Tecnologias utilizadas:
 - .NET Core 2.2
 - SQL Server 2017 Linux
 - ElasticSearch 7.2.0
 - Redis
 - AspNetCoreRateLimit (Nuget)
 - Docker Compose

## Uso

Rode o comando ```docker-compose up``` para que os containers fiquem online e a aplicação funcione. Para parar a execução dos containers execute o comando ```docker-compose down```. Se quiser listar os containers disponíveis, execute o comando ```docker-compose ps```. Se quiser ver o log de execução dos containers, execute o comando ```docker-compose logs -f -t```

Defina o arquivo docker-compose no Visual Studio como principal e execute debugando. No VS Code é necessário instalar a extenção do Docker e configurar o ambiente para executar o debugger.

Abra no Postman ou semelhante e digite o endereço base:
http://localhost:15000/api/

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
