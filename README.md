﻿PERGUNTAS: 

1) EXPLIQUE COM SUAS PALAVRAS O QUE É DOMAIN DRIVEN DESIGN E SUA IMPORTÂNCIA NA ESTRATÉGIA DE DESENVOLVIMENTO DE SOFTWARE.

Domain Driven Designer é uma metodologia para modelagem de sistemas, que visa aproximar o desenvolvimento de software a linguagem do usuário se utilizando de uma linguagem única nas comunicaçãoes entre todos os papeis( desenvolvendor, gerente de projeto, usuário, QA). Dessa forma fica fácil de validar as regras de négócio e os casos de uso no código pois a arquitetura gerada se espelha na organização da empresa.
  
2) EXPLIQUE COM SUAS PALAVRAS O QUE É E COMO FUNCIONA UMA ARQUITETURA BASEADA EM MICROSERVICES. EXPLIQUE GANHOS COM ESTE MODELO E DESAFIOS EM SUA IMPLEMENTAÇÃO. 

Arquitetura baseada em microservices, é uma arquitetura onde são construidos varios sistemas (serviços) de forma que cada um trata um único domínio e seus casos de usos. Como grande vantagem temos o isolamento dos serviços que aumenta a resiliência, a flexibilidade e a liberdade da equipe que da manutenção aos serviços, pois alterações feitas não geram impactos em outros serviços. O grande desafio neste modelo é manter a consistência dos dados quando serviços compartilham algum tipo de informação complementar, pois gera por algum intervalo uma inconsistência eventual. Com isso os serviços acabam ficando mais complexos, dificultando a vida da equipe na hora corrigir bugs e de testar os serviços.
 
3) EXPLIQUE QUAL A DIFERENÇA ENTRE COMUNICAÇÃO SINCRONA E ASSINCRONA E QUAL O MELHOR CENÁRIO PARA UTILIZAR UMA OU OUTRA. 

Comunicação sincrona - é uma forma de chamada de execução de código, onde o código chamador aguarda a execução até o final do código chamado para da continuidade a sua rotina. Cenários para utilização dessa forma de comunicação são em casos de uso onde o código chamador depende da resposta do código chamado para dar continuidade a sua rotina, casos como: validação de dados para criação de registro em banco, autenticação de pessoas, autorização de operãções, etc.

Comunicação assincrona - é uma forma de chamada de execução de código, onde o código chamador não aguarda a execução até o final do código chamado para da continuidade a sua rotina. Cenários para utilização dessa forma de comunicação são em casos de uso onde o código chamador não depende da resposta do código chamado para dar continuidade a sua rotina, casos como: notificação de alertas, disparo de eventos, etc.

Arquivo para o post

```json
{
  "origin": {
    "bank": {
      "id": "123",
      "name": "Bank"
    },
    "agency": {
      "number": "5678",
      "validador": "9"
    },
    "accountNumber": {
      "number": "1234",
      "validador": "5"
    },
    "owner": {
      "id": {
        "number": "123456789",
        "validador": "11"
      },
      "name": "Joao",
      "surName": "Silva"
    }
  },
  "destiny": {
    "bank": {
      "id": "123",
      "name": "Bank"
    },
    "agency": {
      "number": "6789",
      "validador": "0"
    },
    "accountNumber": {
      "number": "2345",
      "validador": "6"
    },
    "owner": {
      "id": {
        "number": "234567890",
        "validador": "22"
      },
      "name": "Joao",
      "surName": "Silva"
    }
  },
  "ammount": {
    "total": 500,
    "currency": "BRL"
  }
}
```
