using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teste_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLerArquivo_Click(object sender, EventArgs e)
        {

            Arquivo a = new Arquivo();
            a.lerArquivo();
            MessageBox.Show("Foram cadastradas: " +a.countP + " Pessoas e " + a.countA + " Alunos");
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Banco bd = new Banco();
            string sql = "select pessoa.nome, pessoa.telefone, pessoa.cidade, pessoa.rg, pessoa.cpf, curso.nome, curso.matricula, curso.codigocurso from pessoa inner join matriculaPessoaCurso on pessoa.id = matriculaPessoaCurso.fk_pessoa inner join curso on curso.id = matriculaPessoaCurso.fk_curso";
            DataTable dt = new DataTable();

            dt = bd.executarConsultaGenerica(sql);

            dataGridView1.DataSource = dt;
        }
    }
}
