using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1_Client_Num3
{
    public class Msg_t
    {
        public string Command { get; set; }
        public string Text { get; set; }
        public string Sender { get; set; }
        public Dictionary<string, string> People { get; set; }

        public Msg_t(string command, string text, string sender, Dictionary<string, string> people)
        {
            Command = command;
            Text = text;
            Sender = sender;
            People = people;
        }
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

    public class Contact        //Для контактов
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public bool Online { get; set; }

        public Contact(string name, string id, bool Online)
        {
            Name = name;
            Id = id;
            Online = Online;
        }
    }

    public class GroupChat      //Групповой чат
    {
        public string Name { get; set; }
        public List<string> Participant { get; set; }

        public GroupChat(string name, List<string> participant)
        {
            Name = name;
            Participant = participant ?? new List<string>();
        }
    }
}
