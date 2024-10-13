using CrudUsuario.Models;


namespace CrudUsuario.Repositorios.Interfaces
{
    //04 criando interface 
    /// criando funcoes do usuario
    public interface IUsuarioRepositorio
    {

        ///funcoes assincronas de usuario
        Task<List<UsuarioModel>> BuscarTodosUsuarios();
        Task<UsuarioModel> BuscarPorID(int id);

        Task<UsuarioModel> Adicionar(UsuarioModel usuario);

        Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id);

        Task<bool> Excluir(int id);
    }
}
