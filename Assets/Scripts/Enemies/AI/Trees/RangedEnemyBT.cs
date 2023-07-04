using BehaviourTree;
using Enemies.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class RangedEnemyBT : BehaviourTree.Tree
    {
        protected override Node SetupTree()
        {
            Node root = new Selector(_blackboard, new List<Node>
            {
                new PerformAttack(_blackboard, transform),
                new Sequence(_blackboard, new List<Node>
                {
                    new FindPlayerPosition(_blackboard, transform),
                    new Selector(_blackboard, new List<Node>
                    {
                        new MoveAwayFromPlayer(_blackboard, transform),
                        new MoveToMinimumAttackRange(_blackboard, transform)
                    })

                })
        });
            return root;
        }
    }
}

