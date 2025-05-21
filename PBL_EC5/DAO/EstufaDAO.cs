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
            parametros[0] = new SqlParameter("idEstufa", estufa.IdEstufa);
            parametros[1] = new SqlParameter("nome", estufa.Nome);
            parametros[2] = new SqlParameter("localizacao", estufa.Localizacao);
            parametros[3] = new SqlParameter("descricao", estufa.Descricao);
            parametros[4] = new SqlParameter("areaEstufaM2", estufa.AreaEstufaM2);
            parametros[5] = new SqlParameter("alturaM", estufa.AlturaM);

            return parametros;
        }

        protected override EstufaViewModel MontaModel(DataRow registro)
        {
            EstufaViewModel a = new EstufaViewModel();
            a.Id = Convert.ToInt32(registro["id"]);
            a.IdEstufa = registro.Table.Columns.Contains("idEstufa") && registro["idEstufa"] != DBNull.Value
                ? Convert.ToInt32(registro["idEstufa"])
                : 0;
            a.Nome = registro.Table.Columns.Contains("nome") && registro["nome"] != DBNull.Value
                ? registro["nome"].ToString()
                : string.Empty;
            a.Localizacao = registro.Table.Columns.Contains("localizacao") && registro["localizacao"] != DBNull.Value
                ? registro["localizacao"].ToString()
                : string.Empty;
            a.Descricao = registro.Table.Columns.Contains("descricao") && registro["descricao"] != DBNull.Value
                ? registro["descricao"].ToString()
                : string.Empty;
            a.AreaEstufaM2 = registro.Table.Columns.Contains("areaEstufaM2") && registro["areaEstufaM2"] != DBNull.Value
                ? Convert.ToSingle(registro["areaEstufaM2"])
                : (float?)null;
            a.AlturaM = registro.Table.Columns.Contains("alturaM") && registro["alturaM"] != DBNull.Value
                ? Convert.ToSingle(registro["alturaM"])
                : (float?)null;
            a.DataAtualizacao = registro.Table.Columns.Contains("dataAtualizacao") && registro["dataAtualizacao"] != DBNull.Value
                ? Convert.ToDateTime(registro["dataAtualizacao"])
                : (DateTime?)null;

            return a;
        }

        protected override void SetTabela()
        {
            Tabela = "dbo.Estufas";
        }
    }
}
