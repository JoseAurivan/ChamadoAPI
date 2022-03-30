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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        
        [HttpGet]
        public async Task<IActionResult> ListarClientes()
        {
            var result = await _clienteService.ListarCliente();
            if (result.Type == ServiceResultType.Success)
            {
                if (result is ServiceResult<List<Cliente>> resultado)
                {
                    return new JsonResult(resultado.Result);
                }
            }
                
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Cliente(Cliente cliente)
        {
            var resultado =await _clienteService.SalvarCliente(cliente);
            if (resultado.Type == ServiceResultType.Success)
            {
                if (resultado is ServiceResult<int> result)
                {
                    return new JsonResult(result.Result);
                }
            }
                
            return BadRequest();
        }
    }
}