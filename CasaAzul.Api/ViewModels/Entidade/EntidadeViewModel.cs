using CasaAzul.Api.ViewModels.Doacao;
using CasaAzul.Api.ViewModels.Endereco;

namespace CasaAzul.Api.ViewModels.Entidade
{
    public class EntidadeViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Documento { get; set; }
        public string Habilidade { get; set; }
        public string DtNascimento { get; set; }
        public string TipoEntidade { get; set; }
        public string Escolaridade { get; set; }

        public EnderecoViewModel Endereco { get; set; }
        public IEnumerable<DoacaoInfoViewModel> Doacoes { get; set; }
        public UserViewModel User { get; set; }
    }
}
