using Microsoft.EntityFrameworkCore;

namespace CasaAzul.Domain.Pagination
{
    public class PageList<T> : List<T>
    {
        public PageList()
        {

        }

        public PageList(int paginaAtual, int tamanhoPagina, int totalItens, List<T> items)
        {
            PaginaAtual = paginaAtual;
            TotalPaginas = (int)Math.Ceiling(totalItens / (double)tamanhoPagina); 
            TamanhoPagina = tamanhoPagina;
            TotalItens = totalItens;
            AddRange(items);
        }

        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanhoPagina { get; set; }
        public int TotalItens { get; set; }

        public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, 
                                                          int numeroPagina,
                                                          int tamanhoPagina)
        {
            var total = source.Count();
            var items = await source.Skip((numeroPagina - 1) * tamanhoPagina)
                                    .Take(tamanhoPagina)
                                    .ToListAsync();

            return new PageList<T>(numeroPagina, tamanhoPagina, total, items);
        }
    }
}
