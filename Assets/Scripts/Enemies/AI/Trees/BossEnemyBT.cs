using BehaviourTree;
using Enemies.AI;
using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class BossEnemyBT : BehaviourTree.Tree
    {
        protected override Node SetupTree()
        {
            HealthComponent healthComponent = transform.root.GetComponentInChildren<HealthComponent>();

            Node root = new Selector(_blackboard, new List<Node>
            {
                new UseAbility(_blackboard,transform),
                new Sequence(_blackboard,new List<Node>
                {
                    new FindPlayerPosition(_blackboard, transform),
                    new PerformAttack(_blackboard,transform)
                }),
                new Selector(_blackboard,new List<Node>
                {
                    new Sequence(_blackboard,new List<Node>
                    {
                        new IsHealthInThreshold(_blackboard,healthComponent,1,0.5f)
                    }),
                    new Sequence(_blackboard, new List<Node>
                    {
                        new IsHealthInThreshold(_blackboard,healthComponent,0.5f,0.0f),
                        new Selector(_blackboard, new List<Node>
                        {
                            new MoveAwayFromPlayer(_blackboard, transform),
                            new MoveToMaximumAttackRange(_blackboard, transform),
                        })
                    })
                }),
            });

            return root;
        }
    }
}

