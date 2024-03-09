IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Benefit_Plans] (
    [Benefit_Plan_ID] numeric(18,0) NOT NULL,
    [Plan_Name] nvarchar(50) NULL,
    [Deductable] numeric(18,0) NULL,
    [Percentage_CoPay] int NULL,
    CONSTRAINT [PK_Benefit_Plans] PRIMARY KEY ([Benefit_Plan_ID])
);
GO

CREATE TABLE [Personal] (
    [Employee_ID] numeric(18,0) NOT NULL,
    [First_Name] nvarchar(50) NULL,
    [Last_Name] nvarchar(50) NULL,
    [Middle_Initial] nvarchar(50) NULL,
    [Address1] nvarchar(50) NULL,
    [Address2] nvarchar(50) NULL,
    [City] nvarchar(50) NULL,
    [State] nvarchar(50) NULL,
    [Zip] numeric(18,0) NULL,
    [Email] nvarchar(50) NULL,
    [Phone_Number] nvarchar(50) NULL,
    [Social_Security_Number] nvarchar(50) NULL,
    [Drivers_License] nvarchar(50) NULL,
    [Marital_Status] nvarchar(50) NULL,
    [Gender] bit NULL,
    [Shareholder_Status] bit NOT NULL,
    [Benefit_Plans] numeric(18,0) NULL,
    [Ethnicity] nvarchar(50) NULL,
    CONSTRAINT [PK_Personal] PRIMARY KEY ([Employee_ID]),
    CONSTRAINT [FK_Personal_Benefit_Plans_Benefit_Plans] FOREIGN KEY ([Benefit_Plans]) REFERENCES [Benefit_Plans] ([Benefit_Plan_ID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Emergency_Contacts] (
    [Employee_ID] numeric(18,0) NOT NULL,
    [Emergency_Contact_Name] nvarchar(50) NULL,
    [Phone_Number] nvarchar(50) NULL,
    [Relationship] nvarchar(50) NULL,
    CONSTRAINT [PK_Emergency_Contacts] PRIMARY KEY ([Employee_ID]),
    CONSTRAINT [FK_Emergency_Contacts_Personal_Employee_ID] FOREIGN KEY ([Employee_ID]) REFERENCES [Personal] ([Employee_ID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Employment] (
    [Employee_ID] numeric(18,0) NOT NULL,
    [Employment_Status] nvarchar(50) NULL,
    [Hire_Date] datetime NULL,
    [Workers_Comp_Code] nvarchar(50) NULL,
    [Termination_Date] datetime NULL,
    [Rehire_Date] datetime NULL,
    [Last_Review_Date] datetime NULL,
    CONSTRAINT [PK_Employment] PRIMARY KEY ([Employee_ID]),
    CONSTRAINT [FK_Employment_Personal_Employee_ID] FOREIGN KEY ([Employee_ID]) REFERENCES [Personal] ([Employee_ID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Job_History] (
    [ID] numeric(18,0) NOT NULL,
    [Employee_ID] numeric(18,0) NOT NULL,
    [Department] nvarchar(50) NULL,
    [Division] nvarchar(50) NULL,
    [Start_Date] datetime NULL,
    [End_Date] datetime NULL,
    [Job_Title] nvarchar(50) NULL,
    [Supervisor] numeric(18,0) NULL,
    [Job_Category] nvarchar(50) NULL,
    [Location] nvarchar(50) NULL,
    [Departmen_Code] numeric(18,0) NULL,
    [Salary_Type] numeric(18,0) NULL,
    [Pay_Period] nvarchar(50) NULL,
    [Hours_per_Week] numeric(18,0) NULL,
    [Hazardous_Training] bit NULL,
    CONSTRAINT [PK_Job_History] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Job_History_Personal_Employee_ID] FOREIGN KEY ([Employee_ID]) REFERENCES [Personal] ([Employee_ID]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Employee_ID] ON [Emergency_Contacts] ([Employee_ID]);
GO

CREATE INDEX [IX_Employee_ID1] ON [Employment] ([Employee_ID]);
GO

CREATE INDEX [IX_Employee_ID2] ON [Job_History] ([Employee_ID]);
GO

CREATE INDEX [IX_Benefit_Plans] ON [Personal] ([Benefit_Plans]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240309130109_v0', N'5.0.17');
GO

COMMIT;
GO

