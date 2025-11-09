using Models;

namespace RestauranteAPI.Repositories
{
    public interface IMenuDiarioRepository
    {
        Task<List<MenuDiario>> GetAllAsync();
        Task<MenuDiario?> GetByIdAsync(int id);
        Task<MenuDiario?> GetByFechaAsync(DateTime fecha);
        Task AddAsync(MenuDiario menuDiario);
        Task UpdateAsync(MenuDiario menuDiario);
        Task DeleteAsync(int id);
    }
}