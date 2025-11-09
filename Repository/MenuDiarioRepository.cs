using Microsoft.Data.SqlClient;
using Models;

namespace RestauranteAPI.Repositories
{
    public class MenuDiarioRepository : IMenuDiarioRepository
    {
        private readonly string _connectionString;
        private readonly IPlatoPrincipalRepository _platoPrincipalRepository;
        private readonly IBebidaRepository _bebidaRepository;
        private readonly IPostreRepository _postreRepository;

        public MenuDiarioRepository(
            IConfiguration configuration,
            IPlatoPrincipalRepository platoPrincipalRepository,
            IBebidaRepository bebidaRepository,
            IPostreRepository postreRepository)
        {
            _connectionString = configuration.GetConnectionString("RestauranteDB") ?? "Not found";
            _platoPrincipalRepository = platoPrincipalRepository;
            _bebidaRepository = bebidaRepository;
            _postreRepository = postreRepository;
        }

        public async Task<List<MenuDiario>> GetAllAsync()
        {
            var menusDiarios = new List<MenuDiario>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT Id, PlatoPrincipalId, BebidaId, PostreId, Fecha, PrecioTotal 
                                FROM MenuDiario 
                                ORDER BY Fecha DESC";
                                
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var menuDiario = new MenuDiario
                            {
                                Id = reader.GetInt32(0),
                                PlatoPrincipalId = reader.GetInt32(1),
                                BebidaId = reader.GetInt32(2),
                                PostreId = reader.GetInt32(3),
                                Fecha = reader.GetDateTime(4),
                                PrecioTotal = Convert.ToDouble(reader.GetDecimal(5))
                            };

                            // Cargar las entidades relacionadas
                            menuDiario.PlatoPrincipal = await _platoPrincipalRepository.GetByIdAsync(menuDiario.PlatoPrincipalId);
                            menuDiario.Bebida = await _bebidaRepository.GetByIdAsync(menuDiario.BebidaId);
                            menuDiario.Postre = await _postreRepository.GetByIdAsync(menuDiario.PostreId);

                            menusDiarios.Add(menuDiario);
                        }
                    }
                }
            }
            return menusDiarios;
        }

        public async Task<MenuDiario?> GetByIdAsync(int id)
        {
            MenuDiario? menuDiario = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT Id, PlatoPrincipalId, BebidaId, PostreId, Fecha, PrecioTotal 
                                FROM MenuDiario 
                                WHERE Id = @Id";
                                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            menuDiario = new MenuDiario
                            {
                                Id = reader.GetInt32(0),
                                PlatoPrincipalId = reader.GetInt32(1),
                                BebidaId = reader.GetInt32(2),
                                PostreId = reader.GetInt32(3),
                                Fecha = reader.GetDateTime(4),
                                PrecioTotal = Convert.ToDouble(reader.GetDecimal(5))
                            };

                            // Cargar las entidades relacionadas
                            menuDiario.PlatoPrincipal = await _platoPrincipalRepository.GetByIdAsync(menuDiario.PlatoPrincipalId);
                            menuDiario.Bebida = await _bebidaRepository.GetByIdAsync(menuDiario.BebidaId);
                            menuDiario.Postre = await _postreRepository.GetByIdAsync(menuDiario.PostreId);
                        }
                    }
                }
            }
            return menuDiario;
        }

        public async Task<MenuDiario?> GetByFechaAsync(DateTime fecha)
        {
            MenuDiario? menuDiario = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT Id, PlatoPrincipalId, BebidaId, PostreId, Fecha, PrecioTotal 
                                FROM MenuDiario 
                                WHERE CAST(Fecha AS DATE) = @Fecha";
                                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Fecha", fecha.Date);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            menuDiario = new MenuDiario
                            {
                                Id = reader.GetInt32(0),
                                PlatoPrincipalId = reader.GetInt32(1),
                                BebidaId = reader.GetInt32(2),
                                PostreId = reader.GetInt32(3),
                                Fecha = reader.GetDateTime(4),
                                PrecioTotal = Convert.ToDouble(reader.GetDecimal(5))
                            };

                            // Cargar las entidades relacionadas
                            menuDiario.PlatoPrincipal = await _platoPrincipalRepository.GetByIdAsync(menuDiario.PlatoPrincipalId);
                            menuDiario.Bebida = await _bebidaRepository.GetByIdAsync(menuDiario.BebidaId);
                            menuDiario.Postre = await _postreRepository.GetByIdAsync(menuDiario.PostreId);
                        }
                    }
                }
            }
            return menuDiario;
        }

        public async Task AddAsync(MenuDiario menuDiario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO MenuDiario (PlatoPrincipalId, BebidaId, PostreId, Fecha, PrecioTotal) 
                                VALUES (@PlatoPrincipalId, @BebidaId, @PostreId, @Fecha, @PrecioTotal);
                                SELECT CAST(SCOPE_IDENTITY() as int)";
                                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PlatoPrincipalId", menuDiario.PlatoPrincipalId);
                    command.Parameters.AddWithValue("@BebidaId", menuDiario.BebidaId);
                    command.Parameters.AddWithValue("@PostreId", menuDiario.PostreId);
                    command.Parameters.AddWithValue("@Fecha", menuDiario.Fecha.Date);
                    command.Parameters.AddWithValue("@PrecioTotal", menuDiario.PrecioTotal);

                    menuDiario.Id = (int)await command.ExecuteScalarAsync();
                }
            }
        }

        public async Task UpdateAsync(MenuDiario menuDiario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"UPDATE MenuDiario 
                                SET PlatoPrincipalId = @PlatoPrincipalId, 
                                    BebidaId = @BebidaId, 
                                    PostreId = @PostreId, 
                                    Fecha = @Fecha, 
                                    PrecioTotal = @PrecioTotal 
                                WHERE Id = @Id";
                                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", menuDiario.Id);
                    command.Parameters.AddWithValue("@PlatoPrincipalId", menuDiario.PlatoPrincipalId);
                    command.Parameters.AddWithValue("@BebidaId", menuDiario.BebidaId);
                    command.Parameters.AddWithValue("@PostreId", menuDiario.PostreId);
                    command.Parameters.AddWithValue("@Fecha", menuDiario.Fecha.Date);
                    command.Parameters.AddWithValue("@PrecioTotal", menuDiario.PrecioTotal);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM MenuDiario WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}