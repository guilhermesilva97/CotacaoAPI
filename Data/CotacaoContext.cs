using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class CotacaoContext : DbContext
    {
        public CotacaoContext(DbContextOptions<CotacaoContext> options): base (options) { }

        public DbSet<Cotacao> Cotacao { get; set; }
        public DbSet<CotacaoItem> CotacaoItem { get; set; }
    }
}
