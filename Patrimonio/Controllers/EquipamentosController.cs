using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patrimonio.Contexts;
using Patrimonio.Domains;
using Patrimonio.Interfaces;
using Patrimonio.Utils;

namespace Patrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentosController : ControllerBase
    {
        private readonly IEquipamentoRepository _equipamentoRepository;

        public EquipamentosController(IEquipamentoRepository repo)
        {
            _equipamentoRepository = repo;
        }

        // GET: api/Equipamentos
        [HttpGet]
        public IActionResult GetEquipamentos()
        {
            return Ok(new { ListaEquipamentos = _equipamentoRepository.Listar() });
        }

        // GET: api/Equipamentos/5
        [HttpGet("{id}")]
        public IActionResult GetEquipamento(int id)
        {
            Equipamento equipamento = _equipamentoRepository.BuscarPorID(id);

            if (equipamento == null)
            {
                return NotFound( new { msg = "Não encontrado" });
            }

            return Ok(new
            {
                Equipamento = equipamento
            });
        }

        // PUT: api/Equipamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutEquipamento(int id, Equipamento equipamento)
        {

            try
            {
                if (id != equipamento.Id)
                {
                    return BadRequest(new { msg = "Id da url e id do equipamento não são iguais" });
                }

                _equipamentoRepository.Alterar(equipamento);
            
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipamentoExists(id))
                {
                    return NotFound(new { msg = "Equipamento não encontrado"});
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Equipamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostEquipamento([FromForm] Equipamento equipamento, IFormFile arquivo)
        {

            #region Upload da Imagem com extensões permitidas apenas
                string[] extensoesPermitidas = { "jpg", "png", "jpeg", "gif" };
                string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas);

                if (uploadResultado == "")
                {
                    return BadRequest("Arquivo não encontrado");
                }

                if (uploadResultado == "Extensão não permitida")
                {
                    return BadRequest("Extensão de arquivo não permitida");
                }

                equipamento.Imagem = uploadResultado; 
            #endregion

            // Pegando o horário do sistema
            equipamento.DataCadastro = DateTime.Now;

            _equipamentoRepository.Cadastrar(equipamento);

            return Created("Equipamento", equipamento);
        }

        // DELETE: api/Equipamentos/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEquipamento(int id)
        {
            var equipamento = _equipamentoRepository.BuscarPorID(id);
            if (equipamento == null)
            {
                return NotFound(new { msg = "Equipamento não encontrado" });
            }

            _equipamentoRepository.Excluir(equipamento);

            // Removendo Arquivo do servidor
            Upload.RemoverArquivo(equipamento.Imagem);

            return NoContent();
        }

        private bool EquipamentoExists(int id)
        {
            if (_equipamentoRepository.BuscarPorID(id) != null)
            {
                return true;
            }
            else return false;
        }
    }
}
