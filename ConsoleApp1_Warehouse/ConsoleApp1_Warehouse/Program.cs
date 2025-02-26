using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1_Warehouse
{
//Создайте приложение которое позволит пользователю подкличиться и отключится к базы данных "Склад". В случае успешного подключения
// выведите информационное сообщение. Если подключение было неуспешным, сообщение об ошибке.
//Добавте следующую функциональность: отображение всей информации о товаре;, отображение всех типов товара;,отображение всех поставщиков.
    class Warehouse
    {
        static void Main(string[] args)
        {
            SqlConnection conn = null;


            try
            {
                conn = new SqlConnection("");
                Console.WriteLine("Установка соединения с базой данных Склад");
                conn.Open();
                Console.WriteLine("Соединение прошло успешно");


            }

            catch
            {
                Console.WriteLine("Ошибка соединения");
            }

            finally
            {
                if (conn != null) 
                {
                    conn.Close();
                    Console.WriteLine("Соединение завершено");
                }
            }
        }
    }

    public class Names
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Supplier { get; set; }
        public int Quantity { get; set; }
        public int CostPrice { get; set; }
        public int Date { get; set; }
    }

    public class AllInfo 
    {
        public static void Info(Names Nm) 
        {
            Console.WriteLine($"ID товара: {Nm.Id}");
            Console.WriteLine($"Имя товара: {Nm.Name}");
            Console.WriteLine($"Тип товара: {Nm.Type}");
            Console.WriteLine($"Поставщик: {Nm.Supplier}");
            Console.WriteLine($"Количество: {Nm.Quantity}");
            Console.WriteLine($"Себестоимость: {Nm.CostPrice}");
            Console.WriteLine($"Дата поставки: {Nm.Date}");

        }
    }
}
