using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DataStructure;
using Application.Enums;
using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Enums;

namespace Application.Services
{
    public class ChamadoService : IChamadoService
    {
        private readonly IContext _context;
        private readonly IClienteService _clienteService;

        public ChamadoService(IContext context, IClienteService chamadoService)
        {
            _context = context;
            _clienteService = chamadoService;
        }

        public async Task<ServiceResult> AbrirChamado(Chamado chamado, int cliente)
        {
            try
            {
                return await TentarSalvarChamado(chamado, cliente);
            }
            catch (Exception e)
            {
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"Erro ao fazer Login"}
                };
            }
        }

        //TODO salvar cliente ou buscar o cliente
        private async Task<ServiceResult> TentarSalvarChamado(Chamado chamado, int cliente)
        {
            if (chamado is not null && cliente != 0)
            {
                var quemAbriu = await _clienteService.ProcurarClienteId(cliente);
                if (quemAbriu.Type == ServiceResultType.Success)
                {
                    if (quemAbriu is ServiceResult<Cliente> resultado)
                    {
                        chamado.Cliente = resultado.Result;

                        chamado.DataAbertura = DateTime.Now;
                        if (chamado.Id == default)
                        {
                            chamado.NumeroProtocolo = Guid.NewGuid().ToString();
                            _context.Chamados.Add(chamado);
                            await _context.SaveChangesAsync();
                            return new ServiceResult<string>(ServiceResultType.Success)
                            {
                                Result = chamado.NumeroProtocolo
                            };
                        }

                        return new ServiceResult(ServiceResultType.NotValid)
                        {
                            Messages = new[]
                            {
                                "Chamado ja existe"
                            }
                        };
                    }
                }
            }

            return new ServiceResult(ServiceResultType.NotValid)
            {
                Messages = new[]
                {
                    "Invalidos"
                }
            };
        }

        public async Task<ServiceResult> MudarChamado(Chamado chamado)
        {
            if (chamado is not null)
            {
                _context.Entry(chamado).State = EntityState.Modified;
                if (chamado.StatusChamado == StatusChamado.Concluido)
                {
                    chamado.DataFechado = DateTime.Now;
                } 
                await _context.SaveChangesAsync();
                return new ServiceResult<Chamado>(ServiceResultType.Success)
                {
                    Result = chamado
                };
            }

            return new ServiceResult(ServiceResultType.NotValid)
            {
                Messages = new[]
                {
                    "Chamado inválido"
                }
            };
        }

        public Task<ServiceResult> FecharChamado(Chamado chamado, Atendente atendente)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResult> ListarChamadosAsync()
        {
            var chamados = await _context.Chamados
                .Where(c => c.StatusChamado != StatusChamado.Rejeitado && c.StatusChamado != StatusChamado.Concluido)
                .Include(c => c.Cliente)
                .OrderBy(c => c.StatusChamado)
                .ThenByDescending(c => c.DataAbertura)
                .ToListAsync();
            if (chamados is null)
            {
                return new ServiceResult(ServiceResultType.NotValid)
                {
                    Messages = new[]
                    {
                        "Chamados inexistentes"
                    }
                };
            }

            return new ServiceResult<List<Chamado>>(ServiceResultType.Success)
            {
                Result = chamados
            };
        }

        public async Task<ServiceResult> BuscarChamadoPorProtocolo(string numeroProtocolo)
        {
            var chamado = await _context.Chamados
                .Where(c => c.NumeroProtocolo.Equals(numeroProtocolo))
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync();
            if (chamado is not null)
            {
                return new ServiceResult<Chamado>(ServiceResultType.Success)
                {
                    Result = chamado
                };
            }

            return new ServiceResult(ServiceResultType.NotValid)
            {
                Messages = new[]
                {
                    "Chamado nao encontrado"
                }
            };
        }

        public async Task<ServiceResult> GetChamadoAsync(int idChamado)
        {
            var chamado = await _context.Chamados.Where(c => c.Id == idChamado)
                .Include(c=>c.Cliente)
                .FirstOrDefaultAsync();
            if (chamado is not null)
            {
                return new ServiceResult<Chamado>(ServiceResultType.Success)
                {
                    Result = chamado
                };
            }

            return new ServiceResult(ServiceResultType.NotValid)
            {
                Messages = new[]
                {
                    "Chamado nao encontrado"
                }
            };
        }
    }
}