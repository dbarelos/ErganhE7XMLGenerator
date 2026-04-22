using Npoi.Mapper;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ErganhE7
{
    public partial class Form1 : Form
    {

        private List<AnaggeliaE7NType> list = null;
        private string XMLData = "";
        public Form1()
        {
            InitializeComponent();

            // Initialize the DataGridView.
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSize = true;
            dataGridView1.DataSource = bindingSource1;

            // Initialize and add a text box column.
            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "f_afm";
            column.Name = "f_afm";
            column.Name = "AΦΜ";
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "f_eponymo";
            column.Name = "Επώνυμο";
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "f_onoma";
            column.Name = "Όνομα";
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "f_onoma_patros";
            column.Name = "Πατρώνυμο";
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "f_apodoxes";
            column.Name = "Αποδοχές κατά την απόλυση";
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "f_eidikothta";
            column.Name = "Ειδικότητα ΕΦΚΑ κατά την πρόσληψη";
            dataGridView1.Columns.Add(column);

            XMLReady(false);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            bool mustDoSetup = false;

            try
            {

                mustDoSetup = string.IsNullOrWhiteSpace(Properties.Settings.Default.ypiresiaSepe) || string.IsNullOrWhiteSpace(Properties.Settings.Default.ypiresiaOaed);

            }
            catch (ConfigurationErrorsException ex)
            {
                mustDoSetup = true;
            }

            if (mustDoSetup)
            {
                MessageBox.Show("Συμπληρώστε πρώτα τις βασικές ρυθμίσεις της εφαρμογής.");

                using (SettingsForm frm = new SettingsForm())
                {
                    frm.ShowDialog(this);
                }
            }
        }

        private AnaggeliaE7NType createE7()
        {
            var e7 = new AnaggeliaE7NType();
            e7.initialize();
            e7.f_ypiresia_oaed = Properties.Settings.Default.ypiresiaOaed;
            e7.f_ypiresia_sepe = Properties.Settings.Default.ypiresiaSepe;
            e7.f_kallikratis_pararthmatos = Properties.Settings.Default.kallikratisPararthmatos;
            e7.f_aa_pararthmatos = Properties.Settings.Default.aaPararthmatos;
            e7.f_yphkoothta = Properties.Settings.Default.yphkoothta;
            //e7.f_afm_proswpoy = Properties.Settings.Default.afmProswpoy;
            e7.f_kad_pararthmatos = Properties.Settings.Default.kadPararthmatos;
            
            return e7;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void openFile()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                XMLReady(false);

                IWorkbook workbook;
                try
                {
                    workbook = WorkbookFactory.Create(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    return;
                }
                
                var importer = new Mapper(workbook);
                var items = importer.Take<Contract>(0, 1);


                list = new List<AnaggeliaE7NType>();
                foreach (var item in items)
                {
                    var e7 = createE7();
                    e7.copyFromContract(item.Value);
                    list.Add(e7);
                }

                var anaggelies = new AnaggeliesE7NType();

                anaggelies.AnaggeliaE7N = list.ToArray();

                var serializer = new XmlSerializer(typeof(AnaggeliesE7NType));

                using(var sw = new Utf8StringWriter())
                {
                    serializer.Serialize(sw, anaggelies);
                    XMLData = sw.ToString();
                }

                XMLReady(true);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private bool ValidateApplicationSettings()
        {
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.ypiresiaSepe))
            {
                MessageBox.Show("Δεν έχει οριστεί η Υπηρεσία ΣΕΠΕ από τις Ρυθμίσεις.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.ypiresiaOaed))
            {
                MessageBox.Show("Δεν έχει οριστεί η Υπηρεσία ΟΑΕΔ από τις Ρυθμίσεις.");
                return false;
            }

            return true;
        }

        private void XMLReady(bool value)
        {
            btnExportXML.Enabled = value;
            if(!value)
            {
                XMLData = "";
            }
            txtXML.Text = XMLData;
            //dataGridView2.DataSource = list;
            //this.dataGridView1.DataSource = bindingSource1;
            bindingSource1.Clear();
            if(list != null)
            {
                foreach (var item in list)
                {
                    bindingSource1.Add(item);
                }
            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            AboutBox frm = new AboutBox();
            frm.ShowDialog();
        }

        private void btnExportXML_Click(object sender, EventArgs e)
        {
            if (!ValidateApplicationSettings())
                return;

            if (XMLData != "")
            {
                if(saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog1.FileName, XMLData);
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            using (SettingsForm frm = new SettingsForm())
            {
                frm.ShowDialog(this);
            }
        }
    }


    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }


}
