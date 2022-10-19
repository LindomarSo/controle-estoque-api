using System.ComponentModel.DataAnnotations;

namespace CasaAzul.Api.ViewModels.Endereco
{
    public class EnderecoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A rua é obrigatória")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O cep é obrigatório")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório")]
        public string Estado { get; set; }

        public string Complemento { get; set; }

        public int? Numero { get; set; }
    }
}
