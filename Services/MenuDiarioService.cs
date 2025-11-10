using Models;
using RestauranteAPI.Repositories;
using RestauranteAPI.Services.DTOs;

namespace RestauranteAPI.Services
{
    public class MenuDiarioService : IMenuDiarioService
    {
        private readonly IMenuDiarioRepository _menuDiarioRepository;
        private readonly IPlatoPrincipalService _platoPrincipalService;
        private readonly IBebidaService _bebidaService;
        private readonly IPostreService _postreService;

        public MenuDiarioService(
            IMenuDiarioRepository menuDiarioRepository,
            IPlatoPrincipalService platoPrincipalService,
            IBebidaService bebidaService,
            IPostreService postreService)
        {
            _menuDiarioRepository = menuDiarioRepository;
            _platoPrincipalService = platoPrincipalService;
            _bebidaService = bebidaService;
            _postreService = postreService;
        }

        public async Task<List<MenuDiario>> GetAllAsync()
        {
            return await _menuDiarioRepository.GetAllAsync();
        }

        public async Task<MenuDiario?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _menuDiarioRepository.GetByIdAsync(id);
        }

        public async Task<MenuDiario?> GetByFechaAsync(DateTime fecha)
        {
            return await _menuDiarioRepository.GetByFechaAsync(fecha);
        }

        public async Task<MenuDiario> AddAsync(MenuDiarioDto menuDiarioDto)
        {
            // Validar que la fecha no sea pasada
            if (menuDiarioDto.Fecha.Date < DateTime.Now.Date)
                throw new ArgumentException("No se pueden crear menús para fechas pasadas.");

            // Verificar que no exista ya un menú para esa fecha
            var menuExistente = await _menuDiarioRepository.GetByFechaAsync(menuDiarioDto.Fecha);
            if (menuExistente != null)
                throw new InvalidOperationException($"Ya existe un menú para la fecha {menuDiarioDto.Fecha.ToShortDateString()}");

            // Validar que los productos existan
            var platoPrincipal = await _platoPrincipalService.GetByIdAsync(menuDiarioDto.PlatoPrincipalId);
            if (platoPrincipal == null)
                throw new ArgumentException($"El plato principal con ID {menuDiarioDto.PlatoPrincipalId} no existe.");

            var bebida = await _bebidaService.GetByIdAsync(menuDiarioDto.BebidaId);
            if (bebida == null)
                throw new ArgumentException($"La bebida con ID {menuDiarioDto.BebidaId} no existe.");

            var postre = await _postreService.GetByIdAsync(menuDiarioDto.PostreId);
            if (postre == null)
                throw new ArgumentException($"El postre con ID {menuDiarioDto.PostreId} no existe.");

            // Crear el menú diario
            var menuDiario = new MenuDiario
            {
                PlatoPrincipalId = menuDiarioDto.PlatoPrincipalId,
                BebidaId = menuDiarioDto.BebidaId,
                PostreId = menuDiarioDto.PostreId,
                Fecha = menuDiarioDto.Fecha.Date,
                PlatoPrincipal = platoPrincipal,
                Bebida = bebida,
                Postre = postre
            };

            // Calcular precio total
            menuDiario.CalcularPrecioTotal();

            await _menuDiarioRepository.AddAsync(menuDiario);
            return menuDiario;
        }

        public async Task<MenuDiario> UpdateAsync(int id, MenuDiarioDto menuDiarioDto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            var menuDiarioExistente = await _menuDiarioRepository.GetByIdAsync(id);
            if (menuDiarioExistente == null)
                throw new ArgumentException($"No se encontró el menú diario con ID {id}");

            // Verificar que no exista otro menú para la nueva fecha
            if (menuDiarioExistente.Fecha != menuDiarioDto.Fecha.Date)
            {
                var menuEnFecha = await _menuDiarioRepository.GetByFechaAsync(menuDiarioDto.Fecha);
                if (menuEnFecha != null && menuEnFecha.Id != id)
                    throw new InvalidOperationException($"Ya existe un menú para la fecha {menuDiarioDto.Fecha.ToShortDateString()}");
            }

            // Validar que los productos existan
            var platoPrincipal = await _platoPrincipalService.GetByIdAsync(menuDiarioDto.PlatoPrincipalId);
            if (platoPrincipal == null)
                throw new ArgumentException($"El plato principal con ID {menuDiarioDto.PlatoPrincipalId} no existe.");

            var bebida = await _bebidaService.GetByIdAsync(menuDiarioDto.BebidaId);
            if (bebida == null)
                throw new ArgumentException($"La bebida con ID {menuDiarioDto.BebidaId} no existe.");

            var postre = await _postreService.GetByIdAsync(menuDiarioDto.PostreId);
            if (postre == null)
                throw new ArgumentException($"El postre con ID {menuDiarioDto.PostreId} no existe.");

            // Actualizar el menú
            menuDiarioExistente.PlatoPrincipalId = menuDiarioDto.PlatoPrincipalId;
            menuDiarioExistente.BebidaId = menuDiarioDto.BebidaId;
            menuDiarioExistente.PostreId = menuDiarioDto.PostreId;
            menuDiarioExistente.Fecha = menuDiarioDto.Fecha.Date;
            menuDiarioExistente.PlatoPrincipal = platoPrincipal;
            menuDiarioExistente.Bebida = bebida;
            menuDiarioExistente.Postre = postre;

            // Recalcular precio total
            menuDiarioExistente.CalcularPrecioTotal();

            await _menuDiarioRepository.UpdateAsync(menuDiarioExistente);
            return menuDiarioExistente;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            var menuDiario = await _menuDiarioRepository.GetByIdAsync(id);
            if (menuDiario == null)
                throw new ArgumentException($"No se encontró el menú diario con ID {id}");

            await _menuDiarioRepository.DeleteAsync(id);
        }
    }
}