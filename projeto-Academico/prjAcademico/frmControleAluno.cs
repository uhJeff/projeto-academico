using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjAcademico
{
    public partial class frmControleAluno : Form
    {
        public frmControleAluno()
        {
            InitializeComponent();
        }

        public void Limpar()
        {
            txtProntuario.Clear();
            txtNome.Clear();
            txtEmail.Clear();
            txtCpf.Clear();
            txtRg.Clear();

            txtProntuario.Enabled = true;
            txtNome.Enabled = false;
            txtEmail.Enabled = false;

            btnPesquisar.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;

            txtProntuario.Focus();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Aluno aluno = new Aluno();
            aluno.Prontuario = txtProntuario.Text;

            if(aluno.Consultar())
            {
                txtNome.Text = aluno.Nome;
                txtEmail.Text = aluno.Email;
                txtCpf.Text = aluno.Cpf;
                txtRg.Text = aluno.Rg;

                txtProntuario.Enabled = false;
                txtNome.Enabled = true;
                txtEmail.Enabled = true;
                btnEditar.Enabled = true;
                btnExcluir.Enabled = true;

                btnPesquisar.Enabled = false;
            }
            else
            {
                MessageBox.Show("Aluno não encontrado.", "Erro", MessageBoxButtons.OK);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Aluno aluno = new Aluno();
            aluno.Prontuario = txtProntuario.Text;
            aluno.Nome = txtNome.Text;
            aluno.Email = txtEmail.Text;

            try 
            {
                aluno.Alterar();
                MessageBox.Show("Os dados do aluno foram editados com sucesso.", "Sucesso", MessageBoxButtons.OK);
            }
            catch 
            { 
                MessageBox.Show("Houve um problema para editar os dados do aluno! Tente novamente", "Erro", MessageBoxButtons.OK);
            }
            finally
            {
                Limpar();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Aluno aluno = new Aluno();
            aluno.Prontuario = txtProntuario.Text;

            try
            {
                aluno.Excluir();
                MessageBox.Show("O aluno foi excluído com sucesso.", "Sucesso", MessageBoxButtons.OK);
            }
            catch
            {
                MessageBox.Show("Houve um problema para excluir o aluno! Tente novamente", "Erro", MessageBoxButtons.OK);
            }
            finally
            {
                Limpar();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            Limpar();

            frmCadastroAluno form = new frmCadastroAluno();
            form.ShowDialog();
        }
    }
}