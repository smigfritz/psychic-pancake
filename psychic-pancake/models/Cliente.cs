using System.Runtime.Serialization;

namespace psychic_pancake.models
{
    public class Cliente
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }

    }
}