USE [DataVaultDb];
GO

-- ==========================================
-- TABLAS BASE
-- ==========================================

CREATE TABLE [dbo].[Usuario](
    [id_usuario] INT IDENTITY(1,1) NOT NULL,
    [nombre] NVARCHAR(100) NOT NULL,
    [correo] NVARCHAR(256) NULL,
    [contrasena] NVARCHAR(255) NOT NULL,
    [rol] NVARCHAR(50) NOT NULL,
    [activo] BIT NOT NULL DEFAULT (1),
    CONSTRAINT PK_Usuario PRIMARY KEY CLUSTERED ([id_usuario]),
    CONSTRAINT UQ_Usuario_Correo UNIQUE ([correo])
);
GO

CREATE TABLE [dbo].[Coordinacion](
    [id_coordinacion] INT IDENTITY(1,1) NOT NULL,
    [id_usuario] INT NOT NULL,
    [cargo] NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_Coordinacion PRIMARY KEY CLUSTERED ([id_coordinacion]),
    CONSTRAINT FK_Coordinacion_Usuario FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[Usuario]([id_usuario])
);
GO

CREATE TABLE [dbo].[Docente](
    [id_docente] INT IDENTITY(1,1) NOT NULL,
    [id_usuario] INT NOT NULL,
    [departamento] NVARCHAR(100) NULL,
    CONSTRAINT PK_Docente PRIMARY KEY CLUSTERED ([id_docente]),
    CONSTRAINT FK_Docente_Usuario FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[Usuario]([id_usuario])
);
GO

CREATE TABLE [dbo].[Estudiante](
    [id_estudiante] INT IDENTITY(1,1) NOT NULL,
    [id_usuario] INT NOT NULL,
    [semestre] NVARCHAR(50) NOT NULL,
    [carrera] NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_Estudiante PRIMARY KEY CLUSTERED ([id_estudiante]),
    CONSTRAINT FK_Estudiante_Usuario FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[Usuario]([id_usuario])
);
GO

CREATE TABLE [dbo].[Gestor](
    [id_gestor] INT IDENTITY(1,1) NOT NULL,
    [id_usuario] INT NOT NULL,
    [area_responsable] NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_Gestor PRIMARY KEY CLUSTERED ([id_gestor]),
    CONSTRAINT FK_Gestor_Usuario FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[Usuario]([id_usuario])
);
GO

CREATE TABLE [dbo].[TI](
    [id_ti] INT IDENTITY(1,1) NOT NULL,
    [id_usuario] INT NOT NULL,
    [especialidad] NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_TI PRIMARY KEY CLUSTERED ([id_ti]),
    CONSTRAINT FK_TI_Usuario FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[Usuario]([id_usuario])
);
GO

CREATE TABLE [dbo].[Recurso](
    [id_recurso] INT IDENTITY(1,1) NOT NULL,
    [titulo] NVARCHAR(150) NOT NULL,
    [descripcion] NVARCHAR(255) NULL,
    [autor] NVARCHAR(50) NOT NULL,
    [semestre] NVARCHAR(20) NULL,
    [tamano] INT NULL CHECK ([tamano] <= 1048576),
    [fecha_subida] DATETIME NOT NULL DEFAULT (GETDATE()),
    [estado] NVARCHAR(50) NULL DEFAULT ('Pendiente'),
    [id_docente] INT NOT NULL,
    CONSTRAINT PK_Recurso PRIMARY KEY CLUSTERED ([id_recurso]),
    CONSTRAINT FK_Recurso_Docente FOREIGN KEY ([id_docente]) REFERENCES [dbo].[Docente]([id_docente])
);
GO

