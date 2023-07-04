using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    //Evaluate all children until one fails or execute the running node
    public class Sequence : Node
    {
        public Sequence(Blackboard blackboard) : base(blackboard) { }
        public Sequence(Blackboard blackboard, List<Node> children) : base(blackboard,children) { }
        
        public override NodeState Evaluate()
        {
            bool isAnyChildRunning = false;
   
            foreach(Node node in _children)
            {
                switch(node.Evaluate())
                {
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        isAnyChildRunning = true;
                        continue;
                    case NodeState.FAILED:
                        _state = NodeState.FAILED;
                        return _state;
                    default:
                        _state = NodeState.SUCCESS;
                        return _state;
                }
            }

            _state = isAnyChildRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return _state;
        }
    }
}

