using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Connection      //Класс для соединения
    {
        static void Main(string[] args)
        {
            SqlConnection conn = null;


            try
            {
                conn = new SqlConnection("");
                Console.WriteLine("Установка соединения с базой данных ");
                conn.Open();
                


            }
        }
    }
}
