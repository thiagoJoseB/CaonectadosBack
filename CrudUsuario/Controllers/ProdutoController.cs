using CrudUsuario.Models;
using CrudUsuario.Repositorios;
using CrudUsuario.Repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

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
        public async Task<ActionResult<ProdutoModel>> Cadastrar([FromBody] ProdutoModel produtoModel, IFormFile imagem)
        {
            if (imagem != null && imagem.Length > 0)
            {
                var imagePath = Path.Combine("uploads", imagem.FileName);

               
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imagem.CopyToAsync(stream);
                }

                produtoModel.ImagemUrl = imagePath;
            }
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




        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            // Verifica e cria a pasta de uploads se não existir
            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            if (file == null || file.Length == 0)
                return BadRequest("Arquivo de imagem não encontrado.");

            var filePath = Path.Combine("uploads", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Retorna o caminho da imagem para ser salvo no Produto
            return Ok(new { ImagemUrl = filePath });
        }



    }
}
