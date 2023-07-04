using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    //Evaluate all children until one fails or execute the running node
    public class Selector : Node
    {
        public Selector(Blackboard blackboard) : base(blackboard) { }
        public Selector(Blackboard blackboard,List<Node> children) : base(blackboard,children) { }

        public override NodeState Evaluate()
        {
            foreach (Node node in _children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.SUCCESS:
                        _state = NodeState.SUCCESS;
                        return _state;
                    case NodeState.RUNNING:
                        _state = NodeState.RUNNING;
                        return _state;
                    case NodeState.FAILED:
                        continue;
                    default:
                        continue;
                }
            }

            _state = NodeState.FAILED;
            return _state;
        }
    }
}
