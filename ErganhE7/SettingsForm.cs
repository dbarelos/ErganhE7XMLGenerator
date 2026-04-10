using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErganhE7
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }


        private void SettingsForm_Load(object sender, EventArgs e)
        {
            txtYpiresiaSepe.Text = Properties.Settings.Default.ypiresiaSepe;
            txtYpiresiaOaed.Text = Properties.Settings.Default.ypiresiaOaed;
            txtKadPararthmatos.Text = Properties.Settings.Default.kadPararthmatos;
            txtKallikraths.Text = Properties.Settings.Default.kallikratisPararthmatos;
            txtAAPararthmatos.Text = Properties.Settings.Default.aaPararthmatos;
            txtYphkoothta.Text = Properties.Settings.Default.yphkoothta;
            //txtKadKyria.Text = Properties.Settings.Default.kadKyria;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ////if (string.IsNullOrWhiteSpace(txtYpiresiaSepe.Text))
            ////{
            ////    MessageBox.Show("Συμπληρώστε την Υπηρεσία ΣΕΠΕ.");
            ////    txtYpiresiaSepe.Focus();
            ////    return;
            ////}

            ////if (string.IsNullOrWhiteSpace(txtYpiresiaOaed.Text))
            ////{
            ////    MessageBox.Show("Συμπληρώστε την Υπηρεσία ΟΑΕΔ.");
            ////    txtYpiresiaOaed.Focus();
            ////    return;
            ////}

            Properties.Settings.Default.ypiresiaSepe = txtYpiresiaSepe.Text.Trim();
            Properties.Settings.Default.ypiresiaOaed = txtYpiresiaOaed.Text.Trim();
            Properties.Settings.Default.kadPararthmatos = txtKadPararthmatos.Text.Trim();
            Properties.Settings.Default.kallikratisPararthmatos = txtKallikraths.Text.Trim();
            Properties.Settings.Default.aaPararthmatos = txtAAPararthmatos.Text.Trim();
            Properties.Settings.Default.yphkoothta = txtYphkoothta.Text.Trim();
            //Properties.Settings.Default.kadKyria = txtKadKyria.Text.Trim();

            Properties.Settings.Default.Save();

            MessageBox.Show("Οι ρυθμίσεις αποθηκεύτηκαν.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
