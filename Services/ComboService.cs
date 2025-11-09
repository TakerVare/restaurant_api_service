
using Microsoft.Data.SqlClient;
using Models;

namespace RestauranteAPI.Services
{
    public class ComboService : IComboService
    {
        private readonly string _connectionString;

        private readonly IPlatoPrincipalService _platoPrincipalService;
        private readonly IBebidaService _bebidaService;
        private readonly IPostreService _postreService;

        public ComboService(IConfiguration configuration, IPlatoPrincipalService platoprincipalrepository, IBebidaService bebidarepository, IPostreService postrerepository)
        {
             _connectionString = configuration.GetConnectionString("RestauranteDB") ?? "Not found";
            _platoPrincipalService = platoprincipalrepository;
            _bebidaService = bebidarepository;
            _postreService = postrerepository;
        }
        

        public async Task<List<Combo>> GetAllAsync()
        {
            var combos = new List<Combo>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, PlatoPrincipal, Bebida, Postre, Descuento FROM Combo";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var combo = new Combo
                            {
                                Id = reader.GetInt32(0),
                                PlatoPrincipal = await _platoPrincipalService.GetByIdAsync(reader.GetInt32(1)),
                                Bebida = await _bebidaService.GetByIdAsync(reader.GetInt32(2)),
                                Postre = await _postreService.GetByIdAsync(reader.GetInt32(3)),
                                Descuento = Convert.ToDouble(reader.GetDecimal(4)),
                            }; 

                            combos.Add(combo);
                        }
                    }
                }
            }
            return combos;
        }

        public async Task<Combo> GetByIdAsync(int id)
        {
            Combo combo = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, PlatoPrincipal, Bebida, Postre, Descuento FROM Combo WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            combo = new Combo
                            {
                                Id = reader.GetInt32(0),
                                PlatoPrincipal = await _platoPrincipalService.GetByIdAsync(reader.GetInt32(1)),
                                Bebida = await _bebidaService.GetByIdAsync(reader.GetInt32(2)),
                                Postre = await _postreService.GetByIdAsync(reader.GetInt32(3)),
                                Descuento = Convert.ToDouble(reader.GetDecimal(4)),
                            };
                        }
                    }
                }
            }
            return combo;
        }

        public async Task AddAsync(Combo combo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Combo (PlatoPrincipal, Bebida, Postre, Descuento) VALUES (@PlatoPrincipal, @Bebida, @Postre, @Descuento)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PlatoPrincipal", _platoPrincipalService.GetByIdAsync(combo.PlatoPrincipal.Id).Id);
                    command.Parameters.AddWithValue("@Bebida", _bebidaService.GetByIdAsync(combo.Bebida.Id).Id);
                    command.Parameters.AddWithValue("@Postre", _postreService.GetByIdAsync(combo.Postre.Id).Id);
                    command.Parameters.AddWithValue("@Descuento", combo.Descuento);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Combo combo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Combo SET PlatoPrincipal = @PlatoPrincipal, Bebida = @Bebida, Postre = @Postre, Descuento = @Descuento WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", combo.Id);
                    command.Parameters.AddWithValue("@PlatoPrincipal", combo.PlatoPrincipal.Id);
                    command.Parameters.AddWithValue("@Bebida", combo.Bebida.Id);
                    command.Parameters.AddWithValue("@Postre", combo.Postre.Id);
                    command.Parameters.AddWithValue("@Descuento", combo.Descuento);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Combo WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}