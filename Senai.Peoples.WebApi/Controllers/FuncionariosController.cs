using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using Senai.Peoples.WebApi.Repositories;

namespace Senai.Peoples.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private IFuncionarioRepository _funcionarioRepository { get; set; }

        public FuncionariosController()
        {
            _funcionarioRepository = new FuncionarioRepository();
        }

        [HttpGet]
        public IEnumerable<FuncionarioDomain> Get()
        {
            return _funcionarioRepository.Listar();
        }

        [HttpPost]
        public IActionResult Post(FuncionarioDomain novoFilme)
        {
            _funcionarioRepository.Adicionar(novoFilme);
            return StatusCode(201);
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            FuncionarioDomain filmeBuscar = _funcionarioRepository.BuscarPorId(id);

            if (filmeBuscar == null)
            {
                return NotFound("nenhum gênero encontrado");
            }
            return Ok(filmeBuscar);

        }



        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, FuncionarioDomain funcionarioAtulizado)
        {
            FuncionarioDomain funcionarioBuscar = _funcionarioRepository.BuscarPorId(id);

            if (funcionarioBuscar == null)
            {
                return NotFound
                    (
                    new
                    {
                        mensagem = "Gênero não encontrado",
                        erro = true
                    }
                    );
            }

            try
            {
                _funcionarioRepository.AtualizarIdUrl(id, funcionarioAtulizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _funcionarioRepository.Deletar(id);

            return Ok("Gênero deletado");
        }
    }
}