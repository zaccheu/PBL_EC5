using System;

namespace PBL_EC5.Models
{
    public class MotorViewModel
    {
        public int Id { get; set; }
        public int IdEstufa { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public char Status { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}