using System.ComponentModel.DataAnnotations;

namespace PBL_EC5.Models
{
    public class UsuarioViewModel : PadraoViewModel
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(15)]
        public string Cpf { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public TipoAdministrador Administrador { get; set; } // '0' ou '1' como char

        public byte[] Salt { get; set; }

        public byte[] SenhaHash { get; set; }
    }
}