﻿using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class Vertex
    {
        public int Value;

        public Vertex(int val)
        {
            Value = val;
        }
    }

    public class SimpleGraph
    {
        public Vertex[] vertex;
        public int[,] m_adjacency;
        public int max_vertex;
        public int count;

        public SimpleGraph(int size)
        {
            max_vertex = size;
            m_adjacency = new int [size, size];
            vertex = new Vertex [size];
            count = 0;
        }

        public void AddVertex(int value)
        {
            if (count == max_vertex) return;

            Vertex newVertex = new Vertex(value);

            for (int i = 0; i < max_vertex; i++)
            {
                if (vertex[i] == null)
                {
                    vertex[i] = newVertex;
                    count++;

                    break;
                }
            }
        }

        public void RemoveVertex(int v)
        {
            if (InvalidIndex(v) || vertex[v] == null) return;
            vertex[v] = null;

            for (int i = 0; i < max_vertex; i++)
            {
                if (i == v) ClearVertexRow(v);
                else m_adjacency[i, v] = 0;
            }

            count--;
        }

        private void ClearVertexRow(int v)
        {
            for (int i = 0; i < max_vertex; i++)
            {
                m_adjacency[v, i] = 0;
            }
        }

        public bool IsEdge(int v1, int v2)
        {
            if (InvalidIndex(v1) || InvalidIndex(v2)) return false;
            if (vertex[v1] == null || vertex[v2] == null) return false;
            return m_adjacency[v1, v2] == 1;
        }

        public void AddEdge(int v1, int v2)
        {
            if (InvalidIndex(v1) || InvalidIndex(v2)) return;
            if (vertex[v1] == null || vertex[v2] == null) return;
            m_adjacency[v1, v2] = 1;
        }

        public void RemoveEdge(int v1, int v2)
        {
            if (InvalidIndex(v1) || InvalidIndex(v2)) return;
            if (vertex[v1] == null || vertex[v2] == null) return;
            m_adjacency[v1, v2] = 0;
        }

        private bool InvalidIndex(int v)
        {
            return v < 0 || v >= max_vertex;
        }
    }
}