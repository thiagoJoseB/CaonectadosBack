using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using CrudUsuario.Models;
///03 responsavel pelas tabelas do banco 
namespace CrudUsuario.Data
{
    public class CadastroUsuarioDBContex : DbContext
    {

        public CadastroUsuarioDBContex(DbContextOptions<CadastroUsuarioDBContex> options) : base(options) 
        { }
      
        //tabela do banco;
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }

   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
