using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace prjAcademico
{
    internal class Aluno
    {
        #region Props
        public string Prontuario { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Email { get; set; }
        #endregion

        #region Métodos
        public bool ValidaProntuario(string prontuario)
        {
            if (prontuario.Length != 9) return false;      //tem menos digítos que o necessário
            string numeros = prontuario.Substring(2, 6);  //separo os números sem o dígito
            string digito = prontuario.Substring(8, 1).ToUpper();
            string digitocalculado;
            int num, resto, dv;
            int[] pesos = { 7, 6, 5, 4, 3, 2 };
            int soma = 0;
            for (int i = 0; i < pesos.Length; i++)
            {
                num = int.Parse(numeros[i].ToString());
                soma += num * pesos[i];
            }

            resto = soma % 11;
            dv = 11 - resto;

            if (dv == 10)
                digitocalculado = "X";
            else if (dv == 11)
                digitocalculado = "1";
            else
                digitocalculado = dv.ToString();

            if (digito == digitocalculado)
                return true;
            else
                return false;
        }

        public bool ValidaCPF(string cpf)
        {
            int soma = 0, resto, dig;
            //retiro caracteres ". / -"
            cpf = cpf.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");
            //verifico se sobrou 11 caracteres
            if (cpf.Length != 11)
                return false;

            //verificação do primeiro dígito
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            }
            //verifico o resto da divisao da soma por 11
            resto = soma % 11;
            //se resto = 0 ou resto = 1 --> digito = 0
            if (resto < 2)
            {
                dig = 0;
            }
            else
            {
                dig = 11 - resto;
            }
            if (dig.ToString() != cpf[9].ToString())
                return false;
            //se chegou até aqui verifico o segundo dígito
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            }
            //verifico o resto da divisao da soma por 11
            resto = soma % 11;
            //se resto = 0 ou resto = 1 --> digito = 0
            if (resto < 2)
            {
                dig = 0;
            }
            else
            {
                dig = 11 - resto;
            }
            if (dig.ToString() != cpf[10].ToString())
                return false;
            else
                return true;
        }

        public bool ValidaRG(string rg)
        {
            int soma = 0;
            int resto;
            string digito;
            //retiro caracteres ". / -"
            rg = rg.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");
            //verifico se sobrou 11 caracteres
            if (rg.Length != 9)
                return false;
            //multiplico os termos
            for (int i = 0; i < 8; i++)
                soma += int.Parse(rg[i].ToString()) * (2 + i);

            resto = soma % 11;
            if (resto == 10)
                digito = "X";
            else if (resto == 0)
                digito = "0";
            else
                digito = (11 - resto).ToString();

            if (digito == rg[8].ToString())
                return true;
            else
                return false;
        }


        string stringConexao = "Data Source=localhost;Initial Catalog=dbAcademico;Integrated Security=true";

        public void Incluir()
        {
            SqlConnection con = new SqlConnection();

            try
            {
                con.ConnectionString = stringConexao;

                SqlCommand comando = new SqlCommand();
                comando.Connection = con;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "Insert into Alunos (Prontuario, Nome, Cpf, Rg, Email) values (@Prontuario, @Nome, @Cpf, @Rg, @Email)";

                comando.Parameters.AddWithValue("@Prontuario", Prontuario);
                comando.Parameters.AddWithValue("@Nome", Nome);
                comando.Parameters.AddWithValue("@Cpf", Cpf);
                comando.Parameters.AddWithValue("@Rg", Rg);
                comando.Parameters.AddWithValue("@Email", Email);

                con.Open();
                comando.ExecuteScalar();
            }
            catch
            {
                throw new Exception("Erro no Banco de Dados!");
            }
            finally
            {
                con.Close();
            };
        }
        
        public bool Consultar()
        {
            SqlConnection con = new SqlConnection();

            try
            {
                con.ConnectionString = stringConexao;

                SqlCommand comando = new SqlCommand();
                comando.Connection = con;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "Select Prontuario, Nome, Cpf, Rg, Email " +
                                      "from Alunos where Prontuario = @Prontuario";
                
                comando.Parameters.AddWithValue("@Prontuario", Prontuario);

                con.Open();
                SqlDataReader dr = comando.ExecuteReader();
                
                if (dr.Read())
                {
                    this.Nome = dr["Nome"].ToString();
                    this.Cpf = dr["Cpf"].ToString();
                    this.Rg = dr["Rg"].ToString();
                    this.Email = dr["Email"].ToString();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch 
            {
                throw new Exception("Erro no Banco de Dados!");
            }
            finally
            {
                con.Close();
            }

        }

        public void Alterar()
        {
            SqlConnection con = new SqlConnection();

            try
            {
                con.ConnectionString = stringConexao;

                SqlCommand comando = new SqlCommand();
                comando.Connection = con;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "Update Alunos " +
                                      "set Nome = @Nome, Email = @Email " +
                                      "where Prontuario = @Prontuario";

                comando.Parameters.AddWithValue("@Nome", Nome);
                comando.Parameters.AddWithValue("@Email", Email);
                comando.Parameters.AddWithValue("@Prontuario", Prontuario);

                con.Open();
                comando.ExecuteScalar();
            }
            catch
            {
                throw new Exception("Erro no Banco de Dados!");
            }
            finally
            {
                con.Close();
            }
        }

        public void Excluir()
        {
            SqlConnection con = new SqlConnection();

            try
            {
                con.ConnectionString = stringConexao;

                SqlCommand comando = new SqlCommand();
                comando.Connection = con;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "Delete from Alunos where Prontuario = @Prontuario";

                comando.Parameters.AddWithValue("@Prontuario", Prontuario);

                con.Open();
                comando.ExecuteScalar();
            }
            catch
            {
                throw new Exception("Erro no Banco de Dados!");
            }
            finally
            {
                con.Close();
            }
        }
        #endregion
    }
}