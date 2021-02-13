# N-Store 
## N-Store is a e-commerce with didactic purposes, implementing a distributed architecture.

# Technologies used 
* .NET 5.0
* IdentityCore
* EntityFrameworkCore
* Jwt
* Polly
* Refit
* MediatR
* RabbitMQ
* EasyNetMQ



## Run command to install and run container RabbitMQ 

`
	docker run -d --hostname rabbit-host --name rabbit-nstore -p 15672:15672 -p 5672:5672 rabbitmq:management
`
