using Models;
using RestauranteAPI.Services.DTOs;

namespace RestauranteAPI.Services
{
    public interface IMenuDiarioService
    {
        Task<List<MenuDiario>> GetAllAsync();
        Task<MenuDiario?> GetByIdAsync(int id);
        Task<MenuDiario?> GetByFechaAsync(DateTime fecha);
        Task<MenuDiario> AddAsync(MenuDiarioDto menuDiarioDto);
        Task<MenuDiario> UpdateAsync(int id, MenuDiarioDto menuDiarioDto);
        Task DeleteAsync(int id);
    }
}