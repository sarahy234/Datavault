USE [DataVaultDb];
GO

PRINT 'Inserting Usuarios...';

INSERT INTO dbo.Usuario (nombre, correo, contrasena, rol, activo)
VALUES 
('AnaPérez',             'ana.perez@est.univalle.edu',    'TestPass!1', 'Estudiante', 1),
('BrunoRojas',           'bruno.rojas@est.univalle.edu',  'TestPass!1', 'Estudiante', 1),
('CarlaSoruco',          'carla.soruco@est.univalle.edu', 'TestPass!1', 'Estudiante', 1),
('DiegoAlvarez',         'diego.alvarez@est.univalle.edu','TestPass!1', 'Estudiante', 1),
('ElenaMamani',          'elena.mamani@est.univalle.edu', 'TestPass!1', 'Estudiante', 1),
('FernandoQuispe',       'fernando.quispe@est.univalle.edu','TestPass!1','Estudiante', 1),
('JuanGomez',            'juan.gomez@est.univalle.edu',   'TestPass!1', 'Docente', 1),
('MarcelaVargas',        'marcela.vargas@est.univalle.edu','TestPass!1','Docente',1),
('LauraOrtiz',           'laura.ortiz@est.univalle.edu',  'TestPass!1', 'Gestor', 1),
('PabloMamani',          'pablo.mamani@est.univalle.edu', 'TestPass!1', 'Coordinacion',1),
('SofiaTI',              'sofia.ti@est.univalle.edu',     'TestPass!1', 'TI', 1);

GO

PRINT 'Inserting role-specific rows...';

-- Docentes
INSERT INTO dbo.Docente (id_usuario, departamento)
VALUES
((SELECT id_usuario FROM dbo.Usuario WHERE correo = 'juan.gomez@est.univalle.edu'), 'Departamento de Ingeniería de Sistemas'),
((SELECT id_usuario FROM dbo.Usuario WHERE correo = 'marcela.vargas@est.univalle.edu'), 'Departamento de Programación');

-- Estudiantes
INSERT INTO dbo.Estudiante (id_usuario, semestre, carrera)
VALUES
((SELECT id_usuario FROM dbo.Usuario WHERE correo = 'ana.perez@est.univalle.edu'), '1', 'Ingeniería de Sistemas'),
((SELECT id_usuario FROM dbo.Usuario WHERE correo = 'bruno.rojas@est.univalle.edu'), '2', 'Ingeniería de Sistemas'),
((SELECT id_usuario FROM dbo.Usuario WHERE correo = 'carla.soruco@est.univalle.edu'), '3', 'Ingeniería de Sistemas'),
((SELECT id_usuario FROM dbo.Usuario WHERE correo = 'diego.alvarez@est.univalle.edu'), '4', 'Ingeniería de Sistemas'),
((SELECT id_usuario FROM dbo.Usuario WHERE correo = 'elena.mamani@est.univalle.edu'), '7', 'Ingeniería de Sistemas'),
((SELECT id_usuario FROM dbo.Usuario WHERE correo = 'fernando.quispe@est.univalle.edu'), '8', 'Ingeniería de Sistemas');

-- Gestor
INSERT INTO dbo.Gestor (id_usuario, area_responsable)
VALUES ((SELECT id_usuario FROM dbo.Usuario WHERE correo = 'laura.ortiz@est.univalle.edu'), 'Biblioteca y Curaduría');

-- Coordinacion
INSERT INTO dbo.Coordinacion (id_usuario, cargo)
VALUES ((SELECT id_usuario FROM dbo.Usuario WHERE correo = 'pablo.mamani@est.univalle.edu'), 'Coordinador Académico');

-- TI
INSERT INTO dbo.TI (id_usuario, especialidad)
VALUES ((SELECT id_usuario FROM dbo.Usuario WHERE correo = 'sofia.ti@est.univalle.edu'), 'Infraestructura y Seguridad');

GO

PRINT '--- Inserting recursos, metadatos, versiones, validaciones, metricas y notificaciones (single batch) ---';

-- En un mismo batch: obtenemos ids de docentes, creamos recursos y luego usamos variables @r1..@r10
DECLARE @docJuan INT = (SELECT TOP 1 d.id_docente FROM dbo.Docente d JOIN dbo.Usuario u ON d.id_usuario = u.id_usuario WHERE u.correo = 'juan.gomez@est.univalle.edu');
DECLARE @docMarcela INT = (SELECT TOP 1 d.id_docente FROM dbo.Docente d JOIN dbo.Usuario u ON d.id_usuario = u.id_usuario WHERE u.correo = 'marcela.vargas@est.univalle.edu');

IF @docJuan IS NULL OR @docMarcela IS NULL
BEGIN
    RAISERROR('Faltan docentes (juan o marcela). Revisa inserciones previas.',16,1);
    RETURN;
END

