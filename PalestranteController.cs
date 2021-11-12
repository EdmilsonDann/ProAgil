using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PalestranteController : ControllerBase
    {
        public IProAgilRepository _repo { get; }
        public PalestranteController(IProAgilRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                 var retorno = await _repo.GetAllPalestrantesAsyncByName(name, true);
                 return Ok(retorno);
            }
            catch (Exception)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

         [HttpGet("palestranteId}")]
        public async Task<IActionResult> Get(int palestranteId)
        {
            try
            {
                 var retorno = await _repo.GetPalestranteAsyncById(palestranteId, true);
                 return Ok(retorno);
            }
            catch (Exception)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        
         [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
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
        public async Task<IActionResult> Put(int palestranteId, Palestrante model)
        {
            try
            {
                //Primeira coisa é tentar encontrar o palestrante se ele ja existe, para poder ser alterado!!
                //e coloca-se falso ali, por que nao quero a pesquisa dos palestrantes
                //quero só saber se o palestrante foi encontrado.
                Palestrante palestrante = await _repo.GetPalestranteAsyncById(palestranteId, false);

                if(palestrante == null)
                {
                    return NotFound();
                }

                 _repo.Update(model); //aqui estou adicionando um model e nao precisa ser assincrono... mas na hora de salvar precisa!!
                                   //porque aqui eu estou mundando o estado do meu entity framework

                 if(await _repo.SaveChangesAsync()) //e aqui ele salva toda a mudança de estado que eu tive
                 {
                     //aqui eu retorno atraves do Created um status Code 201 
                     return Created($"/api/palestrante/{model.Id}", model);
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
        public async Task<IActionResult> Delete(int palestranteId)
        {
            try
            {
                //Primeira coisa é tentar encontrar o palestrante se ele ja existe, para poder ser alterado!!
                //e coloca-se falso ali, por que nao quero a pesquisa dos palestrantes
                //quero só saber se o palestrante foi encontrado.
                Palestrante palestrante = await _repo.GetPalestranteAsyncById(palestranteId, false);

                if(palestrante == null)
                {
                    return NotFound();
                }

                 _repo.Delete(palestrante); //aqui estou adicionando um model e nao precisa ser assincrono... mas na hora de salvar precisa!!
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