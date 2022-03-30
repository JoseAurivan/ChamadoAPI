using System.Threading.Tasks;
using Application.DataStructure;
using Models;

namespace Application.Services.Interfaces
{
    public interface IChamadoService
    {
        Task<ServiceResult> AbrirChamado(Chamado chamado, int cliente);
        Task<ServiceResult> MudarChamado(Chamado chamado);
        Task<ServiceResult> FecharChamado(Chamado chamado, Atendente atendente);
        Task<ServiceResult> ListarChamadosAsync();
        Task<ServiceResult> BuscarChamadoPorProtocolo(string numeroProtocolo);
        Task<ServiceResult> GetChamadoAsync(int idChamado);
    }
}