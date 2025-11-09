namespace RestauranteAPI.Services
{
    public interface IComboService
    {
        Task<List<Combo>> GetAllAsync();
        Task<Combo?> GetByIdAsync(int id);
        Task AddAsync(Combo combo);
        Task UpdateAsync(Combo combo);
        Task DeleteAsync(int id);
        //Task InicializarDatosAsync();

    }
}
