using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json.Linq;

namespace RoomChat.Models
{
    public class ResponseGeneric
    {
        public string response_type { get; set; }
        public string text { get; set; }
        public string title { get; set; }
        public List<Option> options { get; set; }
        public int? time { get; set; }
        public bool? typing { get; set; }
        public string source { get; set; }
        public string description { get; set; }
    }

    public class Option
    {
        public string label { get; set; }
        public Value value { get; set; }
        public ICommand SendCommandOption { get; set; }
        public string finalvalue { get; set; }
        public JToken contextOptions { get; set; }
        public Option optionaux { get; set; }
    }

    public class Value
    {
        public Input input { get; set; }
    }


    public class Input
    {
        public string text { get; set; }
    }


}
