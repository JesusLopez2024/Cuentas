namespace Cuentas.Models
{
    public class Movimiento
    {
        public int Id { get; set; }

        public int Tipo { get; set; } // 1 = Depósito, 2 = Retiro
        public double Monto { get; set; }

        public DateTime Fecha { get; set; }

        public int CuentaId { get; set; }

        public Cuenta? Cuenta { get; set; }
    }
}