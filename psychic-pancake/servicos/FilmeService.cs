using psychic_pancake.models;
using System.Text.Json;

namespace psychic_pancake.servicos
{
    public class FilmeService
    {
        private readonly ILogger<FilmeService> logger;
        private readonly List<Filme> filmes;

        public FilmeService(ILogger<FilmeService> logger) 
        {
            this.logger = logger;

            filmes = new List<Filme>();
        }
        public List<Filme> Get()
        {
            return filmes;
        }
        public bool Add(Filme filme)
        {
            try
            {
                if (filmes.Any(x => x.Id == filme.Id))
                {
                    var id = filmes.Max(x => x.Id) + 1;
                    filme.Id = id;
                }

                filme.Disponivel = true;

                filmes.Add(filme);

                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Erro ao adicionar filme: {JsonSerializer.Serialize(filme)}");

                return false;
            }
        }
        public bool Set(int id, Filme filme)
        {
            try
            {
                var film = filmes.Find(x => x.Id == id);

                if (film == null)
                {
                    logger.LogWarning($"Erro ao atualizar filme: {JsonSerializer.Serialize(filme)}");

                    return false;
                }

                int ix = filmes.IndexOf(film);

                filme.Id = id;
                film = filme;

                filmes[ix] = film;

                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Erro ao atualizar filme: {JsonSerializer.Serialize(filme)}");

                return false;
            }
        }
        public bool Remove(int id)
        {
            try
            {
                var film = filmes.Find(x => x.Id == id);

                if (film == null)
                {
                    logger.LogWarning($"Erro ao remover filme id: {id}");

                    return false;
                }

                int ix = filmes.IndexOf(film);

                film.Disponivel = false;

                filmes[ix] = film;

                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Erro ao remover filme id: {id}");

                return false;
            }
        }
    }
}
