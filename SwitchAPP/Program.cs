using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Switch.Domain.Entities;
using Switch.Infra.CrossCutting.Logging;
using Switch.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SwitchAPP
{
    class Program
    {
        static void Main(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<SwitchContext>();

            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseMySql("Server=DESKTOP-E9PE89C;userid='leo';password=123;database=SwitchDB;", m => m.MigrationsAssembly("Switch.Infra.Data").MaxBatchSize(1000));
            optionsBuilder.EnableSensitiveDataLogging();
                                    
            try
            {
                using (var dbcontext = new SwitchContext(optionsBuilder.Options))
                {
                    dbcontext.GetService<ILoggerFactory>().AddProvider(new Logger());

                    var usuarioNovoLeo = dbcontext.Usuarios.FirstOrDefault(u => u.Nome == "usuarioNovoLeo");

                    var instuicaoEnsino = new InstituicaoEnsino() { Nome = "Faculdate Bilogia" };

                    usuarioNovoLeo.InstituicoesEnsino.Add(instuicaoEnsino);

                    dbcontext.SaveChanges();

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                Console.ReadKey();
            }

            //Console.WriteLine("Ok!");
            Console.ReadKey();
        }

        private static void AulaExibirChangeTracker(SwitchContext dbcontext)
        {            
            var usuario0 = CriarUsuario("usuario0");
            Console.WriteLine("Criando usuario0..");
            Console.WriteLine("Verificando o ChangeTracker de usuario0");
            dbcontext.Usuarios.Add(usuario0);
            ExibirChangeTracker(dbcontext.ChangeTracker);

            // #region Operations

            ////Obtendo
            var usuario1 = dbcontext.Usuarios.FirstOrDefault(u => u.Nome == "usuarioNovo1");
            Console.WriteLine("Obtendo usuario1");
            Console.WriteLine("Verificando o ChangeTracker de usuario1");
            ExibirChangeTracker(dbcontext.ChangeTracker);

            ////Editando
            Console.WriteLine("Editando usuario1");
            usuario1.Nome = "NovoNomeUsuario";
            Console.WriteLine("Verificando o ChangeTracker de usuario1");
            ExibirChangeTracker(dbcontext.ChangeTracker);

            ////Adicionando Novo
            var usuarioNovo2 = CriarUsuario("usuarioNovo2");
            Console.WriteLine("Adicionando usuarioNovo2");
            dbcontext.Usuarios.Add(usuarioNovo2);
            Console.WriteLine("Verificando o ChangeTracker de usuarioNovo2");
            ExibirChangeTracker(dbcontext.ChangeTracker);

            ////Deletando
            Console.WriteLine("Deletando usuario1");
            Console.WriteLine("Verificando o ChangeTracker de usuario1");
            dbcontext.Usuarios.Remove(usuario1);
            ExibirChangeTracker(dbcontext.ChangeTracker);

            ////Detached/desanexado
            var usuario3 = CriarUsuario("Usuario3");
            Console.WriteLine("Status do Usuario3");
            Console.WriteLine(dbcontext.Entry(usuario3).State);
            //#endregion
        }

        private static void CriarUsuarios()
        {
            Usuario usuario1;
            Usuario usuario2;
            Usuario usuario3;
            Usuario usuario4;
            Usuario usuario5;
            Usuario usuario6;
            
            usuario1 = CriarUsuario("usuario1");
            usuario2 = CriarUsuario("usuario2");
            usuario3 = CriarUsuario("usuario3");
            usuario4 = CriarUsuario("usuario4");
            usuario5 = CriarUsuario("usuario5");
            usuario6 = CriarUsuario("usuario6");

            //List<Usuario> usuarios = new List<Usuario>() { usuario1, usuario2, usuario3, usuario4, usuario5, usuario6 };
        }


        public static Usuario CriarUsuario(string nome)
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

        private static void EnviarMensagensAmigos(Usuario usuario)
        {
            
        }

        private static void AtualizarDadosContato(Usuario usuario)
        {
            
        }

        public static void ExibirChangeTracker(ChangeTracker changeTracker)
        {            
            foreach(var entry in changeTracker.Entries())
            {
                Console.WriteLine("Nome da Instancia: " + entry.Entity.GetType().FullName);
                Console.WriteLine("Status da Entidade: " + entry.State);
                Console.WriteLine("-------------");                
            }

            Console.WriteLine("");
            Console.WriteLine("");
        } 
    }
}
