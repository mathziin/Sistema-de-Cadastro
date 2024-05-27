using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Plenitude
{
    public partial class NovoMembro : Form
    {
        string origemCompleto = "";
        string foto = "";
        string pastaDestino = Globais.caminhoFotos;
        string destinoCompleto = "";
        public NovoMembro()
        {
            InitializeComponent();
        }


        private void btn_salvar_Click(object sender, EventArgs e)
        {

            if (destinoCompleto == "")
            {
                if (MessageBox.Show("Sem foto selecionada, deseja continuar?", "ERRO", MessageBoxButtons.YesNo) ==DialogResult.No)
                {
                    return;
                }
            }
            if (destinoCompleto != "")
            {

                System.IO.File.Copy(origemCompleto, destinoCompleto, true);
                if (File.Exists(destinoCompleto))
                {
                    pb_foto.ImageLocation = destinoCompleto;
                }
                else
                {
                    if (MessageBox.Show("Erro ao localizar foto, deseja continuar?", "ERR0", MessageBoxButtons.YesNo) ==DialogResult.No)
                    {
                        return;
                    }
                }
            }

            string queryInsertMembro = String.Format(@"
            INSERT INTO tb_membros
            (T_NOMEMEMBRO, N_IDADEMEMBRO,T_ENDERECO, T_RGMEMBRO, T_CPFMEMBRO, T_DATANASCIMENTO, T_ESTADOCIVIL,
            T_TELEFONECELULAR, T_NOMEMAE, T_NOMEPAI, T_CARGO, T_DATABATISMO, T_BATISMOES, T_FOTO)
            VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')
            ",tb_nomemembro.Text, n_idademembro.Text, tb_endereco.Text, tb_rg.Text, tb_cpf.Text, d_datanascimento.Text, cb_estadocivil.Text, mtb_telefonecelular.Text, tb_nomemae.Text, tb_nomepai.Text,
            cb_cargo.Text, d_databatismo.Text, cb_batismoes.Text, destinoCompleto);
            Banco.dml(queryInsertMembro);
            MessageBox.Show("Novo membro inserido!");

            pb_foto.ImageLocation= destinoCompleto;
            
        }

        private void btn_novo_Click(object sender, EventArgs e)
        {
            tb_nomemembro.Clear();
            n_idademembro.Text = "";
            tb_endereco.Clear();
            tb_rg.Clear();
            tb_cpf.Clear();
            d_datanascimento.Text = "";
            cb_estadocivil.Text = "";
            mtb_telefonecelular.Clear();
            tb_nomemae.Clear();
            tb_nomepai.Clear();
            cb_cargo.Text = "";
            d_databatismo.Text = "";
            cb_batismoes.Text = "";
            tb_nomemembro.Focus();
        }

        private void btn_addfoto_Click(object sender, EventArgs e)
        {
             origemCompleto = "";
             foto = "";
             pastaDestino = Globais.caminhoFotos;
             destinoCompleto = "";
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                origemCompleto= openFileDialog1.FileName;
                foto= openFileDialog1.SafeFileName;
                destinoCompleto = pastaDestino + foto;
            }
            if (File.Exists(destinoCompleto))
            {
                if (MessageBox.Show("Arquivo já existe, deseja substituir?", "Substituir", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

            }
                pb_foto.ImageLocation = origemCompleto;

            }
        }
    }
