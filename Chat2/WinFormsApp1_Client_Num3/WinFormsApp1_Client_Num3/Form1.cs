using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Text.Json;
using System.Security.Cryptography.Pkcs;

namespace WinFormsApp1_Client_Num3
{
    public partial class Form1 : Form
    {
        public Socket socket;
        private bool file_v = false;
        private int fileSize;
        private Dictionary<string, Contact> contacts = new Dictionary<string, Contact>();
        private Dictionary<string, GroupChat> groups = new Dictionary<string, GroupChat>();
        private string name2;

        public Form1()
        {
            InitializeComponent();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private async void button1_Click(object sender, EventArgs e)
        {


            try
            {

                string msg = richTextBox2.Text;

                //������� ������ ������ Msg_t_c 
                Msg_t_c Msg_t_c = new Msg_t_c("msg", msg, null);

                //����������� ������ Msg_t_c � json ������   ->    {"Command":"name","Text":"$textBox1.Text$"}
                string json_msg = JsonSerializer.Serialize(Msg_t_c);

                //��������� �������� ������ �� ������ json_msg
                byte[] requestData = Encoding.UTF8.GetBytes(json_msg + '\n');

                // ���������� ������
                await socket.SendAsync(requestData, SocketFlags.None);

                richTextBox2.Text = "";

            }
            catch (SocketException)
            {
                Console.WriteLine($"�� ������� ���������� ����������� � {socket.RemoteEndPoint}");
            }
        }

        public void add_mgs(Msg_t r_m_t)
        {

            TextBox textbox1 = new TextBox();

            textbox1.BackColor = System.Drawing.SystemColors.Menu;
            textbox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textbox1.ForeColor = System.Drawing.Color.Green;
            textbox1.Size = new System.Drawing.Size(flowLayoutPanel1.Size.Width - 20, 16);


            textbox1.Text = r_m_t.Sender + " : " + r_m_t.Text;

            if (r_m_t.Command.IndexOf("my_msg") == 0)
            {
                textbox1.Text = r_m_t.Text;
                textbox1.ForeColor = System.Drawing.Color.Blue;
                textbox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            }

            if (r_m_t.Command.IndexOf("list_client") == 0)
            {
                textbox1.Text = JsonSerializer.Serialize(r_m_t.People);
            }

            if (r_m_t.Command.IndexOf("file") == 0)
            {

                fileSize = Convert.ToInt32(r_m_t.Text);
                richTextBox1.Text += fileSize;
                file_v = true;
            }

            flowLayoutPanel1.Controls.Add(textbox1);
        }

        public void msg_add(Msg_t_c r_m_t)
        {

            TextBox textBox1 = new TextBox();
            textBox1.Text = r_m_t.Text;

            textBox1.BackColor = System.Drawing.SystemColors.Control;
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox1.Margin = new System.Windows.Forms.Padding(3);
            textBox1.Padding = new System.Windows.Forms.Padding(3);
            textBox1.ForeColor = System.Drawing.Color.Green;
            textBox1.Size = new System.Drawing.Size(flowLayoutPanel1.Size.Width - 10, 23);

            if (r_m_t.Command.IndexOf("my_msg") == 0)
            {
                textBox1.ForeColor = System.Drawing.Color.Blue;
                textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            }


            flowLayoutPanel1.Controls.Add(textBox1);

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            byte[] requestData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Msg_t_c("get_client", "", null)) + '\n');
            await socket.SendAsync(requestData, SocketFlags.None);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            //byte[] requestData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Msg_t_c("file", "", null)) + '\n');
            //await socket.SendAsync(requestData, SocketFlags.None);
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = "�������� �����������";
                    dialog.Filter = "����������� (*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        // ������� ����� PictureBox ��� flowLayoutPanel
                        PictureBox pictureBox = new PictureBox
                        {
                            Size = new Size(200, 200), // ������������� ������������� ������
                            SizeMode = PictureBoxSizeMode.Zoom, // ������������ ����������� � ����������� ���������
                            BorderStyle = BorderStyle.FixedSingle // ��������� �����
                        };

                        // ��������� �����������
                        using (FileStream stream = new FileStream(dialog.FileName, FileMode.Open))
                        {
                            pictureBox.Image = Image.FromStream(stream);
                        }

                        // ��������� PictureBox � flowLayoutPanel
                        flowLayoutPanel1.Controls.Add(pictureBox);

