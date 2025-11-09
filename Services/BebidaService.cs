using RestauranteAPI.Repositories;

namespace RestauranteAPI.Services
{
    public class BebidaService : IBebidaService
    {
        private readonly IBebidaRepository _BebidaRepository;

        public BebidaService(IBebidaRepository BebidaRepository)
        {
            _BebidaRepository = BebidaRepository;
            
        }

        public async Task<List<Bebida>> GetAllAsync()
        {
            return await _BebidaRepository.GetAllAsync();
        }

        public async Task<Bebida?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _BebidaRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Bebida plato)
        {
            if (string.IsNullOrWhiteSpace(plato.Nombre))
                throw new ArgumentException("El nombre del plato no puede estar vacío.");

            if (plato.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.");

            await _BebidaRepository.AddAsync(plato);
        }

        public async Task UpdateAsync(Bebida plato)
        {
            if (plato.Id <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(plato.Nombre))
                throw new ArgumentException("El nombre del plato no puede estar vacío.");

            await _BebidaRepository.UpdateAsync(plato);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _BebidaRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _BebidaRepository.InicializarDatosAsync();
        }
    }
}
