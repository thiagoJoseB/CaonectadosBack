using CrudUsuario.Models;
using CrudUsuario.Repositorios;
using CrudUsuario.Repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudUsuario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        private readonly IProdutoRepositorio produtoRepositorio;
        

        public ProdutoController(IProdutoRepositorio produtoRepositorio)
        {
            this.produtoRepositorio = produtoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProdutoModel>>> BuscarTodosUsuarios()
        {
            List<ProdutoModel> produtos = await produtoRepositorio.BuscarTodosProdutos();

            if (produtos == null || produtos.Count == 0)
            {
                return NoContent();
            }
            return Ok(new { message = "produtos encontrados com sucesso", data = produtos });
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoModel>> BuscarPorId(int Id)
        {


            ProdutoModel produtos = await produtoRepositorio.BuscarPorID(Id);

            if (produtos == null)
            {
                return NotFound(new { message = "produtos não encontrado" });

            }
            return Ok(new { message = "produtos encontrado com sucesso", data = produtos });
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoModel>> Cadastrar([FromBody] ProdutoModel produtoModel)
        {
            ProdutoModel produto = await produtoRepositorio.AdicionarProdutos(produtoModel);

            return CreatedAtAction(nameof(BuscarPorId), new { Id = produto.Id }, new { message = "produto cadastrado com sucesso", data = produto });
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoModel>> Atualizar([FromBody] ProdutoModel produtoModel, int Id)
        {
            produtoModel.Id = Id;
            ProdutoModel produto = await produtoRepositorio.AtualizarProdutos(produtoModel, Id);
            if (produtoModel == null)
            {
                return NotFound(new { message = "Não posivel atualizar o produto" });

            }
            return Ok(new { message = "Usuário atualizado com sucesso", data = produtoModel });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoModel>> Excluir(int Id)
        {

            bool apagado = await produtoRepositorio.ExcluirProdutos(Id);

            if (apagado == false)
            {
                return NotFound(new { message = "Não foi possivel excluir o produto" });

            }
            return Ok(new { message = "produto excluido com sucesso" });
        }


    }
}
