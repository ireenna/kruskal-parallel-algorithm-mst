using GraphCreator.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphCreator.Model;

namespace GraphCreator.Drawer
{
    class EdgeDrawer : BaseDrawer
    {
        private const int W = 20;
        private const int H = 10;

        Pen edgePen = new Pen(Color.Red);
        Pen edgeWeightPen = new Pen(Color.Red);
        Font edgeWeightFont = new Font(FontFamily.GenericSansSerif, 7);
        SolidBrush edgeTextBrush = new SolidBrush(Color.Red);
        public override void Draw()
        {
            foreach (var edge in Project.Edges)
            {
                var startPos = edge.StartVertex.Pos;
                var endPos = edge.EndVertex.Pos;
                var centerPos = GetCenterPos(startPos, endPos);

                edgePen.Width = edge.Style.LineWidth;
                edgePen.Color = edge.Style.LineColor;
                Graphics.DrawLine(edgePen, startPos, endPos);

                
                    edgeTextBrush.Color = edge.Style.TextColor;
                    edgeWeightPen.Color = edge.Style.LineColor;

                    Rectangle rect = new Rectangle(centerPos.X - W / 2, centerPos.Y - H / 2, W, H);
                    Graphics.FillRectangle(Brushes.White, rect);
                    Graphics.DrawRectangle(edgeWeightPen, rect);

                    Graphics.DrawString(edge.Weight.ToString(), edgeWeightFont, edgeTextBrush, centerPos.Clone().Sub(W / 2, H / 2 + 1));
                
            }
        }
        public static Pos GetCenterPos(Pos start, Pos end)
        {
            Pos result = new Pos();
            result.X = (start.X + end.X) / 2;
            result.Y = (start.Y + end.Y) / 2;
            return result;
        }
    }
}
