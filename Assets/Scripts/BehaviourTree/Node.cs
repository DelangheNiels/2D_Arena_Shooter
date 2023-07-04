using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public enum NodeState
    {
        SUCCESS,
        RUNNING,
        FAILED
    }

    public class Node
    {
        public Node Parent;

        protected NodeState _state;
        protected List<Node> _children = new List<Node>();

        protected Blackboard _blackboard;

        public Node(Blackboard blackboard)
        {
            Parent = null;
            _blackboard = blackboard;
        }

        public Node(Blackboard blackboard, List<Node> children)
        {
            _blackboard = blackboard;

            foreach(Node child in children)
                AddChild(child);
        }

        public virtual NodeState Evaluate()
        {
            return NodeState.FAILED;
        }

        private void AddChild(Node node)
        {
            node.Parent = this;
            _children.Add(node);
        }

    }
}

