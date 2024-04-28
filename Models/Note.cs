using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_Windows_Studies.Models
{
    internal class Note
    {
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
