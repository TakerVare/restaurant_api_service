using System;

namespace Models
{
    public class MenuDiario
    {
        public int Id { get; set; }
        public int PlatoPrincipalId { get; set; }
        public PlatoPrincipal? PlatoPrincipal { get; set; }
        public int BebidaId { get; set; }
        public Bebida? Bebida { get; set; }
        public int PostreId { get; set; }
        public Postre? Postre { get; set; }
        public DateTime Fecha { get; set; }
        public double PrecioTotal { get; set; }

        public MenuDiario() { }

        public MenuDiario(int platoPrincipalId, int bebidaId, int postreId, DateTime fecha)
        {
            PlatoPrincipalId = platoPrincipalId;
            BebidaId = bebidaId;
            PostreId = postreId;
            Fecha = fecha;
        }

        public void CalcularPrecioTotal()
        {
            if (PlatoPrincipal != null && Bebida != null && Postre != null)
            {
                PrecioTotal = PlatoPrincipal.Precio + Bebida.Precio + Postre.Precio;
            }
        }
    }
}