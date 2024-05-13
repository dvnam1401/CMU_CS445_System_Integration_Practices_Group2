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

ALTER TABLE [GroupPermissions] DROP CONSTRAINT [FK_GroupPermissions_Groups_GroupId];
GO

ALTER TABLE [GroupPermissions] DROP CONSTRAINT [FK_GroupPermissions_Permissions_PermissionId];
GO

ALTER TABLE [UserGroups] DROP CONSTRAINT [FK_UserGroups_AccountUser_UserId];
GO

ALTER TABLE [UserGroups] DROP CONSTRAINT [FK_UserGroups_Groups_GroupId];
GO

DROP TABLE [GroupPermission];
GO

DROP INDEX [IX_Groups_ParentGroupId] ON [Groups];
GO

DROP INDEX [IX_GroupPermissions_GroupId] ON [GroupPermissions];
GO

ALTER TABLE [UserGroups] DROP CONSTRAINT [PK_UserGroups];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Permissions]') AND [c].[name] = N'PermissionName');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Permissions] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Permissions] DROP COLUMN [PermissionName];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Groups]') AND [c].[name] = N'Description');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Groups] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Groups] DROP COLUMN [Description];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Groups]') AND [c].[name] = N'ParentGroupId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Groups] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Groups] DROP COLUMN [ParentGroupId];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'JobTitle');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [AccountUser] DROP COLUMN [JobTitle];
GO

EXEC sp_rename N'[UserGroups]', N'UserGroup';
GO

EXEC sp_rename N'[Permissions].[Description]', N'description', N'COLUMN';
GO

EXEC sp_rename N'[Permissions].[PermissionId]', N'permission_id', N'COLUMN';
GO

EXEC sp_rename N'[Groups].[GroupName]', N'groupName', N'COLUMN';
GO

EXEC sp_rename N'[Groups].[GroupId]', N'group_id', N'COLUMN';
GO

EXEC sp_rename N'[GroupPermissions].[PermissionId]', N'permission_id', N'COLUMN';
GO

EXEC sp_rename N'[GroupPermissions].[GroupId]', N'group_id', N'COLUMN';
GO

EXEC sp_rename N'[AccountUser].[Email]', N'email', N'COLUMN';
GO

EXEC sp_rename N'[AccountUser].[Department]', N'department', N'COLUMN';
GO

EXEC sp_rename N'[AccountUser].[UserName]', N'user_name', N'COLUMN';
GO

EXEC sp_rename N'[AccountUser].[PhoneNumber]', N'phone_number', N'COLUMN';
GO

EXEC sp_rename N'[AccountUser].[PasswordHash]', N'password_hash', N'COLUMN';
GO

EXEC sp_rename N'[AccountUser].[LastName]', N'last_name', N'COLUMN';
GO

EXEC sp_rename N'[AccountUser].[IsActive]', N'is_active', N'COLUMN';
GO

EXEC sp_rename N'[AccountUser].[FirstName]', N'first_name', N'COLUMN';
GO

EXEC sp_rename N'[UserGroup].[GroupId]', N'group_id', N'COLUMN';
GO

EXEC sp_rename N'[UserGroup].[UserId]', N'user_id', N'COLUMN';
GO

EXEC sp_rename N'[UserGroup].[PermissionStatus]', N'is_enable', N'COLUMN';
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Permissions]') AND [c].[name] = N'description');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Permissions] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Permissions] ALTER COLUMN [description] nvarchar(max) NULL;
GO

ALTER TABLE [Permissions] ADD [permisstion_name] varchar(max) NOT NULL DEFAULT '';
GO

ALTER TABLE [GroupPermissions] ADD [is_enable] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'email');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var5 + '];');
UPDATE [AccountUser] SET [email] = N'' WHERE [email] IS NULL;
ALTER TABLE [AccountUser] ALTER COLUMN [email] nvarchar(256) NOT NULL;
ALTER TABLE [AccountUser] ADD DEFAULT N'' FOR [email];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'department');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var6 + '];');
UPDATE [AccountUser] SET [department] = N'' WHERE [department] IS NULL;
ALTER TABLE [AccountUser] ALTER COLUMN [department] nvarchar(max) NOT NULL;
ALTER TABLE [AccountUser] ADD DEFAULT N'' FOR [department];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'user_name');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var7 + '];');
UPDATE [AccountUser] SET [user_name] = N'' WHERE [user_name] IS NULL;
ALTER TABLE [AccountUser] ALTER COLUMN [user_name] nvarchar(256) NOT NULL;
ALTER TABLE [AccountUser] ADD DEFAULT N'' FOR [user_name];
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'phone_number');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var8 + '];');
UPDATE [AccountUser] SET [phone_number] = '' WHERE [phone_number] IS NULL;
ALTER TABLE [AccountUser] ALTER COLUMN [phone_number] varchar(50) NOT NULL;
ALTER TABLE [AccountUser] ADD DEFAULT '' FOR [phone_number];
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'password_hash');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var9 + '];');
UPDATE [AccountUser] SET [password_hash] = N'' WHERE [password_hash] IS NULL;
ALTER TABLE [AccountUser] ALTER COLUMN [password_hash] nvarchar(max) NOT NULL;
ALTER TABLE [AccountUser] ADD DEFAULT N'' FOR [password_hash];
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'last_name');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var10 + '];');
UPDATE [AccountUser] SET [last_name] = N'' WHERE [last_name] IS NULL;
ALTER TABLE [AccountUser] ALTER COLUMN [last_name] nvarchar(100) NOT NULL;
ALTER TABLE [AccountUser] ADD DEFAULT N'' FOR [last_name];
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'is_active');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var11 + '];');
UPDATE [AccountUser] SET [is_active] = CAST(0 AS bit) WHERE [is_active] IS NULL;
ALTER TABLE [AccountUser] ALTER COLUMN [is_active] bit NOT NULL;
ALTER TABLE [AccountUser] ADD DEFAULT CAST(0 AS bit) FOR [is_active];
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'first_name');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var12 + '];');
UPDATE [AccountUser] SET [first_name] = N'' WHERE [first_name] IS NULL;
ALTER TABLE [AccountUser] ALTER COLUMN [first_name] nvarchar(100) NOT NULL;
ALTER TABLE [AccountUser] ADD DEFAULT N'' FOR [first_name];
GO

