using System.ComponentModel.DataAnnotations;

namespace ProAgil.webapi.Dtos
{
    public class LoteDto
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [Range(5, 1000)]
        public decimal Preco { get; set; }
        public string Inicio { get; set; }
        public string Fim { get; set; }
        public int Quantidade { get; set; }
    }
}