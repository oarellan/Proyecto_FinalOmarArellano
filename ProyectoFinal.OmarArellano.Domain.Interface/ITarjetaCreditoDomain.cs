using ProyectoFinal.OmarArellano.Application.DTO;
using ProyectoFinal.OmarArellano.Transversal.Common;
using System.Threading.Tasks;

namespace ProyectoFinal.OmarArellano.Domain.Interface
{
    public interface ITarjetaCreditoDomain
    {
        Response<string> EnmascaraNumeroTarjeta(TarjetaCreditoDto tarjetaCredito);
        Response<string> CalculaSHA256(string numeroTarjetaCredito);
        Response<byte[]>  EncriptarStringABytesAES(string numeroTarjetaCredito, byte[] Key, byte[] IV);
        Response<string>  DesencriptarByteArrayAStringAES(byte[] encriptado, byte[] Key, byte[] IV);
    }
}
