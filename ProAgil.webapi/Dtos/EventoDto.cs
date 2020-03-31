using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.webapi.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }

        [Required]
        public string Local { get; set; }
        public string DataEvento { get; set; }

        [Required]
        public string Tema { get; set; }

        [Range(20,300)]
        public int QtdPessoas { get; set; }
        public string ImagemUrl { get; set; }

        [Phone]
        public string Telefone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public List<LoteDto> Lotes { get; set;}
        public List<RedeSocialDto> RedesSociais { get; set;}
        public List<PalestranteDto> Palestrantes { get; set;}
    }
}