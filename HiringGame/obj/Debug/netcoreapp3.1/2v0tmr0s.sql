IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AspNetRoles] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] int NOT NULL IDENTITY,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [EmailAddress] nvarchar(max) NULL,
    [MobileNo] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Jobs] (
    [Id] int NOT NULL IDENTITY,
    [IsActive] bit NOT NULL,
    [CreatedById] int NOT NULL,
    [UpdatedById] int NULL,
    [DeletedById] int NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [DeletedDate] datetime2 NULL,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Jobs] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] int NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] int NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Levels] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [JobId] int NOT NULL,
    CONSTRAINT [PK_Levels] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Levels_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Jobs] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Players] (
    [Id] int NOT NULL IDENTITY,
    [IsActive] bit NOT NULL,
    [CreatedById] int NOT NULL,
    [UpdatedById] int NULL,
    [DeletedById] int NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [DeletedDate] datetime2 NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [EmailAddress] nvarchar(max) NULL,
    [MobileNo] nvarchar(max) NULL,
    [CvVirtualName] nvarchar(max) NULL,
    [CvPhysicalName] nvarchar(max) NULL,
    [JobId] int NOT NULL,
    [Score] nvarchar(max) NULL,
    CONSTRAINT [PK_Players] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Players_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Jobs] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Questions] (
    [Id] int NOT NULL IDENTITY,
    [IsActive] bit NOT NULL,
    [CreatedById] int NOT NULL,
    [UpdatedById] int NULL,
    [DeletedById] int NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [DeletedDate] datetime2 NULL,
    [QuestionString] nvarchar(max) NULL,
    [JobId] int NOT NULL,
    [LevelId] int NOT NULL,
    CONSTRAINT [PK_Questions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Questions_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Jobs] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Questions_Levels_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [Levels] ([Id])
);

GO

CREATE TABLE [Answers] (
    [Id] int NOT NULL IDENTITY,
    [IsActive] bit NOT NULL,
    [CreatedById] int NOT NULL,
    [UpdatedById] int NULL,
    [DeletedById] int NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [DeletedDate] datetime2 NULL,
    [AnswerString] nvarchar(max) NULL,
    [IsCorrectAnswer] bit NOT NULL,
    [QuestionId] int NOT NULL,
    CONSTRAINT [PK_Answers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Answers_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Transactions] (
    [Id] int NOT NULL IDENTITY,
    [IsActive] bit NOT NULL,
    [CreatedById] int NOT NULL,
    [UpdatedById] int NULL,
    [DeletedById] int NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [DeletedDate] datetime2 NULL,
    [PlayerId] int NOT NULL,
    [QuestionId] int NULL,
    [SelectedAnswerId] int NULL,
    [Score] nvarchar(max) NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transactions_Players_PlayerId] FOREIGN KEY ([PlayerId]) REFERENCES [Players] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Transactions_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]),
    CONSTRAINT [FK_Transactions_Answers_SelectedAnswerId] FOREIGN KEY ([SelectedAnswerId]) REFERENCES [Answers] ([Id])
);

GO

CREATE INDEX [IX_Answers_QuestionId] ON [Answers] ([QuestionId]);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

CREATE INDEX [IX_Levels_JobId] ON [Levels] ([JobId]);

GO

CREATE INDEX [IX_Players_JobId] ON [Players] ([JobId]);

GO

CREATE INDEX [IX_Questions_JobId] ON [Questions] ([JobId]);

GO

CREATE INDEX [IX_Questions_LevelId] ON [Questions] ([LevelId]);

GO

CREATE INDEX [IX_Transactions_PlayerId] ON [Transactions] ([PlayerId]);

GO

CREATE INDEX [IX_Transactions_QuestionId] ON [Transactions] ([QuestionId]);

GO

CREATE INDEX [IX_Transactions_SelectedAnswerId] ON [Transactions] ([SelectedAnswerId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201009081925_initial2', N'3.1.8');

GO

ALTER TABLE [Levels] ADD [CreatedById] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Levels] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

ALTER TABLE [Levels] ADD [DeletedById] int NULL;

GO

ALTER TABLE [Levels] ADD [DeletedDate] datetime2 NULL;

GO

ALTER TABLE [Levels] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

ALTER TABLE [Levels] ADD [TotalQuestions] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Levels] ADD [UpdatedById] int NULL;

GO

ALTER TABLE [Levels] ADD [UpdatedDate] datetime2 NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201009105528_UpdateLevels', N'3.1.8');

GO

