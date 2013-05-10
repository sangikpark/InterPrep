using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace InterPrep
{
    public class TreeNode<T> where T : IComparable<T>
    {
        public T Data;
        public TreeNode<T> Left;
        public TreeNode<T> Right;

        public TreeNode<T> NextRight;

        public TreeNode(T data)
        {
            Data = data;
        }

        public override string ToString()
        {
            string nodeString = "[" + this.Data + " ";

            // Leaf node
            if (this.Left == null && this.Right == null)
            {
                nodeString += " (Leaf) ";
            }

            if (this.Left != null)
            {
                nodeString += "Left: " + this.Left.ToString();
            }

            if (this.Right != null)
            {
                nodeString += "Right: " + this.Right.ToString();
            }

            nodeString += "] ";

            return nodeString;
        }
    }

    public class BinarySearchTree<T> where T : IComparable<T>
    {
        public TreeNode<T> Root;

        public BinarySearchTree()
        {
            this.Root = null;
        }

        public void Insert(T newdata)
        {
            this.Root = Insert(newdata, this.Root);
        }

        public void Delete(T removedata)
        {
            // TODO
            // this.Root = Delete(removedata, this.Root);
        }

        public T Find_Iterative(T newdata)
        {
            TreeNode<T> node = Find_Iterative(newdata, this.Root);
            return node == null ? default(T) : node.Data;
        }

        public T Find_Recursive(T newdata)
        {
            TreeNode<T> node = Find_Recursive(newdata, this.Root);
            return node == null ? default(T) : node.Data;
        }

        public T FindMin()
        {
            TreeNode<T> node = FindMin(this.Root);
            return node == null ? default(T) : node.Data;
        }

        public T FindMax()
        {
            TreeNode<T> node = FindMax(this.Root);
            return node == null ? default(T) : node.Data;
        }

        public override string ToString()
        {
            return this.Root.ToString();
        }

        // Traversal - preorder
        public void Preorder(TreeNode<T> node)
        {
            if (node != null)
            {
                Console.Write(node.Data + " ");
                Preorder(node.Left);
                Preorder(node.Right);
            }
        }

        // Traversal - inorder
        public void Inorder(TreeNode<T> node)
        {
            if (node != null)
            {
                Inorder(node.Left);
                Console.Write(node.Data + " ");
                Inorder(node.Right);
            }
        }

        // Traversal - postorder
        public void Postorder(TreeNode<T> node)
        {
            if (node != null)
            {
                Postorder(node.Left);
                Postorder(node.Right);
                Console.Write(node.Data + " ");
            }
        }

        // A binary search tree (BST) is a node based binary tree data structure which has the following properties.
        // - The left subtree of a node contains only nodes with keys less than the node's key.
        // - The right subtree of a node contains only nodes with keys greater than the node's key.
        // - Both the left and right subtrees must also be binary search trees.
        public bool IsBST(TreeNode<T> node) // Simple, but wrong
        {
            if (node == null)
                return true;

            if ((node.Left != null) && node.Data.CompareTo(node.Left.Data) < 0)
            {
                return false;
            }

            if ((node.Right != null) && node.Data.CompareTo(node.Right.Data) > 0)
            {
                return false;
            }

            if (!IsBST(node.Left) || !IsBST(node.Right))
            {
                return false;
            }

            return true;
        }

        public bool IsBST(TreeNode<int> node, int min, int max) // Correct
        {
            if (node == null)
                return true;

            if (node.Data <= min || node.Data >= max)
            {
                return false;
            }

            if (!IsBST(node.Left, min, node.Data) || !IsBST(node.Right, node.Data, max))
            {
                return false;
            }

            return true;
        }


        // Iterative
        private TreeNode<T> Find_Iterative(T newdata, TreeNode<T> node)
        {
            if (node == null)
                return null;

            while (node != null)
            {
                int result = newdata.CompareTo(node.Data);

                if (result < 0)
                {
                    node = node.Left;
                }
                else if (result > 0)
                {
                    node = node.Right;
                }
                else
                {
                    return node;
                }
            }

            return null;
        }

        // Recursive
        private TreeNode<T> Find_Recursive(T newdata, TreeNode<T> node)
        {
            if (node == null)
                return null;

            int result = newdata.CompareTo(node.Data);

            if (result < 0)
            {
                return Find_Recursive(newdata, node.Left);
            }
            else if (result > 0)
            {
                return Find_Recursive(newdata, node.Right);
            }
            else
            {
                return node;
            }
        }

        private TreeNode<T> FindMin(TreeNode<T> node)
        {
            if (node != null)
            {
                while (node.Left != null)
                {
                    node = node.Left;
                }
            }

            return node;
        }

        private TreeNode<T> FindMax(TreeNode<T> node)
        {
            if (node != null)
            {
                while (node.Right != null)
                {
                    node = node.Right;
                }
            }

            return node;
        }

        private TreeNode<T> Insert(T newdata, TreeNode<T> node)
        {
            if (node == null)
            {
                node = new TreeNode<T>(newdata);
                return node;
            }

            int result = newdata.CompareTo(node.Data);

            if (result < 0)
            {
                node.Left = Insert(newdata, node.Left);
            }
            else if (result > 0)
            {
                node.Right = Insert(newdata, node.Right);
            }
            else
            {
                throw new Exception("Duplicate item");
            }

            return node;
        }

        private TreeNode<T> Delete(T removedata, TreeNode<T> node)
        {
            // TODO
            return node;
        }

        public int MaxDepth(TreeNode<T> node)
        {
            if (node == null)
            {
                return 0;
            }
            return 1 + Math.Max(MaxDepth(node.Left), MaxDepth(node.Right));
        }

        public int MinDepth(TreeNode<T> node)
        {
            if (node == null)
            {
                return 0;
            }
            return 1 + Math.Min(MinDepth(node.Left), MinDepth(node.Right));
        }

        public bool IsBalanced(TreeNode<T> node)
        {
            return (MaxDepth(node) - MinDepth(node) <= 1);
        }

        private T DataAt(TreeNode<T> node)
        {
            return node == null ? default(T) : node.Data;
        }

        // Q: Populate the NextRight pointers in each node.
        // DFS way
        public static void PopulateNextRight(TreeNode<T> node)
        {
            if (node == null)
                return;

            if (node.Left != null)
                node.Left.NextRight = node.Right;

            if (node.Right != null)
                node.Right.NextRight = (node.NextRight != null) ? node.NextRight.Left : null;

            PopulateNextRight(node.Left);
            PopulateNextRight(node.Right);
        }
    }

    // DFS using stack
    public class DepthFirstSearch<T> where T : IComparable<T>
    {
        private Stack<TreeNode<T>> stack;
        private TreeNode<T> root;

        public DepthFirstSearch(TreeNode<T> root)
        {
            this.root = root;
            stack = new Stack<TreeNode<T>>();
        }

        public bool Search(T data)
        {
            TreeNode<T> current;
            stack.Push(root);

            while (stack.Count > 0)
            {
                current = stack.Pop();
                if (current.Data.CompareTo(data) == 0)
                {
                    return true;
                }
                else
                {
                    if (current.Right != null)
                        stack.Push(current.Right);
                    if (current.Left != null)
                        stack.Push(current.Left);
                }
            }
            return false;
        }
    }

    // BFS using queue
    public class BreadthFirstSearch<T> where T : IComparable<T>
    {
        private Queue<TreeNode<T>> queue;
        private TreeNode<T> root;

        public BreadthFirstSearch(TreeNode<T> root)
        {
            this.root = root;
            queue = new Queue<TreeNode<T>>();
        }

        public bool Search(T data)
        {
            TreeNode<T> current;
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                current = queue.Dequeue();

                if (current.Data.CompareTo(data) == 0)
                {
                    return true;
                }
                else
                {
                    if (current.Left != null)
                        queue.Enqueue(current.Left);
                    if (current.Right != null)
                        queue.Enqueue(current.Right);
                }
            }
            return false;
        }
    }

    // Q: Given the arbitrary tree
    //           1
    //         / \ \
    //       -2  0  3
    //       / \     \
    //     -1   5     4
    //
    // Write function to output as 1, -2 0 3, -1 5 4
    public class ATreeNode
    {
        public int Data;
        public ATreeNode[] Children;
    }

    public class BFS
    {
        private Queue<ATreeNode> queue;
        private ATreeNode root;

        public BFS(ATreeNode root)
        {
            this.root = root;
            queue = new Queue<ATreeNode>();
        }

        public void Traverse()
        {
            ATreeNode current;
            queue.Enqueue(root);

            int nodesInCurrentLevel = 1;
            int nodesInNextLevel = 0;

            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                nodesInCurrentLevel--;

                Console.Write(" " + current.Data);

                if (current.Children != null)
                {
                    foreach (ATreeNode node in current.Children)
                    {
                        queue.Enqueue(node);
                        nodesInNextLevel++;
                    }
                }

                if (nodesInCurrentLevel == 0)
                {
                    if (queue.Count > 0)
                        Console.Write(',');
                    nodesInCurrentLevel = nodesInNextLevel;
                    nodesInNextLevel = 0;
                }
            }
        }
    }

    class BENode
    {
        public char Data;
        public BENode Left;
        public BENode Right;
    }

    class BinaryExpressionTree
    {
        // Q: Given the binary expression tree (node can be +, * or integer)
        //           +
        //         /   \
        //        +     *
        //       / \   / \
        //      1   * 4   5
        //         / \
        //        2   3
        // = ( 1 + ( 2 * 3 ) ) + ( 4 * 5 ) = 27
        // Write EvaluateTree() function
        public static int EvaluateTree(BENode node)
        {
            if (node == null)
            {
                return 0;
            }

            if (node.Left == null && node.Right == null)
            {
                return node.Data - '0'; // This is a leaf node.
            }

            int left, right;
            left = EvaluateTree(node.Left);
            right = EvaluateTree(node.Right);

            int result = 0;
            if (node.Data == '+')
            {
                result = left + right;
            }
            else if (node.Data == '*')
            {
                result = left * right;
            }
            else
            {
                throw new NotSupportedException();
            }

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree<int> intBST = new BinarySearchTree<int>();
            intBST.Insert(3);
            intBST.Insert(1);
            intBST.Insert(2);
            intBST.Insert(5);
            intBST.Insert(4);
            intBST.Insert(6);

            Debug.Assert(intBST.IsBST(intBST.Root) == true);

            Console.WriteLine("Tree: " + intBST);

            Debug.Assert(intBST.Find_Iterative(4) == 4);
            Debug.Assert(intBST.Find_Recursive(4) == 4);

            Debug.Assert(intBST.FindMin() == 1);
            Debug.Assert(intBST.FindMax() == 6);

            intBST.Preorder(intBST.Root);
            Console.WriteLine("");
            intBST.Inorder(intBST.Root);
            Console.WriteLine("");
            intBST.Postorder(intBST.Root);
            Console.WriteLine("");

            Debug.Assert(intBST.IsBalanced(intBST.Root) == true);

            BinarySearchTree<int> unbalBST = new BinarySearchTree<int>();
            unbalBST.Insert(1);
            unbalBST.Insert(2);
            unbalBST.Insert(3);
            unbalBST.Insert(4);
            Debug.Assert(unbalBST.IsBalanced(unbalBST.Root) == false);

            DepthFirstSearch<int> dfs = new DepthFirstSearch<int>(intBST.Root);
            Debug.Assert(dfs.Search(6) == true);

            BreadthFirstSearch<int> bfs = new BreadthFirstSearch<int>(intBST.Root);
            Debug.Assert(bfs.Search(6) == true);

            BENode node2 = new BENode();
            node2.Data = '+';
            
            BENode node3 = new BENode();
            node3.Data = '*';

            BENode node4 = new BENode();
            node4.Data = '1';

            BENode node5 = new BENode();
            node5.Data = '*';

            BENode node6 = new BENode();
            node6.Data = '4';

            BENode node7 = new BENode();
            node7.Data = '5';

            BENode node8 = new BENode();
            node8.Data = '2';

            BENode node9 = new BENode();
            node9.Data = '3';

            BENode root = new BENode();
            root.Data = '+';
            root.Left = node2;
            root.Right = node3;

            node2.Left = node4;
            node2.Right = node5;

            node5.Left = node8;
            node5.Right = node9;

            node3.Left = node6;
            node3.Right = node7;

            int result = BinaryExpressionTree.EvaluateTree(root);

            Console.ReadLine();
        }
    }
}
