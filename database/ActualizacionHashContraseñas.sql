USE [DataVaultDb];
GO

UPDATE dbo.Usuario
SET contrasena = 'AQAAAAIAAYagAAAAEC3chwF8gZD3YZhtncb+511zCk3vNrqAhZ0yB73jVoKDqaSjYhTnyiXnD6wxnfCp7g=='
WHERE correo = 'ana.perez@est.univalle.edu';

UPDATE dbo.Usuario
SET contrasena = 'AQAAAAIAAYagAAAAEOLpx/vpJXLJLRI6UunbZUdrnrA1DZw21Vmn4u/Jy+YQj98W9ONlXUMuqGYUxhJXyw=='
WHERE correo = 'bruno.rojas@est.univalle.edu';

UPDATE dbo.Usuario
SET contrasena = 'AQAAAAIAAYagAAAAEBUgXgBuIHjmWeRj5CcVQTBSMXuFcCmyRTbMdqQnGGn70lZhpNQgDnGFKl/A0wljKQ=='
WHERE correo = 'carla.soruco@est.univalle.edu';

UPDATE dbo.Usuario
SET contrasena = 'AQAAAAIAAYagAAAAEPn5EzzBrfnX8/bhB5nN7acG1iC7YAu/wZLKf/IYZvw70l62aes+2Sj2ptEiSR9Jrw=='
WHERE correo = 'diego.alvarez@est.univalle.edu';

UPDATE dbo.Usuario
SET contrasena = 'AQAAAAIAAYagAAAAEBZwYa2bkPcp0qQnamAC/I1dN9ddK6AX7QfNg1c5e5uNkHGOPYbq3vs06OcwKAiGtg=='
WHERE correo = 'elena.mamani@est.univalle.edu';

UPDATE dbo.Usuario
SET contrasena = 'AQAAAAIAAYagAAAAEGiUBj+ArY0eySUcTF+JCwt5XkfdbvcF1EjdMFjJ8pnOzPKTuU08KzsWnEjjrDYHNw=='
WHERE correo = 'fernando.quispe@est.univalle.edu';

UPDATE dbo.Usuario
SET contrasena = 'AQAAAAIAAYagAAAAEFuY6WrPmRHliJPj9nlxSMIZdSODcUPWRdwCFJ9zI8sqXd0GrdJsb9YlwH88eeJMnQ=='
WHERE correo = 'juan.gomez@est.univalle.edu';

UPDATE dbo.Usuario
SET contrasena = 'AQAAAAIAAYagAAAAEDpMWgWIdqq7jZReLiDdcInr55Sm8L2jRdq9Cs1mZJpb/oAfNGLCYlzA6aBpBDMipw=='
WHERE correo = 'marcela.vargas@est.univalle.edu';

UPDATE dbo.Usuario
SET contrasena = 'AQAAAAIAAYagAAAAEEqNEJRxCW5ZHEdOt6gXkXQ1DD523GuTGDlwBNOr6KKD8J6SN/FOQx2MIgmDz4x47A=='
WHERE correo = 'laura.ortiz@est.univalle.edu';

UPDATE dbo.Usuario
SET contrasena = 'AQAAAAIAAYagAAAAEEHZQHGHt8qgqikDH7TXrWjcE9gbpzKWG87Qdd1HT+MYNm8hd/ahw+tMxb7tWooDcg=='
WHERE correo = 'pablo.mamani@est.univalle.edu';

UPDATE dbo.Usuario
SET contrasena = 'AQAAAAIAAYagAAAAEOM+HAxvYQng/wsZCunIUkdjVjOKTVzVb0HPHLPxgCcLWcELhU0v2Vyhslx63QX08Q=='
WHERE correo = 'sofia.ti@est.univalle.edu';
GO

UPDATE dbo.Usuario
SET IntentosFallidos = 0, BloqueadoHasta = NULL;
GO

