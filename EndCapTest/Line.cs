using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace EndCapTest
{
    class Line : PaintableObject
    {
        public Point p1;
        public Point p2;
        public bool IsArrow = false;
        public Color FgColor = Color.Black;
        private int thickness = 1;
        public int Thickness
        {
            get => thickness;
            set
            {
                if (value < 1) thickness = 1;
                else if (value > 50) thickness = 50;
                else thickness = value;
            }
        }

        public Line()
        {
        }

        public void Paint(Graphics g)
        {
            if (p1 == null || p2 == null) return;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var p = new Pen(FgColor, Thickness);
            if (IsArrow)
                p.EndCap = LineCap.ArrowAnchor;
            g.DrawLine(p, p1, p2);
        }
    }
}
