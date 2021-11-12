using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // aqui vou trabalhar com a questao da apiController
                    // AO colocarmos o Apicontroller ele ja nos retorna no json do metodo chamado os erros de validação.
                    //como era o caso do model.IsValid
    public class EventoController : ControllerBase
    {
        public IProAgilRepository _repo { get; }

        public EventoController(IProAgilRepository repo)
        {
            _repo = repo;

        }

        // GET api/values
        // colocado metodo asyncrono e para informar que sera aberto em cada trade, usamos a Task e dentro colocamos o tipo de retorno <IActionResult>
        //Para cada chamadas apralelas sera aberta uma trade diferente e nao travamos o serviço enquanto ele aguarda o retorno do banco. 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //nao esqueça que essa é uma chamada que abre uma trade e é assincrono, temos que fazer ele esperar ir ao banco pegar tudo e retornoar.. por isso o await
                 var result = await _repo.GetAllEventosAsync(true);
                 return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

        }

        [HttpGet("{EventoId}")] //aqui estou informando o parametro que preciso usar
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                //nao esqueça que essa é uma chamada que abre uma trade e é assincrono, temos que fazer ele esperar ir ao banco pegar tudo e retornoar.. por isso o await
                 var result = await _repo.GetEventoAsyncById(EventoId, true);
                 return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

        }

        [HttpGet("getByTema/{tema}")] //aqui estou informando o parametro que preciso usar
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                //nao esqueça que essa é uma chamada que abre uma trade e é assincrono, temos que fazer ele esperar ir ao banco pegar tudo e retornoar.. por isso o await
                 var result = await _repo.GetAllEventoAsyncByTema(tema, true);
                 return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

        }

         [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                 _repo.Add(model); //aqui estou adicionando um model e nao precisa ser assincrono... mas na hora de salvar precisa!!
                                   //porque aqui eu estou mundando o estado do meu entity framework

                 if(await _repo.SaveChangesAsync()) //e aqui ele salva toda a mudança de estado que eu tive
                 {
                     //aqui eu retorno atraves do Created um status Code 201 
                     return Created($"/api/evento/{model.Id}", model);
                 }

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            //se nao der certo o Created.. e nao der erro .. ele vem aqui.
            return BadRequest();

        }

        
        [HttpPut]
        public async Task<IActionResult> Put(int EventoId, Evento model)
        {
            try
            {
                //Primeira coisa é tentar encontrar o evento se ele ja existe, para poder ser alterado!!
                //e coloca-se falso ali, por que nao quero a pesquisa dos palestrantes
                //quero só saber se o evento foi encontrado.
                Evento evento = await _repo.GetEventoAsyncById(EventoId, false);

                if(evento == null)
                {
                    return NotFound();
                }

                 _repo.Update(model); //aqui estou adicionando um model e nao precisa ser assincrono... mas na hora de salvar precisa!!
                                   //porque aqui eu estou mundando o estado do meu entity framework

                 if(await _repo.SaveChangesAsync()) //e aqui ele salva toda a mudança de estado que eu tive
                 {
                     //aqui eu retorno atraves do Created um status Code 201 
                     return Created($"/api/evento/{model.Id}", model);
                 }

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            //se nao der certo o Created.. e nao der erro .. ele vem aqui.
            return BadRequest();

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {
                //Primeira coisa é tentar encontrar o evento se ele ja existe, para poder ser deletado!!
                //e coloca-se falso ali, por que nao quero a pesquisa dos palestrantes
                //quero só saber se o evento foi encontrado.
                Evento evento = await _repo.GetEventoAsyncById(EventoId, false);

                if(evento == null)
                {
                    return NotFound();
                }

                 _repo.Delete(evento); //aqui estou adicionando um model e nao precisa ser assincrono... mas na hora de salvar precisa!!
                                   //porque aqui eu estou mundando o estado do meu entity framework

                 if(await _repo.SaveChangesAsync()) //e aqui ele salva toda a mudança de estado que eu tive
                 {
                     //aqui eu retorno atraves do Created um status Code 201 
                     return Ok();
                 }

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            //se nao der certo o Created.. e nao der erro .. ele vem aqui.
            return BadRequest();

        }
    }
}