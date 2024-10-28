using CrudUsuario.Data;
using CrudUsuario.Models;
using CrudUsuario.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace CrudUsuario.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly CadastroUsuarioDBContex _dbCo0ntext;

        public ProdutoRepositorio(CadastroUsuarioDBContex cadastroUsuarioDBContex)
        {
            _dbCo0ntext = cadastroUsuarioDBContex;
        }

        public async Task<ProdutoModel> AdicionarProdutos(ProdutoModel produtoModel)
        {
            await _dbCo0ntext.Produtos.AddAsync(produtoModel);
            await _dbCo0ntext.SaveChangesAsync();
            return produtoModel;
        }

        public async Task<ProdutoModel> AtualizarProdutos(ProdutoModel produtoModel, int Id)
        {
            ProdutoModel produtoPorId = await BuscarPorID(Id);

            if (produtoPorId == null)
            {
                throw new Exception($"Produto pora o id: {Id} não foi encontrado no banco de dados");
            }


            produtoPorId.nome = produtoModel.nome;
            produtoPorId.status = produtoModel.status;
            produtoPorId.preco = produtoModel.preco;
            produtoPorId.descricao = produtoModel.descricao;

            _dbCo0ntext.Produtos.Update(produtoPorId);
            await _dbCo0ntext.SaveChangesAsync();

            return produtoModel;

        }

        public async Task<ProdutoModel> BuscarPorID(int Id)
        {
            return await _dbCo0ntext.Produtos.FirstOrDefaultAsync(item => item.Id == Id);
        }

        public async Task<List<ProdutoModel>> BuscarTodosProdutos()
        {
           return await _dbCo0ntext.Produtos.ToListAsync();
        }

        public async Task<bool> ExcluirProdutos(int Id)
        {
            ProdutoModel produtoPorId = await BuscarPorID(Id);

            if (produtoPorId == null)
            {
                throw new Exception($"Produto pora o id: {Id} não foi encontrado no banco de dados");
            }

            _dbCo0ntext.Produtos.Remove(produtoPorId);
            await _dbCo0ntext.SaveChangesAsync();
            return true;
        }
    }
}
