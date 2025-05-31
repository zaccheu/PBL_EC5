using PBL_EC5.Models;
using PBL_EC5.Models.DAO;
using System.Data.SqlClient;
using System.Data;
using System;

namespace PBL_EC5.DAO
{
    public class ClienteDAO : PadraoDAO<ClienteViewModel>
    {
        protected override SqlParameter[] CriaParametros(ClienteViewModel cliente)
        {
            SqlParameter[] parametros = new SqlParameter[8];
            parametros[0] = new SqlParameter("id", cliente.Id);
            parametros[1] = new SqlParameter("id_usuario", (cliente.Id_Usuario == null || cliente.Id_Usuario == 0) ? (object)DBNull.Value : cliente.Id_Usuario);
            parametros[2] = new SqlParameter("razao_Social", cliente.Razao_Social);
            parametros[3] = new SqlParameter("cnpj", cliente.CNPJ);
            parametros[4] = new SqlParameter("cep", string.IsNullOrEmpty(cliente.CEP) ? (object)DBNull.Value : cliente.CEP);
            parametros[5] = new SqlParameter("rua", string.IsNullOrEmpty(cliente.Rua) ? (object)DBNull.Value : cliente.Rua);
            parametros[6] = new SqlParameter("numero", cliente.Numero ?? (object)DBNull.Value);
            parametros[7] = new SqlParameter("ativo", cliente.Ativo);

            return parametros;
        }

        protected override ClienteViewModel MontaModel(DataRow registro)
        {
            ClienteViewModel cliente = new ClienteViewModel();
            cliente.Id = Convert.ToInt32(registro["Id"]);
            cliente.Id_Usuario = registro.Table.Columns.Contains("Id_Usuario") && registro["Id_Usuario"] != DBNull.Value
                ? Convert.ToInt32(registro["Id_Usuario"])
                : (int?)null;
            cliente.Razao_Social = registro.Table.Columns.Contains("Razao_Social") && registro["Razao_Social"] != DBNull.Value
                ? registro["Razao_Social"].ToString()
                : string.Empty;
            cliente.CNPJ = registro.Table.Columns.Contains("CNPJ") && registro["CNPJ"] != DBNull.Value
                ? registro["CNPJ"].ToString()
                : string.Empty;
            cliente.CEP = registro.Table.Columns.Contains("CEP") && registro["CEP"] != DBNull.Value
                ? registro["CEP"].ToString()
                : string.Empty;
            cliente.Rua = registro.Table.Columns.Contains("Rua") && registro["Rua"] != DBNull.Value
                ? registro["Rua"].ToString()
                : string.Empty;
            cliente.Numero = registro.Table.Columns.Contains("Numero") && registro["Numero"] != DBNull.Value
                ? Convert.ToInt32(registro["Numero"])
                : (int?)null;
            cliente.Ativo = registro.Table.Columns.Contains("Ativo") && registro["Ativo"] != DBNull.Value
                ? Convert.ToChar(registro["Ativo"])
                : '0';

            return cliente;
        }

        protected override void SetTabela()
        {
            Tabela = "Cliente";
        }

        public void DesvinculaClientesDoUsuario(int usuarioId)
        {
            SqlParameter[] parametros = {
                new SqlParameter("@id_usuario", usuarioId)
            };

            HelperDAO.ExecutaProc("spDesvinculaCliente", parametros);
        }
    }
}
