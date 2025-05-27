using Microsoft.AspNetCore.Http;
using System;
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
        public TipoAdministrador Administrador { get; set; } = TipoAdministrador.Nao; // '0' ou '1' como char

        [Required]
        public string Senha { get; set; }

        public IFormFile FotoUpload { get; set; }

        public byte[] Foto { get; set; }

        public string FotoBase64
        {
            get
            {
                if (Foto != null)
                    return Convert.ToBase64String(Foto);
                else
                    return string.Empty;
            }
        }
    }
}