CREATE TABLE [dbo].[Metadato](
    [id_metadato] INT IDENTITY(1,1) NOT NULL,
    [titulo] NVARCHAR(50) NOT NULL,
    [materia] NVARCHAR(80) NOT NULL,
    [palabras_clave] NVARCHAR(100) NOT NULL,
    [tipo_recurso] NVARCHAR(50) NOT NULL,
    [id_recurso] INT NOT NULL,
    CONSTRAINT PK_Metadato PRIMARY KEY CLUSTERED ([id_metadato]),
    CONSTRAINT FK_Metadato_Recurso FOREIGN KEY ([id_recurso]) REFERENCES [dbo].[Recurso]([id_recurso])
);
GO

CREATE TABLE [dbo].[Metrica](
    [id_metrica] INT IDENTITY(1,1) NOT NULL,
    [tipo_evento] NVARCHAR(50) NOT NULL,
    [fecha_evento] DATETIME NOT NULL DEFAULT (GETDATE()),
    [id_recurso] INT NOT NULL,
    CONSTRAINT PK_Metrica PRIMARY KEY CLUSTERED ([id_metrica]),
    CONSTRAINT FK_Metrica_Recurso FOREIGN KEY ([id_recurso]) REFERENCES [dbo].[Recurso]([id_recurso])
);
GO

CREATE TABLE [dbo].[Notificacion](
    [id_notificacion] INT IDENTITY(1,1) NOT NULL,
    [tipo] NVARCHAR(50) NOT NULL,
    [mensaje] NVARCHAR(255) NOT NULL,
    [fecha_envio] DATETIME NOT NULL DEFAULT (GETDATE()),
    [leido] BIT NOT NULL DEFAULT (0),
    [id_usuario] INT NOT NULL,
    [id_recurso] INT NULL,
    CONSTRAINT PK_Notificacion PRIMARY KEY CLUSTERED ([id_notificacion]),
    CONSTRAINT FK_Notificacion_Usuario FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[Usuario]([id_usuario]),
    CONSTRAINT FK_Notificacion_Recurso FOREIGN KEY ([id_recurso]) REFERENCES [dbo].[Recurso]([id_recurso])
);
GO

CREATE TABLE [dbo].[Validacion](
    [id_validacion] INT IDENTITY(1,1) NOT NULL,
    [resultado] NVARCHAR(50) NOT NULL,
    [comentario] NVARCHAR(MAX) NULL,
    [fecha] DATETIME NOT NULL DEFAULT (GETDATE()),
    [id_recurso] INT NOT NULL,
    [id_gestor] INT NOT NULL,
    CONSTRAINT PK_Validacion PRIMARY KEY CLUSTERED ([id_validacion]),
    CONSTRAINT FK_Validacion_Recurso FOREIGN KEY ([id_recurso]) REFERENCES [dbo].[Recurso]([id_recurso]),
    CONSTRAINT FK_Validacion_Gestor FOREIGN KEY ([id_gestor]) REFERENCES [dbo].[Gestor]([id_gestor])
);
GO

CREATE TABLE [dbo].[Version](
    [id_version] INT IDENTITY(1,1) NOT NULL,
    [numero_version] INT NULL CHECK ([numero_version] >= 1),
    [autor] NVARCHAR(50) NOT NULL,
    [fecha] DATETIME NOT NULL,
    [id_recurso] INT NOT NULL,
    CONSTRAINT PK_Version PRIMARY KEY CLUSTERED ([id_version]),
    CONSTRAINT FK_Version_Recurso FOREIGN KEY ([id_recurso]) REFERENCES [dbo].[Recurso]([id_recurso])
);
GO

IF COL_LENGTH('dbo.Usuario', 'IntentosFallidos') IS NULL
BEGIN
    ALTER TABLE dbo.Usuario
    ADD IntentosFallidos INT NOT NULL CONSTRAINT DF_Usuario_IntentosFallidos DEFAULT(0);
END
GO

IF COL_LENGTH('dbo.Usuario', 'BloqueadoHasta') IS NULL
BEGIN
    ALTER TABLE dbo.Usuario
    ADD BloqueadoHasta DATETIME NULL;
END
GO