ALTER TABLE [GroupPermissions] ADD CONSTRAINT [PK_GroupPermissions] PRIMARY KEY ([group_id], [permission_id]);
GO

ALTER TABLE [UserGroup] ADD CONSTRAINT [PK_UserGroup] PRIMARY KEY ([user_id], [group_id]);
GO

ALTER TABLE [GroupPermissions] ADD CONSTRAINT [FK_GroupPermissions_Groups_group_id] FOREIGN KEY ([group_id]) REFERENCES [Groups] ([group_id]) ON DELETE CASCADE;
GO

ALTER TABLE [GroupPermissions] ADD CONSTRAINT [FK_GroupPermissions_Permissions_permission_id] FOREIGN KEY ([permission_id]) REFERENCES [Permissions] ([permission_id]) ON DELETE CASCADE;
GO

ALTER TABLE [UserGroup] ADD CONSTRAINT [FK_UserGroup_AccountUser_user_id] FOREIGN KEY ([user_id]) REFERENCES [AccountUser] ([user_id]) ON DELETE CASCADE;
GO

ALTER TABLE [UserGroup] ADD CONSTRAINT [FK_UserGroup_Groups_group_id] FOREIGN KEY ([group_id]) REFERENCES [Groups] ([group_id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240506050535_createAd', N'8.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [UserGroup] DROP CONSTRAINT [FK_UserGroup_AccountUser_user_id];
GO

ALTER TABLE [UserGroup] DROP CONSTRAINT [FK_UserGroup_Groups_group_id];
GO

ALTER TABLE [UserGroup] DROP CONSTRAINT [PK_UserGroup];
GO

EXEC sp_rename N'[UserGroup]', N'UserGroups';
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Permissions]') AND [c].[name] = N'permisstion_name');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Permissions] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Permissions] ALTER COLUMN [permisstion_name] varchar(50) NULL;
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Groups]') AND [c].[name] = N'groupName');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Groups] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Groups] ALTER COLUMN [groupName] varchar(50) NOT NULL;
GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[GroupPermissions]') AND [c].[name] = N'is_enable');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [GroupPermissions] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [GroupPermissions] ALTER COLUMN [is_enable] bit NULL;
GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'user_name');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [AccountUser] ALTER COLUMN [user_name] nvarchar(256) NULL;
GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'phone_number');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [AccountUser] ALTER COLUMN [phone_number] varchar(50) NULL;
GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'password_hash');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [AccountUser] ALTER COLUMN [password_hash] nvarchar(max) NULL;
GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'last_name');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [AccountUser] ALTER COLUMN [last_name] nvarchar(100) NULL;
GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'is_active');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [AccountUser] ALTER COLUMN [is_active] bit NULL;
GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'first_name');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [AccountUser] ALTER COLUMN [first_name] nvarchar(100) NULL;
GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'email');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var22 + '];');
ALTER TABLE [AccountUser] ALTER COLUMN [email] nvarchar(256) NULL;
GO

DECLARE @var23 sysname;
SELECT @var23 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountUser]') AND [c].[name] = N'department');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [AccountUser] DROP CONSTRAINT [' + @var23 + '];');
ALTER TABLE [AccountUser] ALTER COLUMN [department] nvarchar(50) NULL;
GO

ALTER TABLE [UserGroups] ADD CONSTRAINT [PK_UserGroups] PRIMARY KEY ([user_id], [group_id]);
GO

ALTER TABLE [UserGroups] ADD CONSTRAINT [FK_UserGroups_AccountUser_user_id] FOREIGN KEY ([user_id]) REFERENCES [AccountUser] ([user_id]) ON DELETE CASCADE;
GO

ALTER TABLE [UserGroups] ADD CONSTRAINT [FK_UserGroups_Groups_group_id] FOREIGN KEY ([group_id]) REFERENCES [Groups] ([group_id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240508042230_createV1', N'8.0.3');
GO

COMMIT;
GO

