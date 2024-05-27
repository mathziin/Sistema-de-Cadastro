using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plenitude
{
    public partial class GestaoMembro : Form
    {
        public GestaoMembro()
        {
            InitializeComponent();
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GestaoMembro_Load(object sender, EventArgs e)
        {
            dgv_membros.DataSource = Banco.ObterMembroIdNome();
            dgv_membros.Columns[0].Width = 85;
            dgv_membros.Columns[1].Width = 300;
        }

        private void btn_novo_Click(object sender, EventArgs e)
        {
            GestaoMembro gestaoMembro = new GestaoMembro();
            gestaoMembro.ShowDialog();
            dgv_membros.DataSource = Banco.ObterMembroIdNome();
        }

        private void btn_excluir_Click_1(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Confirma exclução?", "Excluir?", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                Banco.DeletarMembro(tb_idmembros.Text);
                dgv_membros.Rows.Remove(dgv_membros.CurrentRow);
            }
        }

        private void dgv_membros_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            int contlinhas = dgv.SelectedRows.Count;
            if (contlinhas > 0)
            {
                DataTable mb = new DataTable();
                string vid = dgv.SelectedRows[0].Cells[0].Value.ToString();
                string vquery = @"
                    SELECT
                        *
                    FROM
                            tb_membros
                    WHERE
                            N_IDMEMBRO=" + vid;
                mb = Banco.dql(vquery);
                tb_idmembros.Text = mb.Rows[0].Field<Int64>("N_IDMEMBRO").ToString();
                tb_nomemembro.Text = mb.Rows[0].Field<string>("T_NOMEMEMBRO");
                n_idademembro.Text = mb.Rows[0].Field<string>("N_IDADEMEMBRO");
                tb_endereco.Text = mb.Rows[0].Field<string>("T_ENDERECO");
                tb_rg.Text = mb.Rows[0].Field<string>("T_RGMEMBRO");
                tb_cpf.Text = mb.Rows[0].Field<string>("T_CPFMEMBRO");
                tb_datanascimento.Text = mb.Rows[0].Field<string>("T_DATANASCIMENTO");
                cb_estadocivil.Text = mb.Rows[0].Field<string>("T_ESTADOCIVIL");
                mtb_telefonecelular.Text = mb.Rows[0].Field<string>("T_TELEFONECELULAR");
                tb_nomemae.Text = mb.Rows[0].Field<string>("T_NOMEMAE");
                tb_nomepai.Text = mb.Rows[0].Field<string>("T_NOMEPAI");
                cb_cargo.Text = mb.Rows[0].Field<string>("T_CARGO");
                tb_databatismo.Text = mb.Rows[0].Field<string>("T_DATABATISMO");
                cb_batismoes.Text = mb.Rows[0].Field<string>("T_BATISMOES");
                pb_foto.ImageLocation = mb.Rows[0].Field<string>("T_FOTO");
            }
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            int linha = dgv_membros.SelectedRows[0].Index;
            string queryAtualizarMembro = string.Format(@"
                UPDATE
                    tb_membros
                SET
                    N_IDMEMBRO='{0}',
                    T_NOMEMEMBRO='{1}',
                    N_IDADEMEMBRO='{2}',
                    T_ENDERECO='{3}',
                    T_RGMEMBRO='{4}',
                    T_CPFMEMBRO='{5}',
                    T_DATANASCIMENTO='{6}',
                    T_ESTADOCIVIL='{7}',
                    T_TELEFONECELULAR='{8}',
                    T_NOMEMAE='{9}',
                    T_NOMEPAI='{10}',
                    T_CARGO='{11}',
                    T_DATABATISMO='{12}',
                    T_BATISMOES='{13}'",
                    tb_idmembros.Text, tb_nomemembro.Text, n_idademembro.Text, tb_endereco.Text,
                    tb_rg.Text, tb_cpf.Text, tb_datanascimento.Text, cb_estadocivil.Text, mtb_telefonecelular.Text,
                    tb_nomemae.Text, tb_nomepai.Text, cb_cargo.Text, tb_databatismo.Text, cb_batismoes.Text);
            Banco.dml(queryAtualizarMembro);
            dgv_membros[1, linha].Value = tb_nomemembro.Text;

            MessageBox.Show("Cadastro atualizado!");
        }
    }
}




            //m.idmembro = Convert.ToInt32(tb_idmembros.Text);
            // m.nomemembro = tb_nomemembro.Text;
            // m.idademembro = n_idademembro.Text;
            // m.endereco = tb_endereco.Text;
            // m.rgmembro = tb_rg.Text;
            // m.cpfmembro = tb_cpf.Text;
            // m.datanascimento = tb_datanascimento.Text;
            // m.estadocivil = cb_estadocivil.Text;
            // m.telefonecelular = mtb_telefonecelular.Text;
            // m.nomemae = tb_nomemae.Text;
            // m.nomepai = tb_nomepai.Text;
            // m.cargo = cb_cargo.Text;
            // m.databatismo = tb_databatismo.Text;
            //  m.batismoes = cb_batismoes.Text;
            //  Banco.dml(m);
            // dgv_membros.DataSource = Banco.dml(queryAtualizarMembro)
            // dgv_membros.CurrentCell = dgv_membros[0, linha];
