using Microsoft.EntityFrameworkCore;
using Cuentas.Models;

namespace Cuentas.Data
{
    public class CuentasContext : DbContext
    {
        public CuentasContext(DbContextOptions<CuentasContext> options)
            : base(options)
        {
        }

        public DbSet<Cuenta> Cuenta { get; set; } = default!;
        public DbSet<Movimiento> Movimiento { get; set; } = default!;
    }
}