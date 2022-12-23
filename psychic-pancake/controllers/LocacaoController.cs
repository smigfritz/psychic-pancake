using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using psychic_pancake.models;
using psychic_pancake.viewModels;
using psychic_pancake.servicos;
using System.Text;

namespace psychic_pancake.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaoController : ControllerBase
    {
        private readonly ILogger<LocacaoController> logger;
        private readonly FilmeService filmeService;
        private readonly LocacaoService locacaoService;
        private readonly ClienteService clienteService;

        public LocacaoController(ILogger<LocacaoController> logger, FilmeService filmeService, LocacaoService locacaoService, ClienteService clienteService)
        {
            this.logger = logger;
            this.filmeService = filmeService;
            this.locacaoService = locacaoService;
            this.clienteService = clienteService;
        }

        // GET: api/<LocacaoController>
        [HttpGet]
        public List<Locacao> Get()
        {
            return locacaoService.Get().Where(x => x.Devolvido == false).ToList();
        }

        // POST api/<LocacaoController>
        [HttpPost]
        public HttpResponseMessage PostAlugar([FromBody] AlugarViewModel locacao)
        {
            string motivo = string.Empty;
            var response = new HttpResponseMessage();

            var locados = Get();
            var cliente = clienteService.Get().Find(x => x.Id == locacao.IdCliente);
            var filme = filmeService.Get().Find(x => x.Id == locacao.IdFilme);

            if (cliente == null)
            {
                motivo = "Cliente não encontrado";
            }
            else if (!cliente.Ativo)
            {
                motivo = "Cliente não está ativo";
            }

            if (filme == null)
            {
                motivo = "Filme não cadastrado";
            }
            else if (!filme.Disponivel)
            {
                motivo = "Filme não está disponível";
            }

            if (locados.Any(x => x.IdCliente == locacao.IdCliente))
            {
                motivo = "Cliente já tem filme pendente de devolução";
            }


            if (motivo == string.Empty)
            {
                filme.Disponivel = false;
                filmeService.Set(filme.Id, filme);

                var loc = new Locacao
                {
                    Id = 0,
                    IdCliente = locacao.IdCliente,
                    IdFilme = locacao.IdFilme,
                    DataLocacao = DateOnly.FromDateTime(DateTime.Now),
                    DataDevolucao = DateOnly.FromDateTime(locacao.DataDevolucao),
                    Devolvido = false,
                };
                locacaoService.Add(loc);

                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                response.ReasonPhrase = motivo;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }

        // POST api/<LocacaoController>/Devolver
        [HttpPost("Devolver")]
        public HttpResponseMessage PostDevolver([FromBody] DevolverViewModel devolucao)
        {
            string motivo = string.Empty;
            var response = new HttpResponseMessage();

            var locado = locacaoService.Get().Find(x => x.Devolvido == false && x.IdCliente == devolucao.IdCliente);
            
            if (locado == null)
            {
                response.ReasonPhrase = "Locação não encontrada";
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            else
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;

                var filme = filmeService.Get().Find(x => x.Id == locado.IdFilme);
                if (filme != null)
                {
                    filme.Disponivel = true;
                    filmeService.Set(filme.Id, filme);
                }

                if (DateOnly.FromDateTime(devolucao.DataDevolucao) > locado.DataDevolucao)
                {
                    response.ReasonPhrase = "Devolução em atraso";
                    
                }

                locado.Devolvido = true;

                locacaoService.Set(locado.Id, locado);
            }

            return response;
        }
    }
}
