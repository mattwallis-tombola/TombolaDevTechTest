DROP DATABASE IF EXISTS coffeebeans;

CREATE DATABASE IF NOT EXISTS coffeebeans;
USE coffeebeans;

CREATE TABLE IF NOT EXISTS Countries (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);

CREATE TABLE IF NOT EXISTS Colours (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    UNIQUE INDEX (Name)
);

CREATE TABLE IF NOT EXISTS CoffeeBeans (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    BeanId VARCHAR(50) NOT NULL,
    Name VARCHAR(100) NOT NULL,
    ColourId INT NOT NULL,
    Description TEXT NOT NULL,
    Cost DECIMAL(10, 2) NOT NULL,
    ImageUrl VARCHAR(255) NOT NULL,
    CountryId INT NOT NULL,
    FOREIGN KEY (CountryId) REFERENCES Countries(Id),
    FOREIGN KEY (ColourId) REFERENCES Colours(Id),
    UNIQUE INDEX (BeanId)
);

CREATE TABLE IF NOT EXISTS BeanOfTheDayHistory (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    DateStamp DATE NOT NULL,
    BeanId VARCHAR(50) NOT NULL,
    FOREIGN KEY (BeanId) REFERENCES CoffeeBeans(BeanId),
    UNIQUE INDEX (DateStamp)
);

INSERT INTO Countries (Name) VALUES 
('Peru'),
('Vietnam'),
('Colombia'),
('Brazil'),
('Honduras');

INSERT INTO Colours (Name) VALUES
('dark roast'),
('medium roast'),
('light roast'),
('green'),
('golden');

