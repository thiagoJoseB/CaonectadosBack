using CrudUsuario.Data;
using CrudUsuario.Models;
using CrudUsuario.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


namespace CrudUsuario.Repositorios
{
    //5 
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly CadastroUsuarioDBContex _dbCo0ntext;
        public UsuarioRepositorio(CadastroUsuarioDBContex cadastroUsuarioDBContex) 
        {
            _dbCo0ntext = cadastroUsuarioDBContex;
        }

        public async Task<UsuarioModel> BuscarPorID(int id)
        {
            ///buscando usuarios do banco por id
            return await _dbCo0ntext.Usuarios.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            //// buscando uma lista de usuario do banco
            return await _dbCo0ntext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            ///adiciona usuario e confirma com saveChanges
            ///
            var emailCadastrado = await _dbCo0ntext.Usuarios.FirstOrDefaultAsync(e => e.Email == usuario.Email);


            if (emailCadastrado != null)
            {
                throw new Exception("Email já existente no banco de dados");
            }

            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
            await _dbCo0ntext.Usuarios.AddAsync(usuario);
           await _dbCo0ntext.SaveChangesAsync();

            return usuario;
        }

       
        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
           UsuarioModel usuarioPorId = await BuscarPorID(id);


            var emailCadastrado = await _dbCo0ntext.Usuarios.FirstOrDefaultAsync(e => e.Email == usuario.Email && e.Id != id);

            if (emailCadastrado != null)
            {
                throw new Exception("Email já existente no banco de dados");
            }

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario pora o id: {id} não fou encontrado no banco de dados");
            }

           
            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Telefone = usuario.Telefone;
            usuarioPorId.Email = usuario.Email;
            usuarioPorId.Senha = usuario.Senha;

            if (!string.IsNullOrEmpty(usuario.Senha))
            {
                usuarioPorId.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
            }

            _dbCo0ntext.Usuarios.Update(usuarioPorId);
              await  _dbCo0ntext.SaveChangesAsync();

            return usuarioPorId;
        }

        

        public async Task<bool> Excluir(int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorID(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario pora o id: {id} não fou encontrado no banco de dados");
            }

            _dbCo0ntext.Usuarios.Remove(usuarioPorId);
            await  _dbCo0ntext.SaveChangesAsync();
            return true;

        }
    }
}
