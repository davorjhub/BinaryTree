using System;

namespace BinayTreeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            BinaryTree bt = new BinaryTree();

            bt.Add(34);
            bt.Add(28);
            bt.Add(45);
            bt.Add(15);
            bt.Add(8);
            bt.Add(55);
            bt.Add(80);
            bt.Add(50);
            bt.Add(4);
            bt.Add(3);
            bt.Add(10);
            bt.Add(20);

            Node node = bt.Find(20);

            int depth = bt.GetTreeDepth();

            Console.WriteLine("Depth: " + depth);

            Console.WriteLine("PreOrder Traversal");
            bt.TraversePreOrder(bt.Root);
            Console.WriteLine();

            Console.WriteLine("InOrder Traversal");
            bt.TraverseInOrder(bt.Root);
            Console.WriteLine();

            Console.WriteLine("PostOrder Traversal");
            bt.TraversePostOrder(bt.Root);
            Console.WriteLine();
        }
    }

    public class Node
    {
        public Node LeftNode { get; set; }
        public Node RigthNode { get; set; }
        public int Data { get; set; }
    }

    public class BinaryTree
    {
        public Node Root { get; set; }

        //Add a new node
        public bool Add(int value)
        {
            Node before = null, after = this.Root;

            while(after != null)
            {
                before = after;

                if (value < after.Data)//Is new node in left tree
                    after = after.LeftNode;
                else if (value > after.Data)//Is new node in rigth tree
                    after = after.RigthNode;
                else
                    return false;
            }

            Node newNode = new Node();
            newNode.Data = value;

            if(this.Root == null)
            {
                this.Root = newNode;
            }
            else
            {
                if(value > before.Data)
                {
                    before.RigthNode = newNode;
                }
                else
                {
                    before.LeftNode = newNode;
                }
            }

            return true;
        }

        public Node Find(int value)
        {
            return this.Find(value, this.Root);
        }

        public void Remove(int value)
        {
            this.Root = Remove(this.Root, value);
        }

        private Node Remove(Node parent, int key)
        {
            if (parent == null) return parent;

            if(key < parent.Data)
            {
                parent.LeftNode = Remove(parent.LeftNode, key);
            }
            else if(key > parent.Data)
            {
                parent.RigthNode = Remove(parent.RigthNode, key);
            }// if value is same as parent´s value then this node to be deleted
            else
            {
                //Node with only one children
                if(parent.LeftNode == null)
                {
                    return parent.RigthNode;
                }
                else if(parent.RigthNode == null)
                {
                    return parent.LeftNode;
                }

                //Get the smallest in the right subtree
                parent.Data = MinValue(parent.RigthNode);

                //Delete the inorder successor
                parent.RigthNode = Remove(parent.RigthNode, parent.Data);
            }

            return parent;
        }

        private int MinValue(Node node)
        {
            int minv = node.Data;
            while(node.LeftNode != null)
            {
                minv = node.LeftNode.Data;
                node = node.LeftNode;
            }
            return minv;
        }

        private Node Find(int key, Node parent)
        {
            if (parent == null)
                return null;

            if(key < parent.Data)
            {
                return Find(key, parent.LeftNode);
            }
            else if(key > parent.Data)
            {
                return Find(key, parent.RigthNode);
            }
            else
            {
                return parent;
            }

        }

        public int GetTreeDepth()
        {
            return GetTreeDepth(this.Root);
        }

        private int GetTreeDepth(Node parent)
        {
            return parent == null ? 0 : Math.Max(GetTreeDepth(parent.LeftNode), GetTreeDepth(parent.RigthNode)) + 1; 
        }

        public void TraversePreOrder(Node parent)
        {
            if(parent != null)
            {
                Console.Write(parent.Data + " ");
                TraversePreOrder(parent.LeftNode);
                TraversePreOrder(parent.RigthNode);
            }
        }

        public void TraverseInOrder(Node parent)
        {
            if(parent != null)
            {
                TraverseInOrder(parent.LeftNode);
                Console.Write(parent.Data + " ");
                TraverseInOrder(parent.RigthNode);
            }
        }

        public void TraversePostOrder(Node parent)
        {
            if(parent != null)
            {
                TraversePostOrder(parent.LeftNode);
                TraversePostOrder(parent.RigthNode);
                Console.Write(parent.Data + " ");
            }
        }
    }
}
