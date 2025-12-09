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
    public partial class frmCadastroAluno : Form
    {
        public frmCadastroAluno()
        {
            InitializeComponent();
        }

        private bool ValidarDados(string prontuario, string cpf, string rg)
        {
            Aluno aluno = new Aluno();
            aluno.Prontuario = prontuario;

            if(aluno.ValidaProntuario(prontuario))
            {
                if(aluno.Consultar())
                {
                    MessageBox.Show("O Prontuário inserido já existe! Tente novamente.", "Erro");

                    txtProntuario.Clear();
                    txtProntuario.Focus();

                    return false;
                }
                else
                {
                    if (aluno.ValidaCPF(cpf))
                    {
                        if (aluno.ValidaRG(rg))
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("O RG inserido não é válido! Tente novamente.", "Erro");

                            txtRg.Clear();
                            txtRg.Focus();

                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("O CPF inserido não é válido! Tente novamente.", "Erro");

                        txtCpf.Clear();
                        txtCpf.Focus();

                        return false;
                    }
                }
            }
            else
            {
                MessageBox.Show("O Prontuário inserido não é válido! Tente novamente.", "Erro");
                
                txtProntuario.Clear();
                txtProntuario.Focus();

                return false;
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Aluno aluno = new Aluno();

            aluno.Prontuario = txtProntuario.Text;
            aluno.Nome = txtNome.Text;
            aluno.Cpf = txtCpf.Text;
            aluno.Rg = txtRg.Text;
            aluno.Email = txtEmail.Text;

            if(ValidarDados(aluno.Prontuario, aluno.Cpf, aluno.Rg))
            {
                try
                {
                    aluno.Incluir();
                    MessageBox.Show("O aluno foi cadastrado com sucesso.", "Sucesso", MessageBoxButtons.OK);
                }
                catch
                {
                    MessageBox.Show("Houve um problema para cadastrar o novo aluno! Tente novamente.", "Erro", MessageBoxButtons.OK);
                }
                finally
                {
                    this.Close();
                }
            }
        }
    }
}