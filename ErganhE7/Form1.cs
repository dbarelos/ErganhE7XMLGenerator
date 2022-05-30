using Npoi.Mapper;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private List<AnaggeliaE7Type> list = null;
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

            XMLReady(false);
        }

        private AnaggeliaE7Type createE7()
        {
            AnaggeliaE7Type e7 = new AnaggeliaE7Type();
            e7.initialize();
            e7.f_ypiresia_oaed = AppSettings.Default.ypiresia_oaed;
            e7.f_ypiresia_sepe = AppSettings.Default.ypiresia_sepe;
            e7.f_kad_kyria = AppSettings.Default.kad_kyria;
            e7.f_kallikratis_pararthmatos = AppSettings.Default.kallikratis_pararthmatos;
            e7.f_aa_pararthmatos = AppSettings.Default.aa_pararthmatos;
            e7.f_yphkoothta = AppSettings.Default.yphkoothta;
            e7.f_afm_proswpoy = AppSettings.Default.afm_proswpoy;
            e7.f_kad_pararthmatos = AppSettings.Default.kad_pararthmatos;
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


                list = new List<AnaggeliaE7Type>();
                foreach (var item in items)
                {
                    AnaggeliaE7Type e7 = createE7();
                    e7.copyFromContract(item.Value);
                    list.Add(e7);
                }

                AnaggeliesE7Type anaggelies = new AnaggeliesE7Type();

                anaggelies.AnaggeliaE7 = list.ToArray();

                var serializer = new XmlSerializer(typeof(AnaggeliesE7Type));

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

        private void exportXML()
        {

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
            if(XMLData != "")
            {
                if(saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog1.FileName, XMLData);
                }
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
