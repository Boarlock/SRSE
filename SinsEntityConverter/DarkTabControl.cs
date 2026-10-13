using System.Drawing;
using System.Windows.Forms;

public class DarkTabControl : TabControl
{
    private readonly Color nearBlackBG = Color.FromArgb(18, 18, 18);
    private readonly Color lightBlackBG = Color.FromArgb(30, 30, 30);
    private readonly Color nearWhiteFG = Color.FromArgb(224, 224, 224);
    private readonly Color blueFG = Color.FromArgb(0, 122, 204);
    private readonly Color hoverBG = Color.FromArgb(45, 45, 48);

    private int hoveredIndex = -1;

    public DarkTabControl()
    {
        DrawMode = TabDrawMode.OwnerDrawFixed;
        SizeMode = TabSizeMode.Fixed;

        // Crucial: Tell the OS we are completely replacing the paint sequence
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        int newHover = -1;
        for (int i = 0; i < TabCount; i++)
        {
            if (GetTabRect(i).Contains(e.Location))
            {
                newHover = i;
                break;
            }
        }
        if (newHover != hoveredIndex)
        {
            hoveredIndex = newHover;
            Invalidate();
        }
    }

    protected override void OnMouseLeave(System.EventArgs e)
    {
        base.OnMouseLeave(e);
        hoveredIndex = -1;
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        // 1. Wipe out the base container area background completely
        using (SolidBrush bgBrush = new SolidBrush(nearBlackBG))
        {
            e.Graphics.FillRectangle(bgBrush, ClientRectangle);
        }

        // 2. Draw the individual tabs
        for (int i = 0; i < TabCount; i++)
        {
            Rectangle tabRect = GetTabRect(i);
            bool isSelected = (i == SelectedIndex);
            bool isHovered = (i == hoveredIndex);

            Color currentTabBG = isSelected ? lightBlackBG : (isHovered ? hoverBG : nearBlackBG);

            // FIX: Inflate the rectangle by 1-2 pixels during the fill phase.
            // This aggressively forces our dark brush to over-draw the native OS border lines.
            Rectangle fillRect = new Rectangle(tabRect.X - 1, tabRect.Y, tabRect.Width + 2, tabRect.Height + 1);

            using (SolidBrush tabBrush = new SolidBrush(currentTabBG))
            {
                e.Graphics.FillRectangle(tabBrush, fillRect);
            }

            // Draw the signature buttonBack blue accent strip on the active tab
            if (isSelected)
            {
                using (SolidBrush accentBrush = new SolidBrush(blueFG))
                {
                    e.Graphics.FillRectangle(accentBrush, fillRect.X, fillRect.Y, fillRect.Width, 3);
                }
            }

            // Draw text cleanly over the custom painted surface
            Color currentText = nearWhiteFG;
            TextRenderer.DrawText(
                e.Graphics,
                TabPages[i].Text,
                Font,
                tabRect,
                currentText,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis
            );
        }

        // 3. Fix the giant container border surrounding the actual form pages
        if (TabCount > 0 && SelectedTab != null)
        {
            SelectedTab.BackColor = lightBlackBG;

            // FIX: Draw a clean custom frame line around the entire child container panel area
            // to box out any remaining white/grey OS border remnants.
            using (Pen containerBorderPen = new Pen(lightBlackBG, 2))
            {
                int displayOffset = DisplayRectangle.Top - 1;
                e.Graphics.DrawRectangle(
                    containerBorderPen,
                    0,
                    displayOffset,
                    ClientRectangle.Width - 1,
                    ClientRectangle.Height - displayOffset - 1
                );
            }
        }
    }
}
