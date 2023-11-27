using ProyectoFinal.OmarArellano.Application.DTO;
using ProyectoFinal.OmarArellano.Transversal.Common;
using System.Threading.Tasks;

namespace ProyectoFinal.OmarArellano.Application.Interface
{
    public interface ITarjetaCreditoApplication
    {
        Task<Response<ComprobacionDto>> ProcesarTarjetaCredito(TarjetaCreditoDto tarjetaCredito);
    }
}
