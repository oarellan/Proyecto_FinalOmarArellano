using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.OmarArellano.Application.DTO
{
    public class ComprobacionDto
    {
        public string NumeroTarjeta { get; set; }
        public string NumeroTarjetaEnmascarada { get; set; }
        public string Sha256Hash { get; set; }
        public Byte[] NumeroTarjetaEncriptado { get; set; }
        public string NumeroTarjetaDesEncriptado { get; set; }
        public bool IsTarjetaOriginalADesencriptada { get; set; }
    }
}
