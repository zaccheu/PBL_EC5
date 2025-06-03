using PBL_EC5.Models;
using PBL_EC5.Models.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PBL_EC5.DAO
{
    public class DadosEstufaDAO : PadraoDAO<DadosEstufaViewModel>
    {
        protected override SqlParameter[] CriaParametros(DadosEstufaViewModel dadosEstufa)
        {
            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("Id", dadosEstufa.Id);
            parametros[1] = new SqlParameter("Id_Estufa", dadosEstufa.Id_Estufa);
            parametros[2] = new SqlParameter("Id_Temperatura", dadosEstufa.Id_Temperatura);
            parametros[3] = new SqlParameter("Temperatura", dadosEstufa.Temperatura);
            parametros[4] = new SqlParameter("Data", dadosEstufa.Data);
            parametros[5] = new SqlParameter("Tensao", dadosEstufa.Tensao);

            return parametros;
        }

        protected override DadosEstufaViewModel MontaModel(DataRow registro)
        {
            DadosEstufaViewModel dadosEstufa = new DadosEstufaViewModel();
            dadosEstufa.Id = Convert.ToInt32(registro["Id"]);
            dadosEstufa.Id_Estufa = registro.Table.Columns.Contains("Id_Estufa") && registro["Id_Estufa"] != DBNull.Value
                ? Convert.ToInt32(registro["Id_Estufa"])
                : 0;
            dadosEstufa.Id_Temperatura = registro.Table.Columns.Contains("Id_Temperatura") && registro["Id_Temperatura"] != DBNull.Value
                ? registro["Id_Temperatura"].ToString()
                : string.Empty;
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

        public bool ConsultaIdTemperatura(string idTemperatura)
        {
            var p = new SqlParameter[]
            {
                 new SqlParameter("id_Temperatura", idTemperatura)
            };

            var tabela = HelperDAO.ExecutaProcSelect("spConsultaIdTemperatura", p);
            if (tabela.Rows.Count == 0)
                return false;
            else
                return true;
        }

        public async Task<List<DadosEstufaViewModel>> BuscarHistorico(FiltroHistoricoRequest dto)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id_estufa", dto.IdEstufa),
                new SqlParameter("dataInicial", dto.DataInicial),
                new SqlParameter("dataFinal", dto.DataFinal)
            };

            var tabela = await Task.Run(() => HelperDAO.ExecutaProcSelect("spBuscarHistoricoDadosEstufa", p));
            var lista = new List<DadosEstufaViewModel>();

            foreach (DataRow row in tabela.Rows)
            {
                lista.Add(MontaModel(row));
            }

            return lista;
        }
    }
}
