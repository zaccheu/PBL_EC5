using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Data.SqlTypes;
using PBL_EC5.Models;

namespace PBL_EC5.DAO
{
    public class UsuarioDAO
    {
        private SqlParameter[] CriaParametros(UsuarioViewModel usuario)
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

        private SqlParameter[] CriaParametrosLogin(UsuarioViewModel usuario)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@email", usuario.Email);
            parametros[1] = new SqlParameter("@senha", usuario.SenhaHash);

            return parametros;
        }

        public void Inserir(UsuarioViewModel usuario)
        {
            string sql = @"
            INSERT INTO acesso.Usuario 
               (Nome, Telefone, Cpf, Cep, Data_Nascimento, Email, Salt, SenhaHash)
            VALUES
               (@Nome, @Telefone, @Cpf, @Cep, @DataNascimento, @Email, @Salt, @SenhaHash);
            ";

            HelperDAO.ExecutaSQL(sql, CriaParametros(usuario));
        }

        public void Alterar(UsuarioViewModel usuario)
        {
            string sql = @"
            UPDATE acesso.Usuario
               SET Nome = @Nome,
                   Telefone = @Telefone,
                   Cpf = @Cpf,
                   Cep = @Cep,
                   Data_Nascimento = @DataNascimento,
                   Email = @Email,
                   Salt = @Salt,
                   SenhaHash = @SenhaHash
             WHERE Id = @Id
            ";

            HelperDAO.ExecutaSQL(sql, CriaParametros(usuario));
        }

        public void Excluir(int id)
        {
            string sql = "delete acesso.Usuario where Id =" + id;
            HelperDAO.ExecutaSQL(sql, null);
        }

        public List<UsuarioViewModel> Listar()
        {
            List<UsuarioViewModel> lista = new List<UsuarioViewModel>();

            string sql = "select * from acesso.Usuario";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

            foreach (DataRow usuario in tabela.Rows)
            {
                lista.Add(MontaLista(usuario));
            }

            return lista;
        }
        public UsuarioViewModel MontaLista(DataRow registro)
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

        public UsuarioViewModel ConsultaPorId(int id)
        {
            string sql = "select * from acesso.Usuario where Id = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaLista(tabela.Rows[0]);
        }

        public UsuarioViewModel ConsultaUser(UsuarioViewModel usuario)
        {
            string sql = "select * from acesso.Usuario where Email = @email and SenhaHash = @senhaHash";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, CriaParametrosLogin(usuario));
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaLista(tabela.Rows[0]);
        }

    }
}
