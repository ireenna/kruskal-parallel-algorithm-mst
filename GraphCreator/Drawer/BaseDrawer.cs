using GraphCreator.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCreator.Drawer
{
    abstract class BaseDrawer
    {
        public Project Project { get; set; }
        public Graphics Graphics { get; set; }
        public abstract void Draw();
    }
}
