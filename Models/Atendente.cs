using Models.Enums;

namespace Models
{
    public class Atendente:Pessoa
    {
        public string Cargo { get; set; }
        public Setor Setor { get; set; }
    }
}