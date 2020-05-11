using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HostFileEditor.Controls;
using HostFileEditor.Models;

namespace HostFileEditor.Forms
{
    public partial class HostFileViewer : Form
    {
        private List<HostFileElementView> _views = new List<HostFileElementView>();
        private Size ViewSize = new Size(400, 33);
        private Size MeSize;

        public delegate void SaveElementsClick(IEnumerable<HostFileElement> elements);

        public event SaveElementsClick SaveElements;


        public delegate void AddEventClicked(bool add);

        public event AddEventClicked AddEvent;

        public delegate void RemoveElementClick(Guid guid);

        public event RemoveElementClick RemoveElement;

        private int _top = 1;

        public IEnumerable<HostFileElement> GetAll()
        {
            return _views.Select(f => f.GetElementWithUpdates());
        }

        public HostFileViewer(IEnumerable<HostFileElement> elements)
        {
            InitializeComponent();
            MeSize = Size;
            _views = elements.Select(f => new HostFileElementView(f, ViewSize)).ToList();

            foreach (var t in _views)
            {
                t.ElementRemove += OnElementRemove;
            }

            AddViews();
        }

        private void OnElementRemove(Guid guid)
        {
            RemoveElement?.Invoke(guid);
        }


        private void AddViews()
        {
            _top = 1;
            Size = MeSize;
            AutoScroll = true;
            foreach (var item in _views)
            {
                AddViewToScreen(item);
            }

            Save.Top = _top + 10;
            Save.Left = (this.Width / 2) - (Save.Width / 2);
        }

        private void AddViewToScreen(HostFileElementView view)
        {
            view.Location = new Point(1, _top);
            AddElementButton.Top = _top;
            _top += view.Height;
            Controls.Add(view);
        }

        private void AddElementButton_Click(object sender, EventArgs e)
        {
            AddEvent?.Invoke(true);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            SaveElements?.Invoke(GetAll());
        }
    }
}