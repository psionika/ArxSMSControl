using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArxSMSConfig
{
    public partial class FormAddEdit : Form
    {
        public string ArxSMSName { get; set; }
        public string ArxSMSTelephone { get; set; }
        public string ArxSMSText { get; set; }
        public string ArxSMSScript { get; set; }
        public string formTitle { get; set; }

        public FormAddEdit()
        {
            InitializeComponent();            
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            ArxSMSName = txbName.Text;
            ArxSMSTelephone = txbTelephone.Text;
            ArxSMSText = txbText.Text;
            ArxSMSScript = txbScript.Text;
        }

        void FormAddEdit_Load(object sender, EventArgs e)
        {
            txbName.Text = ArxSMSName;
            txbTelephone.Text = ArxSMSTelephone;
            txbText.Text = ArxSMSText;
            txbScript.Text = ArxSMSScript;

            this.Text = formTitle;
        }
    }
}
