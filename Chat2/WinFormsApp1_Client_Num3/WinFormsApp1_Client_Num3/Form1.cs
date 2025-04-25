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
            //byte[] requestData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Msg_t_c("file", "", null)) + '\n');
            //await socket.SendAsync(requestData, SocketFlags.None);
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = "Выберите изображение";
                    dialog.Filter = "Изображения (*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        // Создаем новый PictureBox для flowLayoutPanel
                        PictureBox pictureBox = new PictureBox
                        {
                            Size = new Size(200, 200), // Устанавливаем фиксированный размер
                            SizeMode = PictureBoxSizeMode.Zoom, // Масштабируем изображение с сохранением пропорций
                            BorderStyle = BorderStyle.FixedSingle // Добавляем рамку
                        };

                        // Загружаем изображение
                        using (FileStream stream = new FileStream(dialog.FileName, FileMode.Open))
                        {
                            pictureBox.Image = Image.FromStream(stream);
                        }

                        // Добавляем PictureBox в flowLayoutPanel
                        flowLayoutPanel1.Controls.Add(pictureBox);

                        // Отправляем уведомление о файле
                        byte[] requestData = Encoding.UTF8.GetBytes(
                            JsonSerializer.Serialize(new Msg_t_c("file", dialog.FileName, null)) + '\n'
                        );
                        await socket.SendAsync(requestData, SocketFlags.None);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}");
            }


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
                            MessageBox.Show($"{r_m_t.Text} пригласил вас в группу!");
                            break;
                        default:
                            add_mgs(r_m_t);
                            break;
                    }
                }
            }
            catch (SocketException)
            {
                Console.WriteLine($"Не удалось установить подключение с {socket.RemoteEndPoint}");
            }
        }

        public void add_file(byte[] buffer)   //для кнопки 3 (Img)   ????????
        {
            PictureBox pictureB = new PictureBox();
            pictureB.Size = new Size(173, 103);
            pictureB.SizeMode = PictureBoxSizeMode.Zoom;
            using (MemoryStream ms = new MemoryStream(buffer))
                pictureB.Image = Image.FromStream(ms);
            flowLayoutPanel1.Controls.Add(pictureB);

        }

        //private async void Send_Files(object sender, EventArgs e) //Сервер отправляет картинку
        //{
        //    if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
        //        return;

        //    string filename = openFileDialog1.FileName;

        //    byte[] bytes = System.IO.File.ReadAllBytes(filename);


        //    byte[] requestData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Msg_t_c("file", bytes.Length.ToString(), null)) + '\n');

        //    await socket.SendAsync(requestData, SocketFlags.None);

        //    await socket.SendAsync(bytes, SocketFlags.None);


        //    MessageBox.Show("Файл отправлен");
        //}

        // Дополнительное задание
        private async void button5_Click(object sender, EventArgs e)   //Пригласить
        {
            try
            {
                // Проверка выбранного контакта
                if (contactList.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите контакт для приглашения");
                    return;
                }

                // Получение данных контакта
                string contactName = contactList.SelectedItems[0].Text;

                // Формирование сообщения приглашения
                var inviteMessage = JsonSerializer.Serialize(new Msg_t_c(
                    "invite_to_chat",
                    $"{contactName}",
                    null
                ));

                // Отправка приглашения
                byte[] requestData = Encoding.UTF8.GetBytes(inviteMessage + "\n");
                await socket.SendAsync(requestData, SocketFlags.None);

                // Отображение результата
                MessageBox.Show($"Приглашение отправлено пользователю {contactName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке приглашения: {ex.Message}");
            }

        }

        private async void button6_Click(object sender, EventArgs e)  //Создать 
        {
            GroupNameForm groupNameForm = new GroupNameForm();

            if (groupNameForm.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    // Получаем введенное имя группы
                    string groupName = groupNameForm.GroupName;

                    // Создаем новую группу
                    GroupChat group = new GroupChat(groupName);

                    // Создаем сообщение для сервера
                    //var msg = JsonSerializer.Serialize(new Msg_t_c("create_group", group, null));
                    var msg = JsonSerializer.Serialize(new Msg_t_c("create_group", group.Name, null));

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

        public void But(ContactInfo contactData) 
        {
            TextBox txtEmail = new TextBox();
            ContactInfo contactInfo = new ContactInfo(this);

            try
            {
                // Проверяем наличие всех необходимых данных
                if (contactList.SelectedItems.Count == 0 || groupList.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите контакт и группу");
                    return;
                }

                // Получаем выбранные значения
                string contactName = contactList.SelectedItems[0].Text;
                string groupName = groupList.SelectedItems[0].Text;



                // Создаём объект данных контакта
               

                // Создаём сообщение с данными контакта
                var msg = JsonSerializer.Serialize(new Msg_t_c(
                    "add_to_group",
                    $"{contactName}:{groupName}", null));

                // Добавляем завершающий символ новой строки
                msg += "\n";

                // Преобразуем сообщение в байты
                byte[] requestData = Encoding.UTF8.GetBytes(msg);

                // Отправляем данные асинхронно
                //await socket.SendAsync(requestData, SocketFlags.None);

                MessageBox.Show($"Контакт успешно добавлен в группу {groupName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении контакта в группу: {ex.Message}");
            }
        }


        private async void button7_Click(object sender, EventArgs e)  //Добавить
        {
            TextBox txtEmail = new TextBox();
            ContactInfo contactInfo = new ContactInfo(this);
            if (contactInfo.ShowDialog(this) == DialogResult.OK)
            {
                // Добавляем контакт в список
                contactList.Items.Add(new ListViewItem(new string[]
                {
            contactInfo.Id.ToString(),
            contactInfo.Name,
            contactInfo.Email
                }));

                // Обновляем словарь контактов
                contacts[contactInfo.Name] = new Contact(
                    contactInfo.Id.ToString(),
                    contactInfo.Name,
                    contactInfo.Email
                );

                // Отправляем информацию на сервер
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



        public void UpdateContactList(Msg_t r_m_t)  //Обновление списка контактов
        {
            contacts[r_m_t.Text] = new Contact(r_m_t.Text, r_m_t.Text, r_m_t.Text);

            contactList.Items.Clear();
            foreach (var contact in contacts.Values)
            {
                var item = new ListViewItem(new[] { contact.Id, contact.Name, contact.Email});
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

        private async void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = openFileDialog1.FileName;

            byte[] bytes = System.IO.File.ReadAllBytes(filename);


            byte[] requestData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Msg_t_c("file", bytes.Length.ToString(), null)) + '\n');

            await socket.SendAsync(requestData, SocketFlags.None);

            await socket.SendAsync(bytes, SocketFlags.None);


            MessageBox.Show("Файл отправлен");
        }

       

      

       
    }
}

