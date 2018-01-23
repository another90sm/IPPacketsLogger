using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PLogger.Controls
{
    class BufferedTreeView : TreeView
    {
        private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
        private const int TVM_GETEXTENDEDSTYLE = 0x1100 + 45;
        private const int TVS_EX_DOUBLEBUFFER = 0x0004;
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        public bool IsEditing { get; private set; }
        public void Cut() { SendMessage(GetEditControl(), 0x300, IntPtr.Zero, IntPtr.Zero); }
        public void Copy() { SendMessage(GetEditControl(), 0x301, IntPtr.Zero, IntPtr.Zero); }
        public void Paste() { SendMessage(GetEditControl(), 0x302, IntPtr.Zero, IntPtr.Zero); }

        protected override void OnHandleCreated(EventArgs e)
        {
            SendMessage(this.Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)TVS_EX_DOUBLEBUFFER);
            base.OnHandleCreated(e);
        }

        protected override void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
        {
            IsEditing = true;
            base.OnBeforeLabelEdit(e);
        }
        protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
        {
            IsEditing = false;
            base.OnAfterLabelEdit(e);
        }
        private IntPtr GetEditControl()
        {
            IntPtr hEdit = SendMessage(this.Handle, 0x1100 + 15, IntPtr.Zero, IntPtr.Zero);
            if (hEdit == IntPtr.Zero) throw new InvalidOperationException("Not currently editing a label");
            return hEdit;
        }
    }
}
