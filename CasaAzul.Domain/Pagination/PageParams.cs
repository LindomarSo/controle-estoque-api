namespace CasaAzul.Domain.Pagination
{
    public class PageParams
    {
        public const int MaxTamnho = 50;

        private int tamanhoPagina = 10;

        public int NumeroPagina { get; set; }

        public int TamanhoPagina 
        { 
            get { return tamanhoPagina; } 

            set 
            {
                tamanhoPagina = (value > MaxTamnho) ? MaxTamnho : value;
            }
        }

        public string Descricao { get; set; } = string.Empty;
    }
}
