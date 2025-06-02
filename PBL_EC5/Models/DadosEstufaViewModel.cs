using System;

namespace PBL_EC5.Models
{
    public class DadosEstufaViewModel : PadraoViewModel
    {
        public int Id_Estufa { get; set; }
        public double Temperatura { get; set; }
        public DateTime Data { get; set; }
        public double? Tensao { get; set; }
    }
}