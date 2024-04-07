CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `pay rates` (
    `idPay Rates` int NOT NULL AUTO_INCREMENT,
    `Pay Rate Name` varchar(40) CHARACTER SET utf8mb4 NOT NULL,
    `Value` decimal(10,0) NOT NULL,
    `Tax Percentage` decimal(10,0) NOT NULL,
    `Pay Type` int NOT NULL,
    `Pay Amount` decimal(10,0) NOT NULL,
    `PT - Level C` decimal(10,0) NOT NULL,
    CONSTRAINT `PK_pay rates` PRIMARY KEY (`idPay Rates`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `employee` (
    `Employee Number` int unsigned NOT NULL AUTO_INCREMENT,
    `idEmployee` int NOT NULL,
    `Last Name` varchar(45) CHARACTER SET utf8mb4 NOT NULL,
    `First Name` varchar(45) CHARACTER SET utf8mb4 NOT NULL,
    `SSN` decimal(10,0) NOT NULL,
    `Pay Rate` varchar(40) CHARACTER SET utf8mb4 NULL,
    `Pay Rates_idPay Rates` int NOT NULL,
    `Vacation Days` int NULL,
    `Paid To Date` decimal(2,0) NULL,
    `Paid Last Year` decimal(2,0) NULL,
    CONSTRAINT `PK_employee` PRIMARY KEY (`Employee Number`),
    CONSTRAINT `FK_employee_pay rates_Pay Rates_idPay Rates` FOREIGN KEY (`Pay Rates_idPay Rates`) REFERENCES `pay rates` (`idPay Rates`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `birthday` (
    `employeeNumber` int unsigned NOT NULL,
    `dateofbirthday` date NULL,
    CONSTRAINT `PK_birthday` PRIMARY KEY (`employeeNumber`),
    CONSTRAINT `FK_birthday_employee_employeeNumber` FOREIGN KEY (`employeeNumber`) REFERENCES `employee` (`Employee Number`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `detail vacation` (
    `VacationID` int NOT NULL AUTO_INCREMENT,
    `Dayoff` date NULL,
    `resignation content` varchar(500) CHARACTER SET utf8mb4 NULL,
    `employeeNumber` int unsigned NOT NULL,
    `EmployeeNumberNavigationEmployeeNumber` int unsigned NULL,
    `EmployeeNumber1` int unsigned NOT NULL,
    CONSTRAINT `PK_detail vacation` PRIMARY KEY (`VacationID`),
    CONSTRAINT `FK_detail vacation_employee_EmployeeNumberNavigationEmployeeNum~` FOREIGN KEY (`EmployeeNumberNavigationEmployeeNumber`) REFERENCES `employee` (`Employee Number`),
    CONSTRAINT `FK_detail vacation_employee_employeeNumber` FOREIGN KEY (`employeeNumber`) REFERENCES `employee` (`Employee Number`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `employeeNumber` ON `detail vacation` (`employeeNumber`);

CREATE INDEX `IX_detail vacation_EmployeeNumberNavigationEmployeeNumber` ON `detail vacation` (`EmployeeNumberNavigationEmployeeNumber`);

CREATE UNIQUE INDEX `Employee Number_UNIQUE` ON `employee` (`Employee Number`);

CREATE INDEX `fk_Employee_Pay Rates` ON `employee` (`Pay Rates_idPay Rates`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240405033504_v0', '8.0.3');

COMMIT;

START TRANSACTION;

ALTER TABLE `detail vacation` DROP FOREIGN KEY `FK_detail vacation_employee_EmployeeNumberNavigationEmployeeNum~`;

ALTER TABLE `detail vacation` DROP FOREIGN KEY `FK_detail vacation_employee_employeeNumber`;

ALTER TABLE `detail vacation` DROP INDEX `IX_detail vacation_EmployeeNumberNavigationEmployeeNumber`;

ALTER TABLE `detail vacation` DROP COLUMN `EmployeeNumber1`;

ALTER TABLE `detail vacation` DROP COLUMN `EmployeeNumberNavigationEmployeeNumber`;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240405142204_v1', '8.0.3');

COMMIT;

