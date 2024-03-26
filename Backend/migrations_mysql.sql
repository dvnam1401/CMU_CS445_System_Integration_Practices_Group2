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
    `Employee Number` int unsigned NOT NULL,
    `Pay Rates_idPay Rates` int NOT NULL,
    `idEmployee` int NOT NULL,
    `Last Name` varchar(45) NOT NULL,
    `First Name` varchar(45) NOT NULL,
    `SSN` decimal(10,0) NOT NULL,
    `Pay Rate` varchar(40) NULL,
    `Vacation Days` int NULL,
    `Paid To Date` decimal(2,0) NULL,
    `Paid Last Year` decimal(2,0) NULL,
    PRIMARY KEY (`Employee Number`, `Pay Rates_idPay Rates`),
    CONSTRAINT `FK_employee_pay rates_Pay Rates_idPay Rates` FOREIGN KEY (`Pay Rates_idPay Rates`) REFERENCES `pay rates` (`idPay Rates`) ON DELETE CASCADE
);

CREATE TABLE `birthday` (
    `idbirthday` int NOT NULL AUTO_INCREMENT,
    `dateofbirthday` date NULL,
    `employeeNumber` int unsigned NOT NULL,
    `EmployeeNumberNavigationEmployeeNumber` int unsigned NULL,
    `EmployeeNumberNavigationPayRatesIdPayRates` int NULL,
    PRIMARY KEY (`idbirthday`),
    CONSTRAINT `FK_birthday_employee_EmployeeNumberNavigationEmployeeNumber_Emp~` FOREIGN KEY (`EmployeeNumberNavigationEmployeeNumber`, `EmployeeNumberNavigationPayRatesIdPayRates`) REFERENCES `employee` (`Employee Number`, `Pay Rates_idPay Rates`) ON DELETE RESTRICT
);

CREATE TABLE `detail vacation` (
    `idvacation` int NOT NULL AUTO_INCREMENT,
    `dayoff` date NULL,
    `resignation content` varchar(500) NULL,
    `employeeNumber` int unsigned NOT NULL,
    `EmployeeNumberNavigationEmployeeNumber` int unsigned NULL,
    `EmployeeNumberNavigationPayRatesIdPayRates` int NULL,
    PRIMARY KEY (`idvacation`),
    CONSTRAINT `FK_detail vacation_employee_EmployeeNumberNavigationEmployeeNum~` FOREIGN KEY (`EmployeeNumberNavigationEmployeeNumber`, `EmployeeNumberNavigationPayRatesIdPayRates`) REFERENCES `employee` (`Employee Number`, `Pay Rates_idPay Rates`) ON DELETE RESTRICT
);

CREATE INDEX `employeeNumber` ON `birthday` (`employeeNumber`);

CREATE INDEX `IX_birthday_EmployeeNumberNavigationEmployeeNumber_EmployeeNumb~` ON `birthday` (`EmployeeNumberNavigationEmployeeNumber`, `EmployeeNumberNavigationPayRatesIdPayRates`);

CREATE INDEX `employeeNumber1` ON `detail vacation` (`employeeNumber`);

CREATE INDEX `IX_detail vacation_EmployeeNumberNavigationEmployeeNumber_Emplo~` ON `detail vacation` (`EmployeeNumberNavigationEmployeeNumber`, `EmployeeNumberNavigationPayRatesIdPayRates`);

CREATE UNIQUE INDEX `Employee Number_UNIQUE` ON `employee` (`Employee Number`);

CREATE INDEX `fk_Employee_Pay Rates` ON `employee` (`Pay Rates_idPay Rates`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240325132330_v0', '5.0.17');

COMMIT;

