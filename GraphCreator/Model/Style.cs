using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCreator.Model
{
    public class Style
    {
        public int LineWidth { get; set; }

        public Color LineColor { get; set; }

        public Color TextColor { get; set; }

        public Style() => SetDefault();

        public void SetDefault()
        {
            LineWidth = 1;
            LineColor = Color.Red;
            TextColor = Color.Black;
        }
    }
}
