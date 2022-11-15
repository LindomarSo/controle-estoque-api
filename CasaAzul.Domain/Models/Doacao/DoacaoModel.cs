using CasaAzul.Domain.Models.Entidade;
using CasaAzul.Domain.Models.Identity;

namespace CasaAzul.Domain.Models.Doacao
{
    public class DoacaoModel
    {
        public int Id { get; set; }
        public string MaterialDoado { get; set; }
        public string Preco { get; set; }
        public int? Quantidade { get; set; }
        public string Destino { get; set; }
        public DateTime DtEntrada { get; set; }
        public DateTime? DtRetirada { get; set; }
        public string RetiradaPor { get; set; }
        public string Disponibilidade { get; set; }
        public string Habilidade { get; set; }
        public int EntidadeId { get; set; }
        public int UserId { get; set; }

        public string Unidade { get; set; }

        public User User { get; set; }

        public EntidadeModel Entidade { get; set; }
    }
}
