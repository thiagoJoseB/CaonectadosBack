using CrudUsuario.Data;
using CrudUsuario.Repositorios.Interfaces;
using CrudUsuario.Repositorios; // Adicione a referência do namespace onde está seu repositório
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

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

            // Injeção de dependência para o repositório
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>(); // Corrigido aqui

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

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run(); // Inicia a aplicação
        }
    }
}
