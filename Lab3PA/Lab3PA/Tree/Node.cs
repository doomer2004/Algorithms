namespace Lab3PA.Tree;

public class Node
{
    public int Key { get; set; }
    public int Height { get; set; }
    public Node? NodeL { get; set; }
    public Node? NodeR { get; set; }

    public Node(int key, int height)
    {
        Key = key;
        NodeL = null;
        NodeR = null;
        Height = height; 
    }
    public Node(int key)
    {
        Key = key;
        NodeL = null;
        NodeR = null;
        Height = 1; 
    }
}