using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class StateMachine : MonoBehaviour
    {

        private State _currentState;
        private List<Transition> _transitions = new List<Transition>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {
            if (_currentState == null)
                return;

            _currentState.Update();

            CheckTransitions();
        }

        public void AddTransition(State fromState,State toState, Transition.BoolDeligate boolDeligate)
        {
            if (fromState == null || toState == null)
                Debug.Log("a state is null");

            Transition transition = new Transition(fromState, toState, boolDeligate);
            _transitions.Add(transition);
        }

        private void CheckTransitions()
        {
            foreach (Transition transition in _transitions)
            {
                if (transition.FromState != _currentState)
                    continue;

                if (!transition.Deligate())
                    continue;

                _currentState.OnExit();
                _currentState = transition.ToState;
                _currentState.OnEnter();
            }
        }

        public void SetInitialState(State state)
        {
            _currentState = state;
            _currentState.OnEnter();
        }
    }
}

