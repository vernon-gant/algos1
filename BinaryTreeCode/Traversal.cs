using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class BSTNode
    {
        public int NodeKey;
        public BSTNode Parent;
        public BSTNode LeftChild;
        public BSTNode RightChild;

        public BSTNode(int key, BSTNode parent)
        {
            NodeKey = key;
            Parent = parent;
            LeftChild = null;
            RightChild = null;
        }
    }

    public class DummyNode : BSTNode
    {
        public DummyNode(BSTNode root) : base(0, null)
        {
            RightChild = root;
        }
    }


    public class BSTFind
    {
        // null если в дереве вообще нету узлов
        public BSTNode Node;

        // true если узел найден
        public bool NodeHasKey;

        // true, если родительскому узлу надо добавить новый левым
        public bool ToLeft;

        public BSTFind()
        {
            Node = null;
        }
    }

    public class BST
    {
        BSTNode Root;

        public BST(BSTNode node)
        {
            Root = node;
        }

        public BSTFind FindNodeByKey(int key)
        {
            return findNodeByKey(key, Root, null);
        }

        private BSTFind findNodeByKey(int key, BSTNode currentNode, BSTNode parentNode)
        {
            if (currentNode == null)
                return new BSTFind { Node = parentNode, NodeHasKey = false, ToLeft = parentNode != null && parentNode.NodeKey > key };

            if (currentNode.NodeKey == key)
                return new BSTFind { Node = currentNode, NodeHasKey = true };

            if (key < currentNode.NodeKey)
                return findNodeByKey(key, currentNode.LeftChild, currentNode);

            return findNodeByKey(key, currentNode.RightChild, currentNode);
        }

        public bool AddKey(int key)
        {
            var foundNode = FindNodeByKey(key);

            if (foundNode.NodeHasKey)
                return false;

            var newNode = new BSTNode(key, foundNode.Node);

            if (foundNode.Node == null)
                Root = newNode;
            else if (foundNode.ToLeft)
                foundNode.Node.LeftChild = newNode;
            else
                foundNode.Node.RightChild = newNode;

            return true;
        }

        public BSTNode FinMinMax(BSTNode FromNode, bool FindMax)
        {
            return finMinMax(FromNode, FromNode, FindMax);
        }

        private BSTNode finMinMax(BSTNode FromNode, BSTNode ParentNode, bool FindMax)
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

            if (nodeToDelete == Root) nodeToDelete.Parent = new DummyNode(nodeToDelete);

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

                minNode.Parent = nodeToDelete.Parent is DummyNode ? null : nodeToDelete.Parent;
                minNode.LeftChild = nodeToDelete.LeftChild;
                if (minNode.LeftChild != null) minNode.LeftChild.Parent = minNode;
                minNode.RightChild ??= nodeToDelete.RightChild;
                if (minNode.RightChild != null) minNode.RightChild.Parent = minNode;
            }

            if (nodeToDelete.Parent is DummyNode) Root = nodeToDelete.Parent.RightChild;

            return true;
        }

        public int Count()
        {
            return count(Root);
        }

        private int count(BSTNode node)
        {
            if (node == null) return 0;

            return 1 + count(node.RightChild) + count(node.LeftChild);
        }

        public List<BSTNode> DeepAllNodes(int mode)
        {
            return mode switch
            {
                0 => InOrderRecursion(Root),
                1 => PostOrderRecursion(Root),
                _ => PreOrderRecursion(Root)
            };
        }

        public List<BSTNode> DeepAllNodesStack(int mode)
        {
            return mode switch
            {
                0 => InOrderStack(),
                1 => PostOrderStack(),
                _ => PreOrderStack()
            };
        }

        private List<BSTNode> InOrderRecursion(BSTNode currentNode)
        {
            if (currentNode == null) return new List<BSTNode>();

            var nodeList = new List<BSTNode>();
            nodeList.AddRange(InOrderRecursion(currentNode.LeftChild));
            nodeList.Add(currentNode);
            nodeList.AddRange(InOrderRecursion(currentNode.RightChild));

            return nodeList;
        }

        private List<BSTNode> InOrderStack()
        {
            var nodeList = new List<BSTNode>();
            var stack = new Stack<BSTNode>();
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

        private List<BSTNode> PostOrderRecursion(BSTNode currentNode)
        {
            if (currentNode == null) return new List<BSTNode>();

            var nodeList = new List<BSTNode>();
            nodeList.AddRange(PostOrderRecursion(currentNode.LeftChild));
            nodeList.AddRange(PostOrderRecursion(currentNode.RightChild));
            nodeList.Add(currentNode);

            return nodeList;
        }

        private List<BSTNode> PostOrderStack()
        {
            var nodeList = new List<BSTNode>();
            var stack = new Stack<BSTNode>();
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

                if (stack.Count == 0) break;

                currentNode = stack.Peek().RightChild != currentNode ? stack.Peek().RightChild : null;
            }

            return nodeList;
        }

        private List<BSTNode> PreOrderRecursion(BSTNode currentNode)
        {
            if (currentNode == null) return new List<BSTNode>();

            var nodeList = new List<BSTNode>();
            nodeList.Add(currentNode);
            nodeList.AddRange(PreOrderRecursion(currentNode.LeftChild));
            nodeList.AddRange(PreOrderRecursion(currentNode.RightChild));

            return nodeList;
        }

        private List<BSTNode> PreOrderStack()
        {
            var nodeList = new List<BSTNode>();
            var stack = new Stack<BSTNode>();
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

        public List<BSTNode> WideAllNodes()
        {
            var nodesList = new List<BSTNode>();
            var deque = new LinkedList<BSTNode>();
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