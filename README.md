Код создания бд в postgresql
-- Создание таблицы CargoTypes
CREATE TABLE public."CargoTypes" (
    "CargoTypeID" SERIAL PRIMARY KEY,
    "TypeName" VARCHAR(50),
    "Description" VARCHAR(200)
);

-- Создание таблицы Clients
CREATE TABLE public."Clients" (
    "ClientID" SERIAL PRIMARY KEY,
    "FirstName" VARCHAR(50),
    "LastName" VARCHAR(50),
    "Phone" VARCHAR(20),
    "Email" VARCHAR(100),
    "Discount" NUMERIC(5,2) DEFAULT 0
);

-- Создание таблицы Couriers
CREATE TABLE public."Couriers" (
    "CourierID" SERIAL PRIMARY KEY,
    "FirstName" VARCHAR(50),
    "LastName" VARCHAR(50),
    "Phone" VARCHAR(20)
);

-- Создание таблицы Deliveries
CREATE TABLE public."Deliveries" (
    "DeliveryID" SERIAL PRIMARY KEY,
    "OrderID" INTEGER,
    "DeliveryDate" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "TotalPrice" NUMERIC(10,2)
);

-- Создание таблицы Orders
CREATE TABLE public."Orders" (
    "OrderID" SERIAL PRIMARY KEY,
    "ClientID" INTEGER,
    "OrderDate" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CargoDescription" VARCHAR(200),
    "CargoTypeID" INTEGER,
    "BasePrice" NUMERIC(10,2),
    "CourierID" INTEGER,
    "TransportID" INTEGER
);

-- Создание таблицы Transports
CREATE TABLE public."Transports" (
    "TransportID" SERIAL PRIMARY KEY,
    "Type" VARCHAR(50),
    "LicensePlate" VARCHAR(20)
);

-- Создание таблицы миграций для EF Core
CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" VARCHAR(150) NOT NULL,
    "ProductVersion" VARCHAR(32) NOT NULL,
    PRIMARY KEY ("MigrationId")
);

-- Вставка данных в таблицу CargoTypes
INSERT INTO public."CargoTypes" ("TypeName", "Description") VALUES
('Электроника', 'Устройства и компоненты электроники'),
('Одежда', 'Текстильные изделия'),
('Продукты питания', 'Съестные продукты и напитки'),
('Книги', 'Печатные и электронные издания'),
('Мебель', 'Предметы интерьера'),
('Спортивные товары', 'Товары для спорта и активного отдыха'),
('Косметика', 'Косметические средства'),
('Техника', 'Технические устройства');

-- Вставка данных в таблицу Clients
INSERT INTO public."Clients" ("FirstName", "LastName", "Phone", "Email", "Discount") VALUES
('Иван', 'Иванов', '1234567890', 'ivan@example.com', 0.00),
('Мария', 'Петрова', '0987654321', 'maria@example.com', 5.00),
('Сергей', 'Сидоров', '5555555555', 'sergey@example.com', 10.00),
('Анна', 'Кузнецова', '4444444444', 'anna@example.com', 0.00),
('Ольга', 'Михайлова', '3333333333', 'olga@example.com', 15.00);

-- Вставка данных в таблицу Couriers
INSERT INTO public."Couriers" ("FirstName", "LastName", "Phone") VALUES
('Алексей', 'Алексеев', '6666666666'),
('Елена', 'Серова', '7777777777'),
('Дмитрий', 'Дмитриев', '8888888888'),
('Игорь', 'Игорев', '9999999999');

-- Вставка данных в таблицу Transports
INSERT INTO public."Transports" ("Type", "LicensePlate") VALUES
('Грузовик', 'А123БВ'),
('Легковое авто', 'Е456ГД'),
('Фургон', 'И789КЛ');

-- Вставка данных в таблицу Orders
INSERT INTO public."Orders" ("ClientID", "OrderDate", "CargoDescription", "CargoTypeID", "BasePrice", "CourierID", "TransportID") VALUES
(1, '2024-10-01 10:00:00', 'Смартфон', 1, 15000.00, 1, 1),
(2, '2024-10-01 11:00:00', 'Куртка', 2, 3000.00, 2, 2),
(3, '2024-10-01 12:00:00', 'Хлеб', 3, 100.00, 3, 3);

-- Вставка данных в таблицу Deliveries
INSERT INTO public."Deliveries" ("OrderID", "DeliveryDate", "TotalPrice") VALUES
(1, '2024-10-02 09:00:00', 15000.00),
(2, '2024-10-02 09:30:00', 3000.00),
(3, '2024-10-02 10:00:00', 100.00);
