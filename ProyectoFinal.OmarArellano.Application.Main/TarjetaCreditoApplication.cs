using AutoMapper;
using ProyectoFinal.OmarArellano.Application.DTO;
using ProyectoFinal.OmarArellano.Application.Interface;
using ProyectoFinal.OmarArellano.Application.Validator;
using ProyectoFinal.OmarArellano.Domain.Interface;
using ProyectoFinal.OmarArellano.Transversal.Common;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ProyectoFinal.OmarArellano.Application.Main
{
    public class TarjetaCreditoApplication : ITarjetaCreditoApplication
    {
        private readonly ITarjetaCreditoDomain _tarjetaCredito;
        private readonly IMapper _mapper;
        private readonly TarjetaCreditoDtoValidator _tarjetaCreditoValidador;

        public TarjetaCreditoApplication(ITarjetaCreditoDomain tarjetaCredito, IMapper mapper, TarjetaCreditoDtoValidator tarjetaCreditoValidador)
        {  

            _tarjetaCredito = tarjetaCredito;
            _mapper = mapper;
            _tarjetaCreditoValidador = tarjetaCreditoValidador;
        }


        public async Task<Response<ComprobacionDto>> ProcesarTarjetaCredito(TarjetaCreditoDto tarjetaCredito)
        {
            var response = new Response<ComprobacionDto>();
            response.Data = new ComprobacionDto();

            response.Data.NumeroTarjeta = tarjetaCredito.NumeroTarjeta;

            //Sanitizo tarjeta con fluenvalidation
            var rsponseSanitizada = await SanitizaTarjetaCredito(tarjetaCredito);
            if (rsponseSanitizada.IsSuccess)
            {
                //Se guarda el numero de tarjeta enmascarado en una variable de respuesta.
                response.Data.NumeroTarjetaEnmascarada = EnmascaraTarjetaCredito(tarjetaCredito).Data;

                //Se guarda el numero de tarjeta de credito en sha256 en una variable de respuesta.
                response.Data.Sha256Hash = CalculaSHA256(tarjetaCredito.NumeroTarjeta).Data;

                // Generar una clave y un vector de inicialización (IV)
                byte[] key = GenerateRandomKey(32); // Clave de 256 bits (32 bytes)
                byte[] iv = GenerateRandomIV(16);   // IV de 128 bits (16 bytes)


                //Se guarda el numero de tarjeta encriptado en una variable de respuesta.
                response.Data.NumeroTarjetaEncriptado = EncriptarAES256(tarjetaCredito.NumeroTarjeta, key, iv).Data;

                //Se guarda el numero de tarjeta desencriptado en una variable de respuesta.
                response.Data.NumeroTarjetaDesEncriptado = DesEncriptarAES256(response.Data.NumeroTarjetaEncriptado, key, iv).Data;


                //Comparamos Sha256 original con Sha256 Desencriptado, Si la comparativa es true entonces el proceso fue correcto.
                if (response.Data.Sha256Hash == CalculaSHA256(response.Data.NumeroTarjetaDesEncriptado).Data)
                {
                    response.Data.IsTarjetaOriginalADesencriptada = true;
                }
                response.IsSuccess = true;
                return response;
            }

            return rsponseSanitizada;

        }

        /// <summary>
        /// Proceso para sanitizar un numero de tarjera de credito, mediante la libreria fluentvalidation. Vease capa de aplicacion proyecto Validartor para mas detalles.
        /// </summary>
        /// <param name="tarjetaCredito"></param>
        /// <returns></returns>
        public async Task<Response<ComprobacionDto>> SanitizaTarjetaCredito(TarjetaCreditoDto tarjetaCredito)
        {
            var response = new Response<ComprobacionDto>();

            var validationRequest = await _tarjetaCreditoValidador.ValidateAsync(tarjetaCredito).ConfigureAwait(false);

            if (!validationRequest.IsValid)
            {

                response.IsSuccess = false;
                var errors = validationRequest.Errors.Select(x => x.ErrorMessage).ToList();

                string erroresConcatenados = string.Join(" ", errors);
                response.Message = erroresConcatenados;
                return response;
            }

            response.IsSuccess = true;
            return response;
        }

        public Response<string> EnmascaraTarjetaCredito(TarjetaCreditoDto tarjetaCredito)
        {
            var response = _tarjetaCredito.EnmascaraNumeroTarjeta(tarjetaCredito);
            return response;
        }
        public Response<string> CalculaSHA256(string numeroTarjetaCredito)
        {
            var response = _tarjetaCredito.CalculaSHA256(numeroTarjetaCredito);
            return response;
        }

        public Response<byte[]> EncriptarAES256(string numeroTarjetaCredito, byte[] key, byte[] iv)
        {
            var response = _tarjetaCredito.EncriptarStringABytesAES(numeroTarjetaCredito, key, iv);
            return response;
        }


        public Response<string> DesEncriptarAES256(byte[] numeroTarjetaEncriptado, byte[] key, byte[] iv)
        {
            var response = _tarjetaCredito.DesencriptarByteArrayAStringAES(numeroTarjetaEncriptado, key, iv);
            return response;
        }

        static byte[] GenerateRandomKey(int sizeInBytes)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[sizeInBytes];
                rng.GetBytes(key);
                return key;
            }
        }

        static byte[] GenerateRandomIV(int sizeInBytes)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] iv = new byte[sizeInBytes];
                rng.GetBytes(iv);
                return iv;
            }
        }
    }
}
