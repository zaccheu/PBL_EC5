using PBL_EC5.Models;
using PBL_EC5.Models.DAO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PBL_EC5.DAO
{
    public class DadosEstufaDAO : PadraoDAO<DadosEstufaViewModel>
    {
        protected override SqlParameter[] CriaParametros(DadosEstufaViewModel dadosEstufa)
        {
            SqlParameter[] parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("Id_Estufa", dadosEstufa.Id_Estufa);
            parametros[1] = new SqlParameter("Temperatura", dadosEstufa.Temperatura);
            parametros[2] = new SqlParameter("Data", dadosEstufa.Data);
            parametros[3] = new SqlParameter("Tensao", dadosEstufa.Tensao);

            return parametros;
        }

        protected override DadosEstufaViewModel MontaModel(DataRow registro)
        {
            DadosEstufaViewModel dadosEstufa = new DadosEstufaViewModel();
            dadosEstufa.Id_Estufa = registro.Table.Columns.Contains("Id_Estufa") && registro["Id_Estufa"] != DBNull.Value
                ? Convert.ToInt32(registro["Id_Estufa"])
                : 0;
            dadosEstufa.Temperatura = registro.Table.Columns.Contains("Temperatura") && registro["Temperatura"] != DBNull.Value
                ? Convert.ToDouble(registro["Temperatura"])
                : 0.0;
            dadosEstufa.Data = registro.Table.Columns.Contains("Data") && registro["Data"] != DBNull.Value
                ? Convert.ToDateTime(registro["Data"])
                : DateTime.MinValue;
            dadosEstufa.Tensao = registro.Table.Columns.Contains("Tensao") && registro["Tensao"] != DBNull.Value
                ? Convert.ToDouble(registro["Tensao"])
                : 0.0;

            return dadosEstufa;
        }

        protected override void SetTabela()
        {
            Tabela = "DadosEstufa";
            NomeSpListagem = "spListagemDadosComEstufa";
        }
    }
}
