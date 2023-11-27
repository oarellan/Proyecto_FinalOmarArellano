using ProyectoFinal.OmarArellano.Infrastructure.Interface;

namespace ProyectoFinal.OmarArellano.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ITarjetaCreditoRepository TarjetaCredito { get; }
    
        public UnitOfWork(ITarjetaCreditoRepository tarjetaCredito)
        {
            TarjetaCredito = tarjetaCredito;
        }
        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
