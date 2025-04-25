using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1_Client_Num3
{

   
    public partial class GroupNameForm : Form
    {

        public string GroupName { get; private set; }

        private TextBox groupNameTextBox;
        public GroupNameForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            okButton.Click += (s, e) => this.DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            cancelButton.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.Cancel == false && DialogResult == DialogResult.OK)
            {
                GroupName = textBox1.Text.Trim();
            }
        }
    }
}
