using PBL_EC5.Models;
using PBL_EC5.Models.DAO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace PBL_EC5.DAO
{
    public class UsuarioDAO : PadraoDAO<UsuarioViewModel>
    {
        protected override SqlParameter[] CriaParametros(UsuarioViewModel usuario)
        {
            SqlParameter[] parametros = new SqlParameter[7];
            parametros[0] = new SqlParameter("id", usuario.Id);
            parametros[1] = new SqlParameter("nome", usuario.Nome);
            parametros[2] = new SqlParameter("cpf", usuario.Cpf as object ?? DBNull.Value);
            parametros[3] = new SqlParameter("email", usuario.Email);
            parametros[4] = new SqlParameter("administrador", (char)usuario.Administrador); // converte enum para char
            parametros[5] = new SqlParameter("senha", usuario.Senha);

            SqlParameter foto = new SqlParameter("foto", SqlDbType.VarBinary, -1); // -1 indica MAX
            // Verifique se usuario.Foto é byte[] e se não é nulo
            if (usuario.Foto != null && usuario.Foto.Length > 0)
                foto.Value = usuario.Foto;
            else
                foto.Value = DBNull.Value;

            parametros[6] = foto;
            return parametros;
        }

        protected override UsuarioViewModel MontaModel(DataRow registro)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            usuario.Id = Convert.ToInt32(registro["Id"]);
            usuario.Nome = registro["Nome"].ToString();
            usuario.Cpf = registro["Cpf"]?.ToString();
            usuario.Email = registro["Email"]?.ToString();
            usuario.Senha = registro["Senha"]?.ToString();

            if (registro["Administrador"] != DBNull.Value)
                usuario.Administrador = (TipoAdministrador)Convert.ToChar(registro["Administrador"]);

            if (registro["Foto"] != DBNull.Value)
                usuario.Foto = (byte[])registro["Foto"];

            return usuario;
        }

        protected override void SetTabela()
        {
            Tabela = "Usuario";
            ChaveIdentity = true;
        }

        public UsuarioViewModel ConsultaLogin(UsuarioViewModel model)
        {
            //Alterar para senha criptografada aqui
            //model.Senha = Criptografia.Encrypt(model.Senha)

            var p = new SqlParameter[]
            {
                 new SqlParameter("email", model.Email),
                 new SqlParameter("senha", model.Senha)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spConsultaLogin", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }
    }
}
