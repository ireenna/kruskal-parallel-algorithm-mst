using GraphCreator.Helper;
using GraphCreator.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace GraphCreator.Algorithm.SimpleAlgorithms
{
    class KruskalSpanningTree
    {
        public Project project { get; set; }
        public bool IsSync { get; set; }
        public KruskalSpanningTree(Project project, bool isSync)
        {
            this.project = project;
            this.IsSync = isSync;
        }
        public virtual async Task Run()
        {
            
            Edge[] projectEdges = project.Edges.ToArray();
            Edge[] projectEdgesOrdered = project.Edges.OrderBy(x=>x.Weight).ToArray();
            var threadsNum = 3;
            bool isSpanning = false;

            var t2 = new Stopwatch();
            t2.Start();
            projectEdges.First(x => !x.IsDenied).IsSpanning=true;

            if (IsSync)
            {
                for (int i = 0; i < projectEdgesOrdered.Length; i++)
                {
                    var edge = projectEdgesOrdered[i];
                    if (!edge.IsSpanning)
                    {
                        Edge[] tempEdgeTree = projectEdgesOrdered.Where(x => x.IsSpanning).ToArray();
                        var isNormal = CheckOnCycle(edge, tempEdgeTree);

                        if (isNormal)
                        {
                            edge.IsSpanning = true;
                        }
                    }
                }
            }
            else
            {
                var edgesDividedForThreads = project.Edges.Partition(threadsNum).Select(x => x.ToArray()).ToArray();

                    foreach (var edgeGroup in edgesDividedForThreads)
                    {
                        Thread backgroundThread = new Thread(() =>
                        {
                            while (!isSpanning)
                            {
                                Edge[] spanningEdges = projectEdges.Where(x => x.IsSpanning).ToArray();
                                List<Edge> notVisitedEdges = edgeGroup.Where(x => !x.IsSpanning && !x.IsDenied && !x.IsDeniedByMain).ToList();

                                if (!notVisitedEdges.Any())
                                {
                                    Thread.CurrentThread.Abort();
                                }

                                for (int i = 0; i < notVisitedEdges.Count; i++)
                                {
                                    lock (notVisitedEdges)
                                    {
                                        var edge = notVisitedEdges[i];
                                        var isNormal = CheckOnCycle(edge, spanningEdges);
                                        if (!isNormal)
                                        {
                                            edge.IsDenied = true;
                                        }
                                    }
                                }
                            }
                        });
                        backgroundThread.Start();
                    }


                


                Thread mainThread = new Thread(() =>
                {
                    for (int i = 0; i < projectEdgesOrdered.Length; i++)
                    {
                        lock (projectEdgesOrdered[i])
                        {
                            var edge = projectEdgesOrdered[i];
                            if (!edge.IsSpanning && !edge.IsDenied && !edge.IsDeniedByMain)
                            {
                                Edge[] tempEdgeTree = projectEdgesOrdered.Where(x => x.IsSpanning).ToArray();
                                var isNormal = CheckOnCycle(edge, tempEdgeTree);

                                if (isNormal)
                                {
                                    edge.IsSpanning = true;
                                    if (!projectEdgesOrdered.Any(x => !x.IsDenied && !x.IsSpanning && !x.IsDeniedByMain))
                                    {
                                        isSpanning = true;
                                        Thread.CurrentThread.Abort();
                                    }
                                }
                                else
                                {
                                    edge.IsDeniedByMain = true;
                                }
                            }
                        }

                    }

                });
                mainThread.Start();
                mainThread.Join();
            }
            t2.Stop();

            if (project.Edges.Count(x=>x.IsSpanning) != project.Vertexes.Count - 1)
            {
                throw new Exception("There are not all vertexes binded.");
            }

            var edges = projectEdges.Where(x => x.IsSpanning).ToList();
            var vertexes = edges.Select(x => x.StartVertex).Union(edges.Select(x => x.EndVertex)).ToList();
            if (!GraphHelper.IsGraphSpanning(vertexes,edges))
            {
                throw new Exception("The graph is not spanning.");
            }
            Console.WriteLine("Time !parallel: " + t2.ElapsedMilliseconds);


            

            foreach (var sEdge in project.Edges.Where(x=>x.IsSpanning))
            {
                sEdge.Style.LineWidth = 2;
                sEdge.Style.LineColor = Color.Black;
            }

            MessageBox.Show("Час виконання алгоритму: " + t2.ElapsedMilliseconds);

        }
        public static bool CheckOnCycle(Edge edgeToAdd, Edge[] tempEdgeTree)
        {
            List<Edge> tempEdgeTree1 = new List<Edge>(tempEdgeTree);
            tempEdgeTree1.Add(edgeToAdd);
            if (GraphHelper.isCyclicUtil(edgeToAdd.EndVertex, tempEdgeTree1, null, new List<Vertex>()))
                return false;

            if (GraphHelper.isCyclicUtil(edgeToAdd.StartVertex, tempEdgeTree1, null, new List<Vertex>()))
                return false;

            return true;

        }
    }
    public static class ListExtensions
    {
        public static List<T>[] Partition<T>(this List<T> list, int totalPartitions)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (totalPartitions < 1)
                throw new ArgumentOutOfRangeException("totalPartitions");

            List<T>[] partitions = new List<T>[totalPartitions];

            int maxSize = (int)Math.Ceiling(list.Count / (double)totalPartitions);
            int k = 0;

            for (int i = 0; i < partitions.Length; i++)
            {
                partitions[i] = new List<T>();
                for (int j = k; j < k + maxSize; j++)
                {
                    if (j >= list.Count)
                        break;
                    partitions[i].Add(list[j]);
                }
                k += maxSize;
            }

            return partitions;
        }
    }
}
