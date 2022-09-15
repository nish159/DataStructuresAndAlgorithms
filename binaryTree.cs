using System;

BinaryTree tree = new BinaryTree();
tree.Insert(6);
//tree.PrintInOrder();
Console.WriteLine(" ");

tree.Insert(4);
tree.PrintInOrder();
Console.WriteLine(" ");

tree.Insert(7);
tree.PrintInOrder();
Console.WriteLine(" ");

tree.Insert(3);
tree.PrintInOrder();
Console.WriteLine(" ");

tree.Insert(1);
tree.PrintInOrder();
Console.WriteLine(" ");

tree.Insert(9);
tree.PrintInOrder();
Console.WriteLine(" ");

// you will need a variable to return a node
var node = tree.Find(7);
Console.WriteLine(node.value + " ");

var max = tree.Depth();
Console.WriteLine(max + " ");

var total = tree.Count();
Console.WriteLine($"Nodes: {total}");

tree.RemoveNode(6);
tree.PrintInOrder();


public class Node
{
    public int value { get; set; }
    public Node? left;
    public Node? right;
}

public class BinaryTree
{
    // want to keep the root private to control the root 
    private Node? root;

    /// <summary>
    /// Prints the value of the nodes in the tree in order
    /// </summary>
    public void PrintInOrder()
    {
        PrintInOrder(root);
    }

    public void RemoveNode(int value)
    {
        root = RemoveNode(root, value);
    }

    // ref is the original and not the copy
    private Node? RemoveNode(Node? node, int value)
    {
        // find the node that we want to remove
        if (node == null)
        {
            return node;
        }

        if (node.value < value) // the value we are looking for is greated than the value in the node -> go right
        {
            node.right = RemoveNode(node.right, value);
        }
        else if (node.value > value) // the value we are looking for is less than then value in the node -> go left
        {
            node.left = RemoveNode(node.left, value);
        }
        else
        {
            // the value is equal to the node we want to remove so we remove the current node
            // 1. The node to remove is a leaf
            // 2. The node to remove has one child
            // 3. The node to remove has 2 children

            if (node.left == null && node.right == null)
            {
                return null;
            }
            else if (node.left == null)
            {
                return node.right;
            }
            else if (node.right == null)
            {
                return node.left;
            }
            else // The node to remove has 2 children
            {
                // The new value of the node we are removing will be the min value in the right subtree
                var minright = MinValue(node.right);
                node.value = minright;

                // now we have to remove the node in the right subtree that has the min value
                node.right = RemoveNode(node.right, minright);
            }
        }
        return node;
    }

    public int MinValue(Node? node)
    {
        if (node.left == null)
        {
            return node.value;
        }
        return MinValue(node.left);
    }

    public int Count()
    {
        return Count(root);
    }

    private int Count(Node? node)
    {
        if (node == null)
        {
            return 0;
        }

        int leftNodes = Count(node.left);
        int rightNodes = Count(node.right);

        var total = leftNodes + rightNodes + 1;
        return total;
    }

    public int Depth()
    {
        return Depth(root);
    }

    private int Depth(Node? node)
    {
        // base case always remains at the top
        if (node == null)
        {
            return 0;
        }

        int depthLeft = Depth(node.left);
        int depthRight = Depth(node.right);

        // find max of the current node and store value
        var max = Math.Max(depthLeft, depthRight);
        return max + 1;
    }

    /// <summary>
    /// Finds the node in the tree with the given value
    /// </summary>
    /// <param name="value">The value to be found</param>
    /// <returns>The Node with the given value if found, otherwise null</returns>
    public Node? Find(int value)
    {
        return Find(root, value);
    }

    public void Insert(int value)
    {
        if (root == null)
        {
            Node? newNode = new Node();
            newNode.value = value;
            root = newNode;
        }
        else
        {
            Insert(root, value);
        }
    }

    /// <summary>
    /// Finds the node in the tree with the given value
    /// </summary>
    /// <param name="node"></param>
    /// <param name="value"></param>
    private Node? Find(Node? node, int value)
    {
        if (node == null)
        {
            return node;
        }

        if (node.value == value)
        {
            return node;
        }
        else if (value > node.value)
        {
            // return to sync up the tree
            return Find(node.right, value);
        }
        else if (value < node.value)
        {
            return Find(node.left, value);
        }
        return node;
    }

    /// <summary>
    /// Inserts a given value into the tree
    /// </summary>
    /// <param name="value"></param>
    private void Insert(Node? node, int value)
    {
        // Is the root null!!!
        Node? newNode = new Node();
        newNode.value = value;

        if (node == null /* if the list is empty*/)
        {
            node = newNode;
        }
        else if (value < node.value /* if the value is less than the node value*/)
        {
            if (node.left == null)
            {
                node.left = newNode;
                return;
            }
            Insert(node.left, value);
        }
        else if (value > node.value /* if the value is greater than the node value*/)
        {
            if (node.right == null)
            {
                node.right = newNode;
                return;
            }
            Insert(node.right, value);
        }
    }

    private void PrintInOrder(Node? node)
    {
        if (node == null)
        {
            return;
        }

        PrintInOrder(node.left); // Print the left subtree
        // Every node in the left sub-tree has been printed

        Console.Write(node.value + " "); // Print out the value in the current node

        PrintInOrder(node.right); // Print the right subtree
        // Every node in the right sub-tree has been printer
    }
}
