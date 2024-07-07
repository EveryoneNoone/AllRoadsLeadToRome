db.createUser(
    {
        user: "admin",
        pwd: "password",
        roles: [
            {
                role: "readWrite",
                db: "UserDB"
            }
        ]
    }
);
db.createCollection("Notifications");
db.createCollection("Templates");

db.Templates.insert({
    "Name": "Welcome",
    "Value": "{name} добро пожаловать в сервис",
    "TemplateType": 1
});

db.Templates.insert({
    "Name": "Welcome",
    "Value": "{name} добро пожаловать в сервис 'все дороги ведут в Рим'. Наслаждайтесь доставками",
    "TemplateType": 2
});

db.Templates.insert({
    "Name": "Welcome",
    "Value": "Добро пожаловать",
    "TemplateType": 3
});


db.Templates.insert({
    "Name": "CreateOrder",
    "Value": "Заказ создан {OrderId}",
    "TemplateType": 1
});

db.Templates.insert({
    "Name": "CreateOrder",
    "Value": "Заказ на доставку под номером {OrderId} создан",
    "TemplateType": 2
});

db.Templates.insert({
    "Name": "CreateOrder",
    "Value": "Заказ с номером {OrderId} создан",
    "TemplateType": 3
});