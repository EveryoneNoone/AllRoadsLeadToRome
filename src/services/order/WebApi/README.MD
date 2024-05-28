1 развернуть PostgreSQL в докер
    1.1 docker run --name AllRoadsLeadToRome.Order -p 5432:5432 -e POSTGRES_PASSWORD=123456 postgres:16.3
2 применить миграции
    2.1 в файле Program.cs закомментировать 33-36 строки, 38 раскомментировать
    2.2 открыть AllRoadsLeadToRome\src\services\order\Infrastructure в терминале и выполнить команду dotnet ef database update
    2.3 откатить шаг 2.1
3 развернуть RabbitMQ в докер
    3.1 запустить команду docker pull rabbitmq:management  
    3.2 запустить команду docker run -p 15672:15672 -p 5672:5672 rabbitmq:management 
    3.3 админская панель будет по адресу http://localhost:15672/#/


Для тестирования:
1. Запустить микросервис
2. Через сваггер создать заказ
3. Через свагер сменить ему статус на завершен
4. В отладчике можно отловить событие смены статуса в консьюмере OrderStatusChangedConsumer

