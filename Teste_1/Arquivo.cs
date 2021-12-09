using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teste_1
{

    class Arquivo
    {
       public int countP = 0, countA = 0;

        string l1 = "";
        StreamReader sr;
        Aluno a = new Aluno();
        public void lerArquivo()
        {
            string linha;
            sr = new StreamReader("D:\\desafio1.txt");
            linha = sr.ReadLine();

            while (linha != null)
            {
                linha = sr.ReadLine();

                try
                {
                    
                    string[] separa = linha.Split('-');
                    
                    if (separa[0] == "Z")
                    {
                        a.Nome = separa[1];
                        a.Telefone = separa[2];
                        a.Cidade = separa[3];
                        a.RG = separa[4];
                        a.CPF = separa[5];
                        bool retorno = a.gravarPessoa();
                        l1 = separa[0];
                        countP++;
                    }
                    else if (separa[0] == "Y")
                    {
                        a.Matricula = separa[1];
                        a.CodigoCurso = separa[2];
                        a.NomeCurso = separa[3];
                        bool retorno = a.gravarAluno();
                        if(l1 == "Z")
                        {
                            a.consultaAluno(a.CodigoCurso);
                            a.consultaPessoa(a.CPF);
                            retorno = matriculaPessoaCurso();
                                                  
                            l1 = "";
                            
                        }
                        countA++;
                    }
                    
                }
               
                catch (Exception)
                {

                    return;
                }
                    
            }

            sr.Close();

        }

         public bool matriculaPessoaCurso()
        {

            Banco banco = new Banco();
            SqlConnection cn = banco.abrirConexao();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand command = new SqlCommand();

            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;

            command.CommandText = "insert into matriculaPessoaCurso values(@fk_pessoa, @fk_curso);";
            command.Parameters.Add("@fk_pessoa", SqlDbType.Int);
            command.Parameters.Add("@fk_curso", SqlDbType.Int);


            command.Parameters[0].Value = a.Id;
            command.Parameters[1].Value = a.ID;


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

        

    }
}
