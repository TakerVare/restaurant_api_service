namespace RestauranteAPI.Services.DTOs
{
    public class MenuDiarioDto
    {
        public int PlatoPrincipalId { get; set; }
        public int BebidaId { get; set; }
        public int PostreId { get; set; }
        public DateTime Fecha { get; set; }
    }
}