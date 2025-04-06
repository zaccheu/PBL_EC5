using System;

namespace PBL_EC5.Models
{
    public class EstufaViewModel
    {
        public int IdEstufa { get; set; }
        public string Nome { get; set; }
        public string Localizacao { get; set; }
        public string Descricao { get; set; }
        public float? AreaEstufaM2 { get; set; }
        public float? AlturaM { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}