using RestauranteAPI.Repositories;

namespace RestauranteAPI.Services
{
    public class ComboService : IComboService
    {
        private readonly IComboRepository _comboRepository;

        public ComboService(IComboRepository comboRepository)
        {
            _comboRepository = comboRepository;
        }

        public async Task<List<Combo>> GetAllAsync()
        {
            return await _comboRepository.GetAllAsync();
        }

        public async Task<Combo?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _comboRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Combo combo)
        {
            if (combo.PlatoPrincipal == null)
                throw new ArgumentException("El combo debe tener un plato principal.");

            if (combo.Bebida == null)
                throw new ArgumentException("El combo debe tener una bebida.");

            if (combo.Postre == null)
                throw new ArgumentException("El combo debe tener un postre.");

            if (combo.Descuento < 0 || combo.Descuento > 1)
                throw new ArgumentException("El descuento debe estar entre 0 y 1.");

            await _comboRepository.AddAsync(combo);
        }

        public async Task UpdateAsync(Combo combo)
        {
            if (combo.Id <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (combo.PlatoPrincipal == null)
                throw new ArgumentException("El combo debe tener un plato principal.");

            if (combo.Bebida == null)
                throw new ArgumentException("El combo debe tener una bebida.");

            if (combo.Postre == null)
                throw new ArgumentException("El combo debe tener un postre.");

            await _comboRepository.UpdateAsync(combo);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _comboRepository.DeleteAsync(id);
        }
    }
}