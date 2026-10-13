using System.ComponentModel;
using System.Runtime.InteropServices;

namespace SinsEntityConverter
{
    public class InvisibleScrollTextBox : RichTextBox
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool ShowScrollBar(IntPtr hWnd, int bar, bool show);

        private const int SB_BOTH = 3; // 3 = Both Scroll Bars

        public InvisibleScrollTextBox()
        {
            Multiline = true;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (!LicenseManager.UsageMode.Equals(LicenseUsageMode.Designtime))
                HideScrollBar();
        }

        private void HideScrollBar()
        {
            // Pass 'false' to hide the vertical scroll bar graphically
            ShowScrollBar(Handle, SB_BOTH, false);
        }

    }
}
