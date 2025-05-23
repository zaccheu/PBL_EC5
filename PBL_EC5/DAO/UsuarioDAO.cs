﻿using PBL_EC5.Models;
using PBL_EC5.Models.DAO;
using System;
using System.Data;
using System.Data.SqlClient;

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
            parametros[5] = new SqlParameter("salt", usuario.Salt as object ?? DBNull.Value);
            parametros[6] = new SqlParameter("senhahash", usuario.SenhaHash as object ?? DBNull.Value);
            return parametros;
        }

        protected override UsuarioViewModel MontaModel(DataRow registro)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            usuario.Id = Convert.ToInt32(registro["Id"]);
            usuario.Nome = registro["Nome"].ToString();
            usuario.Cpf = registro["Cpf"]?.ToString();
            usuario.Email = registro["Email"]?.ToString();

            if (registro["Administrador"] != DBNull.Value)
                usuario.Administrador = (TipoAdministrador)Convert.ToChar(registro["Administrador"]);

            if (registro["Salt"] != DBNull.Value)
                usuario.Salt = (byte[])registro["Salt"];

            if (registro["SenhaHash"] != DBNull.Value)
                usuario.SenhaHash = (byte[])registro["SenhaHash"];

            return usuario;
        }

        protected override void SetTabela()
        {
            Tabela = "Usuario";
        }
    }
}
