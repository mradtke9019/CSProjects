using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Timer = System.Windows.Forms.Timer;
using Graphics.Objects;

namespace Graphics
{
    public partial class GraphicForm : Form
    {
        List<IDrawable> objects;

        public GraphicForm()
        {
            InitializeComponent();
        }

        private void GraphicForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 10;
            timer.Tick += Timer_Tick;
            this.Paint += GraphicForm_Paint;
            objects = new List<IDrawable>();
            //objects.Add(new Circle(100, new MathLib.Vector2D(0, 0), ));\
            objects.Add(new Line(new MathLib.Vector2D(0,0), new MathLib.Vector2D(0, 200), Color.Red));
        }

        private void GraphicForm_Paint(object? sender, PaintEventArgs e)
        {
            objects.ForEach(x => 
            { 
                x.Update(); 
                x.Display(e.Graphics); 
            });
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Invalidate();
        }


    }
}
