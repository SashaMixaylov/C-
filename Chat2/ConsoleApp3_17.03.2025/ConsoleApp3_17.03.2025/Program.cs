/*using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

//Сервер
IPEndPoint iPEndPiont = new IPEndPoint(IPAddress.Any, 8888);

Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
socket.Bind(iPEndPiont);
socket.Listen();
Console.WriteLine("Сервер развернут по адрессу:" + socket.LocalEndPoint);

Console.WriteLine("Ожидаем подключение клиента");

Socket client = await socket.AcceptAsync();

while (true) 
{
  
    byte[] buffer = new byte[512];
    int bytes = 0; // количество считанных байтов
                   // считываем данные 
    
   

    bytes = await client.ReceiveAsync(buffer, SocketFlags.None);
        // добавляем полученные байты в список
        
   
    // выводим отправленные клиентом данные
    var responseText = Encoding.UTF8.GetString(buffer, 0, bytes);
    Console.WriteLine(responseText);
    //byte[] data = Encoding.UTF8.GetBytes(DateTime.Now.ToLongTimeString());

//    await client.SendAsync(data, SocketFlags.None);

  //  Console.WriteLine($"Клиенту {client.RemoteEndPoint}отправлены данные");

    

}



//Console.WriteLine("Клиент подключен:" + client.RemoteEndPoint);*/