using GraphCreator.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphCreator.Drawer
{
    public class DrawerManager
    {
        private Project project;
        private Timer timer = new Timer();

        private Panel drawPanel;
        private Graphics drawPanelGraphics;
        private Bitmap buffer;
        private Graphics bufferGraphics;

        private List<BaseDrawer> Drawers { get; set; } = new List<BaseDrawer>();

        private void RegisterDrawers()
        {
            Drawers.Add(new EdgeDrawer());
            Drawers.Add(new VertexDrawer());
        }

        public DrawerManager(Project project, Panel drawPanel)
        {
            RegisterDrawers();
            this.project = project; 
            this.drawPanel = drawPanel;

            timer.Interval = 25;
            timer.Tick += Draw;
            InitGraphics();
        }

        private void InitGraphics()
        {
            drawPanelGraphics = drawPanel.CreateGraphics();
            buffer = new Bitmap(drawPanel.Width, drawPanel.Height);
            bufferGraphics = Graphics.FromImage(buffer);
        }

        public void StartAutoDraw()
        {
            timer.Start();
        }
        
        public void Draw(object sender = null, object e = null)
        {

            bufferGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            bufferGraphics.Clear(Color.White);
            foreach (var drawer in Drawers)
            {
                drawer.Project = project;
                drawer.Graphics = bufferGraphics;
                drawer.Draw();
            }

            drawPanelGraphics.DrawImage(buffer, 0, 0);
        }
    }
}
