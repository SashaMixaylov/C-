using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Email { get; set; }
        

        public Contact(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email; 
           
          
        }
    }

    public class GroupChat      //Групповой чат
    {
        public string Name { get; set; }
        public List<string> Participant { get; set; }
        public Dictionary<string, string> People { get; private set; }

        public GroupChat(string name)
        {
            Name = name;
            Participant =  new List<string>();
        }
        public Dictionary<string, string> GetParticipants() 
        {
            return People;
        }
    }

    

}
