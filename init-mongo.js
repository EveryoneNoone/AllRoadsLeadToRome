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
    "Value": "{name} ����� ���������� � ������",
    "TemplateType": 1
});

db.Templates.insert({
    "Name": "Welcome",
    "Value": "{name} ����� ���������� � ������ '��� ������ ����� � ���'. ������������� ����������",
    "TemplateType": 2
});

db.Templates.insert({
    "Name": "Welcome",
    "Value": "����� ����������",
    "TemplateType": 3
});


db.Templates.insert({
    "Name": "CreateOrder",
    "Value": "����� ������ {OrderId}",
    "TemplateType": 1
});

db.Templates.insert({
    "Name": "CreateOrder",
    "Value": "����� �� �������� ��� ������� {OrderId} ������",
    "TemplateType": 2
});

db.Templates.insert({
    "Name": "CreateOrder",
    "Value": "����� � ������� {OrderId} ������",
    "TemplateType": 3
});