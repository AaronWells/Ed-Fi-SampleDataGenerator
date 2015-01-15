﻿using System;

namespace EdFi.SampleDataGenerator.Utility
{
    using System.Collections.Generic;
    using System.Linq;

    public class GraphCycleException : Exception { }

    internal enum VertexColor
    {
        /// <summary>
        /// Not visited
        /// </summary>
        White,
        /// <summary>
        /// Currently being visited
        /// </summary>
        Grey,
        /// <summary>
        /// Already visited
        /// </summary>
        Black
    }

    public class DirectedGraph<TKey> where TKey : IComparable
    {
        /// <summary>
        /// Internal list of vertex keys
        /// </summary>
        private readonly List<TKey> vertices;

        /// <summary>
        /// Internal list of edge definitions
        /// </summary>
        private readonly List<Tuple<TKey, TKey>> edges = new List<Tuple<TKey, TKey>>();

        /// <summary>
        /// Directed Graph Constructor
        /// </summary>
        /// <param name="keys">all the vertices in the graph with duplicates removed</param>
        public DirectedGraph(IEnumerable<TKey> keys)
        {
            vertices = new List<TKey>(keys.Distinct());
            vertices.Sort();
        }

        /// <summary>
        /// Set the downstream dependencies on a given vertex
        /// </summary>
        /// <param name="key">the vertex</param>
        /// <param name="keys">the vertices on which the vertex depends</param>
        public void SetDependencies(TKey key, IEnumerable<TKey> keys)
        {
            if (!this.vertices.Contains(key)) return;
            foreach (var tmpKey in keys.Where(
                tmpKey => this.vertices.Contains(tmpKey)
                && !this.edges.Exists(x => x.Item1.CompareTo(key) == 0 && x.Item2.CompareTo(tmpKey) == 0)))
            {
                this.edges.Add(new Tuple<TKey, TKey>(key, tmpKey));
            }
        }

        /// <summary>
        /// Return all keys in an evaluation order where all dependencies are satisfied
        /// </summary>
        /// <returns>an ordered list of vertices</returns>
        public IEnumerable<TKey> GetEvaluationOrder()
        {
            var visited = new VertexColor[vertices.Count];
            var dependencies = new List<TKey>();
            foreach (var vertex in vertices)
            {
                DepthFirstSearch(vertex, visited, dependencies);
            }
            return dependencies;
        }

        /// <summary>
        /// Classic depth first search algorithm
        /// </summary>
        /// <param name="vertex">the vertex being evaluated</param>
        /// <param name="vertexColors">an array of vertex colors</param>
        /// <param name="evaluationOrder">the order of evaluation</param>
        private void DepthFirstSearch(TKey vertex, IList<VertexColor> vertexColors, ICollection<TKey> evaluationOrder)
        {
            var idx = vertices.FindIndex(v => v.CompareTo(vertex) == 0);
            switch (vertexColors[idx])
            {
                case VertexColor.White:
                    // not yet visited
                    vertexColors[idx] = VertexColor.Grey;
                    var tmpEdges = edges.Where(e => e.Item1.CompareTo(vertex) == 0);
                    foreach (var tuple in tmpEdges)
                    {
                        this.DepthFirstSearch(tuple.Item2, vertexColors, evaluationOrder);
                    }
                    evaluationOrder.Add(vertex);
                    vertexColors[idx] = VertexColor.Black;
                    break;
                case VertexColor.Grey:
                    // currently visiting
                    throw new GraphCycleException();
                case VertexColor.Black:
                    // already evaluated, do nothing
                    break;
            }
        }
    }
}
