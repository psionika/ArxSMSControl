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

namespace ArxSMSConfig
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            saveDataSet();
            Application.Exit();
        }

        void saveDataSet()
        {
            dataSet1.WriteXml("data.xml", XmlWriteMode.IgnoreSchema);
        }

        void loadDataSet()
        {
            if (!File.Exists("data.xml")) return;

            dataSet1.Clear();
            dataSet1.ReadXml("data.xml");

            dataGridView1.DataSource = sMSBindingSource;
        }

        void FormMain_Load(object sender, EventArgs e)
        {
            loadDataSet();
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            addTask();
        }

        void addTask()
        {
            using (var fae = new FormAddEdit())
            {
                fae.ArxSMSName = "";
                fae.ArxSMSTelephone = "";
                fae.ArxSMSText = "";
                fae.ArxSMSScript = "";

                fae.formTitle = "New task";

                if (fae.ShowDialog() == DialogResult.OK)
                {
                    var newRow = dataSet1.Tables["SMS"].NewRow();

                    newRow["Name"] = fae.ArxSMSName;
                    newRow["Telephone"] = fae.ArxSMSTelephone;
                    newRow["Text"] = fae.ArxSMSText;
                    newRow["Script"] = fae.ArxSMSScript;

                    dataSet1.Tables["SMS"].Rows.Add(newRow);
                }
            }
        }

        void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;

            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var pt = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                pt.X += e.Location.X;
                pt.Y += e.Location.Y;
                contextMenuStrip1.Show(dataGridView1, pt);
            }
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            deleteTask();   
        }

        void deleteTask()
        {
            if (dataGridView1.CurrentRow == null) return;

            var result = MessageBox.Show("Are you sure you want to remove the current item?",
                "Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            sMSBindingSource.RemoveCurrent();
            saveDataSet();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addTask();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteTask();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            editTask();     
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editTask();
        }

        void editTask()
        {
            if (dataGridView1.CurrentRow == null) return;

            using (var fae = new FormAddEdit())
            {
                fae.ArxSMSName = dataGridView1.CurrentRow.Cells["nameDataGridViewTextBoxColumn"].Value.ToString();
                fae.ArxSMSTelephone = dataGridView1.CurrentRow.Cells["telephoneDataGridViewTextBoxColumn"].Value.ToString();
                fae.ArxSMSText = dataGridView1.CurrentRow.Cells["textDataGridViewTextBoxColumn"].Value.ToString();
                fae.ArxSMSScript = dataGridView1.CurrentRow.Cells["scriptDataGridViewTextBoxColumn"].Value.ToString();
                fae.formTitle = "Edit task";

                fae.ShowDialog();

                if (fae.DialogResult != DialogResult.OK) return;

                var taskRow = ((DataRowView)dataGridView1.CurrentRow.DataBoundItem).Row;

                taskRow["Name"] = fae.ArxSMSName;
                taskRow["Telephone"] = fae.ArxSMSTelephone;
                taskRow["Text"] = fae.ArxSMSText;
                taskRow["Script"] = fae.ArxSMSScript;

                saveDataSet();
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            editTask();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            using (var formAbout = new FormAbout())
            {
                formAbout.ShowDialog();
            }
        }
    }
}
