using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste_1
{
    class Aluno : Pessoa
    {
        private int _id;

        private string _matricula, _codigoCurso, _nomeCurso;
        public string Matricula { get => _matricula; set => _matricula = value; }
        public string CodigoCurso { get => _codigoCurso; set => _codigoCurso = value; }
        public string NomeCurso { get => _nomeCurso; set => _nomeCurso = value; }
        public int ID { get => _id; set => _id = value; }
   

        public bool gravarAluno()
        {
            Banco banco = new Banco();
            SqlConnection cn = banco.abrirConexao();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand command = new SqlCommand();

            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;

            command.CommandText = "insert into curso values(@matricula, @codigocurso, @nome);";
            command.Parameters.Add("@matricula", SqlDbType.VarChar);
            command.Parameters.Add("@codigocurso", SqlDbType.VarChar);
            command.Parameters.Add("@nome", SqlDbType.VarChar);

            command.Parameters[0].Value = _matricula;
            command.Parameters[1].Value = _codigoCurso;
            command.Parameters[2].Value = _nomeCurso;

            try
            {
                command.ExecuteNonQuery();
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                banco.fecharConexao();
            }


        }

       public string consultaAluno(string codigoCurso)
        {
            Banco bd = new Banco();

            try
            {
                SqlConnection cn = bd.abrirConexao();
                SqlCommand command = new SqlCommand("Select * from curso", cn);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetString(2)==codigoCurso)
                    {
                        _id = reader.GetInt32(0);
                    }
                }
                return null;


            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                bd.fecharConexao();
            }
        }

      
    }
      
}