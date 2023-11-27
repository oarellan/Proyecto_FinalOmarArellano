using ProyectoFinal.OmarArellano.Application.DTO;
using ProyectoFinal.OmarArellano.Infrastructure.Data;
using ProyectoFinal.OmarArellano.Infrastructure.Interface;
using System.Threading.Tasks;

namespace ProyectoFinal.OmarArellano.Infrastructure.Repository
{
    public class TarjetaCreditoRepository : ITarjetaCreditoRepository
    {
        private readonly DapperContext _context;
        public TarjetaCreditoRepository(DapperContext context)
        {
            _context = context;
        }
        public Task<bool> InsertAsync(TarjetaCreditoDto tarjetaCredito)
        {
            return Task.FromResult(true);
        }
    }
}
