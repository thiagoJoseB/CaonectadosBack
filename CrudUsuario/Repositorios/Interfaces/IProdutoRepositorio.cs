using CrudUsuario.Models;

namespace CrudUsuario.Repositorios.Interfaces
{
    public interface IProdutoRepositorio
    {
        Task<List<ProdutoModel>> BuscarTodosProdutos();
        Task<ProdutoModel> BuscarPorID(int Id);

        Task<ProdutoModel> AdicionarProdutos(ProdutoModel produtoModel);

        Task<ProdutoModel> AtualizarProdutos(ProdutoModel produtoModel, int Id);

        Task<bool> ExcluirProdutos(int Id);
    }
}

