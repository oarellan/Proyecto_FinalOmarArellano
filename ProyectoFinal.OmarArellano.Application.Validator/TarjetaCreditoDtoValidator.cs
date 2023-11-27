using FluentValidation;
using ProyectoFinal.OmarArellano.Application.DTO;
using System.Text.RegularExpressions;

namespace ProyectoFinal.OmarArellano.Application.Validator
{
    public class TarjetaCreditoDtoValidator : AbstractValidator<TarjetaCreditoDto>
    {
        private const string caracteresValidos = @"^\d{16}$";

        public TarjetaCreditoDtoValidator()
        {
            RuleFor(objectDto => objectDto.NumeroTarjeta)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(SoloCaracteresValidos).WithMessage("Cadena no valida");
        }
        private bool SoloCaracteresValidos(string NumeroTarjeta)
        {
            Regex regex = new Regex(caracteresValidos);
            return regex.IsMatch(NumeroTarjeta);
        }
    }
}
