using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Services;

namespace RestauranteAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PostreController : ControllerBase
   {
    private static List<Postre> postres = new List<Postre>();

    private readonly IPostreService _postreService;

    public PostreController(IPostreService postreService)
        {
            _postreService = postreService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Postre>>> GetPostres()
        {
            var postres = await _postreService.GetAllAsync();
            return Ok(postres);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Postre>> GetPostre(int id)
        {
            var postre = await _postreService.GetByIdAsync(id);
            if (postre == null)
            {
                return NotFound();
            }
            return Ok(postre);
        }

        [HttpPost]
        public async Task<ActionResult<Postre>> CreatePostre(Postre postre)
        {
            await _postreService.AddAsync(postre);
            return CreatedAtAction(nameof(GetPostre), new { id = postre.Id }, postre);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePostre(int id, Postre updatedPostre)
        {
            var existingPostre = await _postreService.GetByIdAsync(id);
            if (existingPostre == null)
            {
                return NotFound();
            }

            existingPostre.Nombre = updatedPostre.Nombre;
            existingPostre.Precio = updatedPostre.Precio;
            existingPostre.Calorias = updatedPostre.Calorias;

            await _postreService.UpdateAsync(existingPostre);
            return NoContent();
        }

        
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeletePostre(int id)
       {
           var postre = await _postreService.GetByIdAsync(id);
           if (postre == null)
           {
               return NotFound();
           }
           await _postreService.DeleteAsync(id);
           return NoContent();
       }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _postreService.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

   }
}