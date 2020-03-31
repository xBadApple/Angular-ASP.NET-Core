using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Respository;
using ProAgil.webapi.Dtos;

namespace ProAgil.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;
        private readonly IMapper _mapper;
        public EventoController(IProAgilRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {  
            try
            {
                var eventos = await _repo.GetAllEventoAsync(true);
                var result = _mapper.Map<EventoDto[]>(eventos);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpPost("upload")]
        public IActionResult Upload()
        {  
            try
            {
                var file = Request.Form.Files[0];
                var folderPath = Path.Combine("Resources","Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderPath);

                if(file.Length > 0)
                {
                    var fileName = file.FileName.Replace("\""," ").Trim();
                    var fullPath = Path.Combine(pathToSave, fileName);

                    using(var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyToAsync(stream);
                    }
                }
                return Ok();
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }
     
        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {  
            try
            {   
                var evento = await _repo.GetEventoAsyncById(EventoId, true);
                var result = _mapper.Map<EventoDto>(evento);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }
        
        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {  
            try
            {
                var eventos = await _repo.GetAllEventoAsyncByTema(tema, true);
                var result = _mapper.Map<EventoDto[]>(eventos);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {  
            try
            {
                var evento = _mapper.Map<Evento>(model);
                _repo.Add(evento);

                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> Put(int eventoId, Evento model)
        {  
            try
            {
                var evento = await _repo.GetEventoAsyncById(eventoId, false);
                if(evento == null) return NotFound();

                _repo.Update(model);

                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{eventoId}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }

        [HttpDelete("{eventoId}")]
        public async Task<IActionResult> Delete(int eventoId)
        {  
            try
            {
                var evento = await _repo.GetEventoAsyncById(eventoId, false);
                if(evento == null) return NotFound();

                _repo.Delete(evento);
                
                if(await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }
    }
}