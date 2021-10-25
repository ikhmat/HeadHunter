using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime DateOfSending { get; set; }
        public string ChatId { get; set; }
        public string SenderId { get; set; }
    }
}
