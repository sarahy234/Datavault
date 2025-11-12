using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositorio.Models; // Usa tus modelos del proyecto principal

class Program
{
    static void Main()
    {
        // Crear una instancia del hasher
        var hasher = new PasswordHasher<Usuario>();

        // Lista de correos de prueba (los mismos que insertaste)
        string[] correos = {
            "ana.perez@est.univalle.edu",
            "bruno.rojas@est.univalle.edu",
            "carla.soruco@est.univalle.edu",
            "diego.alvarez@est.univalle.edu",
            "elena.mamani@est.univalle.edu",
            "fernando.quispe@est.univalle.edu",
            "juan.gomez@est.univalle.edu",
            "marcela.vargas@est.univalle.edu",
            "laura.ortiz@est.univalle.edu",
            "pablo.mamani@est.univalle.edu",
            "sofia.ti@est.univalle.edu"
        };

        // Contraseña original (la misma que usaste en los inserts)
        string passwordPlano = "TestPass!1";

        Console.WriteLine("=== HASHES DE CONTRASEÑAS ===\n");

        foreach (var correo in correos)
        {
            var usuario = new Usuario { Correo = correo };
            string hash = hasher.HashPassword(usuario, passwordPlano);

            Console.WriteLine($"Correo: {correo}");
            Console.WriteLine($"Hash: {hash}\n");
        }

        Console.WriteLine("Copia los hashes generados y actualiza tu tabla Usuario.");
    }
}

