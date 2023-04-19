
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPMessageSender.Structures
{
    public class Line
    {
        public string key { get; set; }
        public string value { get; set; }
        private bool enabled {get; set;}
        public Line() {
            enabled = false; 
        }

        public Line(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
        public bool isChecked() { return enabled; }
        public void check() { enabled = true; }
        public void uncheck() { enabled = false; }
    }
}