-- Insertar recursos (vinculados a docentes)
INSERT INTO dbo.Recurso (titulo, descripcion, autor, semestre, tamano, fecha_subida, estado, id_docente)
VALUES
('Introducción a Bases de Datos', 'Apuntes y ejemplos de SQL: SELECT, JOINs, índices y normalización.', 'Juan Gomez', '1', 10240, DATEADD(day,-12,GETDATE()), 'Publicado', @docJuan),
('Estructuras de Datos - Árboles', 'Diapositivas con definiciones y ejercicios sobre árboles binarios y AVL.', 'Juan Gomez', '2', 20480, DATEADD(day,-10,GETDATE()), 'Publicado', @docJuan),
('Programación Web ASP.NET - Proyecto', 'Proyecto ejemplo con controladores, vistas y modelos.', 'Marcela Vargas', '3', 30720, DATEADD(day,-8,GETDATE()), 'Publicado', @docMarcela),
('Algoritmos Avanzados - Backtracking', 'Problemas resueltos y prácticas de backtracking y PD.', 'Juan Gomez', '7', 40960, DATEADD(day,-6,GETDATE()), 'Pendiente', @docJuan),
('Redes de Computadoras - Laboratorio', 'Guía y prácticas de laboratorio: captura de paquetes y análisis TCP/UDP.', 'Marcela Vargas', '4', 15360, DATEADD(day,-5,GETDATE()), 'Publicado', @docMarcela),
('Sistemas Operativos - Guía', 'Apuntes de gestión de procesos, hilos y memoria.', 'Juan Gomez', '5', 25600, DATEADD(day,-4,GETDATE()), 'Publicado', @docJuan),
('Ingeniería de Software - UML y Casos', 'Ejemplos de diagramas UML, casos de uso y buenas prácticas.', 'Marcela Vargas', '6', 12288, DATEADD(day,-3,GETDATE()), 'Publicado', @docMarcela),
('Bases de Datos Avanzadas - Indices', 'Material sobre índices, planes de ejecución y optimización.', 'Juan Gomez', '2', 11264, DATEADD(day,-2,GETDATE()), 'Publicado', @docJuan),
('Prácticas de Programación - Java', 'Colección de ejercicios resueltos en Java.', 'Marcela Vargas', '1', 18432, DATEADD(day,-1,GETDATE()), 'Publicado', @docMarcela),
('Seguridad Informática - Introducción', 'Conceptos básicos de criptografía y seguridad en redes.', 'Juan Gomez', '8', 22016, GETDATE(), 'Publicado', @docJuan);

-- Recuperar ids de recursos recién insertados (por títulos únicos)
DECLARE @r1 INT = (SELECT TOP 1 id_recurso FROM dbo.Recurso WHERE titulo = 'Introducción a Bases de Datos');
DECLARE @r2 INT = (SELECT TOP 1 id_recurso FROM dbo.Recurso WHERE titulo = 'Estructuras de Datos - Árboles');
DECLARE @r3 INT = (SELECT TOP 1 id_recurso FROM dbo.Recurso WHERE titulo = 'Programación Web ASP.NET - Proyecto');
DECLARE @r4 INT = (SELECT TOP 1 id_recurso FROM dbo.Recurso WHERE titulo = 'Algoritmos Avanzados - Backtracking');
DECLARE @r5 INT = (SELECT TOP 1 id_recurso FROM dbo.Recurso WHERE titulo = 'Redes de Computadoras - Laboratorio');
DECLARE @r6 INT = (SELECT TOP 1 id_recurso FROM dbo.Recurso WHERE titulo = 'Sistemas Operativos - Guía');
DECLARE @r7 INT = (SELECT TOP 1 id_recurso FROM dbo.Recurso WHERE titulo = 'Ingeniería de Software - UML y Casos');
DECLARE @r8 INT = (SELECT TOP 1 id_recurso FROM dbo.Recurso WHERE titulo = 'Bases de Datos Avanzadas - Indices');
DECLARE @r9 INT = (SELECT TOP 1 id_recurso FROM dbo.Recurso WHERE titulo = 'Prácticas de Programación - Java');
DECLARE @r10 INT = (SELECT TOP 1 id_recurso FROM dbo.Recurso WHERE titulo = 'Seguridad Informática - Introducción');

-- Verificar que las variables de recurso no sean NULL (evita FK fails)
IF @r1 IS NULL OR @r2 IS NULL OR @r3 IS NULL OR @r4 IS NULL OR @r5 IS NULL OR @r6 IS NULL OR @r7 IS NULL OR @r8 IS NULL OR @r9 IS NULL OR @r10 IS NULL
BEGIN
    RAISERROR('Algún recurso no se insertó correctamente. Revisa títulos duplicados o errores.',16,1);
    RETURN;
END

