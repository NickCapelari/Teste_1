using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste_1
{
    class Pessoa
    {

        private int _idP;
        private string _nome, _telefone, _cidade, _rg, _cpf;

        public string Nome { get => _nome; set => _nome = value; }
        public string Telefone { get => _telefone; set => _telefone = value; }
        public string Cidade { get => _cidade; set => _cidade = value; }
        public string RG { get => _rg; set => _rg = value; }
        public string CPF { get => _cpf; set => _cpf = value; }

        public int Id { get => _idP; set => _idP = value; }

       



        public bool gravarPessoa()
        {
            Banco banco = new Banco();
            SqlConnection cn = banco.abrirConexao();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand command = new SqlCommand();

            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;

            command.CommandText = "insert into pessoa values(@nome, @telefone, @cidade, @RG, @CPF);";
            command.Parameters.Add("@nome", SqlDbType.VarChar);
            command.Parameters.Add("@telefone", SqlDbType.VarChar);
            command.Parameters.Add("@cidade", SqlDbType.VarChar);
            command.Parameters.Add("@RG", SqlDbType.VarChar);
            command.Parameters.Add("@CPF", SqlDbType.VarChar);
            command.Parameters[0].Value = _nome;
            command.Parameters[1].Value = _telefone;
            command.Parameters[2].Value = _cidade;
            command.Parameters[3].Value = _rg;
            command.Parameters[4].Value = _cpf;

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
        public string consultaPessoa(string cpf)
        {
            Banco bd = new Banco();

            try
            {
                SqlConnection cn = bd.abrirConexao();
                SqlCommand command = new SqlCommand("Select * from pessoa", cn);

                SqlDataReader reader = command.ExecuteReader();
                //reader.Read();
                //_id = reader.GetInt32(0);

               while (reader.Read())
               {
                    if (reader.GetString(5) == cpf)
                    {
                        _idP = reader.GetInt32(0);
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
