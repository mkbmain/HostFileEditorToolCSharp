using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HostFileEditor.Models;
using HostFileEditor.Utils;

namespace HostFileEditor.Forms
{
    public partial class HostFileEditorControl : Form
    {
        HostFileViewer _hostFileViewer;

        private Point HostFileViewerStartingPoint = new Point(1,1);

        public HostFileEditorControl()
        {
            InitializeComponent();
            ResetViewer(HostFileIo.ReadHostFile());
        }

        private void HostFileViewerOnSaveElements(IEnumerable<HostFileElement> elements)
        {
            HostFileIo.SaveHostFile(elements);
            ResetViewer(HostFileIo.ReadHostFile());
        }

        private void HostFileViewerOnRemoveElement(Guid guid)
        {
            ResetViewer(_hostFileViewer.GetAll().Where(f=>f.Guid!= guid).ToArray());
        }

        private void HostFileViewerOnDisposed(object sender, EventArgs e)
        {
            if (_ignoreDispose)
            {
                return;
            }

            Application.Exit();
        }

        /// <summary>
        /// This is to get round a graphical glitch if i remove all the controls and re add them they skew up don't know why
        /// </summary>
        /// <param name="elements"></param>
        private void ResetViewer(IEnumerable<HostFileElement> elements)
        {
            _ignoreDispose = true;
            _hostFileViewer?.Dispose();
            _hostFileViewer = new HostFileViewer(elements) {Location = HostFileViewerStartingPoint};
            _hostFileViewer.AddEvent += HostFileViewerOnAddEvent;
            _hostFileViewer.SaveElements+= HostFileViewerOnSaveElements;
            _hostFileViewer.Disposed += HostFileViewerOnDisposed;
            _hostFileViewer.RemoveElement += HostFileViewerOnRemoveElement;
            _hostFileViewer.Show();
            _ignoreDispose = false;
        }

        private bool _ignoreDispose = false;

        private void HostFileViewerOnAddEvent(bool add)
        {
            var collection = _hostFileViewer.GetAll().ToList();
            collection.Add(new HostFileElement());
            ResetViewer(collection);
        }
    }
}