using CrudUsuario.Data;
using CrudUsuario.Repositorios.Interfaces;
using CrudUsuario.Repositorios; // Adicione a referência do namespace onde está seu repositório
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.Extensions.FileProviders;

namespace CrudUsuario
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adiciona serviços ao contêiner.
            builder.Services.AddControllers();

            // Configura o DbContext para MySQL
            builder.Services.AddDbContext<CadastroUsuarioDBContex>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DataBase");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            // Injeção de dependência para os repositórios
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();

            var app = builder.Build();

            // Configura o pipeline de requisições HTTP.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Adiciona suporte para arquivos estáticos
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
                RequestPath = "/uploads"
            });


            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

           

            app.Run(); // Inicia a aplicação
        }
    }
}
