using System;
using System.Drawing;
using System.Windows.Forms;
using HostFileEditor.Models;

namespace HostFileEditor.Controls
{
    public class HostFileElementView : Control
    {
        /// <summary>
        /// Copys a hostfile element to not return the same reference 
        /// </summary>>
        private static HostFileElement Copy(HostFileElement element)
        {
            return new HostFileElement
            {
                Domain = element.Domain,
                LineId = element.LineId,
                Ip = element.Ip
            };
        }

        protected readonly TextBox IpAddress;
        protected readonly TextBox DomainName;
        protected readonly Button RemoveBtn;
        protected HostFileElement Element { get; set; }

        public delegate void RemoveElementLine(Guid guid);
        public event RemoveElementLine ElementRemove;
        
        public HostFileElementView(HostFileElement element, Size size)
            : this(element, size.Width, size.Height)
        {
        }

        private HostFileElementView(HostFileElement element, int width, int height)
        {

            Element = Copy(element);
            Size = new Size(width, height);
            var textBoxHeight = Convert.ToInt32((width / 2) - width * 0.1);
            IpAddress = new TextBox
            {
                Size = new Size(Convert.ToInt32((width / 2) - width * 0.1), textBoxHeight),
                Text = element.Ip,
                Left = 1
            };
            if (IpAddress.Width > 180)
            {
                IpAddress.Width = 180;
            }
            
            RemoveBtn = new Button
            {
                Name = "RemoveButton",
                Text = "-",
                Width = 15,
                Height = IpAddress.Height,
            };
            
            DomainName = new TextBox
            {
                Size = new Size(Convert.ToInt32(width - ((RemoveBtn.Height+IpAddress.Width )*1.01 )), textBoxHeight),
                Text = element.Domain
            };

            DomainName.Left = IpAddress.Right + Convert.ToInt32((Width - IpAddress.Width - DomainName.Width - width * 0.05));

            RemoveBtn.Left = DomainName.Right + 3;


            RemoveBtn.Click += RemoveBtnOnClick;
            
            Controls.Add(IpAddress);
            Controls.Add(RemoveBtn);
            Controls.Add(DomainName);
        }

        private void RemoveBtnOnClick(object sender, EventArgs e)
        {
            ElementRemove?.Invoke(Element.Guid);
        }

        public HostFileElement GetOriginalElementData()
        {
            return Copy(Element);
        }

        public HostFileElement GetElementWithUpdates()
        {
            return new HostFileElement
            {
                Domain = DomainName.Text,
                Ip = IpAddress.Text,
                Guid = Element.Guid,
                LineId = Element.LineId
            };
        }
    }
}