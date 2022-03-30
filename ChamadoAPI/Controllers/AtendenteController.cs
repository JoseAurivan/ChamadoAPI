using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DataStructure;
using Application.Enums;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ChamadoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtendenteController : ControllerBase
    {
        private readonly IAtendenteService _atendenteService;

        public AtendenteController(IAtendenteService atendenteService)
        {
            _atendenteService = atendenteService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarAtendentes()
        {
            var result = await _atendenteService.ListarAtendentesAsync();
            if (result.Type == ServiceResultType.Success)
            {
                if (result is ServiceResult<List<Atendente>> resultado)
                {
                    return new JsonResult(resultado.Result);
                }
            }
                
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarAtendente(Atendente atendente)
        {
            var resultado =await _atendenteService.SalvarAtendenteAsync(atendente);
            if (resultado.Type == ServiceResultType.Success)
                return Ok();
            return BadRequest();
        }

        [HttpPost("{cpf}")]
        public async Task<IActionResult> LogarAtendente([FromBody] string email, string cpf)
        {
            var result = await _atendenteService.LogarAtendente(email, cpf);
            if (result.Type == ServiceResultType.Success)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}