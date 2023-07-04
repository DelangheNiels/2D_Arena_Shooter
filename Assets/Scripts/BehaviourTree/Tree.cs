using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root;
        protected Blackboard _blackboard;

        protected void Start()
        {
            _blackboard = new Blackboard();
            _root = SetupTree();
        }

        protected void Update()
        {
            if (_root == null)
                return;

            _root.Evaluate();
        }

        protected abstract Node SetupTree();
    }
}

