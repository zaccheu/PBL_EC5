using System;
using System.Data;
using System.Data.SqlClient;

namespace PBL_EC5.Models.DAO
{
    public class EstufaDAO : PadraoDAO<EstufaViewModel>
    {
        protected override SqlParameter[] CriaParametros(EstufaViewModel estufa)
        {
            SqlParameter[] parametros = new SqlParameter[7];
            parametros[0] = new SqlParameter("Id", estufa.Id);
            parametros[1] = new SqlParameter("Id_Cliente", estufa.Id_Cliente != 0 ? estufa.Id_Cliente : (object)DBNull.Value);
            parametros[2] = new SqlParameter("Numero_Serie", estufa.Numero_Serie);
            parametros[3] = new SqlParameter("Marca", estufa.Marca);
            parametros[4] = new SqlParameter("Potencia", estufa.Potencia);
            parametros[5] = new SqlParameter("Tensao", estufa.Tensao);
            parametros[6] = new SqlParameter("Ativo", estufa.Ativo);

            return parametros;
        }

        protected override EstufaViewModel MontaModel(DataRow registro)
        {
            EstufaViewModel estufa = new EstufaViewModel();
            estufa.Id = Convert.ToInt32(registro["Id"]);
            estufa.Id_Cliente = registro.Table.Columns.Contains("Id_Cliente") && registro["Id_Cliente"] != DBNull.Value
                ? Convert.ToInt32(registro["Id_Cliente"])
                : 0;
            estufa.Numero_Serie = registro.Table.Columns.Contains("Numero_Serie") && registro["Numero_Serie"] != DBNull.Value
                ? registro["Numero_Serie"].ToString()
                : string.Empty;
            estufa.Marca = registro.Table.Columns.Contains("Marca") && registro["Marca"] != DBNull.Value
                ? registro["Marca"].ToString()
                : string.Empty;
            estufa.Potencia = registro.Table.Columns.Contains("Potencia") && registro["Potencia"] != DBNull.Value
                ? Convert.ToDouble(registro["Potencia"])
                : 0.0;
            estufa.Tensao = registro.Table.Columns.Contains("Tensao") && registro["Tensao"] != DBNull.Value
                ? Convert.ToInt32(registro["Tensao"])
                : 0;
            estufa.Ativo = registro.Table.Columns.Contains("Ativo") && registro["Ativo"] != DBNull.Value
                ? Convert.ToChar(registro["Ativo"])
                : '0';
            estufa.NomeCliente = registro.Table.Columns.Contains("NomeCliente") && registro["NomeCliente"] != DBNull.Value
                ? registro["NomeCliente"].ToString()
                : string.Empty;

            return estufa;
        }

        protected override void SetTabela()
        {
            Tabela = "Estufa";
            NomeSpListagem = "spListagemEstufaComCliente";
        }
    }
}
