using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SomeTree
{
    //This class represents the data based on the tree structure.

    public class BSTree<T> where T : IComparable<T>, ICloneable
    {
        public delegate void TreeDel(T item);
        Node root = null;
        Node newlroot = null;
        Node newrroot = null;
        Node temp = null;

        private class Node
        {
            public Node parent = null;
            public T item = default(T);
            public Node left = null;
            public Node right = null;
            public uint lCount = 0;
            public uint rCount = 0;

            public Node(Node par, T item)
            {
                this.item = item;
                this.parent = par;
            }
        }

        protected virtual int compare(T item1, T item2)
        {
            ///if (Equals(item1, item2)) return 0;
            return item1.CompareTo(item2);
        }

        public void insert(T item)
        {
            if (Equals(item, null)) return;
            if (root == null) { root = new Node(null, item); return; }
            this.insert(null, this.root, item);
        }

        void insert(Node basenode, Node node, T item)
        {

            if (basenode != null) item = basenode.item;
            int equal = this.compare(item, node.item);

            if (equal == 0) return;
            else if (equal > 0)
            {
                if (node.right != null)
                {
                    if (node.parent != null) node.parent.rCount += (basenode != null) ? (1 + basenode.lCount + basenode.rCount) : 1;
                    insert(null, node.right, item);
                }
                if (basenode != null) node.right = basenode;
                else node.right = new Node(node, item);
                node.rCount += (basenode != null) ? (1 + basenode.lCount + basenode.rCount) : 1;
                return;
            }
            else if (equal < 0)
            {
                if (node.left != null)
                {
                    if (node.parent != null) node.parent.lCount += (basenode != null) ? (1 + basenode.lCount + basenode.rCount) : 1;
                    insert(null, node.left, item);
                }
                if (basenode != null) node.left = basenode;
                else node.left = new Node(node, item);
                node.lCount += (basenode != null) ? (1 + basenode.lCount + basenode.rCount) : 1;
                return;
            }



        }

        public void traverse(TreeDel meth)
        {
            traverse(this.root, meth);
        }

        void traverse(Node node, TreeDel f)
        {
            if (node == null) return;
            if (f != null) f((T)node.item.Clone());

            if (node.left != null)
                traverse(node.left, f);
            if (node.right != null)
                traverse(node.right, f);
        }



        public T search(T item)
        {
            return search(this.root, item);
        }

        T search(Node node, T item)
        {
            bool equal = Equals(node.item, item);
            if (equal) return node.item;
            else if (!equal) return search(node.left, item);
            else if (!equal) return search(node.right, item);
            else return default(T);
        }


    }

    class Demo
    {
        public static void Print(string v)
        {
            Console.WriteLine("{0}", v);
        }

        static void Main()
        {



        }
    }




}
