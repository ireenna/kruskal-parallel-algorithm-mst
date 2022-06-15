using GraphCreator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCreator.Helper
{
    public static class GraphHelper
    {
        public static List<Vertex> GetLinkedVertexes(Project project, Vertex vertex, bool includeLocked = true, bool includeVisited = true, bool directionRestrict = true, bool sort = true)
        {
            var childVertexes = new List<Vertex>();
            foreach (var edge in project.Edges)
            {
                if (!edge.ContainVertex(vertex)) continue;
                var childVertex = edge.StartVertex == vertex ? edge.EndVertex : edge.StartVertex;

                if (!includeLocked && childVertex.IsLocked) continue;
                if (!includeVisited && childVertex.IsVisited) continue;

                childVertexes.Add(childVertex);
            }

            if (sort)
                childVertexes = childVertexes.OrderBy(v => v.Index).ToList();

            return childVertexes;
        }
        public static List<Vertex> GetLinkedVertexes(List<Edge> edges, Vertex vertex, bool includeLocked = true, bool includeVisited = true, bool directionRestrict = true, bool sort = true)
        {
            var childVertexes = new List<Vertex>();
            foreach (var edge in edges)
            {
                if (!edge.ContainVertex(vertex)) continue;
                var childVertex = edge.StartVertex == vertex ? edge.EndVertex : edge.StartVertex;

                if (!includeLocked && childVertex.IsLocked) continue;
                if (!includeVisited && childVertex.IsVisited) continue;

                childVertexes.Add(childVertex);
            }

            if (sort)
                childVertexes = childVertexes.OrderBy(v => v.Index).ToList();

            return childVertexes;
        }

        public static void ResetGraphMeta(Project project)
        {
            project.Vertexes.ForEach(v =>
            {
                v.IsLocked = false;
                v.IsVisited = false;
                v.Mark = 0;
            });

            project.Edges.ForEach(e =>
            {
                e.IsDenied = false;
            });
        }
        
        public static bool isCyclicUtil(Vertex v, List<Edge> edges, Vertex parent)
        {
            v.IsVisited = true;

            Vertex i;
            foreach (Vertex it in GetLinkedVertexes(edges, v))
            {
                i = it;
                
                if (!it.IsVisited)
                {
                    if (isCyclicUtil(i, edges, v))
                        return true;
                }
                else if (i != parent)
                    return true;
            }
            return false;
        }
        public static bool isCyclicUtil(Vertex v, List<Edge> edges, Vertex parent, List<Vertex> visitedList)
        {
            visitedList.Add(v);

            Vertex i;
            foreach (Vertex it in GetLinkedVertexes(edges, v))
            {
                i = it;
                
                if (!visitedList.Contains(it))
                {
                    if (isCyclicUtil(i, edges, v,visitedList))
                        return true;
                }
                
                else if (i != parent)
                    return true;
            }
            return false;
        }
        public static bool IsGraphSpanning(List<Vertex> vertexes, List<Edge>edges)
        {
            vertexes.ForEach(x => x.IsVisited = false);
            
            if (isCyclicUtil(vertexes.First(), edges, null))
                return false;
            
            if (vertexes.Any(x => !x.IsVisited))
            {
                return false;
            }
            return true;
        }
        public static bool isConnectedUtil(Vertex v, Project project, Vertex parent)
        {
            v.IsVisited = true;

            Vertex i;
            foreach (Vertex it in GetLinkedVertexes(project, v))
            {
                i = it;
                
                if (!it.IsVisited)
                {
                    if (isConnectedUtil(i, project, v))
                        return true;
                }
            }
            return false;
        }
        public static bool IsGraphConnected(Project project)
        {
            project.Vertexes.ForEach(x => x.IsVisited = false);
            
            if (isConnectedUtil(project.Vertexes.First(), project, null))
                return false;
            
            if (project.Vertexes.Any(x => !x.IsVisited))
            {
                return false;
            }
            return true;
        }

        public static void ClearStyle(Project project)
        {
            project.Edges.ForEach(e => e.Style.SetDefault());
            project.Vertexes.ForEach(e => e.Style.SetDefault());
        }
    }
}
