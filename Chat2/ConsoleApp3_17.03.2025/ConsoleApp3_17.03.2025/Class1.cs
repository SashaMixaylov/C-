using ConsoleApp3_17._03._2025;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

/*Разработайте набор консольных приложений. Первое
приложение: серверное приложение, которое на запросы
клиента возвращает текущее время или дату на сервере.
Второе приложение: клиентское приложение, запрашивающее дату или время. Пользователь с клавиатуры
определяет, что нужно запросить. После отсылки даты
или времени сервер разрывает соединение. Клиентское
приложение отображает полученные данные.*/


IPEndPoint iPEndPiont = new IPEndPoint(IPAddress.Any, 8888);

Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
socket.Bind(iPEndPiont);
socket.Listen();
List<Socket> sockets = new List<Socket>(); //Создание списка клиентов
Dictionary<string,string> people = new Dictionary<string,string>();
Dictionary<string, List<string>> groups = new Dictionary<string, List<string>>();
Dictionary<string, List<string>> userGroups = new Dictionary<string, List<string>>();


Console.WriteLine("Сервер развернут по адрессу:" + socket.LocalEndPoint);
Console.WriteLine("Ожидаем подключение клиента");

while (true) 
{
    
    Socket client = await socket.AcceptAsync();

    //Console.WriteLine("Новый клиент " + client.RemoteEndPoint);
    sockets.Add(client);  //Добавление нового клиента в список
    teask_server(client);
   // task3.Start();
}




