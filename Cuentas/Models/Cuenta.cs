namespace Cuentas.Models
{
    public class Cuenta
    {
        public int Id { get; set; }

        public string numero { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;

        public double creditos { get; set; }
        public double debitos { get; set; }
        public double balance { get; set; }

        public Cuenta()
        {
            creditos = 0;
            debitos = 0;
            balance = 0;
        }

        public Cuenta(string numero, string descripcion, double creditos, double debitos)
        {
            this.numero = numero;
            this.descripcion = descripcion;
            this.creditos = creditos;
            this.debitos = debitos;
            setBalance();
        }

        public void setCredito(double monto)
        {
            creditos += monto;
            setBalance();
        }

        public void setDebito(double monto)
        {
            debitos += monto;
            setBalance();
        }

        public void setBalance()
        {
            balance = creditos - debitos;
        }
    }
}