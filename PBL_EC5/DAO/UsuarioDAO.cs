using PBL_EC5.Models;
using PBL_EC5.Models.DAO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace PBL_EC5.DAO
{
    public class UsuarioDAO : PadraoDAO<UsuarioViewModel>
    {
        protected override SqlParameter[] CriaParametros(UsuarioViewModel usuario)
        {
            SqlParameter[] parametros = new SqlParameter[8];
            parametros[0] = new SqlParameter("id", usuario.Id);
            parametros[1] = new SqlParameter("nome", usuario.Nome);
            parametros[2] = new SqlParameter("telefone", usuario.Telefone);
            parametros[3] = new SqlParameter("cep", usuario.Cep);
            parametros[4] = new SqlParameter("cpf", usuario.Cpf);
            parametros[5] = new SqlParameter("dataNascimento", usuario.DataNascimento > SqlDateTime.MinValue.Value ? (object)usuario.DataNascimento : DBNull.Value);
            parametros[6] = new SqlParameter("email", usuario.Email);
            parametros[7] = new SqlParameter("salt", usuario.Salt);
            return parametros;
        }

        protected override UsuarioViewModel MontaModel(DataRow registro)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            usuario.Id = Convert.ToInt32(registro["Id"]);
            usuario.Nome = registro["Nome"].ToString();
            usuario.Telefone = registro["Telefone"].ToString();
            usuario.Cpf = registro["Cpf"].ToString();
            usuario.Cep = registro["Cep"].ToString();

            if (registro["Data_Nascimento"] != DBNull.Value)
                usuario.DataNascimento = Convert.ToDateTime(registro["Data_Nascimento"]);

            usuario.Email = registro["Email"]?.ToString();

            if (registro["Salt"] != DBNull.Value)
                usuario.Salt = (byte[])registro["Salt"];
            if (registro["SenhaHash"] != DBNull.Value)
                usuario.SenhaHash = (byte[])registro["SenhaHash"];

            return usuario;
        }

        protected override void SetTabela()
        {
            Tabela = "acesso.Usuario";
        }
    }
}
