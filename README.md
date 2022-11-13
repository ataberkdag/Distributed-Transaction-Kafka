# Distributed-Transaction-Kafka

.Net 6 Distributed Transaction sample with Kafka, Consul and Ocelot Gateway.

## Services

### Gateway

- Redirects Http calls to Microservices with Ocelot and Consul. (Service Discovery)

![alt text](https://github.com/ataberkdag/Distributed-Transaction-Kafka/blob/master/images/Consul.png?raw=true)

##### Implementations

- Ocelot
- Consul

### Order

- Creates OrderPlaced event. (OutboxWorker sends event.)
- Consumes StockDecreased and StockFailed events.

##### Implementations

- Consul implementation.
- Confluent Kafka implementation.
- CQRS implementation.
- Repository and UnitOfWork pattern implementations.

### Stock

- Consumes OrderPlaced event.
- Creates StockDecreased and StockFailed events. (OutboxWorker sends event.)

##### Implementations

- Consul implementation.
- Confluent Kafka implementation.
- CQRS implementation.
- Repository and UnitOfWork pattern implementations.

### OutboxWorker

- Reads OutboxTables of Order and Stock Microservices.
- Sends events.

##### Implementations

- Confluent Kafka implementation.
- Dapper implementation.


## Run with Docker (Does not include Order Service, Stock Service, Gateway, OutboxWorker)

```bash
docker-compose -f docker-compose.yml up -d
```

NOTE: Enable "Auto Topic Create" for Kafka if you use different docker-compose.yml. Sample docker-compose.yml includes "Auto Topic Create" for Kafka. (TOPIC_AUTO_CREATE: 1)

## Migration

To apply migrations follow this command on Package Manager Console for Order and Stock Microservices. (Set starting project to API and set default project to Infrastructure on Package Manager Console)

```bash
update-database
```
