using GraphCreator.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCreator.Drawer
{
    class VertexDrawer : BaseDrawer
    {

        Font font = new Font(FontFamily.GenericSansSerif, 12);
        SolidBrush fontBrush = new SolidBrush(Color.Black);
        Brush solidBrush = new SolidBrush(Color.White);
        Pen borderPen = new Pen(Color.Red);

        public override void Draw()
        {
            foreach (var vertex in Project.Vertexes)
            {
                int x = vertex.Pos.X;
                int y = vertex.Pos.Y;
                int r = Vertex.RADIUS;

                Graphics.FillEllipse(solidBrush, x - r, y - r, r * 2, r * 2);

                borderPen.Width = vertex.Style.LineWidth;
                borderPen.Color = vertex.Style.LineColor;
                Graphics.DrawEllipse(borderPen, x - r, y - r, r * 2, r * 2);

                fontBrush.Color = vertex.Style.TextColor;
                Graphics.DrawString(vertex.Index.ToString(), font, fontBrush, x - 9, y - 9);
            }
        }
    }
}
