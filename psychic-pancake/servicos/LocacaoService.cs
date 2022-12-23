using Microsoft.AspNetCore.Mvc;
using psychic_pancake.models;
using System.Text.Json;

namespace psychic_pancake.servicos
{
    public class LocacaoService
    {
        private readonly ILogger<LocacaoService> logger;
        private readonly List<Locacao> locacoes;

        public LocacaoService(ILogger<LocacaoService> logger)
        {
            this.logger = logger;

            locacoes = new List<Locacao>();
        }

        public List<Locacao> Get()
        {
            return locacoes;
        }
        public bool Add(Locacao locacao)
        {
            try
            {
                if (locacoes.Any(x => x.Id == locacao.Id))
                {
                    var id = locacoes.Max(x => x.Id) + 1;
                    locacao.Id = id;
                }

                locacoes.Add(locacao);

                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Erro ao adicionar locacao: {JsonSerializer.Serialize(locacao)}");

                return false;
            }
        }
        public bool Set(int id, Locacao locacao)
        {
            try
            {
                var loc = locacoes.Find(x => x.Id == id);

                if (loc == null)
                {
                    logger.LogWarning($"Erro ao atualizar locacao: {JsonSerializer.Serialize(locacao)}");

                    return false;
                }

                int ix = locacoes.IndexOf(loc);

                locacao.Id = id;
                loc = locacao;

                locacoes[ix] = loc;

                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Erro ao atualizar locacao: {JsonSerializer.Serialize(locacao)}");

                return false;
            }
        }
        public bool Remove(int id)
        {
            try
            {
                var loc = locacoes.Find(x => x.Id == id);

                if (loc == null)
                {
                    logger.LogWarning($"Erro ao remover locacao id: {id}");

                    return false;
                }

                int ix = locacoes.IndexOf(loc);

                loc.Devolvido = true;

                locacoes[ix] = loc;

                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Erro ao remover locacao id: {id}");

                return false;
            }
        }
    }

}
