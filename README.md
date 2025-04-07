# Desafio Técnico Senior

## Como iniciar o projeto

Execute o seguinte comando para iniciar o projeto com todas as dependências configuradas:

```bash
docker-compose up -d --build
```

## Sequência de inicialização

1. Subir o PostgreSQL.
2. Subir o Redis.
3. Subir o RabbitMQ.
4. Iniciar as filas.
5. Iniciar o backend.

## Instruções para executar

1. **Criar usuário admin**  
   Endpoint: `POST /api/User/create-admin`  
   Corpo da requisição:
   ```json
   {
     "email": "admin@example.com",
     "password": "admin123"
   }
   ```

2. **Criar revenda**  
   Endpoint: `POST /api/Reseller`  
   Corpo da requisição:
   ```json
   {
     "document": "123456789",
     "registredName": "Nome Registrado",
     "tradeName": "Nome Fantasia",
     "email": "revenda@example.com",
     "addresses": [
       {
         "postalCode": 12345,
         "country": "Brasil",
         "province": "SP",
         "city": "São Paulo",
         "street": "Rua Exemplo",
         "number": 100
       }
     ]
   }
   ```

3. **Criar produto**  
   Endpoint: `POST /api/Product/{resellerId}`  
   Corpo da requisição:
   ```json
   {
     "name": "Produto Exemplo",
     "description": "Descrição do produto",
     "price": 10.99
   }
   ```

4. **Fazer login na plataforma**  
   Endpoint: `POST /api/User/login`  
   Corpo da requisição:
   ```json
   {
     "email": "admin@example.com",
     "password": "admin123"
   }
   ```

5. **Fazer pedido para uma revenda**  
   Endpoint: `POST /api/Order/{resellerId}`  
   Corpo da requisição:
   ```json
   {
     "orderDetails": [
       {
         "productId": 1,
         "quantity": 2
       }
     ]
   }
   ```

## Documentação da API

A documentação completa da API pode ser encontrada no arquivo [`swaggerapi.json`](./swaggerapi.json).

## Tecnologias usadas

1. RabbitMQ
2. Redis
3. PostgreSQL
4. React/Next.js
5. Polly

## Observações

1. Um representante não pode ter dois produtos com o mesmo nome.

## Observabilidade e Monitoramento

Para monitorar os logs e o desempenho da aplicação, é possível integrar uma ferramenta de observabilidade no projeto. Recomendamos o uso de **Application Insights**.

### Configuração de Observabilidade

1. **Application Insights**  
   Caso esteja utilizando o Azure, é possível integrar o **Application Insights** para monitorar logs, métricas e rastreamento distribuído.

### Benefícios

- Identificação rápida de problemas.
- Monitoramento em tempo real do desempenho.
- Melhor rastreamento de erros e falhas.