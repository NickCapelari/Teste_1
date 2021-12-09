using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Teste_1
{
    class Banco
    {

        private string stringConexao = "Data Source=localhost; Initial Catalog=Teste1; User ID=usuario; password=senha;language=Portuguese";

        private SqlConnection cn;

        private void conexao()
        {
            cn = new SqlConnection(stringConexao);
        }

        public SqlConnection abrirConexao()
        {
            try
            {
                conexao();
                cn.Open();
                return cn;
            }
            catch (Exception)
            {
                return null;

            }
        }

        public void fecharConexao()
        {
            try
            {
                cn.Close();
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public DataTable executarConsultaGenerica(string sql)
        {
            try
            {
                abrirConexao();

                SqlCommand command = new SqlCommand(sql, cn);
                command.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception)
            {

                return null;
            }
            finally
            {
                fecharConexao();
            }


        }
    }
}
