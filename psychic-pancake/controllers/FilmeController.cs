using Microsoft.AspNetCore.Mvc;
using psychic_pancake.models;
using psychic_pancake.servicos;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace psychic_pancake.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly ILogger<FilmeController> logger;
        private readonly FilmeService filmeService;

        public FilmeController(ILogger<FilmeController> logger, FilmeService filmeService)
        {
            this.logger = logger;
            this.filmeService = filmeService;
        }

        // GET: api/<FilmeController>
        [HttpGet]
        public IEnumerable<Filme> Get()
        {
            return filmeService.Get();
        }

        // GET api/<FilmeController>/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return filmeService.Get().Find(x => x.Id == id) ?? (object)new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.NotFound };
        }

        // POST api/<FilmeController>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] string titulo)
        {
            var filme = new Filme()
            {
                Titulo = titulo,
                Disponivel = true
            };

            var response = new HttpResponseMessage()
            {
                StatusCode = filmeService.Add(filme) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NotFound
            };

            return response;
        }

        // PUT api/<FilmeController>/5
        [HttpPut("{id}")]
        public HttpResponseMessage Put(int id, [FromBody] Filme filme)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = filmeService.Set(id, filme) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NotFound
            };

            return response;
        }

        // DELETE api/<FilmeController>/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = filmeService.Remove(id) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NotFound
            };

            return response;
        }
    }
}
