using Microsoft.AspNetCore.Mvc;
using psychic_pancake.models;
using psychic_pancake.servicos;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace psychic_pancake.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> logger;
        private readonly ClienteService clienteService;

        public ClienteController(ILogger<ClienteController> logger, ClienteService clienteService)
        {
            this.logger = logger;
            this.clienteService = clienteService;
        }

        // GET: api/<ClienteController>
        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            return clienteService.Get();
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return clienteService.Get().Find(x => x.Id == id) ?? (object)new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.NotFound };
        }

        // POST api/<ClienteController>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] string nome)
        {
            var cliente = new Cliente
            {
                Id = 0,
                Ativo = true,
                Nome = nome
            };

            var response = new HttpResponseMessage()
            {
                StatusCode = clienteService.Add(cliente) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NotFound
            };

            return response;
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public HttpResponseMessage Put(int id, [FromBody] Cliente cliente)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = clienteService.Set(id, cliente) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NotFound
            };

            return response;
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = clienteService.Remove(id) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NotFound
            };

            return response;
        }
    }
}
