using System;

namespace HostFileEditor.Models
{
    public class HostFileElement
    {
        public HostFileElement()
        {
            Guid = Guid.NewGuid();
        }

        public string Ip { get; set; }
        public string Domain { get; set; }

        public int LineId { get; set; }

        public Guid Guid { get; set; }
    }
}