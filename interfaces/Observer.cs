using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPMessageSender.interfaces
{
    public interface Observer
    {
        void AddResult(int code, string report, string mu);
    }
}
