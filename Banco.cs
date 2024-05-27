using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Plenitude
{
    internal class Banco
    {
        private static SQLiteConnection conexao;

        //Funções genéricas

        private static SQLiteConnection ConexaoBanco()
        {
            conexao = new SQLiteConnection("Data Source="+Globais.caminhoBanco + Globais.nomeBanco);
            conexao.Open();
            return conexao;
        }

        public static DataTable dql(string sql) // Data query language
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = vcon.CreateCommand();
                cmd.CommandText = sql;
                da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                da.Fill(dt);
                vcon.Close();
                return dt;
            }
            catch (Exception ex)
            {
                ConexaoBanco().Close();
                throw ex;
            }
        }

        public static void dml(string q, string msgOK=null, string msgERRO=null) // Data Manipulation Language
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = ConexaoBanco().CreateCommand();

                cmd.CommandText = q;
                da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();
                if(msgOK!= null)
                {
                    MessageBox.Show(msgOK);
                }
            }
            catch (Exception ex)
            {
                if(msgERRO!= null)
                {
                    MessageBox.Show(msgERRO+"\n"+ ex.Message);
                }
                throw ex;
            }
        }


        public static DataTable ObterTodosUsuarios()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = ConexaoBanco().CreateCommand();

                    cmd.CommandText = "SELECT * FROM tb_usuarios";
                    da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                    da.Fill(dt);
                    vcon.Close();
                    return dt;
                }catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable ObterTodosMembros()
        {
            SQLiteDataAdapter ma = null;
            DataTable mb = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = ConexaoBanco().CreateCommand();

                cmd.CommandText = "SELECT * FROM tb_membros";
                ma = new SQLiteDataAdapter(cmd.CommandText, vcon);
                ma.Fill(mb);
                vcon.Close();
                return mb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /////////Funções do FORM F_NovoUsuario

        public static void NovoUsuario(Usuario u)
        {
            if (existeUsername(u))
            {
                MessageBox.Show("Username já existe");
                return;
            }
            try
            {
                var vcon = ConexaoBanco();
                var cmd = vcon.CreateCommand();
                cmd.CommandText = "INSERT INTO tb_usuarios (T_NOMEUSUARIO, T_USERNAME, T_SENHAUSUARIO, T_STATUSUSUARIO, N_NIVELUSUARIO) VALUES (@nome,@username,@senha,@status,@nivel)";
                cmd.Parameters.AddWithValue("@nome", u.nome);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@senha", u.senha);
                cmd.Parameters.AddWithValue("@status", u.status);
                cmd.Parameters.AddWithValue("@nivel", u.nivel);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Novo Usuário inserido com sucesso!");
                vcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir novo usuário!");
                throw ex;
            }
        }

        public static void NovoMembro(Membros m)
        {
            if (existeMembro(m))
            {
                MessageBox.Show("Membro já existe");
                return;
            }
            try
            {
                var vcon = ConexaoBanco();
                var cmd = vcon.CreateCommand();
                cmd.CommandText = "INSERT INTO tb_membros (T_NOMEMEMBRO, N_IDADEMEMBRO, T_ENDERECO, T_RGMEMBRO, T_CPFMEMBRO, T_DATANASCIMENTO, T_ESTADOCIVIL, T_TELEFONECELULAR, T_NOMEMAE, T_NOMEPAI, T_CARGO, T_DATABATISMO, T_BATISMOES) VALUES (@nome,@idade,@endereco,@rg,@cpf,@datanascimento,@estadocivil,@telefonecelular,@mae,@pai,@cargo,@databatismo,@batismoes)";
                cmd.Parameters.AddWithValue("@nome", m.nomemembro);
                cmd.Parameters.AddWithValue("@idade", m.idademembro);
                cmd.Parameters.AddWithValue("@endereco", m.endereco);
                cmd.Parameters.AddWithValue("@rg", m.rgmembro);
                cmd.Parameters.AddWithValue("@cpf", m.cpfmembro);
                cmd.Parameters.AddWithValue("@datanascimento", m.datanascimento);
                cmd.Parameters.AddWithValue("@estadocivil", m.estadocivil);
                cmd.Parameters.AddWithValue("@telefonecelular", m.telefonecelular);
                cmd.Parameters.AddWithValue("@mae", m.nomemae);
                cmd.Parameters.AddWithValue("@pai", m.nomepai);
                cmd.Parameters.AddWithValue("@cargo", m.cargo);
                cmd.Parameters.AddWithValue("@databatismo", m.databatismo);
                cmd.Parameters.AddWithValue("@batismoes", m.batismoes);
                cmd.Parameters.AddWithValue("@foto", m.foto);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Novo Usuário inserido com sucesso!");
                vcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir novo usuário!");
                throw ex;
            }
        }

        //Funções do F_GestaoUsuario

        public static DataTable ObterUsuarioIdNome()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = ConexaoBanco().CreateCommand();

                cmd.CommandText = "SELECT N_IDUSUARIO as 'ID Usuário',T_NOMEUSUARIO as 'Nome Usuário' FROM tb_usuarios";
                da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                da.Fill(dt);
                vcon.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable ObterMembroIdNome()
        {
            SQLiteDataAdapter ma = null;
            DataTable mb = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = ConexaoBanco().CreateCommand();

                cmd.CommandText = "SELECT N_IDMEMBRO as 'ID',T_NOMEMEMBRO as 'Membro' FROM tb_membros";
                ma = new SQLiteDataAdapter(cmd.CommandText, vcon);
                ma.Fill(mb);
                vcon.Close();
                return mb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable ObterDadosUsuario(string id)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = ConexaoBanco().CreateCommand();

                cmd.CommandText = "SELECT * FROM tb_usuarios WHERE N_IDUSUARIO="+id;
                da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                da.Fill(dt);
                vcon.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable ObterDadosMembros(string idmembro)
        {
            SQLiteDataAdapter ma = null;
            DataTable mb = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = ConexaoBanco().CreateCommand();

                cmd.CommandText = "SELECT * FROM tb_membros WHERE N_IDMEMBRO="+idmembro;
                ma = new SQLiteDataAdapter(cmd.CommandText, vcon);
                ma.Fill(mb);
                vcon.Close();
                return mb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void AtualizarUsuario(Usuario u)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = ConexaoBanco().CreateCommand();

                cmd.CommandText = "UPDATE tb_usuarios SET T_NOMEUSUARIO='"+u.nome+"',T_USERNAME='"+u.username+"',T_SENHAUSUARIO='"+u.senha+"', T_STATUSUSUARIO='"+u.status+"',N_NIVELUSUARIO= "+u.nivel+" WHERE N_IDUSUARIO="+u.id;
                da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void AtualizarMembro(Membros m)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = ConexaoBanco().CreateCommand();

                cmd.CommandText = "UPDATE tb_membros SET T_NOMEMEMBRO='"+m.nomemembro+"',N_IDADEMEMBRO='"+m.idademembro+"',T_ENDERECO='"+m.endereco+"',T_RGMEMBRO='"+m.rgmembro+"',T_CPFMEMBRO='"+m.cpfmembro+"',T_DATANASCIMENTO='"+m.datanascimento+"',T_ESTADOCIVIL='" + m.estadocivil + "',T_TELEFONECELULAR='" + m.telefonecelular + "',T_NOMEMAE='" + m.nomemae + "',T_NOMEPAI='" + m.nomepai + "',T_CARGO='" + m.cargo + "',T_DATABATISMO='" + m.databatismo + "', T_BATISMOES='" + m.batismoes + "' WHERE N_IDMEMBRO=" + m.idmembro;
                da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeletarUsuario(string id)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = ConexaoBanco().CreateCommand();

                cmd.CommandText = "DELETE FROM tb_usuarios WHERE N_IDUSUARIO=" + id;
                da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeletarMembro(string idmembro)
        {
            SQLiteDataAdapter ma = null;
            DataTable mb = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = ConexaoBanco().CreateCommand();

                cmd.CommandText = "DELETE FROM tb_membros WHERE N_IDMEMBRO=" + idmembro;
                ma = new SQLiteDataAdapter(cmd.CommandText, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //FIM Funções do F_GestaoUsuario

        /////////FIM DAS FURNÇÕES F_NOVOUSUARIO

        ///ROTINAS GERAIS
        public static bool existeUsername(Usuario u)
        {
            bool res;
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            var vcon = ConexaoBanco();
            var cmd = vcon.CreateCommand();
            cmd.CommandText = "SELECT T_USERNAME FROM tb_usuarios WHERE T_USERNAME='"+u.username+"'";
            da = new SQLiteDataAdapter(cmd.CommandText, vcon);
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                res = true;
            }
            else
            {
                res = false;
            }
            vcon.Close();
            return res;
        }

        public static bool existeMembro(Membros m)
        {
            bool res;
            SQLiteDataAdapter ma = null;
            DataTable mb = new DataTable();

            var vcon = ConexaoBanco();
            var cmd = vcon.CreateCommand();
            cmd.CommandText = "SELECT T_NOMEMEMBRO FROM tb_membros WHERE T_NOMEMEMBRO='"+m.nomemembro+"'";
            ma = new SQLiteDataAdapter(cmd.CommandText, vcon);
            ma.Fill(mb);
            if (mb.Rows.Count > 0)
            {
                res = true;
            }
            else
            {
                res = false;
            }
            vcon.Close();
            return res;
        }

        internal static void dm1(string queryInsertMembro)
        {
        }
    }
}