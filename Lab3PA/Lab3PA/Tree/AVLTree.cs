using System;
using System.IO;

namespace Lab3PA.Tree;

public class AVLTree
{
    private int Height(Node? node)
    {
        return node?.Height ?? 0;
    }

    private int BalanceFactor(Node? node)
    {
        return Height(node.NodeR) - Height(node.NodeL);
    }

    private void FixHeight(Node? node)
    {
        var hL = Height(node.NodeL);
        var hR = Height(node.NodeR);
        node.Height = (hL > hR ? hL : hR) + 1;
    }

    private Node? RotateRight(Node? node)
    {
        var s = node.NodeL;
        node.NodeL = s.NodeR;
        s.NodeR = node;
        FixHeight(node);
        FixHeight(s);
        return s;
    }

    private Node? RotateLeft(Node? node)
    {
        var s = node.NodeR;
        node.NodeR = s.NodeL;
        s.NodeL = node;
        FixHeight(node);
        FixHeight(s);
        return s;
    }

    private Node? Balance(Node? node)
    {
        FixHeight(node);
        if(BalanceFactor(node) == 2)
        {
            if(BalanceFactor(node.NodeR) < 0)
                node.NodeR = RotateRight(node.NodeR);
            return RotateLeft(node);
        }

        if (BalanceFactor(node) != -2) return node;
        if(BalanceFactor(node.NodeL) > 0)
            node.NodeL = RotateLeft(node.NodeL);
        return RotateRight(node);
    }

    public Node? Insert(Node? node, int key)
    {
        if (node == null) return new Node(key);
        if (key < node.Key)
            node.NodeL = Insert(node.NodeL, key);
        else
            node.NodeR = Insert(node.NodeR, key);
        return Balance(node);
        Console.WriteLine();
    }

    private Node? FindMin(Node? node)
    {
        return node.NodeL != null ? FindMin(node.NodeL) : node;
    }

    private Node? RemoveMin(Node? node)
    {
        if (node.NodeL == null)
            return node.NodeR;
        node.NodeL = RemoveMin(node.NodeL);
        return Balance(node);
    }

    public Node? Remove(Node? node, int key)
    {
        if (node == null) return null;
        
        if (key < node.Key)
            node.NodeL = Remove(node.NodeL, key);
        
        else if (key > node.Key)
            node.NodeR = Remove(node.NodeR, key);
        
        else
        {
            var l = node.NodeL;
            var r = node.NodeR;
            
            if (r == null) return l;
            
            var min = FindMin(r);
            min.NodeR = RemoveMin(r);
            min.NodeL = l;
            
            return Balance(min);
        }

        return Balance(node);
    }
    
    public string PrintTree(Node root, int space, int height, string dir) {
        if (root == null) {
            return null;
        }

        var ss = "";
        space += height;
        ss += PrintTree(root.NodeR, space - 2, height, "right");
        if (dir.Equals("main"))
        {
            ss += "\n";
            ss += (new string(' ', (space) * 2 - 4));
            ss += (root.Key);
            ss += "\n";
        }
        else
        {
            ss += "\n";
            ss += (new string(' ', (space - height) * 2));
            if (dir.Equals("right")) ss += ("┌");
            if(dir.Equals("left")) ss += ("└");
            ss += (new string('-', height - 2));
            ss += (new string('>', 1));
            ss += (root.Key);
            ss += "\n";
        }
        ss += PrintTree(root.NodeL, space - 2, height, "left");
        return ss;
    }
    

    public void SaveTree(Node node, StreamWriter sw)
    {
        if (node == null) sw.WriteLine("#");
        
        else
        {
            sw.WriteLine(node.Key);
            sw.WriteLine(node.Height);
            
            SaveTree(node.NodeL, sw);
            SaveTree(node.NodeR, sw);
        }
    }

    public Node? ReadTree(Node node, StreamReader sr)
    {
        if (sr.EndOfStream)
        {
            return null;
        }
        var el = sr.ReadLine();
        Console.WriteLine(el);
        if (el.Equals("#") || el.Equals("")) return null;
        var height = sr.ReadLine();
        node = new Node(int.Parse(el), int.Parse(height));
        node.NodeL = ReadTree(node.NodeL, sr);
        node.NodeR = ReadTree(node.NodeR, sr);
        Console.WriteLine(node.Key);
        return node;
    }
}