async Task teask_server(Socket client) 
{

    string name_client = "";
    string RemoteEP = client.RemoteEndPoint.ToString();
    try
    {
        while (true)
        {

            var buffer = new List<byte>();  //буффер для накопления входящих данных

            var bytesRead = new byte[1];
            //byte[] buffer = new byte[512];
            while (true) 
            {
                var count = await client.ReceiveAsync(bytesRead, SocketFlags.None);
                //если считанный байт представляет конечный символ то выходим
                if (count == 0 || bytesRead[0] == '\n') break;
                //иначе добавляем в буффер
                buffer.Add(bytesRead[0]);
            }

            string responseText = Encoding.UTF8.GetString(buffer.ToArray());

            Console.WriteLine("responseText" + responseText);
           // int bytes = 0; // количество считанных байтов
                           // считываем данные 


            //bytes = await client.ReceiveAsync(buffer, SocketFlags.None);
            // добавляем полученные байты в список

            // выводим отправленные клиентом данные
            //var responseText = Encoding.UTF8.GetString(buffer, 0, bytes);

            string resp_json = responseText.ToString();

            Msg_t_c? r_m_t = JsonSerializer.Deserialize<Msg_t_c>(resp_json);

            Console.WriteLine($"r_m_t.Command= {r_m_t.Command}-- r_m_t.Text{r_m_t.Text}");


            if (r_m_t.Command.IndexOf("name") == 0)
            {
                name_client = r_m_t.Text;

                byte[] requestData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Msg_t("add_client", name_client, "server", null)) + '\n');

                if (!people.ContainsKey(RemoteEP))
                {
                    people.Add(RemoteEP, name_client);
                }
                else
                {
                    people[RemoteEP] = name_client;
                }

                await client.SendAsync(requestData, SocketFlags.None);

                Console.WriteLine("команда-имя");
            }
            else if (r_m_t.Command.IndexOf("get_clients") == 0)
            {
                /* string all_name_cln = "";

                 foreach (var person in people)
                 {
                     all_name_cln += $"key: {person.Key}  value: {person.Value}\n";
                 }*/

                byte[] requestData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Msg_t("list_client","", name_client, people)) + '\n');

                await client.SendAsync(requestData, SocketFlags.None);

                Console.WriteLine("команда-пользователи");
            }

            else if (r_m_t.Command.IndexOf("file") == 0) 
            {
                string path = @"C:\Users\Sasha\Pictures\hub.png";   // путь к файлу

                var buffer_file = new List<byte>();  //буффер для накопления входящих данных

                var bytesRead1 = new byte[1];
                //byte[] buffer = new byte[512];
                int fileSize = Convert.ToInt32(r_m_t.Text);
                while (true)
                {
                    var count = await client.ReceiveAsync(bytesRead1, SocketFlags.None);
                    //если считанный байт представляет конечный символ то выходим
                    
                    //иначе добавляем в буффер
                    buffer_file.Add(bytesRead1[0]);

                    fileSize--;
                    if (fileSize == 0) break;
                }

                byte[] file_ = buffer_file.ToArray();

                // чтение из файла

                foreach (Socket socket in sockets)
                {
                   

                    byte[] requestData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Msg_t("file", file_.Length.ToString(), name_client, null)) + '\n');

                    await client.SendAsync(requestData, SocketFlags.None);

                    await client.SendAsync(file_, SocketFlags.None);
                }
                
            }

            else if (r_m_t.Command.IndexOf("msg") == 0)
            {

                byte[] requestData = Encoding.UTF8.GetBytes(resp_json + '\n');

                foreach (Socket socket in sockets)
                {
                    //await socket.SendAsync(requestData, SocketFlags.None);

                    if (socket != client)
                    {
                        await socket.SendAsync(requestData, SocketFlags.None);
                    }
                    else
                    {
                        r_m_t.Command = "my_msg";
                        byte[] requestData2 = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(r_m_t) + '\n');
                        await socket.SendAsync(requestData2, SocketFlags.None);
                    }

                }

                Console.WriteLine(resp_json);
            }

            else if (r_m_t.Command.IndexOf("invite") == 0)  //  Обработки приглашений
            {
                string invitedUser = r_m_t.Text;
                string senderName = name_client;

                // Находим сокет приглашенного пользователя
                Socket targetSocket = null;
                foreach (var person in people)
                {
                    if (person.Value == invitedUser)
                    {
                        targetSocket = sockets.FirstOrDefault(s =>
                            s.RemoteEndPoint.ToString() == person.Key);
                        break;
                    }
                }

                if (targetSocket != null)
                {
                    var inviteMsg = JsonSerializer.Serialize(new Msg_t("invite",
                        $"{senderName}:{invitedUser}", "server", null)) + '\n';

                    byte[] requestData = Encoding.UTF8.GetBytes(inviteMsg);
                    await targetSocket.SendAsync(requestData, SocketFlags.None);

                    Console.WriteLine($"Отправлено приглашение от {senderName} пользователю {invitedUser}");
                }
            }

            
            else if (r_m_t.Command.IndexOf("create_group") == 0)  // Обработка создания группы
            {
                string groupName = r_m_t.Text;

                
                if (!groups.ContainsKey(groupName))
                {
                    groups[groupName] = new List<string>();

                    var responseMsg = JsonSerializer.Serialize(new Msg_t(
                        "group_created",
                        groupName,
                        "server",
                        null)) + '\n';

                    byte[] requestData = Encoding.UTF8.GetBytes(responseMsg);
                    await client.SendAsync(requestData, SocketFlags.None);

                    Console.WriteLine($"Создана новая группа: {groupName}");

                    // Отправляем обновление всем подключенным клиентам
                    foreach (var socket in sockets)
                    {
                        var updateMsg = JsonSerializer.Serialize(new Msg_t(
                            "group_list_update",
                            string.Join(",", groups.Keys),
                            "server",
                            null)) + '\n';

                        byte[] updateData = Encoding.UTF8.GetBytes(updateMsg);
                        await socket.SendAsync(updateData, SocketFlags.None);
                    }
                }
                else
                {
                    var errorMsg = JsonSerializer.Serialize(new Msg_t(
                        "error",
                        "Группа с таким названием уже существует",
                        "server",
                        null)) + '\n';

                    byte[] errorData = Encoding.UTF8.GetBytes(errorMsg);
                    await client.SendAsync(errorData, SocketFlags.None);
                }
            }

            
            else if (r_m_t.Command.IndexOf("add_to_group") == 0)  // Обработку добавления пользователя в группу
            {
                var parts = r_m_t.Text.Split(':');
                string userName = parts[0];
                string groupName = parts[1];

                if (!groups.ContainsKey(groupName))
                {
                    var errorMsg = JsonSerializer.Serialize(new Msg_t(
                        "error",
                        $"Группа '{groupName}' не существует",
                        "server",
                        null)) + '\n';

                    byte[] errorData = Encoding.UTF8.GetBytes(errorMsg);
                    await client.SendAsync(errorData, SocketFlags.None);
                    return;
                }

                if (!people.ContainsValue(userName))
                {
                    var errorMsg = JsonSerializer.Serialize(new Msg_t(
                        "error",
                        $"Пользователь '{userName}' не найден",
                        "server",
                        null)) + '\n';

                    byte[] errorData = Encoding.UTF8.GetBytes(errorMsg);
                    await client.SendAsync(errorData, SocketFlags.None);
                    return;
                }

                if (!groups[groupName].Contains(userName))
                {
                    groups[groupName].Add(userName);

                    if (!userGroups.ContainsKey(userName))
                    {
                        userGroups[userName] = new List<string>();
                    }
                    userGroups[userName].Add(groupName);

                    var successMsg = JsonSerializer.Serialize(new Msg_t(
                        "group_join_success",
                        $"{userName}:{groupName}",
                        "server",
                        null)) + '\n';

                    byte[] successData = Encoding.UTF8.GetBytes(successMsg);
                    await client.SendAsync(successData, SocketFlags.None);

                    Console.WriteLine($"Пользователь {userName} добавлен в группу {groupName}");
                }
            }

            Console.WriteLine($"Новый клиент - {name_client}  remoteEP - {RemoteEP}");
        }
    }

    catch (SocketException)
    {
        sockets.Remove(client);
        people.Remove(client.RemoteEndPoint.ToString());
        Console.WriteLine($"Не удалось установить подключение с {socket.RemoteEndPoint} - {name_client}");
    }
    catch (IOException ex)
    {
        Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка при передаче файла: {ex.Message}");
    }

}



