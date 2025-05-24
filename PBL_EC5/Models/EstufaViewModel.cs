namespace PBL_EC5.Models
{
    public class EstufaViewModel : PadraoViewModel
    {
        public int Id_Cliente { get; set; }
        public string Numero_Serie { get; set; }
        public string Marca { get; set; }
        public double Potencia { get; set; }
        public byte[] Foto { get; set; }
        public int Tensao { get; set; }
    }
}