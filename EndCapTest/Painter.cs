using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace EndCapTest
{
    class Painter
    {
        internal enum DrawType
        {
            Line, Arrow
        }

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

        private Image mainImg;
        public Image MainImage
        {
            get
            {
                var i = (Image)mainImg.Clone();
                var g = Graphics.FromImage(i);
                g.DrawImage(mainImg, new Point(0, 0));
                return i;
            }
        }

        private DrawType objectType = DrawType.Line;
        private PaintableObject obj;
        public DrawType ObjectType
        {
            get => objectType;
            set
            {
                objectType = value;
                switch (value)
                {
                    case DrawType.Line:
                        obj = new Line();
                        ((Line) obj).Thickness = thickness;
                        break;
                    case DrawType.Arrow:
                        obj = new Line();
                        ((Line) obj).IsArrow = true;
                        ((Line)obj).Thickness = thickness;
                        break;
                }
            }
        }

        private List<Point> points = new List<Point>();

        private void Draw(Graphics g)
        {
            switch (ObjectType)
            {
                case DrawType.Line:
                case DrawType.Arrow:
                    if (points.Count > 1)
                    {
                        Line ln = (Line)obj;
                        ln.p1 = points[0];
                        ln.p2 = points[1];
                        ln.Paint(g);
                    }

                    break;
            }
        }

        public void Paint(Graphics g)
        {
            if (mainImg == null)
            {
                mainImg = new Bitmap(
                    (int)g.VisibleClipBounds.Width,
                    (int)g.VisibleClipBounds.Height,
                    g
                    );
            }
            var tg = Graphics.FromImage(mainImg);
            Draw(tg);
        }

        public void Preview(Graphics g)
        {
            if (mainImg == null)
            {
                mainImg = new Bitmap(
                    (int)g.VisibleClipBounds.Width,
                    (int)g.VisibleClipBounds.Height,
                    g
                );
            }
            var img = (Image)mainImg.Clone();
            var tg = Graphics.FromImage(img);
            tg.Clear(Color.White);
            tg.DrawImage(mainImg, new Point(0, 0));
            Draw(tg);
            g.DrawImage(img, new Point(0, 0));
        }

        public void SetStartPoint(Point pt)
        {
            points.Clear();
            points.Add(pt);
        }

        public void SetPoint(Point pt, int num = 0)
        {
            num -= 1;
            if (points.Count > num)
                points[num] = pt;
            else points.Add(pt);
        }

        public void Show(Graphics g)
        {
            if (mainImg != null)
                g.DrawImage(mainImg, new Point(0, 0));
        }
    }
}