-- Insertar metadatos (vinculados por id_recurso)
INSERT INTO dbo.Metadato (titulo, materia, palabras_clave, tipo_recurso, id_recurso)
VALUES
('Apuntes BD', 'Bases de Datos', 'sql,joins,indices,normalizacion', 'Apuntes', @r1),
('Árboles - Ejercicios', 'Estructuras de Datos', 'arboles,bfs,dfs,recursion,avl', 'Diapositiva', @r2),
('ASP.NET MVC - Proyecto', 'Programación Web', 'asp.net,mvc,razor,controllers,views', 'Proyecto', @r3),
('Backtracking - Prácticas', 'Algoritmos', 'backtracking,pd,recursion', 'Ejercicios', @r4),
('Redes - Laboratorio', 'Redes de Computadoras', 'tcp,udp,wireshark', 'Laboratorio', @r5),
('SO - Procesos y Memoria', 'Sistemas Operativos', 'procesos,hilos,memoria,swap', 'Guía', @r6),
('UML - Casos', 'Ingeniería de Software', 'uml,casos,diagramas', 'Documento', @r7),
('Índices y Optimización', 'Bases de Datos', 'indices,optimización,plan-ejecucion', 'Artículo', @r8),
('Java - Colección ejercicios', 'Programación', 'java,ejercicios,estructuras', 'Ejercicios', @r9),
('Seguridad - Conceptos', 'Seguridad Informática', 'criptografía,hash,confidencialidad', 'Apuntes', @r10);

-- Insertar 1 versión por recurso
INSERT INTO dbo.Version (numero_version, autor, fecha, id_recurso)
VALUES
(1, 'Juan Gomez', DATEADD(day,-12,GETDATE()), @r1),
(1, 'Juan Gomez', DATEADD(day,-10,GETDATE()), @r2),
(1, 'Marcela Vargas', DATEADD(day,-8,GETDATE()), @r3),
(1, 'Juan Gomez', DATEADD(day,-6,GETDATE()), @r4),
(1, 'Marcela Vargas', DATEADD(day,-5,GETDATE()), @r5),
(1, 'Juan Gomez', DATEADD(day,-4,GETDATE()), @r6),
(1, 'Marcela Vargas', DATEADD(day,-3,GETDATE()), @r7),
(1, 'Juan Gomez', DATEADD(day,-2,GETDATE()), @r8),
(1, 'Marcela Vargas', DATEADD(day,-1,GETDATE()), @r9),
(1, 'Juan Gomez', GETDATE(), @r10);

-- Obtener id_gestor (Laura) para validaciones
DECLARE @gestorLaura INT = (SELECT TOP 1 g.id_gestor FROM dbo.Gestor g JOIN dbo.Usuario u ON g.id_usuario = u.id_usuario WHERE u.correo = 'laura.ortiz@est.univalle.edu');

IF @gestorLaura IS NULL
BEGIN
    RAISERROR('No se encontró gestor Laura.',16,1);
    RETURN;
END

-- Insertar validaciones (ejemplo)
INSERT INTO dbo.Validacion (resultado, comentario, fecha, id_recurso, id_gestor)
VALUES
('Pendiente', 'Revisión inicial: comprobar formatos y autoría.', DATEADD(day,-5,GETDATE()), @r4, @gestorLaura),
('Aprobado', 'Formato correcto, publicado.', DATEADD(day,-2,GETDATE()), @r2, @gestorLaura);

-- Insertar métricas (vistas/descargas)
INSERT INTO dbo.Metrica (tipo_evento, fecha_evento, id_recurso)
VALUES
('Vista', DATEADD(hour,-48,GETDATE()), @r1),
('Vista', DATEADD(hour,-30,GETDATE()), @r2),
('Descarga', DATEADD(hour,-12,GETDATE()), @r3),
('Vista', DATEADD(hour,-6,GETDATE()), @r5),
('Vista', GETDATE(), @r6);

-- Obtener ids de algunos usuarios estudiantes para notificaciones
DECLARE @ana INT = (SELECT id_usuario FROM dbo.Usuario WHERE correo = 'ana.perez@est.univalle.edu');
DECLARE @bruno INT = (SELECT id_usuario FROM dbo.Usuario WHERE correo = 'bruno.rojas@est.univalle.edu');
DECLARE @carla INT = (SELECT id_usuario FROM dbo.Usuario WHERE correo = 'carla.soruco@est.univalle.edu');

-- Insertar notificaciones
INSERT INTO dbo.Notificacion (tipo, mensaje, fecha_envio, leido, id_usuario, id_recurso)
VALUES
('Publicacion', 'Se ha publicado: Introducción a Bases de Datos', DATEADD(day,-11,GETDATE()), 0, @ana, @r1),
('Publicacion', 'Se ha publicado: Estructuras de Datos - Árboles', DATEADD(day,-9,GETDATE()), 0, @bruno, @r2),
('Aviso', 'Recordatorio: revisión de Algoritmos Avanzados pendiente', DATEADD(day,-4,GETDATE()), 0, @carla, @r4);

PRINT '--- All inserts in single batch completed. ---';
GO
