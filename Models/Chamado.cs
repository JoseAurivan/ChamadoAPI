#nullable enable
using System;
using Models.Enums;
using Models.Interface;

namespace Models
{
    public class Chamado:IModelBase
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string? Resposta { get; set; }
        public string? NumeroProtocolo { get; set; }
        public Cliente? Cliente { get; set; }
        public StatusChamado StatusChamado { get; set; }
        public Setor SetorDestimno { get; set; }
        public DateTime? DataAbertura { get; set; }
        public DateTime? DataFechado { get; set; }
    }
}