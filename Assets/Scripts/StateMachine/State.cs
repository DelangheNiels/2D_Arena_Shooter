using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public abstract class State
    {
        public State()
        {
        }
        public abstract void OnEnter();

        public abstract void OnExit();

        public virtual void Update()
        {

        }
    }
}

