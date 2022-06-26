using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics.Objects
{
    public interface IDrawable
    {
        public void Update();
        public void Display(System.Drawing.Graphics e);
    }
}
