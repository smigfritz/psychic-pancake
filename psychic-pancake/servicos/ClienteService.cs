using psychic_pancake.models;
using System.Collections.Generic;
using System.Text.Json;

namespace psychic_pancake.servicos
{
    public class ClienteService
    {
        private readonly ILogger<ClienteService> logger;
        private readonly List<Cliente> clientes;

        public ClienteService(ILogger<ClienteService> logger)
        {
            this.logger = logger;

            this.clientes = new List<Cliente>();
        }
        public List<Cliente> Get()
        {
            return clientes;
        }
        public bool Add(Cliente cliente)
        {
            try
            {
                if (clientes.Any(x => x.Id == cliente.Id))
                {
                    var id = clientes.Max(x => x.Id) + 1;
                    cliente.Id = id;
                }

                clientes.Add(cliente);

                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Erro ao adicionar cliente: {JsonSerializer.Serialize(cliente)}");

                return false;
            }
        }
        public bool Set(int id, Cliente cliente)
        {
            try
            {
                var cli = clientes.Find(x => x.Id == id);

                if (cli == null)
                {
                    logger.LogWarning($"Erro ao atualizar cliente: {JsonSerializer.Serialize(cliente)}");

                    return false;
                }

                int ix = clientes.IndexOf(cli);

                cliente.Id = id;
                cli = cliente;

                clientes[ix] = cli;

                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Erro ao atualizar cliente: {JsonSerializer.Serialize(cliente)}");

                return false;
            }
        }
        public bool Remove(int id)
        {
            try
            {
                var cli = clientes.Find(x => x.Id == id);
                
                if (cli == null)
                {
                    logger.LogWarning($"Erro ao remover cliente id: {id}");

                    return false;
                }

                int ix = clientes.IndexOf(cli);

                cli.Ativo = false;

                clientes[ix] = cli;

                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Erro ao remover cliente id: {id}");

                return false;
            }
        }
    }

}
