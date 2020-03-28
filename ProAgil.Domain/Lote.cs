using System;

namespace ProAgil.Domain
{
    public class Lote
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public int Quantidade { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get;}
    }
}