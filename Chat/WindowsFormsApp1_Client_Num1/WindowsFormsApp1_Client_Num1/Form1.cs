using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Text.Json;
using System.Runtime.InteropServices.ComTypes;

namespace WindowsFormsApp1_Client_Num1
{
    public partial class Form1: Form
    {
        public Socket socket;
        private bool file_v = false;
        private int fileSize;
        private Dictionary<string, Contact> contacts = new Dictionary<string, Contact>();
        private Dictionary<string, GroupChat> groups = new Dictionary<string, GroupChat>();

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

                //Создаем объект класса Msg_t_c 
                Msg_t_c Msg_t_c = new Msg_t_c("msg", msg, null);

                //Сериализуем объект Msg_t_c в json строку   ->    {"Command":"name","Text":"$textBox1.Text$"}
                string json_msg = JsonSerializer.Serialize(Msg_t_c);

                //формируем байтовый массив из строки json_msg
                byte[] requestData = Encoding.UTF8.GetBytes(json_msg + '\n');

                // отправляем данные
                await socket.SendAsync(requestData, SocketFlags.None);

                richTextBox2.Text = "";

            }
            catch (SocketException)
            {
                Console.WriteLine($"Не удалось установить подключение с {socket.RemoteEndPoint}");
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
            byte[] requestData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Msg_t_c("file", "", null)) + '\n');
            await socket.SendAsync(requestData, SocketFlags.None);
        }

        public async void connect(string name) 
        {
            //Подключение к серверу
            socket.ConnectAsync("127.0.0.1", 8888);

            string msg = name;

            //Создаем объект класса Msg_t_c 
            Msg_t_c Msg_t_c = new Msg_t_c("name", msg, null);

            //Сериализуем объект Msg_t_c в json строку   ->    {"Command":"name","Text":"$textBox1.Text$"}
            string json_msg = JsonSerializer.Serialize(Msg_t_c);

            //формируем байтовый массив из строки json_msg
            byte[] requestData = Encoding.UTF8.GetBytes(json_msg + '\n');

            //отправляем сообщение серверу
            await socket.SendAsync(requestData, SocketFlags.None);


            // запуск задачи на прослушивание сервера
            receive_(socket);
        }

        async Task receive_(Socket socket)
        {
            try
            {
                while (true)
                {



                    // буфер для накопления входящих данных
                    var buffer = new List<byte>();

                    // буфер для считывания одного байта
                    var bytesRead = new byte[1];

                    // считываем данные до конечного символа
                    while (true)
                    {
                        var count = await socket.ReceiveAsync(bytesRead, SocketFlags.None);
                        //смотрим, если считанный байт представляет конечный символ, выходим
                        if (!file_v)
                            if (count == 0 || bytesRead[0] == '\n') break;
                        //иначе добавляем в буфер
                        buffer.Add(bytesRead[0]);
                        if (file_v)
                        {
                            //richTextBox1.Text += fileSize + "-" + Encoding.UTF8.GetString(bytesRead);
                            fileSize--;
                            if (fileSize == 0) break;
                        }

                    }
                    Msg_t? r_m_t;

                    if (file_v)
                    {
                        add_file(buffer.ToArray());

                        file_v = false;
                    }
                    else
                    {
                        string responseText = Encoding.UTF8.GetString(buffer.ToArray());

                        Msg_t? r_m_t = JsonSerializer.Deserialize<Msg_t>(responseText);
                        add_mgs(r_m_t);
                    }

                    if (r_m_t != null)
                    {
                        switch (r_m_t.Command)
                        {
                            case "contact_list":
                                UpdateContactList(r_m_t);
                                break;
                            case "group_list":
                                UpdateGroupList(r_m_t);
                                break;
                            case "group_invite":
                                MessageBox.Show($"{r_m_t.Text} пригласил вас в группу!");
                                break;
                            default:
                                add_mgs(r_m_t);
                                break;
                        }
                    }



                }
            }
            catch (SocketException)
            {
                Console.WriteLine($"Не удалось установить подключение с {socket.RemoteEndPoint}");
            }
        }

        public void add_file(byte[] buffer)
        {
            PictureBox pictureB = new PictureBox();
            pictureB.Size = new Size(173, 103);
            pictureB.SizeMode = PictureBoxSizeMode.Zoom;
            using (MemoryStream ms = new MemoryStream(buffer))
            pictureB.Image = Image.FromStream(ms);
            flowLayoutPanel1.Controls.Add(pictureB);

        }

        private async void Send_Files(object sender, EventArgs e) //Сервер отправляет картинку
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
           
            string filename = openFileDialog1.FileName;

            byte[] bytes = System. IO.File.ReadAllBytes(filename);


            byte[] requestData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Msg_t_c("file", bytes.Length.ToString(), null)) + '\n');

            await socket.SendAsync(requestData, SocketFlags.None);

            await socket.SendAsync(bytes, SocketFlags.None);


            MessageBox.Show("Файл отправлен");
        }

        // Дополнительное задание
        private async void button5_Click(object sender, EventArgs e)   //Пригласить
        {
            if (contactList.SelectedItems.Count > 0)
            {
                var contactName = contactList.SelectedItems[0].Text;

                var msg = JsonSerializer.Serialize(new Msg_t_c("invite", contactName, null)) + '\n';
                byte[] requestData = Encoding.UTF8.GetBytes(msg);
                await socket.SendAsync(requestData, SocketFlags.None);
            }

        }

        private async void button6_Click(object sender, EventArgs e)  //Создать 
        {
            var groupNameForm = new Form();

            // Показываем диалог и проверяем результат
            if (groupNameForm.ShowDialog() == DialogResult.OK)
            {
                var groupName = groupNameForm.GroupName;

                try
                {
                    // Создаем сообщение для сервера
                    var msg = JsonSerializer.Serialize(new Msg_t_c("create_group", groupName, null));

                    // Отправляем запрос на сервер
                    byte[] requestData = Encoding.UTF8.GetBytes(msg + "\n");
                    await socket.SendAsync(requestData, SocketFlags.None);

                    // Обновляем локальный список групп
                    groupList.Items.Add(groupName);

                    MessageBox.Show($"Группа '{groupName}' успешно создана!",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при создании группы: {ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void button7_Click(object sender, EventArgs e)  //Добавить
        {
            if (contactList.SelectedItems.Count > 0 && groupList.SelectedItems.Count > 0)
            {
                var contactName = contactList.SelectedItems[0].Text;
                var groupName = groupList.SelectedItems[0].Text;

                var msg = JsonSerializer.Serialize(new Msg_t_c("add_to_group", $"{contactName}:{groupName}", null)) + '\n';
                byte[] requestData = Encoding.UTF8.GetBytes(msg);
                await socket.SendAsync(requestData, SocketFlags.None);
            }
        }

        public void UpdateContactList(Msg_t r_m_t)  //Обновление списка контактов
        {
            contacts[r_m_t.Text] = new Contact(r_m_t.Text, r_m_t.Sender, true);

            contactList.Items.Clear();
            foreach (var contact in contacts.Values)
            {
                var item = new ListViewItem(new[] { contact.Name, contact.Online ? "Онлайн" : "Офлайн" });
                contactList.Items.Add(item);
            }
        }

        public void UpdateGroupList(Msg_t r_m_t)   //Обновление списка группового чата
        {
            var groupData = JsonSerializer.Deserialize<GroupChat>(r_m_t.Text);
            groups[groupData.Name] = groupData;

            groupList.Items.Clear();
            foreach (var group in groups.Values)
            {
                groupList.Items.Add(group.Name);
            }
        }


    }

}
