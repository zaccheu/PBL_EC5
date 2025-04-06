using PBL_EC5.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PBL_EC5.Models.DAO
{
    public class EstufaDAO
    {
        private SqlParameter[] CriaParametros(EstufaViewModel estufa)
        {
            SqlParameter[] parametros = new SqlParameter[8];
            new SqlParameter("@IdEstufa", estufa.IdEstufa);
            new SqlParameter("@Nome", (object)estufa.Nome ?? DBNull.Value);
            new SqlParameter("@Localizacao", (object)estufa.Localizacao ?? DBNull.Value);
            new SqlParameter("@Descricao", (object)estufa.Descricao ?? DBNull.Value);
            new SqlParameter("@AreaEstufaM2", (object)estufa.AreaEstufaM2 ?? DBNull.Value);
            new SqlParameter("@AlturaM", (object)estufa.AlturaM ?? DBNull.Value);

            return parametros;
        }

        public void Inserir(EstufaViewModel estufa)
        {
            string sql = @"
                INSERT INTO Estufas
                   (IdEstufa, Nome, Localizacao, Descricao, AreaEstufaM2, AlturaM, DataAtualizacao)
                VALUES
                   (@IdEstufa, @Nome, @Localizacao, @Descricao, @AreaEstufaM2, @AlturaM, GETDATE());
            ";

            HelperDAO.ExecutaSQL(sql, CriaParametros(estufa));
        }

        public void Alterar(EstufaViewModel estufa)
        {
            string sql = @"
                UPDATE Estufas
                SET Nome = @Nome,
                    Localizacao = @Localizacao,
                    Descricao = @Descricao,
                    AreaEstufaM2 = @AreaEstufaM2,
                    AlturaM = @AlturaM,
                    DataAtualizacao = GETDATE()
                WHERE IdEstufa = @IdEstufa
            ";

            HelperDAO.ExecutaSQL(sql, CriaParametros(estufa));
        }

        public void Excluir(int idEstufa)
        {
            string sql = "DELETE FROM Estufas WHERE IdEstufa =" + idEstufa;
            HelperDAO.ExecutaSQL(sql, null);
        }

        public List<EstufaViewModel> ListarEstufas()
        {
            List<EstufaViewModel> lista = new List<EstufaViewModel>();

            string sql = "SELECT * FROM Estufas";

            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

            foreach (DataRow row in tabela.Rows)
            {
                lista.Add(MontaEstufa(row));
            }

            return lista;
        }

        public EstufaViewModel MontaEstufa(DataRow registro)
        {
            EstufaViewModel estufa = new EstufaViewModel();
            estufa.IdEstufa = Convert.ToInt32(registro["IdEstufa"]);
            estufa.Nome = registro["Nome"]?.ToString();
            estufa.Localizacao = registro["Localizacao"]?.ToString();
            estufa.Descricao = registro["Descricao"]?.ToString();
            if (registro["AreaEstufaM2"] != DBNull.Value)
                estufa.AreaEstufaM2 = Convert.ToSingle(registro["AreaEstufaM2"]);
            if (registro["AlturaM"] != DBNull.Value)
                estufa.AlturaM = Convert.ToSingle(registro["AlturaM"]);
            if (registro["DataAtualizacao"] != DBNull.Value)
                estufa.DataAtualizacao = Convert.ToDateTime(registro["DataAtualizacao"]);
            return estufa;
        }

        public EstufaViewModel ConsultaPorId(int id)
        {
            string sql = "select * from acesso.Usuario where Id = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaEstufa(tabela.Rows[0]);
        }
    }
}
