using System;

namespace PBL_EC5.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        public string Cep { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Email { get; set; }

        // Campos de segurança (hash + salt)
        public byte[] Salt { get; set; }
        public byte[] SenhaHash { get; set; }
    }
}