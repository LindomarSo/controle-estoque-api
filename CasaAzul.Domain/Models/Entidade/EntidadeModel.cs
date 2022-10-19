using CasaAzul.Domain.Models.Doacao;
using CasaAzul.Domain.Models.Endereco;
using CasaAzul.Domain.Models.Identity;

namespace CasaAzul.Domain.Models.Entidade
{
    public class EntidadeModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Documento { get; set; }
        public DateTime? DtNascimento { get; set; }
        public string TipoEntidade { get; set; }
        public string Escolaridade { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public EnderecoModel Endereco { get; set; }
        public IEnumerable<DoacaoModel> Doacoes { get; set; }
    }
}
