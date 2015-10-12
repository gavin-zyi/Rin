using Rin.Lang;
using Rin.Lang.Parsing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rin.Launcher
{
    class Program : Form
    {
        private Graphics graphics;
        private SolidBrush brush;
        private Function drawLoop;

        public Program()
        {
            InitializeComponent();

            Paint += OnDraw;

            var runtime = new Runtime();

            runtime.RegisterFunction("draw", args => drawLoop = (Function)args[0]);
            runtime.RegisterFunction("set_fill", args =>
                {
                    var color = (List)args[0];
                    brush = new SolidBrush(Color.FromArgb(
                        ((int)((Number)color.Value[0]).Value), 
                        ((int)((Number)color.Value[1]).Value),
                        ((int)((Number)color.Value[2]).Value)));
                });
            runtime.RegisterFunction("poly", args =>
                {
                    var points = new List<Point>();

                    for (int i = 0; i < args.Length; i += 2)
                    {
                        points.Add(new Point(((int)((Number)args[i]).Value), ((int)((Number)args[i + 1]).Value)));
                    }

                    graphics.FillPolygon(brush, points.ToArray());
                });


            var a = Eval<Rin.Lang.Module>(runtime, @"

class Point(x, y, z):
    func rotate_x(angle):
        var rad, cosa, sina, ny, nz
        rad = angle * maths.PI / 180
        cosa = maths.cos(rad)
        sina = maths.sin(rad)
        ny = y * cosa - z * sina
        nz = y * sina + z * cosa
        return Point(x, ny, nz)

    func rotate_y(angle):
        var rad, cosa, sina, nx, nz
        rad = angle * maths.PI / 180
        cosa = maths.cos(rad)
        sina = maths.sin(rad)
        nz = z * cosa - x * sina
        nx = z * sina + x * cosa
        return Point(nx, y, nz)

    func rotate_z(angle):
        var rad, cosa, sina, nx, ny
        rad = angle * maths.PI / 180
        cosa = maths.cos(rad)
        sina = maths.sin(rad)
        nx = x * cosa - y * sina
        ny = x * sina + y * cosa
        return Point(nx, ny, z)

    func project(width, height, fov, dist):
        var factor = fov / (dist + z)
        var nx = x * factor + width / 2
        var ny = y * factor + height / 2

        return Point(nx, ny, z)

var vertices = [
	Point(-1, 1, -1),
	Point(1, 1, -1),
	Point(1, -1, -1),
	Point(-1, -1, -1),
	Point(-1, 1, 1),
	Point(1, 1, 1),
	Point(1, -1, 1),
	Point(-1, -1, 1)
]

var faces  = [[0,1,2,3],[1,5,6,2],[5,4,7,6],[4,0,3,7],[0,4,5,1],[3,2,6,7]]
 
var colors = [[255,0,0],[0,255,0],[0,0,255],[255,255,0],[0,255,255],[255,0,255]]

var angle = 0

func loop():
    var t = []
    for v in vertices:
        var r = v.rotate_x(angle).rotate_y(angle)
        var p = r.project(400, 250, 200, 4)
        t.add(p)

    var avg_z = []

    class AvgZ(index, z):
        pass
    
    for i in range(0, faces.length):
        var f = faces[i]
        avg_z.add(AvgZ(i, (t[f[0]].z + t[f[1]].z + t[f[2]].z + t[f[3]].z) / 4.0))

    func sort_avg(a, b):
        return b.z - a.z
    avg_z.sort(sort_avg)

    for i in range(0, faces.length):
        var f = faces[avg_z[i].index]
        set_fill(colors[avg_z[i].index])
        poly(t[f[0]].x, t[f[0]].y, t[f[1]].x, t[f[1]].y, t[f[2]].x, t[f[2]].y, t[f[3]].x, t[f[3]].y)

    angle = angle + 0.05

draw(loop)

");

        }

        private void OnDraw(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;
            try
            {
                drawLoop.Call(Args.Empty);
            }
            catch (Exception ex)
            {
                
                throw;
            }

            Refresh();
        }


    
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new Program());
        }
        
        private static TAny Eval<TAny>(Runtime runtime, string source) where TAny : Any
        {
            var scanner = new Scanner(source);
            var parser = new Parser(runtime, scanner);
            var expr = parser.Parse();

            var func = expr.Compile();
            return (TAny) func();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Program
            // 
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.DoubleBuffered = true;
            this.Name = "Program";
            this.ResumeLayout(false);

        }
    }
}
