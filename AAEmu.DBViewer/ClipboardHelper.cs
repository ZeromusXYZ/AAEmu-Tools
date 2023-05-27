using System;
using System.Threading;

namespace AAEmu.ClipboardHelper
{
    // Source: https://stackoverflow.com/questions/899350/how-do-i-copy-the-contents-of-a-string-to-the-clipboard-in-c
    /// <summary>
    /// Usage: new SetClipboardHelper( DataFormats.Text, "See, I'm on the clipboard" ).Go();
    /// </summary>
    class SetClipboardHelper : StaHelper
    {
        readonly string _format;
        readonly object _data;

        public SetClipboardHelper(string format, object data)
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
