using ProyectoFinal.OmarArellano.Application.DTO;
using ProyectoFinal.OmarArellano.Domain.Interface;
using ProyectoFinal.OmarArellano.Infrastructure.Interface;
using ProyectoFinal.OmarArellano.Transversal.Common;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ProyectoFinal.OmarArellano.Domain.Core
{
    public class TarjetaCreditoDomain : ITarjetaCreditoDomain
    {
        private readonly IUnitOfWork _unitOfWork;
        public TarjetaCreditoDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Response<string> EnmascaraNumeroTarjeta(TarjetaCreditoDto tarjetaCredito)
        {
            var responseDomain = new Response<string>();

            var numeroenmascarado = EnmascararTarjeta(tarjetaCredito.NumeroTarjeta);
            responseDomain.Data = numeroenmascarado;
            responseDomain.IsSuccess = true;
            return responseDomain;
        }

        /// <summary>
        /// Metodo que enmascara los digitos de una tarjeta de credito, excepto los ultimos 4.
        /// </summary>
        /// <param name="digitosTarjeta"></param>
        /// <returns></returns>
        static string EnmascararTarjeta(string digitosTarjeta)
        {
            // Enmascarar todos los dígitos, excepto los últimos 4
            int longitud = digitosTarjeta.Length;
            string digitosEnmascarados = new string('*', longitud - 4) + digitosTarjeta.Substring(longitud - 4);

            return digitosEnmascarados;
        }

        /// <summary>
        /// Algoritmo para convertir el string a sha256 en hexadecimal
        /// </summary>
        /// <param name="numeroTarjetaCredito"></param>
        /// <returns></returns>
        public Response<string> CalculaSHA256(string numeroTarjetaCredito)
        {
            var responseDomain = new Response<string>();

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(numeroTarjetaCredito);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder stringBuilder = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

                responseDomain.Data = stringBuilder.ToString();
            }

            responseDomain.IsSuccess = true;
            return responseDomain;
        }

        //Algoritmo que para encriptar un numero de tarjeta de credito
        public Response<byte[]>  EncriptarStringABytesAES(string numeroTarjetaCredito, byte[] Key, byte[] IV)
        {
            var responseDomain = new Response<byte[]>();

            using (AesCryptoServiceProvider aesEncriptar = new AesCryptoServiceProvider())
            {
                aesEncriptar.Key = Key;
                aesEncriptar.IV = IV;

                ICryptoTransform encriptar = aesEncriptar.CreateEncryptor(aesEncriptar.Key, aesEncriptar.IV);

                using (MemoryStream msEncriptar = new MemoryStream())
                {
                    using (CryptoStream csEncriptar = new CryptoStream(msEncriptar, encriptar, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncriptar = new StreamWriter(csEncriptar))
                        {
                            swEncriptar.Write(numeroTarjetaCredito);
                        }

                        responseDomain.Data = msEncriptar.ToArray();
                    }
                }
            }
            return responseDomain;
        }

        //Algoritmo para desencriptar un arreglo de bytes 
        public Response<string> DesencriptarByteArrayAStringAES(byte[] encriptado, byte[] Key, byte[] IV)
        {
            var responseDomain = new Response<string>();

            using (AesCryptoServiceProvider aesDesencriptar = new AesCryptoServiceProvider())
            {
                aesDesencriptar.Key = Key;
                aesDesencriptar.IV = IV;

                ICryptoTransform desencriptar = aesDesencriptar.CreateDecryptor(aesDesencriptar.Key, aesDesencriptar.IV);

                using (MemoryStream msDesencriptar = new MemoryStream(encriptado))
                {
                    using (CryptoStream csDesencriptar = new CryptoStream(msDesencriptar, desencriptar, CryptoStreamMode.Read))
                    {
                        using (StreamReader swDesencriptar = new StreamReader(csDesencriptar))
                        {
                            responseDomain.Data = swDesencriptar.ReadToEnd();
                        }
                    }
                }
            }
            return responseDomain;
        }

       
    }
}
