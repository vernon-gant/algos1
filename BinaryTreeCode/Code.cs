using System;
using System.Collections.Generic;

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
        BSTNode<T> Root; // корень дерева, или null

        public BST(BSTNode<T> node)
        {
            Root = node;
        }

        public BSTFind<T> FindNodeByKey(int key)
        {
            return findNodeByKey(key, Root, Root.Parent);
        }

        private BSTFind<T> findNodeByKey(int key, BSTNode<T>? currentNode, BSTNode<T> parentNode)
        {
            if (currentNode == null)
                return new BSTFind<T> { Node = parentNode, NodeHasKey = false, ToLeft = parentNode.NodeKey > key };

            if (currentNode.NodeKey == key)
                return new BSTFind<T> { Node = currentNode, NodeHasKey = true, };

            if (key < currentNode.NodeKey)
                return findNodeByKey(key, currentNode.LeftChild, currentNode.Parent);

            return findNodeByKey(key, currentNode.RightChild, currentNode.Parent);
        }

        public bool AddKeyValue(int key, T val)
        {
            // добавляем ключ-значение в дерево
            return false; // если ключ уже есть
        }

        public BSTNode<T> FinMinMax(BSTNode<T> FromNode, bool FindMax)
        {
            // ищем максимальный/минимальный ключ в поддереве
            return null;
        }

        public bool DeleteNodeByKey(int key)
        {
            // удаляем узел по ключу
            return false; // если узел не найден
        }

        public int Count()
        {
            return 0; // количество узлов в дереве
        }
    }
}