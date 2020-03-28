using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Respository;
using Microsoft.AspNetCore.Http;
using ProAgil.Domain;

namespace ProAgil.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalestranteController : ControllerBase
    {
        private readonly IProAgilRepository _repo;

        public PalestranteController(IProAgilRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _repo.GetAllPalestrantesAsync(true));
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> Get(int id, bool includeEvento)
        {
            try
            {
                var result = await _repo.GetPalestranteAsyncById(id, includeEvento);
                return Ok(result);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> Get(string name, bool includeEvento)
        {
            try
            {
                var result = await _repo.GetPalestrantesAsyncByName(name, includeEvento);
                return Ok(result);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Palestrante palestrante)
        {
            try
            {
                _repo.Add(palestrante);
                if(await _repo.SaveChangesAsync()) return Created($"/api/palestrante{palestrante.Id}", palestrante);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int palestranteId, Palestrante palestrante)
        {
            try
            {
                var evento = _repo.GetEventoAsyncById(palestranteId, false);
                if(evento == null) return NotFound();

                _repo.Update(palestrante);

                if(await _repo.SaveChangesAsync()) 
                    return Created($"api/palestrante{palestrante.Id}", palestrante);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }

        [HttpDelete("{palestranteId}")]
        public async Task<IActionResult> Delete(int palestranteId)
        {
            try
            {
                var pal = await _repo.GetPalestranteAsyncById(palestranteId, false);
                if(pal == null) return NotFound();

                _repo.Delete(pal);
                if(await _repo.SaveChangesAsync())
                    return Ok();
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }
    }
}