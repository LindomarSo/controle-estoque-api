using CasaAzul.Domain.Models.Entidade;

namespace CasaAzul.Domain.Models.Endereco
{
    public class EnderecoModel
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Complemento { get; set; }
        public int? Numero { get; set; }
        public int EntidadeId { get; set; }

        public EntidadeModel Entidade { get; set; }
    }
}
