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
    public class AtendenteService:IAtendenteService
    {
        private readonly IContext _context;

        public AtendenteService(IContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult> SalvarAtendenteAsync(Atendente atendente)
        {
            try
            {
               return await TentarSalvarAtendente(atendente);
            }
            catch (Exception e)
            {
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"Erro ao fazer Login"}
                };
            }

        }

        public async Task<ServiceResult> TentarSalvarAtendente(Atendente atendente)
        {
            
            if (atendente is null)
            {
                return new ServiceResult(ServiceResultType.NotValid)
                {
                    Messages = new[]
                    {
                        "Erro ao econtrar o atendente"
                    }
                };
            }

            if (atendente.Id == default)
            {
                _context.Atendentes.Add(atendente);
                await _context.SaveChangesAsync();

                return new ServiceResult<Atendente>(ServiceResultType.Success)
                {
                    Result = atendente
                };
            }

            return new ServiceResult(ServiceResultType.NotValid)
            {
                Messages = new[]
                {
                    "Atendente ja existente"
                }
            };
        }

        public async Task<ServiceResult> ListarAtendentesAsync()
        {
            var atendentes = await _context.Atendentes.ToListAsync();
            if (atendentes is null)
            {
                return new ServiceResult(ServiceResultType.NotValid)
                {
                    Messages = new[]
                    {
                        "Atendentes ja existente"
                    }
                };
            }
            return new ServiceResult<List<Atendente>>(ServiceResultType.Success)
            {
                Result = atendentes
            };
        }

        public async Task<ServiceResult> LogarAtendente(string email, string cpf)
        {
            var atendente = await _context.Atendentes.Where(c => c.Email == email && c.Cpf == cpf)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if(atendente is not null)
            {
                return new ServiceResult<Atendente>(ServiceResultType.Success)
                {
                    Result = atendente
                };
            }

            return new ServiceResult(ServiceResultType.NotValid)
            {
                Messages = new[]
                {
                    "Atendente nao encontrado"
                }
            };

        }
    }
}