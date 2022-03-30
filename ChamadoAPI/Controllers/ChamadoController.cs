using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DataStructure;
using Application.Enums;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Models;

namespace ChamadoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamadoController : ControllerBase
    {
        private readonly IChamadoService _chamadoService;
        private readonly ILogger _logger;

        public ChamadoController(IChamadoService chamadoService, ILogger<ChamadoController> logger)
        {
            _chamadoService = chamadoService;
            _logger = logger;
        }

        [HttpPost("{cliente:int}")]
        public async Task<IActionResult> AbrirChamado([FromBody]Chamado chamado, int cliente)
        {
            var values = Request.Headers["Origin"];
            _logger.LogInformation(values);
            var request = await _chamadoService.AbrirChamado(chamado, cliente);
            if (request.Type == ServiceResultType.Success)
            {
                if (request is ServiceResult<string> resultado)
                {
                    return new JsonResult(resultado.Result);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> ListarChamados()
        {
           var result = await _chamadoService.ListarChamadosAsync();
           if (result.Type == ServiceResultType.Success)
           {
               if (result is ServiceResult<List<Chamado>> resultado)
               {
                   return new JsonResult(resultado.Result);
               }
           }
                
           return BadRequest();
        }
        
        [HttpGet("{idChamado:int}")]
        public async Task<IActionResult> ObterChamado(int idChamado)
        {
            var result = await _chamadoService.GetChamadoAsync(idChamado);
            if (result.Type == ServiceResultType.Success)
            {
                if (result is ServiceResult<Chamado> resultado)
                {
                    return new JsonResult(resultado.Result);
                }
            }
                
            return BadRequest();
        }

        [HttpPost("{protocolo}")]
        public async Task<IActionResult> ProcurarChamado(string protocolo)
        {
            var result = await _chamadoService.BuscarChamadoPorProtocolo(protocolo);
            if (result.Type == ServiceResultType.Success)
            {
                if (result is ServiceResult<Chamado> resultado)
                {
                    return new JsonResult(resultado.Result);
                }
            }
                
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> ResponderChamado(Chamado chamado)
        {
            var result = await _chamadoService.MudarChamado(chamado);
            if (result.Type == ServiceResultType.Success)
            {
                return Ok();
            }
            
            return BadRequest();
        }

    }
}