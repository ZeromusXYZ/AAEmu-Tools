using System;
using System.Threading;
using System.Windows.Forms;

namespace AAEmu.DBEditor.utils
{
    // Source: https://stackoverflow.com/questions/899350/how-do-i-copy-the-contents-of-a-string-to-the-clipboard-in-c
    /// <summary>
    /// Usage: new SetClipboardHelper( DataFormats.Text, "See, I'm on the clipboard" ).Go();
    /// </summary>
    class ClipboardHelper : StaHelper
    {
        readonly string _format;
        readonly object _data;

        public ClipboardHelper(string format, object data)
        {
            _format = format;
            _data = data;
        }

        protected override void Work()
        {
            var obj = new System.Windows.Forms.DataObject(
                _format,
                _data
            );

            System.Windows.Forms.Clipboard.SetDataObject(obj, true);
        }

        public static void CopyToClipBoard(string cliptext)
        {
            try
            {
                // Because nothing is ever as simple as the next line >.>
                // Clipboard.SetText(s);
                // Helper will (try to) prevent errors when copying to clipboard because of threading issues
                var cliphelp = new ClipboardHelper(DataFormats.Text, cliptext);
                cliphelp.DontRetryWorkOnFailed = false;
                cliphelp.Go();
            }
            catch
            {
            }
        }

        public static void OnClick(object sender, EventArgs e)
        {
            if (sender is Label label)
                CopyToClipBoard(label.Text);
        }

        public static void MakeFormLabelsClickable(Form form)
        {
            void MakeControlClickable(Control thisControl)
            {
                // CopyToClipboard for Labels
                if (thisControl is Label label)
                {
                    // Ignore non-named labels
                    if (!label.Name.ToLower().StartsWith("label"))
                    {
                        label.Click += OnClick;
                        label.Cursor = Cursors.Hand;
                    }
                }
                // Loop child controls
                foreach (Control control in thisControl.Controls)
                    MakeControlClickable(control);
            }

            // Loop Form controls
            foreach (Control control in form.Controls)
                MakeControlClickable(control);
        }
    }

    abstract class StaHelper
    {
        readonly ManualResetEvent _complete = new ManualResetEvent(false);

        public void Go()
        {
            var thread = new Thread(new ThreadStart(DoWork))
            {
                IsBackground = true,
            };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        // Thread entry method
        private void DoWork()
        {
            try
            {
                _complete.Reset();
                Work();
            }
            catch (Exception ex)
            {
                if (DontRetryWorkOnFailed)
                    throw;
                else
                {
                    try
                    {
                        Thread.Sleep(1000);
                        Work();
                    }
                    catch
                    {
                        // ex from first exception
                        System.Windows.Forms.MessageBox.Show(ex.Message, "Copy to Clipboard", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        // LogAndShowMessage(ex);
                    }
                }
            }
            finally
            {
                _complete.Set();
            }
        }

        public bool DontRetryWorkOnFailed { get; set; }

        // Implemented in base class to do actual work.
        protected abstract void Work();
    }


}
