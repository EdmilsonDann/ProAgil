using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Repository;


namespace ProAgil.WebAPI.Controllers
{
   [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //propriedade readonly
        public ProAgilContext Context { get; }

        public ValuesController(ProAgilContext context)
        {
            this.Context = context;

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
                 var result = await Context.Eventos.ToListAsync();
                 return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

        }


        // QUANDO Trabalhamos com IActionResult, precisamos retornar sempre com Ok, pois irá criar um OkObject Result para responder
        // Utlizamos ele pra nao ficar tao acoplato -> public ActionResult<Evento> Get(int id) " Aqui podemos ver o forte acomplamento sendo necessário o retorno sempre do Evento"
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                //nao esqueça que essa é uma chamada que abre uma trade e é assincrono, temos que fazer ele esperar ir ao banco pegar tudo e retornoar.. por isso o await
                  var result = await Context.Eventos.FirstOrDefaultAsync(e=>e.Id == id);
                 return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
           
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
