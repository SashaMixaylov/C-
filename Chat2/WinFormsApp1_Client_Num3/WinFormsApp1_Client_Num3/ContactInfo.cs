using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1_Client_Num3
{
    public partial class ContactInfo : Form
    {
        Guid mid = Guid.NewGuid();
        Form1 obj1;
        //private static int nextId = 0;
        public int Id { get; set; }
        public string Name { get;  set; }
        public string Email { get;  set; }
     
        public ContactInfo( Form1 obj)
        {
            Id =  mid.ToString().GetHashCode();
            this.obj1 = obj;
            InitializeComponent();
        }




        private void button1_Click(object sender, EventArgs e)  //OK
        {
            //Пример
            //obj1.UpdateGroupList();
            //
            /*var contactData = new ContactInfo(this)
            {
                Id = this.Id,
                Name = contactName,
                Email = txtEmail.Text.Trim()
            };
            obj1.But(contactData);
            okButton.Click += (s, e) => this.DialogResult = DialogResult.OK;*/

            Name = txtUserName.Text.Trim();  // Предполагается, что есть текстбокс contactNameTextBox
            Email = txtEmail.Text.Trim();       // Предполагается, что есть текстбокс emailTextBox

            obj1.But(this);

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancelButton.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
        }
    }
}
