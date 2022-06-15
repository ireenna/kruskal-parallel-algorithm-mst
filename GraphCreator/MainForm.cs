using GraphCreator.Drawer;
using GraphCreator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphCreator.Algorithm;
using System.Windows.Threading;
using GraphCreator.Helper;

namespace GraphCreator
{
    public partial class MainForm : Form
    {

        private Project Project { get; set; }
        private DrawerManager DrawerManager { get; set; }

        private void InitCore()
        {
            Project = new Project();
            
            DrawerManager = new DrawerManager(Project, drawPanel);
            DrawerManager.StartAutoDraw();

        }

        public MainForm()
        {
            InitializeComponent();
            InitCore();
        }

        private void GenerateRandomGraph(int vertexCount)
        {
            Random rand = new Random();
            int generatePosition()
            {
                return rand.Next(0, 600);
            }
            int generateWeight()
            {
                return rand.Next(1, 20);
            }

            for (int i = 1; i <= vertexCount; i++)
            {
                var v = new Vertex(i, new Pos(generatePosition(), generatePosition()));
                Project.Vertexes.Add(v);
            }

            foreach (var vertex1 in Project.Vertexes)
            {

                for (int i = 0; i < rand.Next(1, 4); i++)
                {
                    var vertex2 = Project.Vertexes.Where(x => x != vertex1).ToList()[rand.Next(0, Project.Vertexes.Count - 1)];
                    if (!Project.Edges.Any(x => (x.StartVertex == vertex1 && x.EndVertex == vertex2) || (x.StartVertex == vertex2 && x.EndVertex == vertex1)))
                        Project.Edges.Add(new Edge(vertex1, vertex2) { Weight = generateWeight() });
                }

            }

            while (!GraphHelper.IsGraphConnected(Project))
            {
                var notConnected = Project.Vertexes.Where(x => !x.IsVisited).ToList();
                var notConnectedItem = notConnected[rand.Next(0, notConnected.Count - 1)];
                var connected = Project.Vertexes.Where(x => x.IsVisited).ToList();
                for (int i = 0; i < rand.Next(1, 4); i++)
                {
                    var connectedItem = connected[rand.Next(0, connected.Count - 1)];
                    if (!Project.Edges.Any(x => (x.StartVertex == connectedItem && x.EndVertex == notConnectedItem) || (x.StartVertex == notConnectedItem && x.EndVertex == connectedItem)))
                        Project.Edges.Add(new Edge(connectedItem, notConnectedItem) { Weight = generateWeight() });
                }
            }
            //var v1 = new Vertex(1, new Pos(20, 20));
            //var v2 = new Vertex(2, new Pos(100, 20));
            //var v3 = new Vertex(3, new Pos(20, 200));
            //var v4 = new Vertex(4, new Pos(100, 200));
            //var v5 = new Vertex(5, new Pos(50, 200));
            //var v6 = new Vertex(6, new Pos(100, 70));

            //Project.Vertexes.Add(v1);
            //Project.Vertexes.Add(v2);
            //Project.Vertexes.Add(v3);
            //Project.Vertexes.Add(v4);
            //Project.Vertexes.Add(v5);
            //Project.Vertexes.Add(v6);

            //Project.Edges.Add(new Edge(v1, v2) { Weight = 7 });
            //Project.Edges.Add(new Edge(v1, v3) { Weight = 9 });
            //Project.Edges.Add(new Edge(v1, v6) { Weight = 14 });
            //Project.Edges.Add(new Edge(v3, v6) { Weight = 2 });
            //Project.Edges.Add(new Edge(v3, v2) { Weight = 10 });
            //Project.Edges.Add(new Edge(v3, v4) { Weight = 11 });
            //Project.Edges.Add(new Edge(v2, v4) { Weight = 15 });
            //Project.Edges.Add(new Edge(v4, v5) { Weight = 6 });
            //Project.Edges.Add(new Edge(v5, v6) { Weight = 9 });
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GenerateRandomGraph(Convert.ToInt32(textBox1.Text));
            AlgorithmManager algorithmManager = new AlgorithmManager(Project,true);
            label1.Hide();
            label2.Hide();
            button1.Hide();
            button2.Hide();
            panel1.Hide();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GenerateRandomGraph(Convert.ToInt32(textBox1.Text));
            AlgorithmManager algorithmManager = new AlgorithmManager(Project, false);
            label1.Hide();
            label2.Hide();
            button1.Hide();
            button2.Hide();
            textBox1.Hide();
            panel1.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void drawPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
