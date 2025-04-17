using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3_17._03._2025
{
    public class Msg_t
    {
        public string Command { get; set; }
        public string Text { get; set; }
        public string Sender { get; set; }

        public Dictionary<string, string> People { get; set; }

        public Msg_t(string cmd, string text, string sender, Dictionary<string, string> people)
        {
            Command = cmd;
            Text = text;
            Sender = sender;
            People = people;
        }


        //{"Command":"Тут Команда"}
    }

    public class Msg_t_c
    {
        public string Command { get; set; }
        public string Text { get; set; }
        public Dictionary<string, string> People { get; set; }

        public Msg_t_c(string command, string text, Dictionary<string, string> people)
        {
            Command = command;
            Text = text;
            People = people;
        }
    }
}
