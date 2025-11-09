using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Services;

namespace RestauranteAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ComboController : ControllerBase
   {
    private static List<Combo> combos = new List<Combo>();

    private readonly IComboService _comboService;

    public ComboController(IComboService comboService)
        {
            _comboService = comboService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Combo>>> GetCombos()
        {
            var combos = await _comboService.GetAllAsync();
            return Ok(combos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Combo>> GetCombo(int id)
        {
            var combo = await _comboService.GetByIdAsync(id);
            if (combo == null)
            {
                return NotFound();
            }
            return Ok(combo);
        }

        [HttpPost]
        public async Task<ActionResult<Combo>> CreateCombo(Combo combo)
        {
            await _comboService.AddAsync(combo);
            return CreatedAtAction(nameof(GetCombo), new { id = combo.Id }, combo);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCombo(int id, Combo updatedCombo)
        {
            var existingCombo = await _comboService.GetByIdAsync(id);
            if (existingCombo == null)
            {
                return NotFound();
            }

            // Actualizar el combo existente
            existingCombo.PlatoPrincipal = updatedCombo.PlatoPrincipal;
            existingCombo.Bebida = updatedCombo.Bebida;
            existingCombo.Postre = updatedCombo.Postre;
            existingCombo.Descuento = updatedCombo.Descuento;

            await _comboService.UpdateAsync(existingCombo);
            return NoContent();
        }

        ///Cambio necesario///
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteCombo(int id)
       {
           var combo = await _comboService.GetByIdAsync(id);
           if (combo == null)
           {
               return NotFound();
           }
           await _comboService.DeleteAsync(id);
           return NoContent();
       }

   }
}