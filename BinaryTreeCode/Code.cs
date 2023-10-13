using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace AlgorithmsDataStructures2
{
    public class BSTNode<T>
    {
        public int NodeKey;
        public T NodeValue;
        public BSTNode<T> Parent;
        public BSTNode<T> LeftChild;
        public BSTNode<T> RightChild;

        public BSTNode(int key, T val, BSTNode<T> parent)
        {
            NodeKey = key;
            NodeValue = val;
            Parent = parent;
            LeftChild = null;
            RightChild = null;
        }
    }

    public class DummyNode<T> : BSTNode<T>
    {
        public DummyNode(BSTNode<T> root) : base(0, default(T), null)
        {
            RightChild = root;
        }
    }


    public class BSTFind<T>
    {
        // null если в дереве вообще нету узлов
        public BSTNode<T> Node;

        // true если узел найден
        public bool NodeHasKey;

        // true, если родительскому узлу надо добавить новый левым
        public bool ToLeft;

        public BSTFind()
        {
            Node = null;
        }
    }

    public class BST<T>
    {
        BSTNode<T> Root;

        public BST(BSTNode<T> node)
        {
            Root = node;
        }

        public BSTFind<T> FindNodeByKey(int key)
        {
            return findNodeByKey(key, Root, null);
        }

        private BSTFind<T> findNodeByKey(int key, BSTNode<T> currentNode, BSTNode<T> parentNode)
        {
            if (currentNode == null)
                return new BSTFind<T> { Node = parentNode, NodeHasKey = false, ToLeft = parentNode != null && parentNode.NodeKey > key };

            if (currentNode.NodeKey == key)
                return new BSTFind<T> { Node = currentNode, NodeHasKey = true };

            if (key < currentNode.NodeKey)
                return findNodeByKey(key, currentNode.LeftChild, currentNode);

            return findNodeByKey(key, currentNode.RightChild, currentNode);
        }

        public bool AddKeyValue(int key, T val)
        {
            var foundNode = FindNodeByKey(key);

            if (foundNode.NodeHasKey)
                return false;

            var newNode = new BSTNode<T>(key, val, foundNode.Node);

            if (foundNode.Node == null)
                Root = newNode;
            else if (foundNode.ToLeft)
                foundNode.Node.LeftChild = newNode;
            else
                foundNode.Node.RightChild = newNode;

            return true;
        }

        public BSTNode<T> FinMinMax(BSTNode<T> FromNode, bool FindMax)
        {
            return finMinMax(FromNode, FromNode, FindMax);
        }

        private BSTNode<T> finMinMax(BSTNode<T> FromNode, BSTNode<T> ParentNode, bool FindMax)
        {
            if (FromNode == null)
                return ParentNode;

            return finMinMax(FindMax ? FromNode.RightChild : FromNode.LeftChild, FromNode, FindMax);
        }

        public bool DeleteNodeByKey(int key)
        {
            var foundNode = FindNodeByKey(key);

            if (!foundNode.NodeHasKey) return false;

            var nodeToDelete = foundNode.Node;

            if (nodeToDelete == Root) nodeToDelete.Parent = new DummyNode<T>(nodeToDelete);

            if (nodeToDelete.RightChild == null && nodeToDelete.LeftChild == null)
            {
                nodeToDelete.Parent.LeftChild = nodeToDelete == nodeToDelete.Parent.LeftChild ? null : nodeToDelete.Parent.LeftChild;
                nodeToDelete.Parent.RightChild = nodeToDelete == nodeToDelete.Parent.RightChild ? null : nodeToDelete.Parent.RightChild;
            }
            else if (nodeToDelete.RightChild != null ^ nodeToDelete.LeftChild != null)
            {
                var childNode = nodeToDelete.RightChild ?? nodeToDelete.LeftChild;
                var parentNode = nodeToDelete.Parent;

                parentNode.RightChild = nodeToDelete == parentNode.RightChild ? childNode : parentNode.RightChild;
                parentNode.LeftChild = nodeToDelete == parentNode.LeftChild ? childNode : parentNode.LeftChild;
                childNode.Parent = parentNode;
            }
            else
            {
                var minNode = FinMinMax(nodeToDelete.RightChild, false);

                minNode.Parent.LeftChild = minNode == minNode.Parent.LeftChild ? null : minNode.Parent.LeftChild;
                minNode.Parent.RightChild = minNode == minNode.Parent.RightChild ? null : minNode.Parent.RightChild;

                nodeToDelete.Parent.RightChild = nodeToDelete == nodeToDelete.Parent.RightChild ? minNode : nodeToDelete.Parent.RightChild;
                nodeToDelete.Parent.LeftChild = nodeToDelete == nodeToDelete.Parent.LeftChild ? minNode : nodeToDelete.Parent.LeftChild;

                minNode.Parent = nodeToDelete.Parent is DummyNode<T> ? null : nodeToDelete.Parent;
                minNode.LeftChild = nodeToDelete.LeftChild;
                if (minNode.LeftChild != null) minNode.LeftChild.Parent = minNode;
                minNode.RightChild ??= nodeToDelete.RightChild;
                if (minNode.RightChild != null) minNode.RightChild.Parent = minNode;
            }

            if (nodeToDelete.Parent is DummyNode<T>) Root = nodeToDelete.Parent.RightChild;

            return true;
        }

        public int Count()
        {
            return count(Root);
        }

        private int count(BSTNode<T> node)
        {
            if (node == null) return 0;

            return 1 + count(node.RightChild) + count(node.LeftChild);
        }

        public List<BSTNode<T>> DeepAllNodes(int mode)
        {
            return mode switch
            {
                0 => InOrderRecursion(Root),
                1 => PostOrderRecursion(Root),
                _ => PreOrderRecursion(Root)
            };
        }

        public List<BSTNode<T>> DeepAllNodesStack(int mode)
        {
            return mode switch
            {
                0 => InOrderStack(),
                1 => PostOrderStack(),
                _ => PreOrderStack()
            };
        }

        private List<BSTNode<T>> InOrderRecursion(BSTNode<T> currentNode)
        {
            if (currentNode == null) return new List<BSTNode<T>>();

            var nodeList = new List<BSTNode<T>>();
            nodeList.AddRange(InOrderRecursion(currentNode.LeftChild));
            nodeList.Add(currentNode);
            nodeList.AddRange(InOrderRecursion(currentNode.RightChild));

            return nodeList;
        }

        private List<BSTNode<T>> InOrderStack()
        {
            var nodeList = new List<BSTNode<T>>();
            var stack = new Stack<BSTNode<T>>();
            var currentNode = Root;

            while (currentNode != null || stack.Count > 0)
            {
                while (currentNode != null)
                {
                    stack.Push(currentNode);
                    currentNode = currentNode.LeftChild;
                }

                currentNode = stack.Pop();
                nodeList.Add(currentNode);

                currentNode = currentNode.RightChild;
            }

            return nodeList;
        }

        private List<BSTNode<T>> PostOrderRecursion(BSTNode<T> currentNode)
        {
            if (currentNode == null) return new List<BSTNode<T>>();

            var nodeList = new List<BSTNode<T>>();
            nodeList.AddRange(PostOrderRecursion(currentNode.LeftChild));
            nodeList.AddRange(PostOrderRecursion(currentNode.RightChild));
            nodeList.Add(currentNode);

            return nodeList;
        }

        private List<BSTNode<T>> PostOrderStack()
        {
            var nodeList = new List<BSTNode<T>>();
            var stack = new Stack<BSTNode<T>>();
            var currentNode = Root;
            BSTNode<T> currentParent = null;

            while (currentNode != null || stack.Count > 0)
            {
                while (currentNode != null)
                {
                    stack.Push(currentNode);
                    currentNode = currentNode.LeftChild;
                }

                currentNode = stack.Pop();
                nodeList.Add(currentNode);

                if (stack.Count == 0) break;
                currentNode = stack.Peek().RightChild != currentNode ? stack.Peek().RightChild : null;
            }

            return nodeList;
        }

        private List<BSTNode<T>> PreOrderRecursion(BSTNode<T> currentNode)
        {
            if (currentNode == null) return new List<BSTNode<T>>();

            var nodeList = new List<BSTNode<T>>();
            nodeList.Add(currentNode);
            nodeList.AddRange(PreOrderRecursion(currentNode.LeftChild));
            nodeList.AddRange(PreOrderRecursion(currentNode.RightChild));

            return nodeList;
        }

        private List<BSTNode<T>> PreOrderStack()
        {
            var nodeList = new List<BSTNode<T>>();
            var stack = new Stack<BSTNode<T>>();
            var currentNode = Root;

            while (currentNode != null || stack.Count > 0)
            {
                while (currentNode != null)
                {
                    nodeList.Add(currentNode);
                    stack.Push(currentNode.RightChild);
                    currentNode = currentNode.LeftChild;
                }

                currentNode = stack.Pop();
            }

            return nodeList;
        }

        public List<BSTNode<T>> WideAllNodes()
        {
            var nodesList = new List<BSTNode<T>>();
            var deque = new LinkedList<BSTNode<T>>();
            deque.AddFirst(Root);

            while (deque.Count > 0)
            {
                var currentNode = deque.First!.Value;
                deque.RemoveFirst();
                nodesList.Add(currentNode);
                if (currentNode.LeftChild != null) deque.AddLast(currentNode.LeftChild);
                if (currentNode.RightChild != null) deque.AddLast(currentNode.RightChild);
            }

            return nodesList;
        }
    }
}