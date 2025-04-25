using System.Text;

string path = @"C:\Users\User\Desktop\БВ311\03_04\test_img/Безымянный.png";   // путь к файлу


// чтение из файла
using (FileStream fstream = File.OpenRead(path))
{


    // выделяем массив для считывания данных из файла
    byte[] buffer = new byte[fstream.Length];


    // считываем данные
    await fstream.ReadAsync(buffer, 0, buffer.Length);
    // декодируем байты в строку
    Console.WriteLine($"Текст из файла: {buffer.Length}\n");
    string responseText = Encoding.UTF8.GetString(buffer);
    string textFromFile = Encoding.Default.GetString(buffer);
    Console.WriteLine($"Текст из файла: {responseText}\n {textFromFile}");
}
