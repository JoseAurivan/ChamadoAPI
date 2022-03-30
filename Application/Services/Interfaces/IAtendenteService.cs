using System.Threading.Tasks;
using Application.DataStructure;
using Models;

namespace Application.Services.Interfaces
{
    public interface IAtendenteService
    {
        Task<ServiceResult> SalvarAtendenteAsync(Atendente atendente);
        Task<ServiceResult> ListarAtendentesAsync();

        Task<ServiceResult> LogarAtendente(string email, string cpf);
    }
}