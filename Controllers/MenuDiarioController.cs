using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Services;
using RestauranteAPI.Services.DTOs;
using Models;

namespace RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuDiarioController : ControllerBase
    {
        private readonly IMenuDiarioService _menuDiarioService;

        public MenuDiarioController(IMenuDiarioService menuDiarioService)
        {
            _menuDiarioService = menuDiarioService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<MenuDiario>>> GetMenusDiarios()
        {
            try
            {
                var menus = await _menuDiarioService.GetAllAsync();
                return Ok(menus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los menús diarios", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDiario>> GetMenuDiario(int id)
        {
            try
            {
                var menu = await _menuDiarioService.GetByIdAsync(id);
                if (menu == null)
                {
                    return NotFound(new { message = $"No se encontró el menú diario con ID {id}" });
                }
                return Ok(menu);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el menú diario", error = ex.Message });
            }
        }

        [HttpGet("fecha/{fecha}")]
        public async Task<ActionResult<MenuDiario>> GetMenuDiarioPorFecha(DateTime fecha)
        {
            try
            {
                var menu = await _menuDiarioService.GetByFechaAsync(fecha);
                if (menu == null)
                {
                    return NotFound(new { message = $"No se encontró un menú para la fecha {fecha.ToShortDateString()}" });
                }
                return Ok(menu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el menú diario", error = ex.Message });
            }
        }

        
        [HttpPost]
        public async Task<ActionResult<MenuDiario>> CreateMenuDiario(MenuDiarioDto menuDiarioDto)
        {
            try
            {
                var menu = await _menuDiarioService.AddAsync(menuDiarioDto);
                return CreatedAtAction(nameof(GetMenuDiario), new { id = menu.Id }, menu);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el menú diario", error = ex.Message });
            }
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuDiario(int id, MenuDiarioDto menuDiarioDto)
        {
            try
            {
                var menu = await _menuDiarioService.UpdateAsync(id, menuDiarioDto);
                return Ok(menu);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el menú diario", error = ex.Message });
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuDiario(int id)
        {
            try
            {
                await _menuDiarioService.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el menú diario", error = ex.Message });
            }
        }
    }
}