CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

CREATE TABLE `pay rates` (
    `idPay Rates` int NOT NULL AUTO_INCREMENT,
    `Pay Rate Name` varchar(40) NOT NULL,
    `Value` decimal(10,0) NOT NULL,
    `Tax Percentage` decimal(10,0) NOT NULL,
    `Pay Type` int NOT NULL,
    `Pay Amount` decimal(10,0) NOT NULL,
    `PT - Level C` decimal(10,0) NOT NULL,
    PRIMARY KEY (`idPay Rates`)
);

CREATE TABLE `employee` (
    `Employee Number` int unsigned NOT NULL AUTO_INCREMENT,
    `idEmployee` int NOT NULL,
    `Last Name` varchar(45) NOT NULL,
    `First Name` varchar(45) NOT NULL,
    `SSN` decimal(10,0) NOT NULL,
    `Pay Rate` varchar(40) NULL,
    `Pay Rates_idPay Rates` int NOT NULL,
    `Vacation Days` int NULL,
    `Paid To Date` decimal(2,0) NULL,
    `Paid Last Year` decimal(2,0) NULL,
    PRIMARY KEY (`Employee Number`),
    CONSTRAINT `FK_employee_pay rates_Pay Rates_idPay Rates` FOREIGN KEY (`Pay Rates_idPay Rates`) REFERENCES `pay rates` (`idPay Rates`) ON DELETE CASCADE
);

CREATE TABLE `birthday` (
    `employeeNumber` int unsigned NOT NULL,
    `dateofbirthday` date NULL,
    PRIMARY KEY (`employeeNumber`),
    CONSTRAINT `FK_birthday_employee_employeeNumber` FOREIGN KEY (`employeeNumber`) REFERENCES `employee` (`Employee Number`) ON DELETE CASCADE
);

CREATE TABLE `detail vacation` (
    `employeeNumber` int unsigned NOT NULL,
    `dayoff` date NULL,
    `resignation content` varchar(500) NULL,
    PRIMARY KEY (`employeeNumber`),
    CONSTRAINT `FK_detail vacation_employee_employeeNumber` FOREIGN KEY (`employeeNumber`) REFERENCES `employee` (`Employee Number`) ON DELETE CASCADE
);

CREATE UNIQUE INDEX `Employee Number_UNIQUE` ON `employee` (`Employee Number`);

CREATE INDEX `fk_Employee_Pay Rates` ON `employee` (`Pay Rates_idPay Rates`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240325153147_v0', '5.0.17');

COMMIT;

