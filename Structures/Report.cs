using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPMessageSender.Structures
{
    public class Report
    {
        public List<Line> lines { get; set; }
        public string name { get; set; }
        private bool enabled { get; set; }
        public Report() {
            lines = new List<Line>();
            enabled = false;
        }
        public bool isChecked(){return enabled;}
        public void check() { enabled = true; }
        public void uncheck() { enabled = false; }
    }
}
