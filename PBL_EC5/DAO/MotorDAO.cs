using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using PBL_EC5.DAO;

namespace PBL_EC5.Models.DAO
{
    public class MotorDAO
    {
        private SqlParameter[] CriaParametros(MotorViewModel motor)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("@Id", motor.Id),
                new SqlParameter("@IdEstufa", motor.IdEstufa),
                new SqlParameter("@Nome", (object)motor.Nome ?? DBNull.Value),
                new SqlParameter("@Tipo", (object)motor.Tipo ?? DBNull.Value),
                new SqlParameter("@Status", motor.Status),
                new SqlParameter("@Descricao", (object)motor.Descricao ?? DBNull.Value)
            };

            return parametros;
        }
        
        public MotorViewModel ObterPorId(int id)
        {
            string sql = "SELECT * FROM Motores WHERE Id = @Id";
            SqlParameter[] parametros = { new SqlParameter("@Id", id) };

            DataTable tabela = HelperDAO.ExecutaSelect(sql, parametros);
            if (tabela.Rows.Count == 0)
                return null;

            return MontaMotor(tabela.Rows[0]);
        }

        public void Inserir(MotorViewModel motor)
        {
            string sql = @"
                INSERT INTO Motores
                    (Id, IdEstufa, Nome, Tipo, Status, Descricao, DataAtualizacao)
                VALUES
                   (@Id, @IdEstufa, @Nome, @Tipo, @Status, @Descricao, GETDATE());
            ";

            HelperDAO.ExecutaSQL(sql, CriaParametros(motor));
        }

        public void Alterar(MotorViewModel motor)
        {
            string sql = @"
                UPDATE Motores
                SET IdEstufa = @IdEstufa,
                    Nome = @Nome,
                    Tipo = @Tipo,
                    Status = @Status,
                    Descricao = @Descricao,
                    DataAtualizacao = GETDATE()
                WHERE Id = @Id
            ";

            HelperDAO.ExecutaSQL(sql, CriaParametros(motor));
        }

        public void Excluir(int id)
        {
            string sql = "DELETE FROM Motores WHERE Id =" + id;

            HelperDAO.ExecutaSQL(sql, null);
        }

        public List<MotorViewModel> Listar()
        {
            List<MotorViewModel> lista = new List<MotorViewModel>();

            string sql = "SELECT * FROM Motores";
            
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            
            foreach (DataRow row in tabela.Rows)
            {
                lista.Add(MontaMotor(row));
            }
            return lista;
        }
        private MotorViewModel MontaMotor(DataRow registro)
        {
            MotorViewModel motor = new MotorViewModel();
            motor.Id = Convert.ToInt32(registro["Id"]);
            motor.IdEstufa = Convert.ToInt32(registro["IdEstufa"]);
            motor.Nome = registro["Nome"]?.ToString();
            motor.Tipo = registro["Tipo"]?.ToString();

            if (registro["Status"] != DBNull.Value)
                motor.Status = Convert.ToChar(registro["Status"]);

            motor.Descricao = registro["Descricao"]?.ToString();
            if (registro["DataAtualizacao"] != DBNull.Value)
                motor.DataAtualizacao = Convert.ToDateTime(registro["DataAtualizacao"]);

            return motor;
        }
    }
}
