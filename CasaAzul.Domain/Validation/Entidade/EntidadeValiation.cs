using CasaAzul.Domain.Models.Entidade;
using FluentValidation;

namespace CasaAzul.Domain.Validation.Entidade
{
    public class EntidadeValiation : AbstractValidator<EntidadeModel>
    {
        public EntidadeValiation()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email não válido"); 
        }
    }
}
