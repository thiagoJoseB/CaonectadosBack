using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrudUsuario.Models;
using CrudUsuario.Repositorios.Interfaces;
namespace CrudUsuario.Controllers

{

    /// <summary>
    /// passo 02 criar controller 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase

    {
        private readonly IUsuarioRepositorio usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarTodosUsuarios()
        {
            List<UsuarioModel> usuarios = await usuarioRepositorio.BuscarTodosUsuarios();

            if (usuarios == null || usuarios.Count == 0)
            {
                return NoContent();
            }
            return Ok(new { message ="Usuários encontrados com sucesso" , data = usuarios});
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> BuscarPorId(int id)
        {


            UsuarioModel usuario = await usuarioRepositorio.BuscarPorID(id);

            if (usuario == null) { 
                return NotFound(new {message = "Usuário não encontrado"});
            
            }
            return Ok(new { message = "Usuário encontrado com sucesso", data = usuario });
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Cadastrar([FromBody] UsuarioModel usuarioModel)
        {
            UsuarioModel usuario = await usuarioRepositorio.Adicionar(usuarioModel);

            return CreatedAtAction(nameof(BuscarPorId), new {id = usuario.Id}, new { message = "Usuário cadastrado com sucesso", data = usuario });
            //return Ok(usuario);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> Atualizar([FromBody] UsuarioModel usuarioModel, int id)
        {
            usuarioModel.Id = id;
            UsuarioModel usuario = await usuarioRepositorio.Atualizar(usuarioModel, id);
            if (usuario == null)
            {
                return NotFound(new { message = "Não posivel atualizar o usuário" });

            }
            return Ok(new { message = "Usuário atualizado com sucesso", data = usuario });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioModel>> Excluir( int id)
        {
            
            bool apagado  = await usuarioRepositorio.Excluir(id);

            if (apagado == false)
            {
                return NotFound(new { message = "Não foi possivel excluir o usuário" });

            }
            return Ok(new { message = "Usuário excluido com sucesso"});
        }


    }
}
