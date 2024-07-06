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

CREATE TABLE [Agents] (
    [Id] int NOT NULL IDENTITY,
    [FullName] nvarchar(max) NOT NULL,
    [PhoneNumber] float NOT NULL,
    [EmailAddress] nvarchar(max) NOT NULL,
    [Experience] datetime2 NOT NULL,
    CONSTRAINT [PK_Agents] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Players] (
    [Id] int NOT NULL IDENTITY,
    [FullName] nvarchar(max) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [Nationality] nvarchar(max) NOT NULL,
    [Position] nvarchar(max) NOT NULL,
    [Height] int NOT NULL,
    [Weight] int NOT NULL,
    [MarketValue] float NOT NULL,
    [PreferredFoot] nvarchar(max) NOT NULL,
    [ContractExpirationDate] datetime2 NOT NULL,
    [CurrentClub] nvarchar(max) NOT NULL,
    [AgentId] int NOT NULL,
    [AwardsAndAchievements] nvarchar(max) NOT NULL,
    [HealthStatus] nvarchar(max) NOT NULL,
    [Languages] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Players] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Players_Agents_AgentId] FOREIGN KEY ([AgentId]) REFERENCES [Agents] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Players_AgentId] ON [Players] ([AgentId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240518193858_Inital', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Players]') AND [c].[name] = N'PreferredFoot');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Players] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Players] ALTER COLUMN [PreferredFoot] nvarchar(14) NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Players]') AND [c].[name] = N'Position');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Players] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Players] ALTER COLUMN [Position] nvarchar(30) NOT NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Players]') AND [c].[name] = N'Nationality');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Players] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Players] ALTER COLUMN [Nationality] nvarchar(30) NOT NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Players]') AND [c].[name] = N'Height');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Players] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Players] ALTER COLUMN [Height] float NOT NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Players]') AND [c].[name] = N'FullName');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Players] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Players] ALTER COLUMN [FullName] nvarchar(200) NOT NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Players]') AND [c].[name] = N'CurrentClub');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Players] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Players] ALTER COLUMN [CurrentClub] nvarchar(14) NOT NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Agents]') AND [c].[name] = N'FullName');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Agents] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Agents] ALTER COLUMN [FullName] nvarchar(60) NOT NULL;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Agents]') AND [c].[name] = N'EmailAddress');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Agents] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Agents] ALTER COLUMN [EmailAddress] nvarchar(60) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240519102015_Initial', N'8.0.6');
GO

COMMIT;
GO

