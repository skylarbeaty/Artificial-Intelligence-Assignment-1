using System;
using System.Collections;
using System.Collections.Generic;

namespace AI_Assignment1
{
    public class Graph
    {
        //driver
        static void Main(string[] args)
        {
            //consturct graph
            Graph graph = new Graph();
            graph.AddNode('A');
            graph.AddNode('B');
            graph.AddNode('C');
            graph.AddNode('D');
            graph.AddNode('E');
            graph.AddNode('F');
            graph.AddEdge('A', 'B');
            graph.AddEdge('A', 'C');
            graph.AddEdge('B', 'C');
            graph.AddEdge('B', 'D');
            graph.AddEdge('C', 'D');
            graph.AddEdge('C', 'E');
            graph.AddEdge('D', 'E');
            graph.AddEdge('D', 'F');
            graph.AddEdge('E', 'F');

            //run searchs
            Console.WriteLine("Running BFS Search");
            graph.BFS('A');

            Console.WriteLine("Running DFS recursive Search");
            graph.DFSrecursive('A');

            //stall for view
            Console.ReadLine();
        }

        public struct Node {
            public char name;
            public List<Node> neighbors;
            public Node(char _name) {
                name = _name;
                neighbors = new List<Node>();
            }
            public void Visit() {
                Console.WriteLine("Visited Node " + name.ToString());
            }
        }

        List<Node> nodes;

        public Graph() {
            nodes = new List<Node>();
        }

        public void AddNode(char name) {
            nodes.Add(new Node(name));
        }

        public void AddEdge(char name1, char name2) {
            //find by letter
            Node node1 = nodes.Find(x => x.name == name1);
            Node node2 = nodes.Find(x => x.name == name2);
            //create two way edge
            node1.neighbors.Add(node2);
            node2.neighbors.Add(node1);
        }

        void BFS(char _start) {
            Node start = nodes.Find(x => x.name == _start);
            List<Node> visited = new List<Node>();
            Queue<Node> queue = new Queue<Node>();//queue for BFS
            queue.Enqueue(start);
            while (queue.Count > 0) {
                Node current = queue.Dequeue();//get the first node added
                if (visited.Contains(current))
                    continue;//skip repeats, wont re-enqueue a node's neighbors
                visited.Add(current);
                current.Visit();
                foreach (Node n in current.neighbors)
                    queue.Enqueue(n);//add all neighbors to the stack
            }
        }

        void DFS(char _start)//non-recursive, for my own reference
        {
            Node start = nodes.Find(x => x.name == _start);
            List<Node> visited = new List<Node>();
            Stack<Node> stack = new Stack<Node>();
            stack.Push(start);
            while (stack.Count > 0)
            {
                Node current = stack.Pop();
                if (visited.Contains(current))
                    continue;
                visited.Add(current);
                current.Visit();
                foreach (Node n in current.neighbors)
                    stack.Push(n);
            }
        }
        void DFSrecursive(char _start) {//setup
            Node start = nodes.Find(x => x.name == _start);
            List<Node> visited = new List<Node>();
            Stack<Node> stack = new Stack<Node>();
            stack.Push(start);
            DFSrecursive(stack, visited, start);
        }
        void DFSrecursive(Stack<Node> stack, List<Node> visited, Node current){//actual recursive func
            if (!visited.Contains(current)){//if not visited add its neighbors to stack
                visited.Add(current);
                current.Visit();
                foreach (Node n in current.neighbors)
                    stack.Push(n);
            }
            if(stack.Count != 0)//recursively continue until the stack is empty
                DFSrecursive(stack, visited, stack.Pop());
        }
    }
}
