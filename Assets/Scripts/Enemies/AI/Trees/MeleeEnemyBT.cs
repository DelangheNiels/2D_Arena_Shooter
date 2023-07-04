using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Enemies.AI;

namespace Enemies
{
    public class MeleeEnemyBT : BehaviourTree.Tree
    {
        protected override Node SetupTree()
        {
            Node root = new Selector(_blackboard, new List<Node>
            {
                new PerformAttack(_blackboard, transform),
                new Sequence(_blackboard, new List<Node>
                {
                    new FindPlayerPosition(_blackboard, transform),
                    new MoveToMaximumAttackRange(_blackboard, transform)
                })

        });
            return root;
        }

       
    }
}

