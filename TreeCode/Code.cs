using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class SimpleTreeNode<T>
    {
        public T NodeValue;
        public SimpleTreeNode<T> Parent;
        public List<SimpleTreeNode<T>> Children;

        public SimpleTreeNode(T val, SimpleTreeNode<T> parent)
        {
            NodeValue = val;
            Parent = parent;
            Children = null;
        }
    }

    public class SimpleTree<T>
    {
        public SimpleTreeNode<T> Root;

        public SimpleTree(SimpleTreeNode<T> root)
        {
            Root = root;
        }

        private bool Equal(T v1, T v2)
        {
            if (typeof(T) == typeof(String))
            {
                var str1 = v1 as string;
                var str2 = v2 as string;
                str1 = str1?.Trim();
                str2 = str2?.Trim();

                return String.Compare(str1, str2) == 0;
            }
            var int1 = (int)(object)v1;
            var int2 = (int)(object)v2;

            return int1 == int2;
        }

        public void AddChild(SimpleTreeNode<T> ParentNode, SimpleTreeNode<T> NewChild)
        {
            NewChild.Parent = ParentNode;
            ParentNode.Children ??= new List<SimpleTreeNode<T>>();
            ParentNode.Children.Add(NewChild);
        }

        public void DeleteNode(SimpleTreeNode<T> NodeToDelete)
        {
            NodeToDelete.Parent.Children.Remove(NodeToDelete);
        }

        public List<SimpleTreeNode<T>> GetAllNodes()
        {
            return getAllNodes(Root);
        }

        private List<SimpleTreeNode<T>> getAllNodes(SimpleTreeNode<T> parent)
        {
            var allNodesList = new List<SimpleTreeNode<T>>();

            if (parent.Children != null)
                foreach (var child in parent.Children)
                {
                    allNodesList.AddRange(getAllNodes(child));
                }

            allNodesList.Add(parent);

            return allNodesList;
        }

        public List<SimpleTreeNode<T>> FindNodesByValue(T val)
        {
            return findNodesByValue(val, Root);
        }

        private List<SimpleTreeNode<T>> findNodesByValue(T value, SimpleTreeNode<T> node)
        {
            var sameValueNodes = new List<SimpleTreeNode<T>>();

            if (Equal(value,node.NodeValue))
                sameValueNodes.Add(node);

            if (node.Children != null)
                foreach (var child in node.Children)
                {
                    sameValueNodes.AddRange(findNodesByValue(value,child));
                }

            return sameValueNodes;
        }

        public void MoveNode(SimpleTreeNode<T> OriginalNode, SimpleTreeNode<T> NewParent)
        {
            DeleteNode(OriginalNode);
            OriginalNode.Parent = NewParent;
            NewParent.Children ??= new List<SimpleTreeNode<T>>();
            NewParent.Children.Add(OriginalNode);
        }

        public int Count()
        {
            return count(Root);
        }

        private int count(SimpleTreeNode<T> node)
        {
            int counter = 1;

            if (node.Children != null)
                foreach (var child in node.Children)
                {
                    counter += count(child);
                }

            return counter;
        }

        public int LeafCount()
        {
            return leafCount(Root);
        }

        private int leafCount(SimpleTreeNode<T> node)
        {
            if (node.Children == null)
                return 1;

            int counter = 0;

            foreach (var child in node.Children)
            {
                counter += leafCount(child);
            }

            return counter;
        }

    }

}