INSERT INTO CoffeeBeans (BeanId, Name, ColourId, Description, Cost, ImageUrl, CountryId)
VALUES 
('66a374596122a40616cb8599', 'TURNABOUT', (SELECT Id FROM Colours WHERE Name = 'dark roast'), 'Ipsum cupidatat nisi do elit veniam Lorem magna. Ullamco qui exercitation fugiat pariatur sunt dolore Lorem magna magna pariatur minim. Officia amet incididunt ad proident. Dolore est irure ex fugiat. Voluptate sunt qui ut irure commodo excepteur enim labore incididunt quis duis. Velit anim amet tempor ut labore sint deserunt.', 39.26, 'https://images.unsplash.com/photo-1672306319681-7b6d7ef349cf', (SELECT Id FROM Countries WHERE Name = 'Peru')),
('66a374591a995a2b48761408', 'ISONUS', (SELECT Id FROM Colours WHERE Name = 'golden'), 'Dolor fugiat duis dolore ut occaecat. Excepteur nostrud velit aute dolore sint labore do eu amet. Anim adipisicing quis ut excepteur tempor magna reprehenderit non ut excepteur minim. Anim dolore eiusmod nisi nulla aliquip aliqua occaecat.', 18.57, 'https://images.unsplash.com/photo-1641399756770-9b0b104e67c1', (SELECT Id FROM Countries WHERE Name = 'Vietnam')),
('66a374593ae6cb5148781b9b', 'ZILLAN', (SELECT Id FROM Colours WHERE Name = 'green'), 'Cillum nostrud mollit non ad dolore ad dolore veniam. Adipisicing anim commodo fugiat aute commodo occaecat officia id officia ullamco. Dolore irure magna aliqua fugiat incididunt ullamco ea. Aliqua eu pariatur cupidatat ut.', 33.87, 'https://images.unsplash.com/photo-1522809269485-981d0c303355', (SELECT Id FROM Countries WHERE Name = 'Colombia')),
('66a37459771606d916a226ff', 'RONBERT', (SELECT Id FROM Colours WHERE Name = 'dark roast'), 'Et deserunt nisi in anim cillum sint voluptate proident. Est occaecat id cupidatat cupidatat ex veniam irure veniam pariatur excepteur duis labore occaecat amet. Culpa adipisicing nisi esse consequat adipisicing anim. Fugiat tempor enim ullamco sint anim qui enim. Voluptate duis proident reprehenderit et duis nisi. In consectetur nisi eu cupidatat voluptate ullamco nulla esse cupidatat dolore sit. Cupidatat laboris adipisicing ullamco mollit culpa cupidatat ex laborum consectetur consectetur.', 17.69, 'https://images.unsplash.com/photo-1598198192305-46b0805890d3', (SELECT Id FROM Countries WHERE Name = 'Brazil')),
('66a3745945fcae53593c42e7', 'EARWAX', (SELECT Id FROM Colours WHERE Name = 'green'), 'Labore veniam amet ipsum eu dolor. Aliquip Lorem et eiusmod exercitation. Amet ex eu deserunt labore est ex consectetur ut fugiat. Duis veniam voluptate elit consequat tempor nostrud enim mollit occaecat.', 26.53, 'https://images.unsplash.com/photo-1512568400610-62da28bc8a13', (SELECT Id FROM Countries WHERE Name = 'Vietnam')),
('66a37459591e872ce11c3b41', 'EVENTEX', (SELECT Id FROM Colours WHERE Name = 'light roast'), 'Reprehenderit est laboris tempor quis exercitation laboris. Aute nulla aliqua consectetur nostrud ullamco cupidatat do cillum amet reprehenderit mollit non voluptate. Deserunt consectetur reprehenderit nostrud enim proident ea. Quis quis voluptate ex dolore non reprehenderit minim veniam nisi aute do incididunt voluptate. Duis aliquip commodo cupidatat anim ut ullamco eiusmod culpa velit incididunt.', 36.56, 'https://images.unsplash.com/photo-1692299108834-038511803008', (SELECT Id FROM Countries WHERE Name = 'Vietnam')),
('66a374599018ca32d01fee66', 'NITRACYR', (SELECT Id FROM Colours WHERE Name = 'green'), 'Mollit deserunt tempor qui consectetur excepteur non. Laborum voluptate voluptate laborum non magna et. Ea velit ipsum labore occaecat ea do cupidatat duis adipisicing. Ut eiusmod dolor anim et ea ea. Aliquip mollit aliqua nisi velit consequat nisi. Laborum velit anim non incididunt non qui commodo. Ea voluptate dolore pariatur eu enim.', 22.92, 'https://images.unsplash.com/photo-1692296115158-38194aafa7df', (SELECT Id FROM Countries WHERE Name = 'Brazil')),
('66a37459cca42ce9e15676a3', 'PARAGONIA', (SELECT Id FROM Colours WHERE Name = 'medium roast'), 'Veniam laborum consequat minim laborum mollit id ea Lorem in. Labore aliqua dolore quis sunt aliquip commodo aute excepteur. Voluptate tempor consequat pariatur do esse consectetur sunt ut mollit magna enim.', 37.91, 'https://images.unsplash.com/photo-1522120378538-41fb9564bc75', (SELECT Id FROM Countries WHERE Name = 'Colombia')),
('66a374590abf949489fb28f7', 'XLEEN', (SELECT Id FROM Colours WHERE Name = 'golden'), 'Commodo veniam voluptate elit reprehenderit incididunt. Ut laboris dolor sint cupidatat ut adipisicing. Nostrud magna labore voluptate commodo in sunt proident sunt deserunt dolor ullamco officia tempor dolor. Laboris exercitation est mollit eiusmod nostrud. Sit qui ullamco minim cillum officia irure cillum tempor eu. Et cupidatat proident amet dolore non minim.', 17.59, 'https://images.unsplash.com/photo-1442550528053-c431ecb55509', (SELECT Id FROM Countries WHERE Name = 'Colombia')),
('66a374593a88b14d9fff0e2e', 'LOCAZONE', (SELECT Id FROM Colours WHERE Name = 'green'), 'Deserunt consequat ea incididunt aliquip. Occaecat excepteur minim occaecat aute amet adipisicing. Tempor id veniam ipsum et tempor pariatur anim elit laboris commodo mollit. Ipsum incididunt Lorem veniam id fugiat incididunt consequat est et. Id deserunt eiusmod esse duis cupidatat Lorem. Ullamco Lorem ullamco cupidatat nostrud amet id minim ut voluptate adipisicing ipsum. Fugiat reprehenderit laborum proident eiusmod esse sint adipisicing fugiat ex.', 25.49, 'https://images.unsplash.com/photo-1549420751-ea3f7ab42006', (SELECT Id FROM Countries WHERE Name = 'Vietnam')),
('66a37459b7933d86991ce243', 'ZYTRAC', (SELECT Id FROM Colours WHERE Name = 'light roast'), 'Qui deserunt adipisicing nulla ad enim commodo reprehenderit id veniam consequat ut do ea officia. Incididunt ex esse cupidatat consequat. Sit incididunt ex magna sint reprehenderit id minim non.', 10.27, 'https://images.unsplash.com/photo-1508690207469-5c5aebedf76d', (SELECT Id FROM Countries WHERE Name = 'Vietnam')),
('66a374592169e1bfcca2fb1c', 'FUTURIS', (SELECT Id FROM Colours WHERE Name = 'medium roast'), 'Incididunt exercitation mollit duis consectetur consequat duis culpa tempor. Fugiat nisi fugiat dolore irure in. Fugiat nulla amet dolore labore laboris sint laborum pariatur commodo amet. Ut velit sit proident fugiat cillum cupidatat ea.', 16.44, 'https://images.unsplash.com/photo-1694763768576-0c7c3af6a4d8', (SELECT Id FROM Countries WHERE Name = 'Colombia')),
('66a37459cc0f1fb1d1a24cf0', 'KLUGGER', (SELECT Id FROM Colours WHERE Name = 'green'), 'Pariatur qui Lorem sunt labore Lorem nulla nulla ea excepteur Lorem cillum amet. Amet ea officia incididunt culpa non. Do reprehenderit qui eiusmod dolore est deserunt labore do et dolore eiusmod quis elit.', 32.77, 'https://images.unsplash.com/photo-1692299108333-471157a30882', (SELECT Id FROM Countries WHERE Name = 'Peru')),
('66a37459caf60416d0571db4', 'ZANITY', (SELECT Id FROM Colours WHERE Name = 'dark roast'), 'Velit quis veniam velit et sint. Irure excepteur officia ipsum sint. Est ipsum pariatur exercitation voluptate commodo. Ex irure commodo exercitation labore nulla qui dolore ad quis.', 19.07, 'https://images.unsplash.com/photo-1673208127664-23a2f3b27921', (SELECT Id FROM Countries WHERE Name = 'Honduras')),
('66a3745997fa4069ce1b418f', 'XEREX', (SELECT Id FROM Colours WHERE Name = 'green'), 'Esse ad eiusmod eiusmod nisi cillum magna quis non voluptate nulla est labore in sunt. Magna aliqua pariatur commodo deserunt. Pariatur pariatur pariatur id excepteur ex elit veniam.', 29.42, 'https://images.unsplash.com/photo-1544486864-3087e2e20d91', (SELECT Id FROM Countries WHERE Name = 'Brazil'));

INSERT INTO BeanOfTheDayHistory (DateStamp, BeanId)
VALUES
(CURDATE(), '66a374596122a40616cb8599');

-- Grant priviliges to admin user and flush grant table to ensure privileges have been applied
GRANT ALL PRIVILEGES ON coffeebeans.* TO 'admin'@'%';
FLUSH PRIVILEGES;

SHOW DATABASES;
