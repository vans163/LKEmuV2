using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2
{
    /// <summary>
    /// AStar require it's nodes to implement certain functions (as defined by IAStarNode)
    /// If would be nice if this data could just be added to the node class then removed after AStar
    /// has done it's thing but I don't know a nice way of doing this.
    /// </summary>
    /// <typeparam name="T">The nodes AStar will operate on.</typeparam>
    public class AStar<T> where T : IAStarNode<T>, new()
    {
        public delegate double CostHeuristic(T start, T end);

        CostHeuristic _costHeuristic;
        List<T> _openList = new List<T>();
        T _goal;
        List<T> _path;
        Queue<T> _touched;

        long runindex = 0;

        public List<T> Path
        {
            get { return _path; }
        }

        public AStar(CostHeuristic costHeuristic)
        {
            _costHeuristic = costHeuristic;
        }

        public void FindPath(T root, T goal)
        {
          //  this.runindex = runindex;

            _path = new List<T>();

            _goal = goal;
            //avoid infinite loop without reseting all the cells:
            _goal.ParentNode = default(T);
            root.ParentNode = default(T);

            _openList = new List<T>();

            T rootNode = root;
            root.Cost = 0;
            root.inopenlist = true;
            root.inclosedlist = false;
           // root.runindex = runindex;
            _openList.Add(rootNode);

            ProcessPath();
        }

        private void ProcessPath()
        {
            bool goalHasBeenReached = false;
            while (goalHasBeenReached == false)
            {
                _openList.OrderBy(x => x.Cost);

                T current = _openList.FirstOrDefault();

                if (current == null)
                    return;

                if (current.Equals(_goal))
                {
                    // Backtrace and return path.
                    T trace = _goal;
                    while (trace != null)
                    {
                        _path.Add(trace);
                        trace = trace.ParentNode;
                    }
                    //_path.RemoveAt(0);
                    _path.Reverse();
                    _path.RemoveAt(0);
                    return;
                }

                _openList.RemoveAt(0);
                current.inclosedlist = true;
                current.inopenlist = false;

                // foreach (T neighbour in current.Neighbours)
                for (int x = 0; x < current.Neighbours.Count; x++)
                {
                    //  System.Diagnostics.Debug.Assert(neighbour.Equals(current) == false, "No node should be it's own neighbour.");

                    var neighbour = current.Neighbours[x];
                    double cost = current.Cost + _costHeuristic(neighbour, _goal);

                   /* if (neighbour.runindex != runindex)
                    {
                        neighbour.runindex = runindex;
                        neighbour.inclosedlist = false;
                        neighbour.inopenlist = false;
                    }*/

                    if (neighbour.inopenlist && neighbour.Cost > cost)
                    {
                        _openList.Remove(neighbour);
                        neighbour.inopenlist = false;
                    }

                    if (neighbour.inclosedlist && neighbour.Cost > cost)
                    {
                        neighbour.inclosedlist = false;
                    }

                    if (!neighbour.inopenlist && !neighbour.inclosedlist)
                    {
                        neighbour.Cost = cost;
                        neighbour.ParentNode = current;
                        _openList.Add(neighbour);
                        neighbour.inopenlist = true;
                    }
                }
            }
        }
    }
}