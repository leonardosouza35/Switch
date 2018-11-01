using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Switch.Domain.Entities;
using Switch.Infra.CrossCutting.Logging;
using Switch.Infra.Data.Context;
using System;
using System.Collections.Generic;

namespace SwitchAPP
{
    class Program
    {
        static void Main(string[] args)
        {
            Usuario usuario1;
            Usuario usuario2;
            Usuario usuario3;
            Usuario usuario4;
            Usuario usuario5;
            Usuario usuario6;

            Usuario CriarUsuario(string nome)
            {
                return new Usuario()
                {
                    Nome = nome,
                    SobreNome = "SobreUsuario",
                    Senha = "abc123",
                    Email = "usuario@teste.com",
                    DataNascimento = DateTime.Now,
                    Sexo = Switch.Domain.Enums.SexoEnum.Masculino,
                    UrlFoto = @"c:\temp"
                };
            }

            usuario1 = CriarUsuario("usuario1");
            usuario2 = CriarUsuario("usuario2");
            usuario3 = CriarUsuario("usuario3");
            usuario4 = CriarUsuario("usuario4");
            usuario5 = CriarUsuario("usuario5");
            usuario6 = CriarUsuario("usuario6");

            List<Usuario> usuarios = new List<Usuario>() { usuario1, usuario2, usuario3, usuario4, usuario5, usuario6 };
            


            var optionsBuilder = new DbContextOptionsBuilder<SwitchContext>();
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseMySql("Server=localhost;userid=root;password=admin;database=SwitchDB;", m => m.MigrationsAssembly("Switch.Infra.Data").MaxBatchSize(100));
            
            

            try
            {
                using (var dbcontext = new SwitchContext(optionsBuilder.Options))
                {
                    dbcontext.GetService<ILoggerFactory>().AddProvider(new Logger());

                    dbcontext.Usuarios.AddRange(usuarios);
                    
                    dbcontext.SaveChanges();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

            Console.WriteLine("Ok!");
            Console.ReadKey();
        }
    }
}
