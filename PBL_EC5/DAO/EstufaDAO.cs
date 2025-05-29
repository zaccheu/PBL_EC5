using System;
using System.Data;
using System.Data.SqlClient;

namespace PBL_EC5.Models.DAO
{
    public class EstufaDAO : PadraoDAO<EstufaViewModel>
    {
        protected override SqlParameter[] CriaParametros(EstufaViewModel estufa)
        {
            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("Id", estufa.Id);
            parametros[1] = new SqlParameter("Id_Cliente", estufa.Id_Cliente != 0 ? estufa.Id_Cliente : (object)DBNull.Value);
            //parametros[2] = new SqlParameter("Id_Estado", estufa.Id_Estado);
            parametros[2] = new SqlParameter("Numero_Serie", estufa.Numero_Serie);
            parametros[3] = new SqlParameter("Marca", estufa.Marca);
            parametros[4] = new SqlParameter("Potencia", estufa.Potencia);
            parametros[5] = new SqlParameter("Tensao", estufa.Tensao);

            return parametros;
        }

        protected override EstufaViewModel MontaModel(DataRow registro)
        {
            EstufaViewModel estufa = new EstufaViewModel();
            estufa.Id = Convert.ToInt32(registro["Id"]);
            estufa.Id_Cliente = registro.Table.Columns.Contains("Id_Cliente") && registro["Id_Cliente"] != DBNull.Value
                ? Convert.ToInt32(registro["Id_Cliente"])
                : 0;
            //estufa.Id_Estado = registro.Table.Columns.Contains("Id_Estado") && registro["Id_Estado"] != DBNull.Value
            //    ? Convert.ToInt32(registro["Id_Estado"])
            //    : 0;
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

            return estufa;
        }

        protected override void SetTabela()
        {
            Tabela = "Estufa";
        }
    }
}
