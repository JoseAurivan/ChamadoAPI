using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DataStructure;
using Application.Enums;
using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.Services
{
    public class ClienteService:IClienteService
    {
        private readonly IContext _context;

        public ClienteService(IContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult> ListarCliente()
        {
            var cliente = await _context.Clientes.ToListAsync();
            if (cliente is null)
            {
                return new ServiceResult(ServiceResultType.NotValid)
                {
                    Messages = new[]
                    {
                        "Chamados inexistentes"
                    }
                };
            }

            return new ServiceResult<List<Cliente>>(ServiceResultType.Success)
            {
                Result = cliente
            };
        }

        public async Task<ServiceResult> SalvarCliente(Cliente cliente)
        {
            try
            {
                return await TentarSalvarCliente(cliente);
            }
            catch (Exception e)
            {
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"Erro ao fazer Login"}
                };
            }
        }

        public async Task<ServiceResult> ProcurarClienteId(int id)
        {
           var cliente  =await  _context.Clientes.Where(c => c.Id == id).FirstOrDefaultAsync();
           if (cliente is not null)
           {
               return new ServiceResult<Cliente>(ServiceResultType.Success)
               {
                    Result = cliente
               }; 
           }

           return new ServiceResult(ServiceResultType.NotValid)
           {
               Messages = new[]
               {
                   "Cliente nao encontrado"
               }
           };
        }

        public async Task<ServiceResult> ProcurarClienteCpf(string cpf)
        {
            var cliente  =await  _context.Clientes.Where(c => c.Cpf == cpf).FirstOrDefaultAsync();
            if (cliente is not null)
            {
                return new ServiceResult<Cliente>(ServiceResultType.Success)
                {
                    Result = cliente
                }; 
            }

            return null;
        }

        public async Task<ServiceResult> TentarSalvarCliente(Cliente cliente)
        {
            if (cliente is null)
            {
                return new ServiceResult(ServiceResultType.NotValid)
                {
                    Messages = new[]
                    {
                        "Erro ao econtrar o atendente"
                    }
                };
            }

            var result = await ProcurarClienteCpf(cliente.Cpf);
            
            if (result is not null)
            {
                if (result is ServiceResult<Cliente> resultado)
                {
                    return new ServiceResult<int>(ServiceResultType.Success)
                    {
                        Result = resultado.Result.Id
                    };
                }
            }
            
        
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return new ServiceResult<int>(ServiceResultType.Success)
            {
                Result = cliente.Id
            };


        }


    }
}