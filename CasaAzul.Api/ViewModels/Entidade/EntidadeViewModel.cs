using CasaAzul.Api.ViewModels.Doacao;
using CasaAzul.Api.ViewModels.Endereco;
using CasaAzul.Domain.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace CasaAzul.Api.ViewModels.Entidade
{
    public class EntidadeViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Documento { get; set; }
        public DateTime? DtNascimento { get; set; }
        public string TipoEntidade { get; set; }
        public string Escolaridade { get; set; }

        [Required(ErrorMessage = "O endereco é obrigatório")]
        public EnderecoViewModel Endereco { get; set; }
        public IEnumerable<DoacaoInfoViewModel> Doacoes { get; set; }
        public UserViewModel User { get; set; }
    }
}
