using RestauranteAPI.Repositories;

namespace RestauranteAPI.Services
{
    public class PostreService : IPostreService
    {
        private readonly IPostreRepository _PostreRepository;

        public PostreService(IPostreRepository PostreRepository)
        {
            _PostreRepository = PostreRepository;
            
        }

        public async Task<List<Postre>> GetAllAsync()
        {
            return await _PostreRepository.GetAllAsync();
        }

        public async Task<Postre?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _PostreRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Postre postre)
        {
            if (string.IsNullOrWhiteSpace(postre.Nombre))
                throw new ArgumentException("El nombre del postre no puede estar vacío.");

            if (postre.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.");

            await _PostreRepository.AddAsync(postre);
        }

        public async Task UpdateAsync(Postre postre)
        {
            if (postre.Id <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(postre.Nombre))
                throw new ArgumentException("El nombre del postre no puede estar vacío.");

            await _PostreRepository.UpdateAsync(postre);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _PostreRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _PostreRepository.InicializarDatosAsync();
        }
    }
}
