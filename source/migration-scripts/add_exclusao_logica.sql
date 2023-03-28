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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE TABLE [T_AGENDAMENTO_INVALIDO_LOG] (
        [Id] int NOT NULL IDENTITY,
        [NumeroAgendamento] varchar(100) NOT NULL,
        [Matricula] varchar(30) NOT NULL,
        [Data] varchar(10) NOT NULL,
        [Hora] varchar(10) NOT NULL,
        CONSTRAINT [PK_T_AGENDAMENTO_INVALIDO_LOG] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE TABLE [T_CATEGORIA] (
        [Id] int NOT NULL IDENTITY,
        [Descricao] varchar(100) NOT NULL,
        CONSTRAINT [PK_T_CATEGORIA] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE TABLE [T_DIVERGENCIA_AGENDAMENTO] (
        [Id] int NOT NULL IDENTITY,
        [NumeroAgendamento] varchar(100) NOT NULL,
        [Matricula] varchar(30) NOT NULL,
        [NomeResponsavel] nvarchar(max) NOT NULL,
        [Data] varchar(10) NOT NULL,
        [Hora] varchar(10) NOT NULL,
        [Motorista] tinyint NOT NULL,
        [CPF] tinyint NOT NULL,
        [CNH] tinyint NOT NULL,
        [PlacaCavalo] tinyint NOT NULL,
        [PlacaReboque1] tinyint NOT NULL,
        [PlacaReboque2] tinyint NOT NULL,
        [Conteiner1] tinyint NOT NULL,
        [Conteiner2] tinyint NOT NULL,
        CONSTRAINT [PK_T_DIVERGENCIA_AGENDAMENTO] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE TABLE [T_ITEM_INSPECAO] (
        [Id] int NOT NULL IDENTITY,
        [Descricao] varchar(100) NOT NULL,
        [Impeditivo] bit NOT NULL,
        [ImagemObrigatoria] bit NOT NULL,
        [QuantidadeImagens] int NOT NULL,
        CONSTRAINT [PK_T_ITEM_INSPECAO] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE TABLE [T_TIPO_INSPECAO] (
        [Id] int NOT NULL IDENTITY,
        [Descricao] varchar(100) NOT NULL,
        CONSTRAINT [PK_T_TIPO_INSPECAO] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE TABLE [T_INSPECAO_NAO_REALIZADA] (
        [Id] int NOT NULL IDENTITY,
        [NumeroAgendamento] varchar(100) NOT NULL,
        [Matricula] varchar(30) NOT NULL,
        [NomeResponsavel] varchar(30) NOT NULL,
        [Data] varchar(10) NOT NULL,
        [Hora] varchar(10) NOT NULL,
        [DataAgendamento] varchar(10) NOT NULL,
        [NomeMotorista] varchar(100) NOT NULL,
        [CnhMotorista] varchar(20) NOT NULL,
        [Cpf] varchar(15) NOT NULL,
        [PlacaCavalo] varchar(100) NOT NULL,
        [PlacaCarreta1] varchar(100) NOT NULL,
        [PlacaCarreta2] varchar(100) NOT NULL,
        [Conteiner1] varchar(100) NOT NULL,
        [Conteiner2] varchar(100) NOT NULL,
        [IdCategoria] int NOT NULL,
        [Motivo] varchar(150) NOT NULL,
        CONSTRAINT [PK_T_INSPECAO_NAO_REALIZADA] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_T_INSPECAO_NAO_REALIZADA_T_CATEGORIA_IdCategoria] FOREIGN KEY ([IdCategoria]) REFERENCES [T_CATEGORIA] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE TABLE [T_INSPECAO] (
        [Id] int NOT NULL IDENTITY,
        [NumeroAgendamento] varchar(100) NOT NULL,
        [Matricula] varchar(30) NOT NULL,
        [NomeResponsavel] varchar(50) NOT NULL,
        [DataInicio] varchar(10) NOT NULL,
        [HoraInicio] varchar(10) NOT NULL,
        [DataFim] varchar(10) NOT NULL,
        [HoraFim] varchar(10) NOT NULL,
        [IdTipoInspecao] int NOT NULL,
        [IdCategoria] int NOT NULL,
        [DataAgendamento] varchar(10) NOT NULL,
        [NomeMotorista] varchar(100) NOT NULL,
        [CnhMotorista] varchar(20) NOT NULL,
        [Cpf] varchar(15) NOT NULL,
        [PlacaCavalo] varchar(100) NOT NULL,
        [PlacaCarreta1] varchar(100) NOT NULL,
        [PlacaCarreta2] varchar(100) NOT NULL,
        [Conteiner1] varchar(100) NOT NULL,
        [Conteiner2] varchar(100) NOT NULL,
        [ResultadoInspecao] varchar(100) NOT NULL,
        CONSTRAINT [PK_T_INSPECAO] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_T_INSPECAO_T_CATEGORIA_IdCategoria] FOREIGN KEY ([IdCategoria]) REFERENCES [T_CATEGORIA] ([Id]),
        CONSTRAINT [FK_T_INSPECAO_T_TIPO_INSPECAO_IdTipoInspecao] FOREIGN KEY ([IdTipoInspecao]) REFERENCES [T_TIPO_INSPECAO] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE TABLE [T_ITEM_INSPECAO_TIPO_INSPECAO] (
        [ItensInspecaoId] int NOT NULL,
        [TipoInspecaoId] int NOT NULL,
        CONSTRAINT [PK_T_ITEM_INSPECAO_TIPO_INSPECAO] PRIMARY KEY ([ItensInspecaoId], [TipoInspecaoId]),
        CONSTRAINT [FK_T_ITEM_INSPECAO_TIPO_INSPECAO_T_ITEM_INSPECAO_ItensInspecaoId] FOREIGN KEY ([ItensInspecaoId]) REFERENCES [T_ITEM_INSPECAO] ([Id]),
        CONSTRAINT [FK_T_ITEM_INSPECAO_TIPO_INSPECAO_T_TIPO_INSPECAO_TipoInspecaoId] FOREIGN KEY ([TipoInspecaoId]) REFERENCES [T_TIPO_INSPECAO] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE TABLE [T_PORCENTAGEM_MINIMA_INSPECAO] (
        [Id] int NOT NULL IDENTITY,
        [Porcentagem] int NOT NULL,
        [IdCategoria] int NOT NULL,
        [IdTipoInspecao] int NOT NULL,
        CONSTRAINT [PK_T_PORCENTAGEM_MINIMA_INSPECAO] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_T_PORCENTAGEM_MINIMA_INSPECAO_T_CATEGORIA_IdCategoria] FOREIGN KEY ([IdCategoria]) REFERENCES [T_CATEGORIA] ([Id]),
        CONSTRAINT [FK_T_PORCENTAGEM_MINIMA_INSPECAO_T_TIPO_INSPECAO_IdTipoInspecao] FOREIGN KEY ([IdTipoInspecao]) REFERENCES [T_TIPO_INSPECAO] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE TABLE [T_AUTORIZACAO_ACESSO] (
        [Id] int NOT NULL IDENTITY,
        [IdInspecao] int NOT NULL,
        [Matricula] varchar(30) NOT NULL,
        [Motivo] varchar(150) NOT NULL,
        [Data] varchar(10) NOT NULL,
        [Hora] varchar(10) NOT NULL,
        CONSTRAINT [PK_T_AUTORIZACAO_ACESSO] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_T_AUTORIZACAO_ACESSO_T_INSPECAO_IdInspecao] FOREIGN KEY ([IdInspecao]) REFERENCES [T_INSPECAO] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE TABLE [T_IMAGENS_COMPLEMENTARES] (
        [Id] int NOT NULL IDENTITY,
        [IdInspecao] int NOT NULL,
        [NomeArquivo] varchar(100) NULL,
        [LocalArquivo] varchar(max) NULL,
        [ContentType] varchar(50) NULL,
        [DataUpload] varchar(25) NOT NULL,
        CONSTRAINT [PK_T_IMAGENS_COMPLEMENTARES] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_T_IMAGENS_COMPLEMENTARES_T_INSPECAO_IdInspecao] FOREIGN KEY ([IdInspecao]) REFERENCES [T_INSPECAO] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE TABLE [T_ITEM_INSPECIONADO] (
        [Id] int NOT NULL IDENTITY,
        [IdInspecao] int NOT NULL,
        [IdItem] int NOT NULL,
        [Divergente] varchar(100) NOT NULL,
        CONSTRAINT [PK_T_ITEM_INSPECIONADO] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_T_ITEM_INSPECIONADO_T_INSPECAO_IdInspecao] FOREIGN KEY ([IdInspecao]) REFERENCES [T_INSPECAO] ([Id]),
        CONSTRAINT [FK_T_ITEM_INSPECIONADO_T_ITEM_INSPECAO_IdItem] FOREIGN KEY ([IdItem]) REFERENCES [T_ITEM_INSPECAO] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE TABLE [T_RECUSA_ACESSO] (
        [Id] int NOT NULL IDENTITY,
        [IdInspecao] int NOT NULL,
        [Matricula] varchar(30) NOT NULL,
        [Data] varchar(10) NOT NULL,
        [Hora] varchar(10) NOT NULL,
        CONSTRAINT [PK_T_RECUSA_ACESSO] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_T_RECUSA_ACESSO_T_INSPECAO_IdInspecao] FOREIGN KEY ([IdInspecao]) REFERENCES [T_INSPECAO] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE TABLE [T_IMAGENS_ITEM] (
        [Id] int NOT NULL IDENTITY,
        [IdItemInspecionado] int NOT NULL,
        [NomeArquivo] varchar(100) NULL,
        [LocalArquivo] varchar(max) NULL,
        [ContentType] varchar(50) NULL,
        [DataUpload] varchar(25) NOT NULL,
        CONSTRAINT [PK_T_IMAGENS_ITEM] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_T_IMAGENS_ITEM_T_ITEM_INSPECIONADO_IdItemInspecionado] FOREIGN KEY ([IdItemInspecionado]) REFERENCES [T_ITEM_INSPECIONADO] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Descricao') AND [object_id] = OBJECT_ID(N'[T_CATEGORIA]'))
        SET IDENTITY_INSERT [T_CATEGORIA] ON;
    EXEC(N'INSERT INTO [T_CATEGORIA] ([Id], [Descricao])
    VALUES (1, ''Entrega''),
    (2, ''Retirada''),
    (3, ''Movimentação Interna'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Descricao') AND [object_id] = OBJECT_ID(N'[T_CATEGORIA]'))
        SET IDENTITY_INSERT [T_CATEGORIA] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Descricao') AND [object_id] = OBJECT_ID(N'[T_TIPO_INSPECAO]'))
        SET IDENTITY_INSERT [T_TIPO_INSPECAO] ON;
    EXEC(N'INSERT INTO [T_TIPO_INSPECAO] ([Id], [Descricao])
    VALUES (1, ''Inspeção OEA''),
    (2, ''Inspeção ISPS-Code'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Descricao') AND [object_id] = OBJECT_ID(N'[T_TIPO_INSPECAO]'))
        SET IDENTITY_INSERT [T_TIPO_INSPECAO] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE INDEX [IX_T_AUTORIZACAO_ACESSO_IdInspecao] ON [T_AUTORIZACAO_ACESSO] ([IdInspecao]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE INDEX [IX_T_IMAGENS_COMPLEMENTARES_IdInspecao] ON [T_IMAGENS_COMPLEMENTARES] ([IdInspecao]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE INDEX [IX_T_IMAGENS_ITEM_IdItemInspecionado] ON [T_IMAGENS_ITEM] ([IdItemInspecionado]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE INDEX [IX_T_INSPECAO_IdCategoria] ON [T_INSPECAO] ([IdCategoria]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE INDEX [IX_T_INSPECAO_IdTipoInspecao] ON [T_INSPECAO] ([IdTipoInspecao]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE INDEX [IX_T_INSPECAO_NAO_REALIZADA_IdCategoria] ON [T_INSPECAO_NAO_REALIZADA] ([IdCategoria]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE INDEX [IX_T_ITEM_INSPECAO_TIPO_INSPECAO_TipoInspecaoId] ON [T_ITEM_INSPECAO_TIPO_INSPECAO] ([TipoInspecaoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE INDEX [IX_T_ITEM_INSPECIONADO_IdInspecao] ON [T_ITEM_INSPECIONADO] ([IdInspecao]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE INDEX [IX_T_ITEM_INSPECIONADO_IdItem] ON [T_ITEM_INSPECIONADO] ([IdItem]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE UNIQUE INDEX [IX_T_PORCENTAGEM_MINIMA_INSPECAO_IdCategoria_IdTipoInspecao] ON [T_PORCENTAGEM_MINIMA_INSPECAO] ([IdCategoria], [IdTipoInspecao]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE INDEX [IX_T_PORCENTAGEM_MINIMA_INSPECAO_IdTipoInspecao] ON [T_PORCENTAGEM_MINIMA_INSPECAO] ([IdTipoInspecao]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    CREATE INDEX [IX_T_RECUSA_ACESSO_IdInspecao] ON [T_RECUSA_ACESSO] ([IdInspecao]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503142447_InitDatabase')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220503142447_InitDatabase', N'6.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503172001_placaCarreta2NotRequired')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[T_INSPECAO_NAO_REALIZADA]') AND [c].[name] = N'PlacaCarreta2');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [T_INSPECAO_NAO_REALIZADA] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [T_INSPECAO_NAO_REALIZADA] ALTER COLUMN [PlacaCarreta2] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503172001_placaCarreta2NotRequired')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[T_INSPECAO_NAO_REALIZADA]') AND [c].[name] = N'Conteiner2');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [T_INSPECAO_NAO_REALIZADA] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [T_INSPECAO_NAO_REALIZADA] ALTER COLUMN [Conteiner2] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220503172001_placaCarreta2NotRequired')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220503172001_placaCarreta2NotRequired', N'6.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220822164607_Ajuste_T_ITEM_INSPECAO_TIPO_INSPECAO')
BEGIN
    ALTER TABLE [T_ITEM_INSPECAO_TIPO_INSPECAO] DROP CONSTRAINT [FK_T_ITEM_INSPECAO_TIPO_INSPECAO_T_ITEM_INSPECAO_ItensInspecaoId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220822164607_Ajuste_T_ITEM_INSPECAO_TIPO_INSPECAO')
BEGIN
    ALTER TABLE [T_ITEM_INSPECAO_TIPO_INSPECAO] DROP CONSTRAINT [PK_T_ITEM_INSPECAO_TIPO_INSPECAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220822164607_Ajuste_T_ITEM_INSPECAO_TIPO_INSPECAO')
BEGIN
    EXEC sp_rename N'[T_ITEM_INSPECAO_TIPO_INSPECAO].[ItensInspecaoId]', N'Ordem', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220822164607_Ajuste_T_ITEM_INSPECAO_TIPO_INSPECAO')
BEGIN
    ALTER TABLE [T_ITEM_INSPECAO_TIPO_INSPECAO] ADD [ItemInspecaoId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220822164607_Ajuste_T_ITEM_INSPECAO_TIPO_INSPECAO')
BEGIN
    ALTER TABLE [T_ITEM_INSPECAO_TIPO_INSPECAO] ADD CONSTRAINT [PK_T_ITEM_INSPECAO_TIPO_INSPECAO] PRIMARY KEY ([ItemInspecaoId], [TipoInspecaoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220822164607_Ajuste_T_ITEM_INSPECAO_TIPO_INSPECAO')
BEGIN
    ALTER TABLE [T_ITEM_INSPECAO_TIPO_INSPECAO] ADD CONSTRAINT [FK_T_ITEM_INSPECAO_TIPO_INSPECAO_T_ITEM_INSPECAO_ItemInspecaoId] FOREIGN KEY ([ItemInspecaoId]) REFERENCES [T_ITEM_INSPECAO] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220822164607_Ajuste_T_ITEM_INSPECAO_TIPO_INSPECAO')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220822164607_Ajuste_T_ITEM_INSPECAO_TIPO_INSPECAO', N'6.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220822172408_AddCampoOrdem')
BEGIN
    ALTER TABLE [T_ITEM_INSPECAO_TIPO_INSPECAO] ADD [Ordem] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220822172408_AddCampoOrdem')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220822172408_AddCampoOrdem', N'6.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221014125202_add_exclusao_logica')
BEGIN
    ALTER TABLE [T_TIPO_INSPECAO] ADD [Ativo] tinyint NOT NULL DEFAULT CAST(1 AS tinyint);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221014125202_add_exclusao_logica')
BEGIN
    ALTER TABLE [T_ITEM_INSPECAO] ADD [Ativo] tinyint NOT NULL DEFAULT CAST(1 AS tinyint);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221014125202_add_exclusao_logica')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221014125202_add_exclusao_logica', N'6.0.1');
END;
GO

COMMIT;
GO

