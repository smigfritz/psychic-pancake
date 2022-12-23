using System.Runtime.Serialization;

namespace psychic_pancake.models
{
    public class Locacao
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdFilme { get; set; }
        public DateOnly DataLocacao { get; set; }
        public DateOnly DataDevolucao { get; set; }
        public bool Devolvido { get; set; }
        
    }
}