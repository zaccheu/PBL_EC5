using System.ComponentModel.DataAnnotations;

namespace PBL_EC5.Models
{
    public class ClienteViewModel : PadraoViewModel
    {
        [Display(Name = "Usuario")]
        public int? Id_Usuario { get; set; }

        [Required(ErrorMessage = "A Razão Social é obrigatória.")]
        [StringLength(255, ErrorMessage = "A Razão Social deve ter no máximo 255 caracteres.")]
        [Display(Name = "Razao Social")]
        public string Razao_Social { get; set; }

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [StringLength(14, ErrorMessage = "O CNPJ deve ter 14 caracteres.")]
        [Display(Name = "CNPJ")]
        public string CNPJ { get; set; }

        [StringLength(9, ErrorMessage = "O CEP deve ter no máximo 9 caracteres.")]
        [Display(Name = "CEP")]
        public string CEP { get; set; }

        [StringLength(255, ErrorMessage = "A Rua deve ter no máximo 255 caracteres.")]
        [Display(Name = "Rua")]
        public string Rua { get; set; }

        [Display(Name = "Numero")]
        public int? Numero { get; set; }

        [Required(ErrorMessage = "O campo Ativo é obrigatório.")]
        [Display(Name = "Ativo")]
        public char Ativo { get; set; } = '1';
    }
}