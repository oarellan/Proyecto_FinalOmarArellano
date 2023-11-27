using System;

namespace ProyectoFinal.OmarArellano.Infrastructure.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ITarjetaCreditoRepository TarjetaCredito { get; }
    }
}
