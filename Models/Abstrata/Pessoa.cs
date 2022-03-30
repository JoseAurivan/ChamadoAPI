using System;
using Models.Interface;

namespace Models
{
    public abstract class Pessoa:IModelBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        
    }
}