                        // ���������� ����������� � �����
                        byte[] requestData = Encoding.UTF8.GetBytes(
                            JsonSerializer.Serialize(new Msg_t_c("file", dialog.FileName, null)) + '\n'
                        );
                        await socket.SendAsync(requestData, SocketFlags.None);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� �������� �����������: {ex.Message}");
            }


        }

        public async void connect(string name)
        {
            //����������� � �������
            socket.ConnectAsync("127.0.0.1", 8888);

            string msg = name;

            //������� ������ ������ Msg_t_c 
            Msg_t_c Msg_t_c = new Msg_t_c("name", msg, null);

            //����������� ������ Msg_t_c � json ������   ->    {"Command":"name","Text":"$textBox1.Text$"}
            string json_msg = JsonSerializer.Serialize(Msg_t_c);

            //��������� �������� ������ �� ������ json_msg
            byte[] requestData = Encoding.UTF8.GetBytes(json_msg + '\n');

            //���������� ��������� �������
            await socket.SendAsync(requestData, SocketFlags.None);


            // ������ ������ �� ������������� �������
            receive_(socket);
        }

        async Task receive_(Socket socket)
        {
            try
            {
                while (true)
                {
                    var buffer = new List<byte>();
                    var bytesRead = new byte[1];

                    while (true)
                    {
                        var count = await socket.ReceiveAsync(bytesRead, SocketFlags.None);
                        if (!file_v)
                            if (count == 0 || bytesRead[0] == '\n') break;

                        buffer.Add(bytesRead[0]);
                        if (file_v)
                        {
                            fileSize--;
                            if (fileSize == 0) break;
                        }
                    }

                    if (file_v)
                    {
                        add_file(buffer.ToArray());
                        file_v = false;
                        continue;
                    }

                    string responseText = Encoding.UTF8.GetString(buffer.ToArray());
                    Msg_t r_m_t = JsonSerializer.Deserialize<Msg_t>(responseText);
                    add_mgs(r_m_t);

                    switch (r_m_t.Command)
                    {
                        case "contact_list":
                            UpdateContactList(r_m_t);
                            break;
                        case "group_list":
                            UpdateGroupList(r_m_t);
                            break;
                        case "group_invite":
                            MessageBox.Show($"{r_m_t.Text} ��������� ��� � ������!");
                            break;
                        default:
                            add_mgs(r_m_t);
                            break;
                    }
                }
            }
            catch (SocketException)
            {
                Console.WriteLine($"�� ������� ���������� ����������� � {socket.RemoteEndPoint}");
            }
        }

        public void add_file(byte[] buffer)   //��� ������ 3 (Img)   ????????
        {
            PictureBox pictureB = new PictureBox();
            pictureB.Size = new Size(173, 103);
            pictureB.SizeMode = PictureBoxSizeMode.Zoom;
            using (MemoryStream ms = new MemoryStream(buffer))
                pictureB.Image = Image.FromStream(ms);
            flowLayoutPanel1.Controls.Add(pictureB);

        }

        //private async void Send_Files(object sender, EventArgs e) //������ ���������� ��������
        //{
        //    if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
        //        return;

        //    string filename = openFileDialog1.FileName;

        //    byte[] bytes = System.IO.File.ReadAllBytes(filename);


        //    byte[] requestData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Msg_t_c("file", bytes.Length.ToString(), null)) + '\n');

        //    await socket.SendAsync(requestData, SocketFlags.None);

        //    await socket.SendAsync(bytes, SocketFlags.None);


        //    MessageBox.Show("���� ���������");
        //}

        // �������������� �������
        private async void button5_Click(object sender, EventArgs e)   //����������
        {
            try
            {
                // �������� ���������� ��������
                if (contactList.SelectedItems.Count == 0)
                {
                    MessageBox.Show("����������, �������� ������� ��� �����������");
                    return;
                }

                // ��������� ������ ��������
                string contactName = contactList.SelectedItems[0].Text;

                // ������������ ��������� �����������
                var inviteMessage = JsonSerializer.Serialize(new Msg_t_c(
                    "invite_to_chat",
                    $"{contactName}",
                    null
                ));

                // �������� �����������
                byte[] requestData = Encoding.UTF8.GetBytes(inviteMessage + "\n");
                await socket.SendAsync(requestData, SocketFlags.None);

                // ����������� ����������
                MessageBox.Show($"����������� ���������� ������������ {contactName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� �������� �����������: {ex.Message}");
            }

        }

        private async void button6_Click(object sender, EventArgs e)  //������� 
        {
            GroupNameForm groupNameForm = new GroupNameForm();

            if (groupNameForm.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    // �������� ��������� ��� ������
                    string groupName = groupNameForm.GroupName;

                    // ������� ����� ������
                    GroupChat group = new GroupChat(groupName);

                    // ������� ��������� ��� �������
                    //var msg = JsonSerializer.Serialize(new Msg_t_c("create_group", group, null));
                    var msg = JsonSerializer.Serialize(new Msg_t_c("create_group", group.Name, null));

                    // ���������� ������ �� ������
                    byte[] requestData = Encoding.UTF8.GetBytes(msg + "\n");
                    await socket.SendAsync(requestData, SocketFlags.None);

                    // ��������� ��������� ������ �����
                    groupList.Items.Add(groupName);
                    MessageBox.Show($"������ '{groupName}' ������� �������!",
                        "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"������ ��� �������� ������: {ex.Message}",
                        "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void But(ContactInfo contactData) 
        {
            TextBox txtEmail = new TextBox();
            ContactInfo contactInfo = new ContactInfo(this);

            try
            {
                // ��������� ������� ���� ����������� ������
                if (contactList.SelectedItems.Count == 0 || groupList.SelectedItems.Count == 0)
                {
                    MessageBox.Show("����������, �������� ������� � ������");
                    return;
                }

                // �������� ��������� ��������
                string contactName = contactList.SelectedItems[0].Text;
                string groupName = groupList.SelectedItems[0].Text;



                // ������ ������ ������ ��������
               

                // ������ ��������� � ������� ��������
                var msg = JsonSerializer.Serialize(new Msg_t_c(
                    "add_to_group",
                    $"{contactName}:{groupName}", null));

                // ��������� ����������� ������ ����� ������
                msg += "\n";

                // ����������� ��������� � �����
                byte[] requestData = Encoding.UTF8.GetBytes(msg);

                // ���������� ������ ����������
                //await socket.SendAsync(requestData, SocketFlags.None);

                MessageBox.Show($"������� ������� �������� � ������ {groupName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� ���������� �������� � ������: {ex.Message}");
            }
        }


        private async void button7_Click(object sender, EventArgs e)  //��������
        {
            TextBox txtEmail = new TextBox();
            ContactInfo contactInfo = new ContactInfo(this);
            if (contactInfo.ShowDialog(this) == DialogResult.OK)
            {
                // ��������� ������� � ������
                contactList.Items.Add(new ListViewItem(new string[]
                {
            contactInfo.Id.ToString(),
            contactInfo.Name,
            contactInfo.Email
                }));

                // ��������� ������� ���������
                contacts[contactInfo.Name] = new Contact(
                    contactInfo.Id.ToString(),
                    contactInfo.Name,
                    contactInfo.Email
                );

                // ���������� ���������� �� ������
                var msg = JsonSerializer.Serialize(new Msg_t_c(
                    "add_contact",
                    JsonSerializer.Serialize(new Contact(
                        contactInfo.Id.ToString(),
                        contactInfo.Name,
                        contactInfo.Email
                    )),
                    null
                ));

                byte[] requestData = Encoding.UTF8.GetBytes(msg + "\n");
                await socket.SendAsync(requestData, SocketFlags.None);
            }

            contactInfo.Show();
          
        }



        public void UpdateContactList(Msg_t r_m_t)  //���������� ������ ���������
        {
            contacts[r_m_t.Text] = new Contact(r_m_t.Text, r_m_t.Text, r_m_t.Text);

            contactList.Items.Clear();
            foreach (var contact in contacts.Values)
            {
                var item = new ListViewItem(new[] { contact.Id, contact.Name, contact.Email});
                contactList.Items.Add(item);
            }
        }

        public void UpdateGroupList(Msg_t r_m_t)   //���������� ������ ���������� ����
        {
            var groupData = JsonSerializer.Deserialize<GroupChat>(r_m_t.Text);
            groups[groupData.Name] = groupData;

            groupList.Items.Clear();
            foreach (var group in groups.Values)
            {
                groupList.Items.Add(group.Name);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = openFileDialog1.FileName;

            byte[] bytes = System.IO.File.ReadAllBytes(filename);


            byte[] requestData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Msg_t_c("file", bytes.Length.ToString(), null)) + '\n');

            await socket.SendAsync(requestData, SocketFlags.None);

            await socket.SendAsync(bytes, SocketFlags.None);


            MessageBox.Show("���� ���������");
        }

       

      

       
    }
}

