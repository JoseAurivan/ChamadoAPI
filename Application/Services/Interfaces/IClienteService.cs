using System.Threading.Tasks;
using Application.DataStructure;
using Models;

namespace Application.Services.Interfaces
{
    public interface IClienteService
    {
        Task<ServiceResult> ListarCliente();
        Task<ServiceResult> SalvarCliente(Cliente cliente);
        Task<ServiceResult> ProcurarClienteId(int id);
        Task<ServiceResult> ProcurarClienteCpf(string cpf);
    }
}